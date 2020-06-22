using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResultUploadSystem.Dao;
using ResultUploadSystem.Dao.IDao;
using ResultUploadSystem.Models;

namespace ResultUploadSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            #region
            //创建数据连接
            string Server = Configuration["sqlserver_database:Server"];
            string Database = Configuration["sqlserver_database:Database"];
            string Uid = Configuration["sqlserver_database:Uid"];
            string Pwd = Configuration["sqlserver_database:Pwd"];
            //sqlserver数据库
            string connection = $"server={Server};Database={Database};Uid={Uid};Pwd={Pwd}";

            //依赖上下文
            services.AddDbContext<ResultContext>(options =>
            {
                //使用sqlserver数据库
                options.UseSqlServer(connection);
            });
            #endregion

            #region Mysql
            //创建数据连接【MySql】
            //string Server = Configuration["mysql_database:Server"];
            //string Database = Configuration["mysql_database:Database"];
            //string Uid = Configuration["mysql_database:Uid"];
            //string Pwd = Configuration["mysql_database:Pwd"];
            //string Port = Configuration["mysql_database:Port"];
            ////mysql数据库
            //string connection = $"Server={Server};database={Database};port={Port};uid={Uid};pwd={Pwd}";
            ////依赖上下文
            //services.AddDbContext<ResultContext>(options =>
            //{
            //    使用sqlserver数据库
            //    options.UseMySql(connection);
            //}); 
            #endregion

            services.AddScoped<IResultDao, ResultDao>();
            services.AddScoped<IResultTypeDao, ResultTypeDao>();
            //services.AddIdentity<ResultUser, IdentityRole>(opts=> {
            //    //配置密码规则
            //    opts.Password.RequiredLength = 6;// 密码长度至少6位
            //    opts.Password.RequireDigit = false;//密码可以不包含数字,包含数字位true,默认设置是true
            //    opts.Password.RequireLowercase = false;//可以不包含小写字母,默认设置是true
            //    opts.Password.RequireNonAlphanumeric = false;//可以不包含特殊符号,默认设置为true
            //    opts.Password.RequireUppercase = false;//可以不包含大写字母,默认是true
            //    //配置用户名规则
            //    //opts.User.RequireUniqueEmail = false;//用户名可以是相同的email，默认值也是false
            //    //opts.User.AllowedUserNameCharacters = "asdfghjklzxcvbnmqwertyuiop";//用户名的字符只能使用这里面的，默认为随意


            //}).AddEntityFrameworkStores<ResultContext>()//使用ef配置持久化存储
            //  .AddDefaultTokenProviders();//注入令牌提供者

            //解决跨域问题
            services.AddCors(options =>
            {
                options.AddPolicy("all", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseAuthentication();//启用默认的认证中间件
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //在Configure方法中添加如下代码
            app.UseCors("all");
            //app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Result}/{action=Index}/{id?}");
            });
        }
    }
}
