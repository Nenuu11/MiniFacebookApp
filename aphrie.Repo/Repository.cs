using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aphrie.Repo
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _ctx;
        private readonly DbSet<TEntity> _set;

        public Repository(DbContext ctx)
        {
            _ctx = ctx;
            _set = ctx.Set<TEntity>();
        }

        public DbSet<TEntity> Set { get { return _set; } }
        public DbContext Ctx { get { return _ctx; } }
        public TEntity Add(TEntity entity)
        {
            TEntity e = Set.Add(entity).Entity;
            return Ctx.SaveChanges() > 0 ? e : null;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            TEntity e = (await Set.AddAsync(entity)).Entity;
            return Ctx.SaveChanges() > 0 ? e : null;
        }

        public IQueryable<TEntity> GetAll()
        {
            return Set;
        }

        public IEnumerable<TEntity> GetAllBind()
        {
            return Set.ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllBindAsync()
        {
            return await Set.ToListAsync();
        }

        public TEntity GetById(params object[] id)
        {
            return Set.Find(id);
        }

        public async Task<TEntity> GetByIdAsync(params object[] id)
        {
            return await Set.FindAsync(id);
        }

        public bool Remove(TEntity entity)
        {
            Set.Attach(entity);
            _ctx.Entry(entity).State = EntityState.Deleted;
            return Ctx.SaveChanges() > 0;
        }

        public async Task<bool> RemoveAsync(TEntity entity)
        {
            Set.Attach(entity);
            _ctx.Entry(entity).State = EntityState.Deleted;
            return (await Ctx.SaveChangesAsync()) > 0;
        }

        public bool Update(TEntity entity)
        {
            Set.Attach(entity);
            _ctx.Entry(entity).State = EntityState.Modified;
            return Ctx.SaveChanges() > 0;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            Set.Attach(entity);
            _ctx.Entry(entity).State = EntityState.Modified;
            return (await Ctx.SaveChangesAsync()) > 0;
        }
    }
}
