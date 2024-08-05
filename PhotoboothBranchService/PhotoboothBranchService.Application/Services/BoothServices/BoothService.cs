using AutoMapper;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Booth;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.BoothServices
{
    public class BoothService : IBoothService
    {
        private readonly IBoothRepository _boothRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BoothService(IBoothRepository boothRepository, IMapper mapper, IBranchRepository branchRepository, IDeviceRepository deviceRepository, IBookingRepository bookingRepository)
        {
            _boothRepository = boothRepository;
            _mapper = mapper;
            _branchRepository = branchRepository;
            _deviceRepository = deviceRepository;
            _bookingRepository = bookingRepository;
        }

        // Create
        public async Task<CreateBoothResponse> CreateAsync(CreateBoothRequest createModel, BoothStatus status)
        {
            Booth booth = _mapper.Map<Booth>(createModel);
            var branch = (await _branchRepository.GetAsync(i => i.BranchID == createModel.BranchID)).SingleOrDefault();
            if (branch == null)
            {
                throw new NotFoundException("Not found Branch to create booth");
            }
            booth.Status = status;
            booth.CreateDate = DateTimeHelper.GetVietnamTimeNow();
            await _boothRepository.AddAsync(booth);
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
                    var devices = (await _deviceRepository.GetAsync(i => i.BoothID == id)).ToList();
                    foreach (var device in devices)
                    {
                        await _deviceRepository.RemoveAsync(device);
                    }
                    await _boothRepository.RemoveAsync(booth);
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
            var booths = await _boothRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BoothResponse>>(booths.ToList());
        }

        public async Task<IEnumerable<BoothResponse>> GetAllPagingAsync(BoothFilter filter, PagingModel paging)
        {
            var booths = (await _boothRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var listBoothresponse = _mapper.Map<IEnumerable<BoothResponse>>(booths);
            return listBoothresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
        }
        public async Task<IEnumerable<BoothResponse>> GetAvtiveBoothByTime(GetAvtiveBoothByTimeRequest request)
        {
            var booths = (await _boothRepository.GetAsync(i => i.BranchID == request.BranchID)).ToList(); //get booth from branch

            var boothid = booths.Select(i => i.BoothID).ToList();//convert to id list 
            var bookingBoothid = (await _bookingRepository.GetAsync(i => boothid.Contains(i.BoothID) //id list used here
                         && ((request.StartTime < i.StartTime && i.StartTime < request.EndTime.AddMinutes(5)) || (request.EndTime.AddMinutes(5) > i.EndTime.AddMinutes(5) && i.EndTime.AddMinutes(5) > request.StartTime))
                         && i.IsCancelled == false)).Select(i => i.Booth).Distinct().ToList();

            var result = booths.Except(bookingBoothid); // branch booth's collectiong - findout booth's collection = active booth's collection

            var listBoothresponse = _mapper.Map<IEnumerable<BoothResponse>>(result).ToList();
            return listBoothresponse;
        }
        public async Task<BoothResponse> GetByIdAsync(Guid id)
        {
            var booth = (await _boothRepository.GetAsync(b => b.BoothID == id)).FirstOrDefault();
            return _mapper.Map<BoothResponse>(booth);
        }

        public async Task<IEnumerable<BoothResponse>> GetByName(string name)
        {
            var booths = await _boothRepository.GetAsync(b => b.BoothName.Contains(name));
            return _mapper.Map<IEnumerable<BoothResponse>>(booths.ToList());
        }

        // Update
        public async Task UpdateAsync(Guid id, UpdateBoothRequest updateModel, BoothStatus? status)
        {
            var booth = (await _boothRepository.GetAsync(b => b.BoothID == id)).FirstOrDefault();
            if (booth == null)
            {
                throw new KeyNotFoundException("Booth not found.");
            }

            var updatedBooth = _mapper.Map(updateModel, booth);
            if (status.HasValue)
            {
                updatedBooth.Status = status.Value;
            }
            await _boothRepository.UpdateAsync(updatedBooth);
        }
    }
}
