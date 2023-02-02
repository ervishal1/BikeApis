using BikeApis.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using BikeApis.Repositories.Infrastructures;
using System.Linq;

namespace BikeApis.Repositories.Repos
{
	public class GenericRepo<T> : IGenricRepo<T> where T : class 
	{
		private readonly ApplicationDbContext _context;
		private readonly DbSet<T> _dbSet;

		public GenericRepo(ApplicationDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}

		public bool Create(T entity)
		{
			var t = _dbSet.Add(entity);
			return t != null;
		}

		public bool Delete(T entity)
		{
			var res = _dbSet.Remove(entity);
			return res != null;
		}

		public IEnumerable<T> GetAll()
		{
			return _dbSet;
		}

		public T GetById(int id)
		{
			return _dbSet.Find(id);
		}

		public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
		{
			return _dbSet.Where(expression);
		}
	}
}
