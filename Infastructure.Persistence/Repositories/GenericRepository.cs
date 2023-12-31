﻿using Domain.Core;
using Domain.Core.Entities.Base;
using Domain.Core.Enums;
using Domain.Core.Interfaces;
using Infastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq.Expressions;

namespace Infastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        public DbSet<TEntity>? dbSet { get; set; }
        public IApplicationDbContext? Context { get; set; }
        public IMediator mediator { get; set; }
        public ILogger logger { get; set; }

        public async Task<IEnumerable<TEntity>> GetCollectionAsync(Expression<Func<TEntity, bool>>? filter = null,
                                                             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                             string includeProperties = "")
        {

            IQueryable<TEntity> query = dbSet;
            if (filter != null) query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            if (orderBy != null) return await orderBy(query).ToListAsync();
            else return await query.ToListAsync();
        }

        public IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>>? filter = null,
                                             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                             string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null) query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            if (orderBy != null) return orderBy(query);
            else return query;
        }

        public virtual async Task<(CrudEn, int)> InsertOrUpdate(TEntity entity)
        {
            CrudEn enumRes;
            var id = ((dynamic)entity).Id;
            var entityDb = await GetById(id);

            if (entityDb != null)
            {
                ((ApplicationDbContext)Context).Entry<TEntity>(entityDb).State = EntityState.Detached;
                enumRes = CrudEn.Update;
                Update(entity);
            }
            else
            {
                enumRes = CrudEn.Create;
                Insert(entity);
            }
            return (enumRes, await ((ApplicationDbContext)Context).SaveChangesAsync());
        }

        public async Task PublishDomain(TEntity entity)
        {
            try
            {
                foreach (var dEvent in entity.DomainEvents)
                    await mediator.Publish(dEvent);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
        }
        public async Task<TEntity> GetById(object id) => await dbSet.FindAsync(id);

        public void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            var entry = ((ApplicationDbContext)Context).Entry<TEntity>(entity);
            entry.State = EntityState.Modified;
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            if (entity == null) return -1;
            dbSet.Attach(entity);
            var entry = ((ApplicationDbContext)Context).Entry<TEntity>(entity);
            entry.State = EntityState.Modified;
            return await ((ApplicationDbContext)Context).SaveChangesAsync();
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.AddAsync(entity);
        }


    }
}
