using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Restaurant.Business.Implementations.Home;
using Restaurant.Business.Interfaces;
using Restaurant.Business.Interfaces.Home;
using Restaurant.Business.Interfaces.Setting;
using Restaurant.Core.Interfaces;

namespace Restaurant.Business.Implementations
{
    public class DeliciousService : IDeliciousService
    {
        private AboutService _aboutService;
        private ReservationService _reservationService;
        private AboutOptionService _aboutOptionService;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IWebHostEnvironment _env;
        private ISettingService _settingService;


        public DeliciousService(IUnitOfWork unitOfWork,
                           ISettingService settingService,
                           IWebHostEnvironment env,
                           IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
            _settingService = settingService;
        }
        public IAboutService AboutService => _aboutService ?? new AboutService(_unitOfWork, _settingService, _mapper,_env);

        public IReservationService ReservationService => _reservationService ?? new ReservationService(_unitOfWork,_mapper,_settingService);

        public IAboutOptionService AboutOptionService => _aboutOptionService ?? new AboutOptionService(_unitOfWork,_settingService,_mapper);
    }
}
