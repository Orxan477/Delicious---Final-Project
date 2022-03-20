using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Restaurant.Business.Interfaces.Home;
using Restaurant.Business.Interfaces.Setting;
using Restaurant.Business.Utilities;
using Restaurant.Business.ViewModels.Home.About;
using Restaurant.Core.Interfaces;
using Restaurant.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Business.Implementations.Home
{
    public class AboutService : IAboutService, ISettingService
    {
        private IUnitOfWork _unitOfWork;
        private ISettingService _settingService;
        private IMapper _mapper;
        private IWebHostEnvironment _env;
        private string _errorMessage;

        public AboutService(IUnitOfWork unitOfWork,ISettingService settingService,IMapper mapper, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _settingService = settingService;
            _mapper = mapper;
            _env = env;
        }
        public bool CheckImageValid(IFormFile file, string type, int size)
        {
            if (!Extension.CheckSize(file, size))
            {
                _errorMessage = $"The size of this photo is {size}";
                return false;
            }
            if (!Extension.CheckType(file, type))
            {
                _errorMessage = "The type is not correct";
                return false;
            }
            return true;
        }

        public async Task<About> Get(int id)
        {
            About dbAbout=await _unitOfWork.AboutGetRepository.Get(x=>x.Id==id);
            if (dbAbout is null) throw new System.Exception("not");
            return dbAbout; 
        }

        public async Task<List<About>> GetAll()
        {
            return await _unitOfWork.AboutGetRepository.GetAll();
        }

        public async Task<AboutUpdateVM> GetMap(int id)
        {
            return _mapper.Map<AboutUpdateVM>(await Get(id));
        }

        public string GetSetting(string key)
        {
            return _settingService.GetSetting(key);
        }

        public async Task Update(int id, AboutUpdateVM aboutUpdate)
        {
            About dbAbout = await _unitOfWork.AboutGetRepository.Get(x => x.Id == id);
            await CheckData(dbAbout, aboutUpdate);
            _unitOfWork.AboutCRUDRepository.Update(dbAbout);
            await _unitOfWork.SaveChangeAsync();
        }
        private async Task CheckData(About dbAbout,AboutUpdateVM aboutUpdate)
        {
            bool isCurrentHead = dbAbout.Head.Trim().ToLower() == aboutUpdate.Head.ToLower().Trim();
            if (!isCurrentHead)
            {
                dbAbout.Head = aboutUpdate.Head;
            }
            bool isCurrentNormalContent = dbAbout.NormalContent.Trim().ToLower() == aboutUpdate.NormalContent.ToLower().Trim();
            if (!isCurrentNormalContent)
            {
                dbAbout.NormalContent = aboutUpdate.NormalContent;
            }
            bool isCurrentItalicContent = dbAbout.ItalicContent.Trim().ToLower() == aboutUpdate.ItalicContent.ToLower().Trim();
            if (!isCurrentItalicContent)
            {
                dbAbout.ItalicContent = aboutUpdate.ItalicContent;
            }
            bool isCurrentNormalContent2 = dbAbout.NormalContent2.Trim().ToLower() == aboutUpdate.NormalContent2.ToLower().Trim();
            if (!isCurrentNormalContent2)
            {
                dbAbout.NormalContent2 = aboutUpdate.NormalContent2;
            }
            bool isCurrentLink = dbAbout.Link.Trim().ToLower() == aboutUpdate.Link.ToLower().Trim();
            if (!isCurrentLink)
            {
                dbAbout.Link = aboutUpdate.Link;
            }
            if (aboutUpdate.Photo != null)
            {
                string value = GetSetting("PhotoSize");
                int size = int.Parse(value);

                if (!CheckImageValid(aboutUpdate.Photo, "image/", size))
                {
                    throw new System.Exception(_errorMessage);
                }
                Helper.RemoveFile(_env.WebRootPath, "assets/img", dbAbout.Image);
                string fileName = await Extension.SaveFileAsync(aboutUpdate.Photo, _env.WebRootPath, "assets/img");
                dbAbout.Image = fileName;
            }
        }
    }
}
