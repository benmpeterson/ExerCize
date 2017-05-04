using Exercise.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Services.HelperMethods
{
    class GetAllUsers
    {
        public static List<string> GetUsers()
        {
            using (var context = new ApplicationDbContext())
            {
                var blogs = context.Database.SqlQuery<string>("SELECT Id FROM dbo.ApplicationUser").ToList();
                return blogs;
            }


        }
    }
}
