using aphrie.DBL.Entities;
using aphrie.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace aphrie.BLL.DbManagers
{
    public class MediaManager : Repository<Media>
    {
        public MediaManager(DbContext ctx) : base(ctx)
        {

        }
    }
}
