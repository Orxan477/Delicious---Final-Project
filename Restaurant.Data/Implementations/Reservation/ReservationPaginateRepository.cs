using AutoMapper;
using Restaurant.Core.Interfaces;
using Restaurant.Core.Models;
using Restaurant.Data.DAL;

namespace Restaurant.Data.Implementations
{
    public class ReservationPaginateRepository:PaginateRepository<Reservation>,IReservationPaginateRepository
    {
        private readonly AppDbContext _context;
        public ReservationPaginateRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
    }
}
