using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace BikeApis.Repositories.Infrastructures
{
	public interface IGenricRepo<T> where T : class
	{
		T GetById(int id);
		IEnumerable<T> GetAll();
		bool Create(T entity);
		bool Delete(T entity);
		public IEnumerable<T> Find(Expression<Func<T, bool>> expression);
	}
}
