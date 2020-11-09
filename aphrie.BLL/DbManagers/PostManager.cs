using aphrie.DBL.Entities;
using aphrie.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace aphrie.BLL.DbManagers
{
    public class PostManager: Repository<Post>
    {
        public PostManager(DbContext ctx): base(ctx)
        {
        }

    }
}
