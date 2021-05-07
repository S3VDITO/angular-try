namespace HeroesAPI.DAL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IDbContext<TEntity>
        where TEntity : IEntity
    {
        public Task Create(TEntity entity);

        public Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);

        public Task Update(TEntity entity);

        public Task Delete(Guid guid);
    }
}
