using DomainLayer.Models;
using RepositoryLayer.QueryBuilder;
using RepositoryLayer.RespositoryPattern;
using System;
using System.Collections.Generic;
using System.Text;
using static Dapper.SqlMapper;

namespace ServicesLayer.CustomerService
{
    public class CustomerService : ICustomerService
    {
        #region Property
        private IRepository<Customer> _repository;
        private readonly IOraQueryBuilder<Customer> _oraQueryBuilder;
        #endregion

        #region Constructor
        public CustomerService(IRepository<Customer> repository, IOraQueryBuilder<Customer> oraQueryBuilder)
        {
            _repository = repository;
            _oraQueryBuilder = oraQueryBuilder;
        }
        #endregion

        public IEnumerable<Customer> GetAllCustomers()
        {
           return _repository.GetAll();
        }

        public Customer GetCustomer(int id)
        {
            return _repository.Get(id);
        }

        public void InsertCustomer(Customer customer)
        {
            _repository.Insert(customer);
        }
        public void UpdateCustomer(Customer customer)
        {
            _repository.Update(customer);
        }
        public void DeleteCustomer(int id)
        {
            Customer customer = GetCustomer(id);
            _repository.Remove(customer);
            _repository.SaveChanges();
        }
        public IEnumerable<Customer> GetList(Customer entity)
        {
            return _oraQueryBuilder.GetList(entity);
        }
    }
}
