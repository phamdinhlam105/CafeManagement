using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories;
using CafeManagement.Interfaces.Repositories.Report;
using CafeManagement.Interfaces.Repositories.Stock;
using CafeManagement.Repositories;
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
        private IUserRepository _user;
        private IOrderRepository _order ;
        private IOnlineOrderRepository _onlineOrder;
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

        public IUserRepository User
        {
            get
            {
                if( _user == null)
                    lock (_lock)
                        if (_user == null)
                            return _user = new UserRepository(_context);
                return _user;
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

        public IOnlineOrderRepository OnlineOrder
        {
            get
            {
                if(_onlineOrder==null)
                    lock(_lock)
                        if (_onlineOrder==null)
                            return _onlineOrder=new OnlineOrderRepository(_context);
                return _onlineOrder;
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
        public IDailyReportRepository DailyReportRepository
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
        public IMonthlyReportRepository MonthlyReportRepository
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
        public IQuarterlyReportRepository QuarterlyReportRepository
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
        public IYearlyReportRepository YearlyReportRepository
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
        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
