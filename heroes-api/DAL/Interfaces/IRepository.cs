namespace HeroesAPI.DAL.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MongoDB.Driver;

    public interface IRepository<T>
        where T : class
    {
        public Task<List<T>> GetAll();

        public Task<T> GetById(int id);

        public Task<List<T>> GetBySubName(string name);

        public Task<T> Create(T item);

        public Task<T> Update(T item);

        public Task<DeleteResult> Delete(int id);
    }
}
