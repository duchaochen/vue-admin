using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResultUploadSystem.ViewModels
{
    public class ViewResultModel
    {
        //主键
        public int Id { get; set; }

        [Display(Name = "姓名")]//前端显示为姓名 
        public string StuName { get; set; }

        [Display(Name = "标题")]
        public string Title { get; set; }

        [Display(Name = "结果概述")]
        public string Discrption { get; set; }

        [Display(Name ="类型名称")]
        public int TypeId { get; set; }

        [Display(Name = "附件路径")]
        public IFormFile FilePath { get; set; }

        [Display(Name = "密码")]
        public string PassWord { get; set; }
    }
}
