using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using ResultUploadSystem.Models;
using ResultUploadSystem.ViewModels;

namespace ResultUploadSystem.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// 用于记录用户日志
        /// </summary>
        private readonly ILogger<AccountController> _logger;
        /// <summary>
        /// 用于处理用户相关逻辑，如添加，修改密码，添加删除角色
        /// </summary>
        public UserManager<ResultUser> UserManager { get; }
        /// <summary>
        /// 处理注册登录相关逻辑
        /// </summary>
        public SignInManager<ResultUser> SignInManager { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="logger"></param>
        public AccountController(UserManager<ResultUser> userManager, SignInManager<ResultUser> signInManager, ILogger<AccountController> logger)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _logger = logger;
        }

        public IActionResult Login() {

            return View();
        }

        /// <summary>
        /// 处理登录逻辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]//检测服务请求是否被篡改了,这个特性只允许使用post请求
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //验证表单
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //lockoutOnFailure:false,如果登录失败是否锁定用户,false为不锁定
            var result = await SignInManager.PasswordSignInAsync(model.UserName,model.PassWord,model.RememberMe,lockoutOnFailure:false);

            //如果登录成功跳转到首页
            if (result.Succeeded)
            {
                //日志里面写信息
                _logger.LogInformation("Logged in {userName}", model.UserName);
                return RedirectToAction("Index", "Result");
            }
            else
            {
                _logger.LogWarning("Failed to log in {userName}",model.UserName);
                ModelState.AddModelError("","用户名或密码错误");
                return View(model);
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model) {

            if (ModelState.IsValid)
            {
                var user = new ResultUser() {UserName = model.UserName,Email = model.Email };
                //创建用户是需要带密码进入，否则密码保存失败,返回时Identity
                //Succeeded 属性代表了操作成功了
                //Errors 属性，包含了IdentityErrors对象的集合，描述了错误信息
                var result = await UserManager.CreateAsync(user, model.PassWord);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User {userName} 注册", model.UserName);
                    return RedirectToAction("Login");
                }

                //如果没有登录成功就会执行下面添加错误信息
                foreach (var error in result.Errors)
                {
                    //第二个参数为错误信息
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff() {
            var userName = HttpContext.User.Identity.Name;
            //登出
            await SignInManager.SignOutAsync();
            _logger.LogInformation("User {userName} 登出", userName);
            return RedirectToAction("Login","Account");
        }
    }
}