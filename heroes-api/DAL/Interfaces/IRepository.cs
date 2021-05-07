namespace HeroesAPI.DAL.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MongoDB.Driver;

    public interface IRepository<T>
        where T : class
    {
        public Task<IEnumerable<T>> GetAll();

        public Task<T> GetById(int id);

        public Task<IEnumerable<T>> GetBySubName(string name);

        public Task<T> Create(T item);

        public Task<T> Update(T item);

        public Task Delete(int id);
    }
}
