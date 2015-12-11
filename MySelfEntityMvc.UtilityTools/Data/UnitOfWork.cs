using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelfEntityMvc.UtilityTools.Data
{
    /// <summary>
    /// Context across all repositories
    /// </summary>
    public class UnitOfWork
    {
        #region UnitOfWork Members
        /// <summary>
        /// Returns the active object context
        /// </summary>
        public EFDbContext ObjectContext
        {
            get
            {
                return ContextManager.GetObjectContext();
            }
        }

        /// <summary>
        /// Save all changes to all repositories
        /// </summary>
        /// <returns>Integer with number of objects affected</returns>
        public int SaveChanges()
        {
            return ObjectContext.SaveChanges();
        }

        /// <summary>
        /// Terminates the current repository context
        /// </summary>
        public void Terminate()
        {
            ContextManager.SetRepositoryContext(null);
        }

        #endregion
    }
}
