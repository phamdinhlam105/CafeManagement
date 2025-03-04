﻿using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using CafeManagement.Models.Order;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories
{
    public class OrderRepository: BaseRepository<Order>,IOrderRepository
    {
        public OrderRepository(CafeManagementDbContext _context):base(_context) { }

        public override async Task<Order> GetById(Guid id)
        {
            return await _context.Orders
                .Include(o=>o.Details)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
