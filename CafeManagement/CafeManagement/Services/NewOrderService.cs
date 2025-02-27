﻿using CafeManagement.Enums;
using CafeManagement.Interfaces.Services;
using CafeManagement.Models.Order;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services
{
    public class NewOrderService : INewOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public NewOrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AddOrderDetail(Order order, OrderDetail detail)
        {
            order.Details.Add(detail);
            _unitOfWork.OrderDetail.Add(detail);
        }

        public void ChangeNote(Order order, string newnote)
        {
            order.Note=newnote;
            _unitOfWork.Order.Update(order);
        }

        public void ChangeStatus(Order order, OrderStatus status)
        {
            order.OrderStatus = status;
            //update history status{}
            _unitOfWork.Order.Update(order);
        }

        public void CreateOrder(Order order)
        {
            _unitOfWork.Order.Add(order);
        }

   
        public Order GetById(Guid orderId) //eager load
        {
            Order order = _unitOfWork.Order.GetById(orderId);
            return order;
        }
    }
}
