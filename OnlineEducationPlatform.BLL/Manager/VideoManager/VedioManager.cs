using AutoMapper;
using OnlineEducationPlatform.DAL.Data.Models;
using OnlineEducationPlatform.DAL.Repositories;
using OnlineEducationPlatform.BLL.Dto.VideoDto;

namespace OnlineEducationPlatform.BLL.Manager
{
    public class VedioManager:IVedioManager
    {
        private readonly IVedioRepo _vedioRepo;
        private readonly IMapper _mapper;

        public VedioManager(IVedioRepo vedioRepo, IMapper mapper)
        {
            _vedioRepo = vedioRepo;
            _mapper = mapper;
        }
        public async Task AddAsync(VedioAddVm vedioAddDto) /// Done 
        {
            var video = _mapper.Map<Video>(vedioAddDto);
            await _vedioRepo.AddAsync(video);

        }


        public async Task<bool> DeleteAsync(int id)
        {
            var video = await _vedioRepo.GetByIdAsync(id);
            if (video != null)
            {
                var result = await _vedioRepo.DeleteAsync(video.Id);
                //SaveChangesAsync();
                if (result == true)
                {
                    return true;
                }
                return false;
            }
            return false;
        }


        public async Task<IEnumerable<VedioReadVm>> GetAllAsync()
        {
            var vedeos = await _vedioRepo.GetAllAsync();
            return _mapper.Map<List<VedioReadVm>>(vedeos);
        }



        public async Task<VedioReadVm> GetByIdAsync(int id)
        {
            var video = await _vedioRepo.GetByIdAsync(id);
            if (video != null)
            {
                return _mapper.Map<VedioReadVm>(video);
            }
            return null;
        }


        public async Task<VedioUpdateVm> UpdateAsync(VedioUpdateVm vedioUpdateDto)
        {
            var video = await _vedioRepo.GetByIdAsync(vedioUpdateDto.Id);
            if (video != null)
            {
                video.Id = vedioUpdateDto.Id;
                video.Url = vedioUpdateDto.Url;
               
                video.Title = vedioUpdateDto.Title;

                await _vedioRepo.UpdateAsync(video);
            }
            return null;
        }
    }
}
