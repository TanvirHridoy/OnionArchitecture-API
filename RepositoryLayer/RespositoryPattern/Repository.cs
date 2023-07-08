using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using RepositoryLayer.ConnectionManager;
using Dapper;
using System.Threading.Tasks;

namespace RepositoryLayer.RespositoryPattern
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        #region property
        private readonly ApplicationDbContext _applicationDbContext;
        private DbSet<T> entities;
        private readonly IOraConnection _oraConnection;
        #endregion

        #region Constructor
        public Repository(ApplicationDbContext applicationDbContext, IOraConnection oraConnection)
        {
            _applicationDbContext = applicationDbContext;
            entities = _applicationDbContext.Set<T>();
            _oraConnection = oraConnection;

        }
        #endregion

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            _applicationDbContext.SaveChanges();
        }

        public T Get(int Id)
        {
            return entities.SingleOrDefault(c => c.Id == Id);
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            _applicationDbContext.SaveChanges();
        }

        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
        }

        public void SaveChanges()
        {
            _applicationDbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            _applicationDbContext.SaveChanges();
        }

        public IEnumerable<T> GetList(T entity)
        {

            IDbConnection connection = _oraConnection.GetConnection();

            var result = connection.Query<T>(GetColumnList(entity));

            _oraConnection.CloseConnection(connection);

            return result;
        }

        private static string GetColumnList(T entity)
        {
            string selectedColumns = "Select ";
            foreach (var prop in entity.GetType().GetProperties())
            {
                if (!prop.Name.Contains("_"))
                {
                    selectedColumns = selectedColumns + ConvertToPascalCase(prop.Name) + " AS " + prop.Name + ",";
                }
            }

            return selectedColumns + " From " + ConvertToPascalCase(entity.GetType().Name);
        }
        private static string ConvertToPascalCase(string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }

		public async Task<int> InsertAsync(T entity)
        {
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			entities.Add(entity);
			return await _applicationDbContext.SaveChangesAsync();
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _applicationDbContext.SaveChangesAsync();
		}

	}
}
