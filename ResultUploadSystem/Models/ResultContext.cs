using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultUploadSystem.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ResultUploadSystem.Models
{
    /// <summary>
    /// 操作数据库都context
    /// </summary>
    public class ResultContext : IdentityDbContext
    {
        //一定要创建一个带有参数为DbContextOptions<ResultContext>类型的构造方法
        public ResultContext(DbContextOptions<ResultContext> dbContextOptions) : base(dbContextOptions)
        {
            //Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<Result> Results { get; set; }
        public DbSet<ResultType> ResultTypes { get; set; }
        public DbSet<ResultUser> ResultUsers { get; set; }
    }
}
