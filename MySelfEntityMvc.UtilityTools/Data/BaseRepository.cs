using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using MySelfEntityMvc.Models.EntityCustom;
using MySelfEntityMvc.Models.Enumerations;

namespace MySelfEntityMvc.UtilityTools.Data
{
    /// <summary>
    /// Entity Framework repository
    /// </summary>
    /// <typeparam name="TEntity">Object model</typeparam>
    public class BaseRepository<TEntity> where TEntity : class
    {
        #region [Fields]

        private IDbSet<TEntity> entities;

        #endregion

        #region [Properties]
        /// <summary>
        /// 
        /// </summary>
        protected UnitOfWork UnitOfWork { get; set; }

        #endregion

        #region [Constructor]

        /// <summary>
        /// Empty constructor
        /// </summary>
        public BaseRepository()
        {
            this.UnitOfWork = new UnitOfWork();
        }

        #endregion

        #region [Protected Properties]

        /// <summary>
        /// 
        /// </summary>
        protected IDbSet<TEntity> Entities
        {
            get
            {
                bool nullEntity = ContextManager.IsObjectContextisNull() || this.entities == null;
                return nullEntity ? this.entities = UnitOfWork.ObjectContext.Set<TEntity>() : this.entities;
            }
            //get { return this.entities ?? (this.entities = this.UnitOfWork.ObjectContext.Set<TEntity>()); }
        }

        #endregion

        #region [Public Methods]

        /// <summary>
        /// Updates entity object
        /// </summary>
        /// <param name="entity"></param>
        public TEntity Update(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                //this.UnitOfWork.ObjectContext.Set<TEntity>().Attach(entity);
                this.UnitOfWork.ObjectContext.SetModified(entity);
                this.UnitOfWork.SaveChanges();

                return entity;
            }
            catch (DbEntityValidationException databaseException)
            {
                this.UnitOfWork.ObjectContext.SetDetached(entity);
                throw this.TrackDBEntityException(databaseException);
            }
        }

        /// <summary>
        /// Find Entity by primary key values
        /// </summary>
        /// <param name="keyValues">primary key values</param>
        /// <returns></returns>
        public TEntity Find(params object[] keyValues)
        {
            return this.Entities.Find(keyValues);
        }

        /// <summary>
        /// Add entity to the repository
        /// </summary>
        /// <param name="entity">the entity to add</param>  
        public TEntity Insert(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                TEntity result = this.Entities.Add(entity);
                this.UnitOfWork.SaveChanges();

                return result;
            }
            catch (DbEntityValidationException databaseException)
            {
                this.UnitOfWork.ObjectContext.SetDetached(entity);
                throw this.TrackDBEntityException(databaseException);
            }
        }

        /// <summary>
        /// Mark entity to be deleted within the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        public void Delete(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.Remove(entity);
                this.UnitOfWork.SaveChanges();
            }
            catch (DbEntityValidationException databaseException)
            {
                throw this.TrackDBEntityException(databaseException);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            return this.Entities.Where(expression);
        }

        /// <summary>
        /// 
        /// </summary>
        public IQueryable<TEntity> GetAll()
        {
            return this.Entities.AsQueryable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public TEntity AddEntityToContext(TEntity entity)
        {
            return this.Entities.Add(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public TEntity DeleteEntityFromContext(TEntity entity)
        {
            return this.Entities.Remove(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public TEntity UpdateEntityToContext(TEntity entity)
        {
            this.Entities.Attach(entity);
            this.UnitOfWork.ObjectContext.SetModified(entity);
            return entity;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool SqlBulkCopy(DataTable dt, string tableName)
        {
            bool issuccess = false;
            if (!string.IsNullOrEmpty(tableName) && dt != null && dt.Rows.Count > 0)
            {
                using (System.Data.SqlClient.SqlBulkCopy sqlBC = new System.Data.SqlClient.SqlBulkCopy(UnitOfWork.ObjectContext.Connection.ConnectionString))
                {
                    sqlBC.BatchSize = dt.Rows.Count;
                    sqlBC.BulkCopyTimeout = 90;
                    sqlBC.DestinationTableName = tableName;
                    try
                    {
                        sqlBC.WriteToServer(dt);
                        issuccess = true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return issuccess;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SaveChanges()
        {
            var temp = this.UnitOfWork.SaveChanges();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            this.UnitOfWork.Terminate();
            this.UnitOfWork.ObjectContext.Dispose();
            this.UnitOfWork = null;
        }

        #endregion

        #region [Private Methods]

        /// <summary>
        /// Handle exceptions.
        /// </summary>
        /// <param name="databaseException"></param>
        /// <returns></returns>
        private Exception TrackDBEntityException(DbEntityValidationException databaseException)
        {
            var msg = string.Empty;

            foreach (var validationErrors in databaseException.EntityValidationErrors)
                foreach (var validationError in validationErrors.ValidationErrors)
                    msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

            var failException = new Exception(msg, databaseException);

            return failException;
        }

        #endregion

        #region 新增自定义方法
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileds"></param>
        /// <returns></returns>
        public TEntity Update(TEntity entity, params string[] fileds)
        {
            if (null == entity)
            {
                // 参数有误
                return null;
            }
            Type _type = typeof(TEntity);
            this.UnitOfWork.ObjectContext.Set<TEntity>().Attach(entity);
            if (null == fileds || fileds.Length == 0)
            {
                // 全字段操作
                this.UnitOfWork.ObjectContext.SetModified(entity);
                //this.UnitOfWork.Entry<TEntity>(entity).State = EntityState.Modified;
                //db.ObjectStateManager.ChangeObjectState(entity, System.Data.Entity.EntityState.Modified);      // 手动设置为修改状态
            }
            else
            {
                // 部分字段操作
                var _stateEntry =
                    ((IObjectContextAdapter)this.UnitOfWork.ObjectContext).ObjectContext.ObjectStateManager.GetObjectStateEntry(entity);
                // 部分字段修改
                for (int i = 0; i < fileds.Length; i++)
                {
                    _stateEntry.SetModifiedProperty(fileds[i]);
                }
            }
            return entity;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deleWhere"></param>
        /// <returns></returns>
        public bool DeleteByConditon(Expression<Func<TEntity, bool>> deleWhere)
        {
            List<TEntity> entitys = this.Where(deleWhere).ToList();
            entitys.ForEach(m => this.UnitOfWork.ObjectContext.SetDeleted(m));
            return this.UnitOfWork.SaveChanges() > 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByExpression"></param>
        /// <returns></returns>
        public DataPage<TEntity> FindListByPage(Expression<Func<TEntity, bool>> where, int pageIndex, int pageSize, params OrderModelFieldEnum[] orderByExpression)
        {
            DataPage<TEntity> dataPage = new DataPage<TEntity>();
            int totalCount = this.Where(where).Count();
            var data = this.Where(where);
            data = CommonSort(data, orderByExpression);
            var dataList = data.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            dataPage.Current = pageIndex;
            dataPage.Size = pageSize;
            dataPage.RecordCount = totalCount;
            dataPage.PageCount = (int)Math.Ceiling((double)totalCount / pageSize);
            dataPage.Results = dataList;
            return dataPage;
        }

        /// <summary>
        /// 多个排序通用方法
        /// </summary>
        /// <param name="data">要排序的数据</param>
        /// <param name="orderByExpression">字典集合(排序条件,是否倒序)</param>
        /// <returns>排序后的集合</returns>
        public IQueryable<TEntity> CommonSort(IQueryable<TEntity> data, params OrderModelFieldEnum[] orderByExpression)
        {
            //创建表达式变量参数
            var parameter = Expression.Parameter(typeof(TEntity), "o");

            if (orderByExpression != null && orderByExpression.Length > 0)
            {
                for (int i = 0; i < orderByExpression.Length; i++)
                {
                    //根据属性名获取属性
                    var property = typeof(TEntity).GetProperty(orderByExpression[i].PropertyName);
                    //创建一个访问属性的表达式
                    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    var orderByExp = Expression.Lambda(propertyAccess, parameter);

                    string OrderName = "";
                    if (i > 0)
                    {
                        OrderName = orderByExpression[i].IsDESC ? "ThenByDescending" : "ThenBy";
                    }
                    else
                        OrderName = orderByExpression[i].IsDESC ? "OrderByDescending" : "OrderBy";

                    MethodCallExpression resultExp = Expression.Call(typeof(Queryable), OrderName, new Type[] { typeof(TEntity), property.PropertyType },
                  data.Expression, Expression.Quote(orderByExp));

                    data = data.Provider.CreateQuery<TEntity>(resultExp);
                }
            }
            return data;
        }
        #endregion

    }
}
