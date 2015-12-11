using System;
using System.Collections;
using System.Configuration;
using System.Threading;
using System.Web;

namespace MySelfEntityMvc.UtilityTools.Data
{
    internal static class ContextManager
    {
        private const string Key = "MySelfEntityMvc.UtilityTools.Data";
        private const string ConnectionKey = "MySelfEntityMvcEntities";
        private static readonly Hashtable ContextQueue = new Hashtable();
        private static string connectionString = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        static ContextManager()
        {
            if (ConfigurationManager.ConnectionStrings[ConnectionKey] == null || string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings[ConnectionKey].ToString()))
            {
                throw new ArgumentNullException("Please Create and Define Coke.Core.Data.Connection key in ConnectionStrings section of the Confuguration File");
            }
            connectionString = ConfigurationManager.ConnectionStrings[ConnectionKey].ToString();
        }

        /// <summary>
        /// Create and Store and return the active object context
        /// </summary>
        public static EFDbContext GetObjectContext()
        {

            EFDbContext objectContext = GetCurrentObjectContext();
            if (objectContext == null)
            {
                objectContext = new EFDbContext(connectionString);
                StoreCurrentObjectContext(objectContext);
            }
            return objectContext;
        }

        public static bool IsObjectContextisNull()
        {
            return GetCurrentObjectContext() == null ? true : false;
        }

        /// <summary>
        /// Gets the repository context
        /// </summary>
        /// <returns>An object representing the repository context</returns>
        public static object GetRepositoryContext()
        {
            return GetObjectContext();
        }

        /// <summary>
        /// Sets the repository context
        /// </summary>
        /// <param name="repositoryContext">An object representing the repository context</param>
        public static void SetRepositoryContext(object repositoryContext)
        {
            if (repositoryContext == null)
            {
                RemoveCurrentObjectContext();
            }
            else if (repositoryContext is EFDbContext)
            {
                StoreCurrentObjectContext((EFDbContext)repositoryContext);
            }
        }

        /// <summary>
        /// gets the current object context
        /// </summary>
        private static EFDbContext GetCurrentObjectContext()
        {
            EFDbContext objectContext = null;
            if (HttpContext.Current == null)
                objectContext = GetCurrentThreadObjectContext();
            else
                objectContext = GetCurrentHttpContextObjectContext();
            return objectContext;
        }

        /// <summary>
        /// sets the current session
        /// </summary>
        private static void StoreCurrentObjectContext(EFDbContext objectContext)
        {
            if (HttpContext.Current == null)
                StoreCurrentThreadObjectContext(objectContext);
            else
                StoreCurrentHttpContextObjectContext(objectContext);
        }

        /// <summary>
        /// remove current object context
        /// </summary>
        private static void RemoveCurrentObjectContext()
        {
            if (HttpContext.Current == null)
                RemoveCurrentThreadObjectContext();
            else
                RemoveCurrentHttpContextObjectContext();
        }

        /// <summary>
        /// gets the object context for the current thread
        /// </summary>
        private static EFDbContext GetCurrentHttpContextObjectContext()
        {
            EFDbContext objectContext = null;
            if (HttpContext.Current.Items.Contains(Key))
                objectContext = (EFDbContext)HttpContext.Current.Items[Key];
            return objectContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectContext"></param>
        private static void StoreCurrentHttpContextObjectContext(EFDbContext objectContext)
        {
            if (HttpContext.Current.Items.Contains(Key))
                HttpContext.Current.Items[Key] = objectContext;
            else
                HttpContext.Current.Items.Add(Key, objectContext);
        }

        /// <summary>
        /// remove the session for the current HttpContext
        /// </summary>
        private static void RemoveCurrentHttpContextObjectContext()
        {
            EFDbContext objectContext = GetCurrentHttpContextObjectContext();
            if (objectContext != null)
            {
                HttpContext.Current.Items.Remove(Key);
                objectContext.Dispose();
            }
        }

        /// <summary>
        /// gets the session for the current thread
        /// </summary>
        private static EFDbContext GetCurrentThreadObjectContext()
        {
            EFDbContext objectContext = null;
            Thread threadCurrent = Thread.CurrentThread;
            if (threadCurrent.Name == null)
                threadCurrent.Name = Guid.NewGuid().ToString();
            else
            {
                object threadObjectContext = null;
                lock (ContextQueue.SyncRoot)
                {
                    threadObjectContext = ContextQueue[BuildContextThreadName()];
                }
                if (threadObjectContext != null)
                    objectContext = (EFDbContext)threadObjectContext;
            }
            return objectContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectContext"></param>
        private static void StoreCurrentThreadObjectContext(EFDbContext objectContext)
        {
            lock (ContextQueue.SyncRoot)
            {
                if (ContextQueue.Contains(BuildContextThreadName()))
                    ContextQueue[BuildContextThreadName()] = objectContext;
                else
                    ContextQueue.Add(BuildContextThreadName(), objectContext);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static void RemoveCurrentThreadObjectContext()
        {
            lock (ContextQueue.SyncRoot)
            {
                if (ContextQueue.Contains(BuildContextThreadName()))
                {
                    var objectContext = (EFDbContext)ContextQueue[BuildContextThreadName()];
                    if (objectContext != null)
                    {
                        objectContext.Dispose();
                    }
                    ContextQueue.Remove(BuildContextThreadName());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string BuildContextThreadName()
        {
            return Thread.CurrentThread.Name;
        }
    }
}
