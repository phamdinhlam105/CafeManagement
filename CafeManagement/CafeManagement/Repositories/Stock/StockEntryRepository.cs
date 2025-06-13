﻿using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.Stock;
using CafeManagement.Models.Stock;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories.Stock
{
    public class StockEntryRepository : BaseRepository<StockEntry>, IStockEntryRepository
    {
        public StockEntryRepository(CafeManagementDbContext _context) : base(_context) { }
        public override async Task<IEnumerable<StockEntry>> GetAll()
        {
            return await _context.StockEntries
                .Include(se => se.StockEntryDetails)
                .ThenInclude(dt => dt.Ingredient).ToListAsync();
        }
        public async Task<IEnumerable<StockEntry>> GetByDate(DateOnly date)
        {
            return await _context.StockEntries
                 .Include(se => se.StockEntryDetails)
                 .Where(se=> DateOnly.FromDateTime(se.EntryDate) == date)
                 .ToListAsync();
        }
    }
}
