﻿using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using CafeManagement.Models;

namespace CafeManagement.Repositories
{
    public class DailyStockRepository:BaseRepository<DailyStock>,IDailyStockRepository
    {
        public DailyStockRepository(CafeManagementDbContext _context) : base(_context) { }
    }
}
