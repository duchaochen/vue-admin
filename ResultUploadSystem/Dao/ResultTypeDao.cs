using Microsoft.EntityFrameworkCore;
using ResultUploadSystem.Dao.IDao;
using ResultUploadSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultUploadSystem.Dao
{
    public class ResultTypeDao : IResultTypeDao
    {
        private ResultContext _resultContext;
        public ResultTypeDao(ResultContext resultContext) {

            _resultContext = resultContext;
        }
        public Task<List<ResultType>> GetListAsync()
        {
            return _resultContext.ResultTypes.ToListAsync();
        }
    }
}
