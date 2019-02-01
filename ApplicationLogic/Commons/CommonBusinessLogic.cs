
using ApplicationLogic.Applications.Messages;
using AutoMapper;
using Domain.Models;
using Domain.Models.Common;
using MongoDB.Driver;
using Repository;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationLogic.Commons
{
    public class CommonBusinessLogic : ICommonBusinessLogic
    {
        private readonly IMongoDbRepository _mongoDbRepository;
        public CommonBusinessLogic(IMapper mapper,
           IMongoDbRepository mongoDbRepository)
        {
            _mongoDbRepository = mongoDbRepository;
        }

        public async Task<CommonData> GetAllForStandardForm()
        {
            IList<CommonData> allValue = await _mongoDbRepository.Find(Builders<CommonData>.Filter.Empty);
            CommonData data = allValue.FirstOrDefault();
            IList<City> city = await _mongoDbRepository.Find(Builders<City>.Filter.Empty);
            data.City = city.ToArray();
            return allValue.FirstOrDefault();
        }

        public async Task<List<Category>> GetAllCategory()
        {
            var allValue = await _mongoDbRepository.Find(Builders<Category>.Filter.Empty);
            return allValue.ToList();
        }
        //public async Task<List<GetApplycationReponse>> GetApplyByJob (string jobId)
        //{
        //    var filter = Builders<Application>.Filter.Where(x => x.JobId.Equals(jobId));
        //    List<GetApplycationReponse> listApply =Mapper.Map<IList<Application>,IList<GetApplycationReponse>>(await _mongoDbRepository.Find(filter)).ToList();
        //    return listApply.ToList();
        //}
    }
}
