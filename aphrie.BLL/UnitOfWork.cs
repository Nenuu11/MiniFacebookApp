using aphrie.BLL.DbManagers;
using aphrie.DBL;
using System;

namespace aphrie.BLL
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationDbContext _ctx;
        public FriendManager FriendManager { get; set; }
        public MediaManager MediaManager { get; set; }
        public PostManager PostManager { get; set; }
        public PostReactionManager PostReactionManager { get; set; }

        public UnitOfWork(ApplicationDbContext ctx)
        {
            _ctx = ctx;
            FriendManager = new FriendManager(_ctx);
            MediaManager = new MediaManager(_ctx);
            PostManager = new PostManager(_ctx);
            PostReactionManager = new PostReactionManager(_ctx);

        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
