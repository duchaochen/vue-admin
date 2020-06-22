using Microsoft.AspNetCore.Http;
using ResultUploadSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultUploadSystem.Middeware
{
    /// <summary>
    /// 自定义控制器
    /// </summary>
    public class BaseMiddeware
    {
        private readonly RequestDelegate _next;
        /// <summary>
        /// 定义授权常量
        /// </summary>
        public const string AuthorizationHeader = "AuthorizationHeader";
        /// <summary>
        /// 定义认证常量
        /// </summary>
        public const string WWWAuthenticateHeader = "WWW-Authenticate";
        /// <summary>
        /// 方便后面传递用户，要登录的用户
        /// </summary>
        //private BaseUser _user;

        ///// <summary>
        ///// 构造函数
        ///// </summary>
        ///// <param name="next"></param>
        ///// <param name="user"></param>
        //public BaseMiddeware(RequestDelegate next, BaseUser user) {
        //    _next = next;
        //    _user = user;
        //}

        /// <summary>
        /// 定义一个中间件方法，这个是固定方法
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {

           

        }
    }
}
