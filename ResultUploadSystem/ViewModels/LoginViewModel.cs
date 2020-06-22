using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResultUploadSystem.ViewModels
{
    public class LoginViewModel
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "{0} 不能为空")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        /// <summary>
        /// 生成的是密码框
        /// </summary>
        [Required(ErrorMessage ="{0} 不能为空")]
        [DataType(DataType.Password)]
        public string  PassWord { get; set; }
        /// <summary>
        /// 记住登录状态
        /// </summary>
        [Display(Name = "记住登录状态")]
        public bool  RememberMe { get; set; }
    }
}
