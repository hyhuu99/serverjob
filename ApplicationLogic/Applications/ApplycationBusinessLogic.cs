
using ApplicationLogic.Applications.Messages;
using AutoMapper;
using Domain.Models;
using MongoDB.Driver;
using Repository;
using Shared;
using Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationLogic.Applications
{
    public class ApplycationBusinessLogic : IApplycationBusinessLogic
    {
        private readonly IMongoDbRepository _mongoDbRepository;
        public ApplycationBusinessLogic(IMapper mapper,
           IMongoDbRepository mongoDbRepository)
        {
            _mongoDbRepository = mongoDbRepository;
        }

        public async Task CreateApplication(CreateApplycationRequest apply)
        {
            var filter = Builders<Resume>.Filter.Where(x => x.Email == apply.Email);
            var resume = await _mongoDbRepository.Find(filter);
            var applyModel = Mapper.Map<Application>(apply);
            applyModel.Id = IdGeneratorHelper.IdGenerator();
            applyModel.AppliedDate = DateTime.Now;
            applyModel.Resume = resume.FirstOrDefault();

            var filterJob = Builders<Job>.Filter.Where(x => x.Id == apply.JobId);
            var jobResult = await _mongoDbRepository.Find<Job>(filterJob);
            var jobModel = jobResult.FirstOrDefault();
            jobModel.Vacancies += 1;
            await _mongoDbRepository.Replace(jobModel);
            await _mongoDbRepository.Create(applyModel);
        }

        public async Task<bool> IsExistApplyByJob(string jobId, string email)
        {
            var roleFilter = Builders<UserRole>.Filter.Where(x => x.Email.Equals(email));
            var userRole = await _mongoDbRepository.Find(roleFilter);
            if(string.IsNullOrEmpty(email) || userRole.FirstOrDefault().RoleName.Equals(EUserRole.Hr.ToString()))
            {
                return true;
            }
            var filter = Builders<Application>.Filter.Where(x => x.Email.Equals(email) && x.JobId == jobId);
            IList<Application> listApply = await _mongoDbRepository.Find(filter);
            return listApply.Any();
        }

        public async Task<List<GetApplycationReponse>> GetApplyByJob (string jobId)
        {
            var filter = Builders<Application>.Filter.Where(x => x.JobId.Equals(jobId));
            List<GetApplycationReponse> listApply = Mapper.Map<IList<Application>,IList<GetApplycationReponse>>(await _mongoDbRepository.Find(filter)).ToList();
            return listApply.ToList();
        }

        public async Task<GetApplycationReponse> GetApplyById (string applyId)
        {
            var result = await _mongoDbRepository.Get<Application>(applyId);
            return Mapper.Map<GetApplycationReponse>(result);
        }
    }
}
