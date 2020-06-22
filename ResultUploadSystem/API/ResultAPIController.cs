using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResultUploadSystem.Dao.IDao;
using ResultUploadSystem.Models;
using ResultUploadSystem.ViewModels;

namespace ResultUploadSystem.API
{
    [Route("api/result")]
    [ApiController]
    public class ResultAPIController : ControllerBase
    {
        private IResultDao _resultDao;
        private IResultTypeDao _resulstTypeDao;
        public ResultAPIController(IResultDao resultDao, IResultTypeDao resulstTypeDao)
        {
            _resultDao = resultDao;
            _resulstTypeDao = resulstTypeDao;
        }

        [HttpGet]
        public IActionResult PageList(int PageIndex = 1, int PageSize = 3)
        {

            var result = _resultDao.PageResultList(PageIndex, PageSize, out int count);
            //将result实体转换成APIResultModel实体
            var dataValues = result.Select(r => new APIResultModel()
            {
                Id = r.Id,
                StuName = r.StuName,
                PassWord = r.PassWord,
                FilePath = r.FilePath,
                Title = r.Title,
                Discrption = r.Discrption,
                Type = r.Type.Name
            });

            return Ok(dataValues);
        }

        [HttpGet("{id}",Name ="Get")]
        public async Task<IActionResult> GetById(int id=0)
        {
            var r = await _resultDao.GetByIdAsync(id);
            //将result实体转换成APIResultModel实体
            var dataValue = new APIResultModel()
            {
                Id = r.Id,
                StuName = r.StuName,
                PassWord = r.PassWord,
                FilePath = r.FilePath,
                Title = r.Title,
                Discrption = r.Discrption,
                Type = r.Type.Name
            };

            return Ok(dataValue);
        }
        [HttpPost]
        public async Task<IActionResult> Add(ViewResultModel viewResultModel)
        {
            Dictionary<string,string> value = new Dictionary<string, string>();

            return Ok(value);
        }
    }
}