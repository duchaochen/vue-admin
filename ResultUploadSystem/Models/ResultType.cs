using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResultUploadSystem.Models
{
    public class ResultType
    {        
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "类型名称")]
        public string Name { get; set; }
        /// <summary>
        /// 导航到result表
        /// </summary>
        public List<Result> Results { get; set; }


    }
}
