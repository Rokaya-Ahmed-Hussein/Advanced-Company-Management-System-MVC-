using DTO.Data.Cnotext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Repositories.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly EddbAppContext eddbContext;
        #region Default Constractor
        public GenericRepository(EddbAppContext eddbAppCont)
        {
            eddbContext = eddbAppCont;
            
        }
        #endregion

        #region GetAllEntities
        public List<TEntity> GetAllEntities()
        {
           return eddbContext.Set<TEntity>().ToList();
        }

        #endregion

        #region GetEntityByI
        public TEntity? GetEntityById(int id)
        {
            return eddbContext.Set<TEntity>().Find(id);

        }

        #endregion

        #region Add
        public void Add(TEntity entity)
        {
            eddbContext.Set<TEntity>().Add(entity);
        }
        #endregion

        #region DeleteEntityByID
        public void Delete(int id)
        {
            var entity = GetEntityById(id);
            if (entity != null)
                eddbContext.Set<TEntity>().Remove(entity);

        }
        #endregion

        #region SaveChanges
        public void SaveChanges()
        {
            eddbContext.SaveChanges();
        }
        #endregion

        #region Update
        public void Update(TEntity entity) { }
        #endregion

    }
}
