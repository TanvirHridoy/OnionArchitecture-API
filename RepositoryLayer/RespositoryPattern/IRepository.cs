﻿using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.RespositoryPattern
{
   public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T Get(int Id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Remove(T entity);
        void SaveChanges();
        IEnumerable<T> GetList(T entity);

		//Async Methods added by Asad
		Task<int> InsertAsync(T entity);

	}
}
