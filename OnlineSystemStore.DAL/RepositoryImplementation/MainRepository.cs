using Microsoft.EntityFrameworkCore;
using OnlineSystemStore.DAL.DB;
using OnlineSystemStore.Domain.InterfaceRepository;
using System.Linq.Expressions;


namespace OnlineSystemStore.DAL.RepositoryImplementation
{
    public class MainRepository<T> : IMainRepository<T> where T : class
    {
        private readonly OnlineDataStore db;

        public MainRepository(OnlineDataStore _db)
        {
            db = _db;
        }

        #region =========================== [Get Data]

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await db.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await db.Set<T>().FindAsync(id);
        }

        public Tkey? Max<Tkey>(Expression<Func<T, Tkey>> userKey)
        {
            return db.Set<T>().Select(userKey).DefaultIfEmpty().Max();
        }

        #endregion

        #region =========================== [Opration Data]

        public async Task AddAsync(T model)
        {
            await db.Set<T>().AddAsync(model);
        }

        public void Update(T model)
        {
            db.Set<T>().Update(model);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await GetByIdAsync(id);

            if (data is null)
            {
                return false;
            }

            db.Set<T>().Remove(data);

            return true;
        }

        public async Task<bool> SaveDataAsync()
        {
            return (await db.SaveChangesAsync() > 0);
        }

        #endregion 
    }
}
