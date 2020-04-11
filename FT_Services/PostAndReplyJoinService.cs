using FT_Data;
using FT_Models.PostAndReplyJoinViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT_Services
{
    public class PostAndReplyJoinService
    {
        private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();

        private readonly Guid _userID;

        public PostAndReplyJoinService() { }

        public PostAndReplyJoinService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreatePostAndReplyJoin(PostAndReplyJoinCreate model)
        {
            int postID = _dbContext.Posts.Single(x => x.PostID == model.PostID).PostID;
            int replyID = _dbContext.Replies.Single(x => x.ReplyID == model.ReplyID).ReplyID;
            var entity = new PostAndReplyJoin()
            {
                PostID = postID,
                ReplyID = replyID
            };

            _dbContext.PostAndReplyJoins.Add(entity);
            return _dbContext.SaveChanges() == 1;
        }

        public IEnumerable<PostAndReplyJoinListItem> GetPostAndReplyJoins ()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.PostAndReplyJoins
                        //.Where(x => x.Post.PostID = _userID && x.Reply.ReplyID = _userID)
                        .Select(x => new PostAndReplyJoinListItem
                        {
                            ReplyID = x.ReplyID,
                            PostID = x.PostID,
                            PostContent = x.Post.PostContent,
                            PostCreated = x.Post.PostCreated,
                            ReplyContent = x.Reply.ReplyContent,
                            ReplyCreated = x.Reply.ReplyCreated
                        });

                return query.ToArray();
            }
        }

        public PostAndReplyJoinDetail GetPostAndReplyJoinByID(int id)
        {
            var entity = _dbContext.PostAndReplyJoins
                .Single(x => x.JoinID == id); // && x.PostCreator == _userID);

            return new PostAndReplyJoinDetail
            {
                JoinID = entity.JoinID,
                PostID = entity.PostID,
                PostContent = entity.Post.PostContent,
                PostCreated = entity.Post.PostCreated,
                ReplyID = entity.ReplyID,
                ReplyContent = entity.Reply.ReplyContent,
                ReplyCreated = entity.Reply.ReplyCreated,
            };
        }

        public bool UpdatePostAndReplyJoin(PostAndReplyJoinEdit model)
        {
            var entity = _dbContext.PostAndReplyJoins
                .Single(x => x.JoinID == model.JoinID); // && x.ReplyCreator == _userID);

            entity.Post.PostContent = model.PostContent;
            entity.Reply.ReplyContent = model.ReplyContent;
            // May add in future
            //entity.ModifiedUtc = DateTimeOffset.UtcNow;

            return _dbContext.SaveChanges() == 1;
        }

        public bool DeletePostAndReplyJoin(int id)
        {
            var entity = _dbContext.PostAndReplyJoins
                .Single(x => x.JoinID == id); // && (x.ReplyCreator == _userID || x.Post.PostCreator == _userID || x.Post.Thread.ThreadCreator == _userID));

            _dbContext.PostAndReplyJoins.Remove(entity);

            return _dbContext.SaveChanges() == 1;
        }


        public bool ValidateUser(int id)
        {
            var entity = _dbContext.PostAndReplyJoins
                .Single(x => x.JoinID == id);

            if (entity.Reply.ReplyCreator == _userID || entity.Post.PostCreator == _userID || entity.Post.Thread.ThreadCreator == _userID)
                return true;

            return false;
        }
    }
}
