using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSystemStore.Domain.InterfaceRepository
{
    public interface IMainRepository<T> where T : class
    {
        #region =========================== [Get Data]

        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(int id);

        Tkey? Max<Tkey>(Expression<Func<T, Tkey>> userKey);


        #endregion

        #region =========================== [Opration Data]

        Task AddAsync(T model);

        void Update(T model);

        Task<bool> DeleteAsync(int id);

        Task<bool> SaveDataAsync();

        #endregion
    }
}
