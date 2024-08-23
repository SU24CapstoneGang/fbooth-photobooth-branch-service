using AutoMapper;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PhotoBox;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.PhotoBoxServices
{
    public class PhotoBoxService : IPhotoBoxService
    {
        private readonly IPhotoBoxRepository _photoBoxRepository;
        private readonly IMapper _mapper;
        private readonly ILayoutRepository _layoutRepository;

        public PhotoBoxService(IPhotoBoxRepository photoBoxRepository, IMapper mapper, ILayoutRepository layoutRepository)
        {
            _photoBoxRepository = photoBoxRepository;
            _mapper = mapper;
            _layoutRepository = layoutRepository;
        }

        public async Task<CreatePhotoBoxResponse> CreateAsync(CreatePhotoBoxRequest createModel)
        {
            var layout = (await _layoutRepository.GetAsync(l => l.LayoutID == createModel.LayoutID)).FirstOrDefault();

            if (layout != null)
            {
                var photobox = (await _photoBoxRepository.GetAsync(pb => pb.LayoutID == layout.LayoutID));
                if (photobox.Count() < layout.PhotoSlot)
                {
                    PhotoBox photoBox = _mapper.Map<PhotoBox>(createModel);
                    await _photoBoxRepository.AddAsync(photoBox);
                    return _mapper.Map<CreatePhotoBoxResponse>(photoBox);
                }
                throw new InvalidOperationException("Cannot create photo box, maximum photo slots reached.");
            }
            throw new NotFoundException($"Not found layout");
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var photoBox = (await _photoBoxRepository.GetAsync(i => i.PhotoBoxID == id)).FirstOrDefault();
                if (photoBox != null)
                {
                    await _photoBoxRepository.RemoveAsync(photoBox);

                }
                else
                {
                    throw new NotFoundException($"Not found {id} photo box");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<PhotoBoxResponse>> GetAllAsync()
        {
            var photoBox = await _photoBoxRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PhotoBoxResponse>>(photoBox.ToList());
        }

        public async Task<IEnumerable<PhotoBoxResponse>> GetAllPagingAsync(PhotoBoxFilter filter, PagingModel paging)
        {
            var photoBoxes = (await _photoBoxRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var photoBoxesResponse = _mapper.Map<IEnumerable<PhotoBoxResponse>>(photoBoxes);
            return photoBoxesResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
        }

        public async Task<PhotoBoxResponse> GetByIdAsync(Guid id)
        {
            var photoBox = await _photoBoxRepository.GetAsync(m => m.PhotoBoxID == id);
            return _mapper.Map<PhotoBoxResponse>(photoBox);
        }

        public async Task UpdateAsync(Guid id, UpdatePhotoBoxRequest updateModel)
        {
            var photoBox = (await _photoBoxRepository.GetAsync(m => m.PhotoBoxID == id)).FirstOrDefault();
            if (photoBox == null)
            {
                throw new KeyNotFoundException("Photo box not found.");
            }

            var updatePhotoBox = _mapper.Map(updateModel, photoBox);
            await _photoBoxRepository.UpdateAsync(updatePhotoBox);
        }
    }
}
