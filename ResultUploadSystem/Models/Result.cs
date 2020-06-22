using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ResultUploadSystem.Models
{
    public class Result
    {
        //主键
        [Key]
        public int Id { get; set; }
        [Required]//必须要输入的特性
        [MaxLength(10)]//输入长度为10个,不加此特性表示为不限制
        [Display(Name = "姓名")]//前端显示为姓名 
        public string StuName { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name = "结果概述")]
        public string Discrption { get; set; }
        [Display(Name = "创建时间")]
        public DateTime Create { get; set; }
        /// <summary>
        /// 外键
        /// </summary>
        public int TypeId { get; set; }
        /// <summary>
        /// 导航属性
        /// </summary>
        public ResultType Type { get; set; }

        [Display(Name = "附件路径")]
        public string FilePath { get; set; }
        [Display(Name ="密码")]
        public string PassWord { get; set; }
    }
}
