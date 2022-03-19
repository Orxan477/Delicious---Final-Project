﻿using AutoMapper;
using Restaurant.Business.Interfaces;
using Restaurant.Business.ViewModels.Reservation;
using Restaurant.Core.Interfaces;
using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Restaurant.Business.Implementations
{
    public class ReservationService : IReservationService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ReservationService(IUnitOfWork unitOfWork,
                                   IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public int GetPageCount(int take)
        {
            return _unitOfWork.ReservationPaginateRepository.GetPageCount(take, p => !p.IsCheck && !p.IsClose);
        }

        public async Task<List<Reservation>> GetPaginate(int count,int page)
        {
           return await _unitOfWork.ReservationPaginateRepository.GetPaginate(count, page, p => !p.IsCheck && !p.IsClose);
           
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
    }
}
