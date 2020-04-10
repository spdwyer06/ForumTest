using FT_Data;
using FT_Models.ThreadViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT_Services
{
    public class ThreadService
    {
        private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();

        private readonly Guid _userID;

        public ThreadService() { }

        public ThreadService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateThread(ThreadCreate model)
        {
            var entity = new Thread()
            {
                ThreadCreator = _userID,
                ThreadTitle = model.ThreadTitle,
                ThreadDescription = model.ThreadDescription,
                ThreadCreated = DateTimeOffset.Now
            };

            _dbContext.Threads.Add(entity);
            return _dbContext.SaveChanges() == 1;
        }

        public IEnumerable<ThreadListItem> GetThreads()
        {
            var query = _dbContext.Threads
                    .Select(x => new ThreadListItem
                    {
                        ThreadID = x.ThreadID,
                        ThreadTitle = x.ThreadTitle,
                        ThreadCreator = x.ThreadCreator,
                        ThreadCreated = x.ThreadCreated,
                        Posts = x.Posts
                    });

            return query.ToArray();
        }

        public IEnumerable<ThreadListItem> GetMyThreads()
        {
            var query = _dbContext.Threads
                    .Where(x => x.ThreadCreator == _userID)
                    .Select(x => new ThreadListItem
                    {
                        ThreadID = x.ThreadID,
                        ThreadTitle = x.ThreadTitle,
                        ThreadCreator = x.ThreadCreator,
                        ThreadCreated = x.ThreadCreated,
                        Posts = x.Posts
                    });

            return query.ToArray();
        }

        public ThreadDetail GetThreadByID(int id)
        {
            var entity = _dbContext.Threads
                .Single(x => x.ThreadID == id);

            return new ThreadDetail
            {
                ThreadID = entity.ThreadID,
                ThreadCreator = entity.ThreadCreator,
                ThreadTitle = entity.ThreadTitle,
                ThreadDescription = entity.ThreadDescription,
                ThreadCreated = entity.ThreadCreated,
                Posts = entity.Posts
            };
        }

        public bool UpdateThread(ThreadEdit model)
        {
            var entity = _dbContext.Threads
                .Single(x => x.ThreadID == model.ThreadID); 

            entity.ThreadTitle = model.ThreadTitle;
            entity.ThreadDescription = model.ThreadDescription;

            return _dbContext.SaveChanges() == 1;
        }

        public bool DeleteThread(int threadID)
        {
            var entity = _dbContext.Threads
                .Single(x => x.ThreadID == threadID); 

            _dbContext.Threads.Remove(entity);

            return _dbContext.SaveChanges() == 1;
        }

        public bool ValidateUser(int threadID)
        {
            var entity = _dbContext.Threads
                .Single(x => x.ThreadID == threadID);

            if (entity.ThreadCreator == _userID)
                return true;

            return false;
        }
    }
}
