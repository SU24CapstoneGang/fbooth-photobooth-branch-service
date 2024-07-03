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
        public PhotoService(IPhotoRepository photoRepository, IMapper mapper,
            ICloudinaryService cloudinaryService, IPhotoSessionRepository photoSessionRepository,
            IBackgroundRepository backgroundRepository)
        {
            _photoRepository = photoRepository;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
            _photoSessionRepository = photoSessionRepository;
            _backgroundRepository = backgroundRepository;
        }

        public async Task<CreatePhotoResponse> CreateAsync(CreatePhotoRequest createModel)
        {
            Photo photo = _mapper.Map<Photo>(createModel);
            await _photoRepository.AddAsync(photo);
            return _mapper.Map<CreatePhotoResponse>(photo);
        }

        public async Task<PhotoResponse> CreatePhotoAsync(IFormFile file, CreatePhotoRequest createPhotoRequest)
        {

            // validate
            if (createPhotoRequest.BackgroundID.HasValue)
            {
                if (createPhotoRequest.Version == Domain.Enum.PhotoVersion.Original)
                {
                    throw new Exception("An original photo can not has Background");
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

            //upload to cloudinary
            string folder;
            if (createPhotoRequest.Version == Domain.Enum.PhotoVersion.Original)
            {
                folder = "FBooth-OriginalPhoto";
            }
            else
            {
                folder = "FBooth-FinnalPicture";
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
                await _photoRepository.RemoveAsync(photoToDelete);
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
                throw new KeyNotFoundException("Photo not found.");
            }

            var updatedFinalPicture = _mapper.Map(updateModel, photo);
            await _photoRepository.UpdateAsync(updatedFinalPicture);
        }
    }
}
