using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Booth;
using PhotoboothBranchService.Application.DTOs.Slot;
using PhotoboothBranchService.Application.DTOs.Sticker;
using PhotoboothBranchService.Application.Services.CloudinaryServices;
using PhotoboothBranchService.Application.Services.SlotServices;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;
using System.Text;

namespace PhotoboothBranchService.Application.Services.BoothServices
{
    public class BoothService : IBoothService
    {
        private readonly IBoothRepository _boothRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IBoothPhotoRepository _boothPhotoRepository;
        private readonly ISlotService _slotService;
        private readonly IMapper _mapper;

        public BoothService(IBoothRepository boothRepository, IMapper mapper, IBranchRepository branchRepository, 
            IAccountRepository accountRepository, 
            ICloudinaryService cloudinaryService, IBoothPhotoRepository boothPhotoRepository, ISlotService slotService)
        {
            _boothRepository = boothRepository;
            _mapper = mapper;
            _branchRepository = branchRepository;
            _accountRepository = accountRepository;
            _cloudinaryService = cloudinaryService;
            _boothPhotoRepository = boothPhotoRepository;
            _slotService = slotService;
        }

        // Create
        public async Task<CreateBoothResponse> CreateAsync(CreateBoothRequest createModel)
        {
            Booth booth = _mapper.Map<Booth>(createModel);
            var branch = (await _branchRepository.GetAsync(i => i.BranchID == createModel.BranchID)).SingleOrDefault();
            if (branch == null)
            {
                throw new NotFoundException("Not found Branch to create booth");
            }
            booth.ActiveCode = await this.GenerateActiveCode();
            booth.Status = BoothStatus.Inactive;
            await _boothRepository.AddAsync(booth);
            await _slotService.AutoCreateSlotByBooth(new AutoCreateSlotRequest
                { 
                    BoothID = booth.BoothID, 
                    Price = createModel.PricePerSlot
                });
            return _mapper.Map<CreateBoothResponse>(booth);
        }

        // Delete
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                Booth? booth = (await _boothRepository.GetAsync(f => f.BoothID == id)).FirstOrDefault();
                if (booth != null)
                {
                    await _boothRepository.RemoveAsync(booth);
                } else
                {
                    throw new NotFoundException("Booth not found");
                }
            }
            catch
            {
                throw;
            }
        }

        // Read
        public async Task<IEnumerable<BoothResponse>> GetAllAsync()
        {
            var booths = await _boothRepository.GetAsync(null, bth => bth.BoothPhotos);
            return _mapper.Map<IEnumerable<BoothResponse>>(booths.ToList().OrderByDescending(i => i.CreatedDate));
        }
        public async Task<IEnumerable<AdminBoothResponse>> AdminGetAllAsync()
        {
            var booths = await _boothRepository.GetAsync(null, bth => bth.BoothPhotos);
            return _mapper.Map<IEnumerable<AdminBoothResponse>>(booths.ToList().OrderByDescending(i => i.CreatedDate));
        }
        public async Task<IEnumerable<BoothResponse>> CustomerGetAllAsync()
        {
            var branchIdList = (await _branchRepository.GetAsync(i => i.Status == BranchStatus.Active)).Select(i => i.BranchID).ToList();
            var booths = await _boothRepository.GetAsync(i => branchIdList.Contains(i.BranchID) && i.Status != BoothStatus.Inactive, i => i.BoothPhotos);
            return _mapper.Map<IEnumerable<BoothResponse>>(booths.ToList().OrderByDescending(i => i.CreatedDate));
        }
        public async Task<IEnumerable<BoothResponse>> StaffGetAllAsync(string? email)
        {
            var acc = (await _accountRepository.GetAsync(i => i.Email.Equals(email) && i.Status == AccountStatus.Active)).FirstOrDefault();
            if (acc == null || acc.Role != AccountRole.Staff)
            {
                throw new ForbiddenAccessException();
            }
            if (!acc.BranchID.HasValue || acc.BranchID == null) 
            {
                throw new ForbiddenAccessException("Staff has not been assign to any branch");
            }
            var booths = await _boothRepository.GetAsync(i => i.BranchID == acc.BranchID);
            return _mapper.Map<IEnumerable<BoothResponse>>(booths.ToList().OrderByDescending(i => i.CreatedDate));
        }
        public async Task<IEnumerable<BoothResponse>> GetAllPagingAsync(BoothFilter filter, PagingModel paging)
        {
            var booths = (await _boothRepository.GetAsync(null, bth => bth.BoothPhotos)).ToList().AutoFilter(filter);
            var listBoothresponse = _mapper.Map<IEnumerable<BoothResponse>>(booths);
            return listBoothresponse.AsQueryable().OrderByDescending(i => i.CreatedDate).AutoPaging(paging.PageSize, paging.PageIndex);
        }
        public async Task<BoothResponse> GetByIdAsync(Guid id)
        {
            var booth = (await _boothRepository.GetAsync(b => b.BoothID == id, b => b.BoothPhotos)).FirstOrDefault();
            return _mapper.Map<BoothResponse>(booth);
        }

        public async Task<IEnumerable<BoothResponse>> GetByName(string name)
        {
            var booths = await _boothRepository.GetAsync(b => b.BoothName.Contains(name));
            return _mapper.Map<IEnumerable<BoothResponse>>(booths.ToList());
        }

        // Update
        public async Task UpdateAsync(Guid id, UpdateBoothRequest updateModel)
        {
            var booth = (await _boothRepository.GetAsync(b => b.BoothID == id)).FirstOrDefault();
            if (booth == null)
            {
                throw new KeyNotFoundException("Booth not found.");
            }

            var updatedBooth = _mapper.Map(updateModel, booth);
            await _boothRepository.UpdateAsync(updatedBooth);
        }
        public async Task<BoothResponse> ActiveBooth(string code)
        {
            var booth = (await _boothRepository.GetAsync(i => i.ActiveCode == code)).SingleOrDefault();
            if (booth == null)
            {
                throw new BadRequestException("Invalid code, try again");
            }
            else
            {
                booth.Status = BoothStatus.Active;
                await _boothRepository.UpdateAsync(booth);
                return _mapper.Map<BoothResponse>(booth);
            }
        }
        public async Task<BoothResponse> AddPhotoForBooth(Guid boothID, IFormFile file)
        {
            var booth = await GetByIdAsync(boothID);
            if (booth != null)
            {
                //upload to cloudinary
                var uploadResult = await _cloudinaryService.AddPhotoAsync(file, "FBooth-BoothPicture");
                if (uploadResult.Error != null)
                {
                    throw new Exception(uploadResult.Error.Message);
                }

                //create object from cloudinary's return 
                var boothPhoto = new BoothPhoto
                {
                    BoothID = boothID,
                    BoothPhotoUrl = uploadResult.SecureUrl.AbsoluteUri,
                    CouldID = uploadResult.PublicId,
                };

                await _boothPhotoRepository.AddAsync(boothPhoto);

                var updatedBooth = (await _boothRepository.GetAsync(b => b.BoothID == boothID, b => b.BoothPhotos)).FirstOrDefault();
                return _mapper.Map<BoothResponse>(updatedBooth);
            }
            throw new KeyNotFoundException("Booth not found.");
        }

        private async Task<string> GenerateActiveCode()
        {
            var generator = new CodeGenerator();
            var code = "";
            while (code.Equals(""))
            {
                code = generator.GenerateCode();
                var booth =  await _boothRepository.GetAsync(i => i.ActiveCode == code);
                if (booth != null)
                {
                    code = "";
                }
            }
            return code;
        }
        private class CodeGenerator
        {
            private readonly Random random = new Random();
            private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";

            internal string GenerateCode(int length = 12, int chunkSize = 4, char separator = '-')
            {
                StringBuilder code = new StringBuilder();

                for (int i = 0; i < length; i++)
                {
                    code.Append(Characters[random.Next(Characters.Length)]);
                }

                // Insert separator for better readability
                var formattedCode = string.Join(separator.ToString(), Enumerable.Range(0, length / chunkSize)
                                           .Select(i => code.ToString().Substring(i * chunkSize, chunkSize)));

                return formattedCode;
            }
        }
    }
}
