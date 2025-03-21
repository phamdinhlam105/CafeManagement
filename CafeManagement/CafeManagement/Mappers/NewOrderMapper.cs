﻿using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Enums;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Models.Order;

namespace CafeManagement.Mappers
{
    public class NewOrderMapper : INewOrderMapper
    {
        public Order MapToEntity(NewOrderRequest request)
        {
                return new Order
                {
                    Id = Guid.NewGuid(),
                    Note = request.Note,
                    OrderStatus=OrderStatus.New,
                    CustomerId= request.CustomerId

                };
        }

        public NewOrderResponse MapToResponse(Order order)
        {

            NewOrderResponse response = new NewOrderResponse
            {
                Id = order.Id,
                Note = order.Note,
                Total = order.Price,
                Amount = order.Quantity,
                Status = order.OrderStatus
            };
            return response;
        }

        public void UpdateEntityFromRequest(Order order, NewOrderRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
