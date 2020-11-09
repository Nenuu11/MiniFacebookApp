using aphrie.DBL.Entities;
using aphrie.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace aphrie.BLL.DbManagers
{
    public class FriendManager: Repository<Friend>
    {
        public FriendManager(DbContext ctx) : base(ctx)
        {

        }
    }
}
