using Microsoft.EntityFrameworkCore;
using ResultUploadSystem.Dao.IDao;
using ResultUploadSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultUploadSystem.Dao
{
    public class ResultDao : IResultDao
    {
        private ResultContext _resultContext;

        public ResultDao(ResultContext resultContext)
        {

            _resultContext = resultContext;
        }

        public Task InsertAsync(Result result)
        {
            _resultContext.Results.Add(result);
            return _resultContext.SaveChangesAsync();
        }

        public Task<Result> GetByIdAsync(int id)
        {
            return _resultContext.Results.Include<Result,ResultType>(r=>r.Type).FirstOrDefaultAsync(r => r.Id == id);
        }

        public List<Result> GetResultListAsync()
        {
            return _resultContext.Results.ToList();
        }

        public async Task<bool> UpdateAsync(Result result)
        {
            _resultContext.Results.Update(result);
            return await _resultContext.SaveChangesAsync() > 0;
        }

        public List<Result> PageResultList(int PageIndex, int PageSize, out int count)
        {
            //延时加载
            var query = _resultContext.Results.Include<Result,ResultType>(r=>r.Type).AsQueryable();
            //获得数据总数
            count = query.Count();
            var list = query.OrderByDescending(q => q.Create).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            return list;
        }
    }
}
