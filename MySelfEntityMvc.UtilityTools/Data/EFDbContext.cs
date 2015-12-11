using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelfEntityMvc.UtilityTools.Data
{
    public class EFDbContext
    {
        private readonly DbContext context;

        public EFDbContext()
        {
            context = new DbContext("MySelfEntityMvcEntities");
        }

        public EFDbContext(string connectionString)
        {
            context = new DbContext(connectionString);
        }

        public void Dispose()
        {
            if (context != null)
                context.Dispose();
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public DbConnection Connection
        {
            get { return context.Database.Connection; }
        }

        public IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return context.Set<TEntity>();
        }

        public DbSet Set(Type entityType)
        {
            return context.Set(entityType);
        }

        public DbEntityEntry Entry(object entity)
        {
            return context.Entry(entity);
        }

        public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return context.Entry(entity);
        }

        public DbChangeTracker ChangeTracker
        {
            get { return context.ChangeTracker; }
        }

        public DbContextConfiguration Configuration
        {
            get { return context.Configuration; }
        }

        public void SetModified(object entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void SetDetached(object entity)
        {
            context.Entry(entity).State = EntityState.Detached;
        }

        public void SetDeleted(object entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return context.Database.SqlQuery<TElement>(sql, parameters);
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return context.Database.ExecuteSqlCommand(sql, parameters);
        }

        public int ExecuteNonQueryProc(string procName, params object[] parameters)
        {
            try
            {
                var cmd = context.Database.Connection.CreateCommand();
                context.Database.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procName;
                cmd.Parameters.AddRange(parameters);
                cmd.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
                int rows = cmd.ExecuteNonQuery();
                int result = (int)cmd.Parameters["ReturnValue"].Value;
                return rows;
            }
            catch
            {
                throw;
            }
            finally
            {
                context.Database.Connection.Close();
            }
        }


        public object ExecuteQuerySql(string sql, params object[] parameters)
        {
            try
            {
                var cmd = context.Database.Connection.CreateCommand();
                context.Database.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteScalar();
            }
            catch
            {
                throw;
            }
            finally
            {
                context.Database.Connection.Close();
            }
        }

        public DataSet ExecteDataSetProc(string procName, string tableName, params object[] parameters)
        {
            SqlConnection conn = null;
            try
            {
                DataSet ds = new DataSet();
                conn = new SqlConnection(context.Database.Connection.ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = procName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                SqlDataAdapter sqlDater = new SqlDataAdapter(cmd);
                sqlDater.Fill(ds, tableName);
                return ds;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public DataSet ExecteDataSetSql(string sql, string tableName, params object[] parameters)
        {
            SqlConnection conn = null;
            try
            {
                DataSet ds = new DataSet();
                conn = new SqlConnection(context.Database.Connection.ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                SqlDataAdapter sqlDater = new SqlDataAdapter(cmd);
                sqlDater.Fill(ds, tableName);
                return ds;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

    }
}
