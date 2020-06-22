using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResultUploadSystem.ViewModels
{
    public class RegisterViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="用户名")]
        public string UserName { get; set; }

        [Required]
        [Display(Name ="邮箱")]
        public string Email { get; set; }

        [Required]
        [StringLength(16,ErrorMessage ="{0}长度必须大于{2}位小于{1}位",MinimumLength =6)]
        public string PassWord { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="确认密码")]
        [Compare("PassWord", ErrorMessage = "两次密码输入不一致")]
        public string  ConfirmPassWord { get; set; }

    }
}
