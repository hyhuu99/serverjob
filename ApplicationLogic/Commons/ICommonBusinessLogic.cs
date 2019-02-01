using ApplicationLogic.Applications.Messages;
using Domain.Models;
using Domain.Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationLogic.Commons
{
    public interface ICommonBusinessLogic
    {
        Task<CommonData> GetAllForStandardForm();
        Task<List<Category>> GetAllCategory();
    }
}
