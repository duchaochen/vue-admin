using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ResultUploadSystem.Dao.IDao;
using ResultUploadSystem.Models;
using ResultUploadSystem.ViewModels;

namespace ResultUploadSystem.Controllers
{
    public class ResultController : Controller
    {

        private IResultDao _resultDao;
        private IResultTypeDao _resulstTypeDao;
        public ResultController(IResultDao resultDao, IResultTypeDao resulstTypeDao)
        {
            _resultDao = resultDao;
            _resulstTypeDao = resulstTypeDao;
        }

        public IActionResult ResultIndex(int PageIndex = 1, int PageSize = 2)
        {
            var result = _resultDao.GetResultListAsync();
            //分页
            int count = 0;
            List<Result> result1 = _resultDao.PageResultList(PageIndex, PageSize, out count);
            int countPageIndex = count % 5 == 0 ? count / PageSize : count / PageSize + 1;
            
            int startPageIndex = PageIndex - 2 > 0 ? PageIndex - 2 : 1;
            int endPageIndex = startPageIndex + 2 >= countPageIndex ? countPageIndex : startPageIndex + 4 > countPageIndex ? countPageIndex : startPageIndex + 3;


            ViewBag.startPageIndex = startPageIndex;
            ViewBag.endPageIndex = endPageIndex;
            return View(result1);
        }

        public JsonResult Index()
        {
            var result = _resultDao.GetResultListAsync();
            //string jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return Json(result);
        }

        public async Task<IActionResult> Add()
        {
            var resulttypeList = await _resulstTypeDao.GetListAsync();
            IEnumerable<SelectListItem> enumerableList = resulttypeList.Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            });
            ViewBag.enumerableList = enumerableList;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromServices]IHostingEnvironment environment, ViewResultModel viewResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string file_path = string.Empty;
            file_path = Path.Combine("file", Guid.NewGuid().ToString() + Path.GetExtension(viewResult.FilePath.FileName));
            file_path = environment.WebRootPath + "/" + file_path;

            using (var sream = new FileStream(file_path,FileMode.Create)) {
                viewResult.FilePath.CopyTo(sream);
            }

            await _resultDao.InsertAsync(new Models.Result()
            {
                StuName = viewResult.StuName,
                Title = viewResult.Title,
                Discrption = viewResult.Discrption,
                Create = DateTime.Now,
                TypeId = viewResult.TypeId,
                PassWord = viewResult.PassWord,
                FilePath = file_path

            });
            return RedirectToAction("Index");
        }

        public IActionResult Update()
        {

            return View();
        }
    }
}