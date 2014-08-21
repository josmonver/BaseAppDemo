using BaseApp.Data.Contracts;
using System;
using System.Data.Entity;

namespace BaseApp.Data
{
    
    public class BaseAppUow : IUoW, IDisposable
    {
        public BaseAppUow(IRepositoryProvider repositoryProvider)
        {
            CreateDbContext();

            repositoryProvider.DbContext = DbContext;
            RepositoryProvider = repositoryProvider;       
        }

        // Code Camper repositories
        public IAuthRepository Auth { get { return GetRepo<IAuthRepository>(); } }

        /*public IRepository<Room> Rooms { get { return GetStandardRepo<Room>(); } }
        public IRepository<TimeSlot> TimeSlots { get { return GetStandardRepo<TimeSlot>(); } }
        public IRepository<Track> Tracks { get { return GetStandardRepo<Track>(); } }
        public ISessionsRepository Sessions { get { return GetRepo<ISessionsRepository>(); } }
        public IPersonsRepository Persons { get { return GetRepo<IPersonsRepository>(); } }
        public IAttendanceRepository Attendance { get { return GetRepo<IAttendanceRepository>(); } }*/

        /// <summary>
        /// Save pending changes to the database
        /// </summary>
        public void Commit()
        {
            //System.Diagnostics.Debug.WriteLine("Committed");
            DbContext.SaveChanges();
        }

        protected void CreateDbContext()
        {
            DbContext = new AppDbContext();

            // Do NOT enable proxied entities, else serialization fails
            DbContext.Configuration.ProxyCreationEnabled = false;

            // Load navigation properties explicitly (avoid serialization trouble)
            DbContext.Configuration.LazyLoadingEnabled = false;

            // Because Web API will perform validation, we don't need/want EF to do so
            DbContext.Configuration.ValidateOnSaveEnabled = false;

            //DbContext.Configuration.AutoDetectChangesEnabled = false;
            // We won't use this performance tweak because we don't need 
            // the extra performance and, when autodetect is false,
            // we'd have to be careful. We're not being that careful.
        }

        protected IRepositoryProvider RepositoryProvider { get; set; }

        private IRepository<T> GetStandardRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepositoryForEntityType<T>();
        }
        private T GetRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepository<T>();
        }

        private DbContext DbContext { get; set; }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();
                }
            }
        }

        #endregion
    }
}