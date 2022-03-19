using Restaurant.Business.ViewModels.Reservation;
using Restaurant.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Business.Interfaces
{
    public interface IReservationService
    {
        Task<List<Reservation>> GetPaginate(int count,int page);
        int GetPageCount(int take);
        List<ReservationListVM> GetProductList(List<Reservation> products);
    }
}
