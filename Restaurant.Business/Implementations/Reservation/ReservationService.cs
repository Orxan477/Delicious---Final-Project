using AutoMapper;
using Restaurant.Business.Interfaces;
using Restaurant.Business.Interfaces.Setting;
using Restaurant.Business.ViewModels.Reservation;
using Restaurant.Core.Interfaces;
using Restaurant.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Business.Implementations
{
    public class ReservationService : IReservationService,ISettingService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ISettingService _settingService;

        public ReservationService(IUnitOfWork unitOfWork,
                                   IMapper mapper,
                                   ISettingService settingService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _settingService = settingService;
        }
        public int GetPageCount(int take)
        {
            return _unitOfWork.ReservationPaginateRepository.GetPageCount(take, p => !p.IsCheck && !p.IsClose);
        }
        public async Task<List<Reservation>> GetPaginate(int count,int page)
        {
           return await _unitOfWork.ReservationPaginateRepository.GetPaginate(count, page, p => !p.IsCheck && !p.IsClose);
           
        }
        public async Task<List<Reservation>> GetAll()
        {
            return await _unitOfWork.ReservationPaginateRepository.GetAll(p => !p.IsCheck && !p.IsClose);
        }

        public List<ReservationListVM> GetProductList(List<Reservation> reserv)
        {
            List<ReservationListVM> models = new List<ReservationListVM>();
            foreach (var item in reserv)
            {
                ReservationListVM model =_mapper.Map<ReservationListVM>(item);
                models.Add(model);
            }
            return models;
        }

        public async Task<Reservation> Update(int id,int option)
        {
            Reservation dbReservation =await _unitOfWork.ReservationPaginateRepository.Get(x => x.Id == id && !x.IsCheck && !x.IsClose);
            if (dbReservation == null) throw new System.Exception();
            switch (option)
            {
                case 1:
                    dbReservation.IsCheck = true;
                    break;
                case 2:
                    dbReservation.IsClose = true;
                    break;
                default:
                    throw new System.Exception();
            }
            _unitOfWork.ReservationCRUDRepository.Update(dbReservation);
            await _unitOfWork.SaveChangeAsync();
            return dbReservation;
        }

        public async Task ReservationTable(ReservationVM reservationVM)
        {
            string response = GetSetting("ReservationCount");
            int tableCount=int.Parse(response);
            int reservCount = GetAll().Result.Count();
            if (tableCount == reservCount) throw new System.Exception("bad");
            if (reservationVM.PeopleCount > 10) throw new System.Exception("bad");
            Reservation reservation = _mapper.Map<Reservation>(reservationVM);
            await _unitOfWork.ReservationCRUDRepository.CreateAsync(reservation);
            await _unitOfWork.SaveChangeAsync();

        }

        public string GetSetting(string key)
        {
            return _settingService.GetSetting(key);
        }
    }
}
