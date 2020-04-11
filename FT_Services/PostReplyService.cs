using FT_Data;
using FT_Models.PostReplyViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT_Services
{
    public class PostReplyService
    {
        private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();

        private readonly Guid _userID;

        public PostReplyService() { }

        public PostReplyService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreatePostReply(PostReplyCreate model)
        {
            var entity = new PostReply()
            {
                ReplyCreator = _userID,
                ReplyContent = model.ReplyContent,
                PostID = model.PostID,
                ReplyCreated = DateTimeOffset.Now
            };

                _dbContext.Replies.Add(entity);
                return _dbContext.SaveChanges() == 1;
        }

        public IEnumerable<PostReplyListItem> GetReplies()
        {
            var query = _dbContext.Replies
                    .Where(x => x.ReplyCreator == _userID)
                    .Select(x => new PostReplyListItem
                    {
                        ReplyID = x.ReplyID,
                        PostID = x.PostID,
                        ReplyCreator = x.ReplyCreator,
                        ReplyCreated = x.ReplyCreated
                    });

            return query.ToArray();
        }

        public IEnumerable<PostReplyListItem> GetRepliesByPostID(int postID)
        {
            var query = _dbContext.Replies
                .Where(x => x.PostID == postID)
                 .Select(x => new PostReplyListItem
                 {
                     ReplyID = x.ReplyID,
                     PostID = x.PostID,
                     ReplyContent = x.ReplyContent,
                     ReplyCreator = x.ReplyCreator,
                     ReplyCreated = x.ReplyCreated,
                 });

            return query.ToArray();
        }

        public IEnumerable<PostReplyListItem> GetAllReplies()
        {
                var query = _dbContext.Replies
                        //.Where(x => x.PostCreator == _userID)
                        .Select(x => new PostReplyListItem
                        {
                            ReplyID = x.ReplyID,
                            PostID = x.PostID,
                            ReplyCreator = x.ReplyCreator,
                            ReplyCreated = x.ReplyCreated
                        });

                return query.ToArray();
        }

        public PostReplyDetail GetReplyByID(int id)
        {
                var entity = _dbContext.Replies
                    .Single(x => x.ReplyID == id); // && x.PostCreator == _userID);

                return new PostReplyDetail
                {
                    ReplyID = entity.ReplyID,
                    ReplyCreator = entity.ReplyCreator,
                    ReplyContent = entity.ReplyContent,
                    ReplyCreated = entity.ReplyCreated,
                };
        }

        public bool UpdateReply(PostReplyEdit model)
        {
            var entity = _dbContext.Replies
                .Single(x => x.ReplyID == model.ReplyID); // && x.ReplyCreator == _userID);

                entity.ReplyContent = model.ReplyContent;
                // May add in future
                //entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return _dbContext.SaveChanges() == 1;
        }

        public bool DeleteReply(int replyID)
        {
            var entity = _dbContext.Replies
                .Single(x => x.ReplyID == replyID); // && (x.ReplyCreator == _userID || x.Post.PostCreator == _userID || x.Post.Thread.ThreadCreator == _userID));

                _dbContext.Replies.Remove(entity);

                return _dbContext.SaveChanges() == 1;
        }


        public bool ValidateUser(int replyID)
        {
            var entity = _dbContext.Replies
                .Single(x => x.ReplyID == replyID);

            if (entity.ReplyCreator == _userID || entity.Post.PostCreator == _userID || entity.Post.Thread.ThreadCreator == _userID)
                return true;

            return false;
        }
    }
}
