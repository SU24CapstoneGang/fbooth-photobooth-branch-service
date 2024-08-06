using AutoMapper;
using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Photo;
using PhotoboothBranchService.Application.Services.CloudinaryServices;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.PhotoServices
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IPhotoSessionRepository _photoSessionRepository;
        private readonly IBackgroundRepository _backgroundRepository;
        private readonly IStickerRepository _stickerRepository;
        public PhotoService(IPhotoRepository photoRepository, IMapper mapper,
            ICloudinaryService cloudinaryService, IPhotoSessionRepository photoSessionRepository,
            IBackgroundRepository backgroundRepository, IStickerRepository stickerRepository)
        {
            _photoRepository = photoRepository;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
            _photoSessionRepository = photoSessionRepository;
            _backgroundRepository = backgroundRepository;
            _stickerRepository = stickerRepository;
        }

        public async Task<PhotoResponse> CreatePhotoAsync(IFormFile file, CreatePhotoRequest createPhotoRequest)
        {

            // validate
            if (createPhotoRequest.BackgroundID.HasValue)
            {
                if (createPhotoRequest.Version == Domain.Enum.PhotoVersion.Original)
                {
                    throw new Exception("An original photo can not has Background.");
                }
                var background = (await _backgroundRepository.GetAsync(f => f.BackgroundID.Equals(createPhotoRequest.BackgroundID))).FirstOrDefault();
                if (background == null)
                {
                    throw new NotFoundException("Background not found.");
                }
            }
            var photosession = (await _photoSessionRepository.GetAsync(f => f.PhotoSessionID.Equals(createPhotoRequest.PhotoSessionID))).FirstOrDefault();
            if (photosession == null)
            {
                throw new NotFoundException("Photo Session not found.");
            }
            if (photosession.Status == Domain.Enum.PhotoSessionStatus.Ended)
            {
                throw new BadRequestException("Photo session has ended.");
            }
            //upload to cloudinary
            string folder;
            List<PhotoSticker> photoStickers = new List<PhotoSticker>();
            if (createPhotoRequest.Version == Domain.Enum.PhotoVersion.Original)
            {
                folder = "FBooth-OriginalPhoto";
            }
            else
            {
                folder = "FBooth-FinnalPicture";
                if (createPhotoRequest.StickerList.Count > 0)
                {
                    var stickerList = (await _stickerRepository.GetAsync(i => createPhotoRequest.StickerList.Keys.ToList().Contains(i.StickerID))).ToList();
                    if (stickerList.Any(i => i.Status == Domain.Enum.StatusUse.Unusable))
                    {
                        throw new BadRequestException("There are unable to use sticker in request.");
                    }
                    if (stickerList.Count() != createPhotoRequest.StickerList.Count()) 
                    {
                        throw new NotFoundException("There is sticker not found in server.");
                    }
                    foreach (var sticker in stickerList)
                    {
                        photoStickers.Add(new PhotoSticker
                        {
                            Quantity = (short)createPhotoRequest.StickerList[sticker.StickerID],
                            StickerID = sticker.StickerID,
                        });
                    }
                }
            }
            var uploadResult = await _cloudinaryService.AddPhotoAsync(file, folder);
            if (uploadResult.Error != null)
            {
                throw new Exception(uploadResult.Error.Message);
            }

            //create object from cloudinary's return and CreatePhotoRequest
            Photo photo = new Photo
            {
                PhotoURL = uploadResult.SecureUrl.AbsoluteUri,
                CouldID = uploadResult.PublicId,
                PhotoSessionID = createPhotoRequest.PhotoSessionID,
                Version = createPhotoRequest.Version
            };
            if (createPhotoRequest.BackgroundID.HasValue)
            {
                photo.BackgroundID = createPhotoRequest.BackgroundID.Value;
                photo.PhotoStickers = photoStickers;
            }

            await _photoRepository.AddAsync(photo);
            return _mapper.Map<PhotoResponse>(photo);
        }


        public async Task DeleteAsync(Guid id)
        {
            var Photos = await _photoRepository.GetAsync(f => f.PhotoID == id);
            var photoToDelete = Photos.FirstOrDefault();
            if (photoToDelete != null)
            {
                await _cloudinaryService.DeletePhotoAsync(photoToDelete.CouldID);
                photoToDelete.PhotoURL = "";
                photoToDelete.CouldID = "";
                await _photoRepository.UpdateAsync(photoToDelete);
            } else
            {
                throw new NotFoundException("Photo not found.");
            }
        }

        public async Task<IEnumerable<PhotoResponse>> GetAllAsync()
        {
            var photos = await _photoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PhotoResponse>>(photos.ToList());
        }

        public async Task<IEnumerable<PhotoResponse>> GetAllPagingAsync(PhotoFilter filter, PagingModel paging)
        {
            var photos = (await _photoRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var photosResponse = _mapper.Map<IEnumerable<PhotoResponse>>(photos);
            return photosResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
        }

        public async Task<PhotoResponse> GetByIdAsync(Guid id)
        {
            var finalPictures = await _photoRepository.GetAsync(f => f.PhotoID == id);
            return _mapper.Map<PhotoResponse>(finalPictures);
        }

        public async Task UpdateAsync(Guid id, UpdatePhotoRequest updateModel)
        {
            var photo = (await _photoRepository.GetAsync(f => f.PhotoID == id)).FirstOrDefault();
            if (photo == null)
            {
                throw new NotFoundException("Photo not found.");
            }

            var updatedFinalPicture = _mapper.Map(updateModel, photo);
            await _photoRepository.UpdateAsync(updatedFinalPicture);
        }
    }
}
