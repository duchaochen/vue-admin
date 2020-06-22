using ResultUploadSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultUploadSystem.Dao.IDao
{
    public interface IResultDao
    {
        Task<Result> GetByIdAsync(int id);

        List<Result> GetResultListAsync();

        List<Result> PageResultList(int PageIndex,int PageSize,out int count);


        Task InsertAsync(Result result);

        Task<bool> UpdateAsync(Result result);
    }
}
