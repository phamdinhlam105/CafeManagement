﻿using CafeManagement.Models.OrderModel;

namespace CafeManagement.Dtos.Respone
{
    public class FinishOrderResponse
    {
        public Order order {  get; set; }
        public byte[] bill {  get; set; }
    }
}
