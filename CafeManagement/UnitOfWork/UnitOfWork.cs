using CafeManagement.Data;
using CafeManagement.Repositories;
using CafeManagement.Repositories.Interfaces;
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
        private IOrderDetailRepository _orderDetail ;
        private ICustomerRepository _customer ;
        private static UnitOfWork _instance;
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
