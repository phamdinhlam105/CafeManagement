﻿using CafeManagement.Interfaces.Services;
using CafeManagement.Models;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services
{
    public class OrderDetailService:IOrderDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        public void Add(OrderDetail item)
        {
            _unitOfWork.OrderDetail.Add(item);
        }

        public void Delete(Guid id)
        {
            var item = _unitOfWork.OrderDetail.GetById(id);
            if (item != null)
                _unitOfWork.OrderDetail.Delete(item);
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return _unitOfWork.OrderDetail.GetAll();
        }

        public OrderDetail GetById(Guid id)
        {
            return _unitOfWork.OrderDetail.GetById(id);
        }

        public void Update(OrderDetail item)
        {
            if (item != null)
                _unitOfWork.OrderDetail.Update(item);
        }
       
    }
}
