﻿using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using CafeManagement.Interfaces.Repositories.PromotionRepo;
using CafeManagement.Interfaces.Repositories.Report;
using CafeManagement.Interfaces.Repositories.Stock;
using CafeManagement.Repositories;
using CafeManagement.Repositories.PromotionRepo;
using CafeManagement.Repositories.Report;
using CafeManagement.Repositories.Stock;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;

namespace CafeManagement.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CafeManagementDbContext _context;
        private IProductRepository _product;
        private ICategoryRepository _category;
        private IOrderRepository _order ;
        private IOrderDetailRepository _orderDetail ;
        private ICustomerRepository _customer ;
        private IIngredientRepository _ingredientRepository ;
        private IDailyStockRepository _dailyStockRepository ;
        private IDailyStockDetailRepository _dailyStockDetailRepository ;
        private IStockEntryRepository _stockEntryRepository ;
        private IStockEntryDetailRepository _stockEntryDetailRepository ;
        private IDailyReportRepository _dailyReportRepository ;
        private IMonthlyReportRepository _monthlyReportRepository ;
        private IQuarterlyReportRepository _quarterlyReportRepository ;
        private IYearlyReportRepository _yearlyReportRepository ;
        private IPromotion _promotionRepository;
        private IPromotionSchedule _promotionScheduleRepository;
        private IProfileRepository _profileRepository ;
        private static readonly object _lock = new object();

        public UnitOfWork(CafeManagementDbContext context)
        {
            _context = context;
        }
       
        public IProductRepository Product
        {
            get
            {
                if (_product == null)
                    lock (_lock)
                        if (_product == null)
                            return _product = new ProductRepository(_context);
                return _product;
            }
        }
        
        public ICategoryRepository Category
        {
            get
            {
                if (_category == null)
                    lock (_lock)
                        if (_category == null)
                            return _category = new CategoryRepository(_context);
                return _category;
            }
        }

       
        public IOrderRepository Order
        {
            get
            {
                if( (_order == null) )
                    lock (_lock)
                        if (_order == null)
                            return _order = new OrderRepository(_context);
                return _order;
            }
        }

        public IOrderDetailRepository OrderDetail
        {
            get
            {
                if (_orderDetail == null)
                    lock (_lock)
                        if (_orderDetail == null)
                            return _orderDetail = new OrderDetailsRepository(_context);
                return _orderDetail;
            }
        }

        public ICustomerRepository Customer
        {
            get
            {
                if(_customer==null)
                    lock (_lock)
                        if (_customer == null)
                            return _customer = new CustomerRepository(_context);
                return _customer;
            }
        }

        public IIngredientRepository Ingredient
        {
            get
            {
                if(_ingredientRepository==null)
                    lock(_lock)
                        if(_ingredientRepository == null)
                            return _ingredientRepository = new IngredientRepository(_context);
                return _ingredientRepository;
            }
        }
        public IDailyStockRepository DailyStock
        {
            get
            {
                if (_dailyStockRepository == null)
                    lock (_lock)
                        if (_dailyStockRepository == null)
                            return _dailyStockRepository = new DailyStockRepository(_context);
                return _dailyStockRepository;
            }
        }
        public IDailyStockDetailRepository DailyStockDetail
        {
            get
            {
                if (_dailyStockDetailRepository == null)
                    lock (_lock)
                        if (_dailyStockDetailRepository == null)
                            return _dailyStockDetailRepository = new DailyStockDetailRepository(_context);
                return _dailyStockDetailRepository;
            }
        }

        public IStockEntryRepository StockEntry
        {
            get
            {
                if (_stockEntryRepository == null)
                    lock (_lock)
                        if (_stockEntryRepository == null)
                            return _stockEntryRepository = new StockEntryRepository(_context);
                return _stockEntryRepository;
            }
        }
        public IStockEntryDetailRepository StockEntryDetail
        {
            get
            {
                if (_stockEntryDetailRepository == null)
                    lock (_lock)
                        if (_stockEntryDetailRepository == null)
                            return _stockEntryDetailRepository = new StockEntryDetailRepository(_context);
                return _stockEntryDetailRepository;
            }
        }
        public IDailyReportRepository DailyReport
        {
            get
            {
                if (_dailyReportRepository == null)
                    lock (_lock)
                        if (_dailyReportRepository == null)
                            return _dailyReportRepository = new DailyReportRepository(_context);
                return _dailyReportRepository;
            }
        }
        public IMonthlyReportRepository MonthlyReport
        {
            get
            {
                if (_monthlyReportRepository == null)
                    lock (_lock)
                        if (_monthlyReportRepository == null)
                            return _monthlyReportRepository = new MonthlyReportRepository(_context);
                return _monthlyReportRepository;
            }
        }
        public IQuarterlyReportRepository QuarterlyReport
        {
            get
            {
                if (_quarterlyReportRepository == null)
                    lock (_lock)
                        if (_quarterlyReportRepository == null)
                            return _quarterlyReportRepository = new QuarterlyReportRepository(_context);
                return _quarterlyReportRepository;
            }
        }
        public IYearlyReportRepository YearlyReport
        {
            get
            {
                if (_yearlyReportRepository == null)
                    lock (_lock)
                        if (_yearlyReportRepository == null)
                            return _yearlyReportRepository = new YearlyReportRepository(_context);
                return _yearlyReportRepository;
            }
        }
        public IPromotion Promotion
        {
            get
            {
                if (_promotionRepository == null)
                    lock (_lock)
                        if (_promotionRepository == null)
                            return _promotionRepository = new PromotionRepository(_context);
                return _promotionRepository;
            }
        }
        public IPromotionSchedule PromotionSchedule
        {
            get
            {
                if (_promotionScheduleRepository == null)
                    lock (_lock)
                        if (_promotionScheduleRepository == null)
                            return _promotionScheduleRepository = new PromotionScheduleRepository(_context);
                return _promotionScheduleRepository;
            }
        }
        public IProfileRepository Profile
        {
            get
            {
                if(_profileRepository == null)
                    lock(_lock)
                        if(_profileRepository==null)
                            return _profileRepository = new ProfileRepository(_context);
                return _profileRepository;
            }
        }
        public async ValueTask DisposeAsync()
        {

            if (_context != null)
            {
                await _context.DisposeAsync();
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }


    }
}
