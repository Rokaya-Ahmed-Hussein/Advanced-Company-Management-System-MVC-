using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Repositories.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        List<TEntity> GetAllEntities();
        TEntity? GetEntityById(int id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
        void SaveChanges();

    }

  
}
