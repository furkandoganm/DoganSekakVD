using AppCore.DataAccess.Repositories.Bases;
using AppCore.Records;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.DataAccess.Repositories.EntityFramework
{
    public abstract class RepositoryBase<TEntity>: IRepository<TEntity> where TEntity: Record, new()
    {
        private readonly DbContext _db;
        public RepositoryBase(DbContext db)
        {
            _db = db;
        }
        public virtual IQueryable<TEntity> Query(params string[] entitiesToInclude)
        {
            var query = _db.Set<TEntity>().AsQueryable();
            foreach (var entity in entitiesToInclude)
            {
                query = query.Include(entity);
            }
            return query;
        }
        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, params string[] entitiesToInclude)
        {
            var entityQuery = Query(entitiesToInclude);
            return entityQuery.Where(predicate);
        } 

        public void Add(TEntity entity, bool save = true)
        {
            entity.GuId = Guid.NewGuid().ToString();
            _db.Set<TEntity>().Add(entity);
            if (save == true)
                Save();
        }

        public void Update(TEntity entity, bool save = true)
        {
            _db.Set<TEntity>().Update(entity);
            if (save == true)
                Save();
        }

        public void Delete(TEntity entity, bool save = true)
        {
            _db.Set<TEntity>().Remove(entity);
            if (save == true)
                Save();
        }
        public virtual void Delete(string guId, bool save = true)
        {
            var entity = Query(e => e.GuId == guId).SingleOrDefault();
            //_db.Set<TEntity>().Remove(entity);
            Delete(entity, save);
        }

        private bool _disposed = false;

        private void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _db?.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
