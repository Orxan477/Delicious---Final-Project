using AutoMapper;
using Restaurant.Business.Interfaces.Home;
using Restaurant.Business.Interfaces.Setting;
using Restaurant.Business.ViewModels.Home.About;
using Restaurant.Core.Interfaces;
using Restaurant.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Business.Implementations.Home
{
    public class AboutOptionService : IAboutOptionService
    {
        private IUnitOfWork _unitOfWork;
        private ISettingService _settingService;
        private IMapper _mapper;

        public AboutOptionService(IUnitOfWork unitOfWork, ISettingService settingService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _settingService = settingService;
            _mapper = mapper;
         }

        public async Task Create(AboutOptionCreateVM aboutOptionCreate)
        { 
            AboutOption nameContext = await _unitOfWork.AboutOptionGetRepository.Get(x=>x.Option.Trim().ToLower()==aboutOptionCreate.Option.Trim().ToLower());
            if (nameContext is null) throw new System.Exception("Not");
            AboutOption aboutOption = _mapper.Map<AboutOption>(aboutOptionCreate);
            await _unitOfWork.AboutOptionCRUDRepository.CreateAsync(aboutOption);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task Delete(int id)
        {
            AboutOption dbAboutOption = await Get(id);
            if (dbAboutOption is null) throw new System.Exception("not");
            dbAboutOption.IsDeleted = true;
            _unitOfWork.AboutOptionCRUDRepository.Update(dbAboutOption);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<AboutOption> Get(int id)
        {
            AboutOption dbAbout = await _unitOfWork.AboutOptionGetRepository.Get(x => x.Id == id && !x.IsDeleted);
            if (dbAbout is null) throw new System.Exception("not");
            return dbAbout;
        }

        public async Task<List<AboutOption>> GetAll()
        {
            return await _unitOfWork.AboutOptionGetRepository.GetAll(x=>!x.IsDeleted);
        }

        public async Task<AboutOptionUpdateVM> GetMap(int id)
        {
            return _mapper.Map<AboutOptionUpdateVM>(await Get(id));
        }

        public string GetSetting(string key)
        {
            return _settingService.GetSetting(key);
        }

        public async Task Update(int id, AboutOptionUpdateVM aboutUpdate)
        {
            AboutOption dbAboutOption = await Get(id);
            if (dbAboutOption is null) throw new System.Exception("not");
            await CheckData(dbAboutOption, aboutUpdate);
            _unitOfWork.AboutOptionCRUDRepository.Update(dbAboutOption);
            await _unitOfWork.SaveChangeAsync();
        }
        private async Task CheckData(AboutOption dbAboutOption, AboutOptionUpdateVM aboutOptionUpdate)
        {
            bool isCurrentName = dbAboutOption.Option.Trim().ToLower() == aboutOptionUpdate.Option.ToLower().Trim();
            AboutOption nameContext = await _unitOfWork.AboutOptionGetRepository.Get(x => x.Option.Trim().ToLower() == aboutOptionUpdate.Option.Trim().ToLower());
            if (nameContext != null)
            {
                if (!isCurrentName)
                {
                    throw new System.Exception("not");
                }
                if (!isCurrentName)
                {
                    dbAboutOption.Option = aboutOptionUpdate.Option;
                }
            }
            else
            {
                throw new System.Exception("not");
            }
        }
    }
}
