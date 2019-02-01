
using System.Collections.Generic;
using System.Linq;
using ApplicationLogic.Resumes.Messages;
using AutoMapper;
using Domain.Models;
using Repository;
using Shared;
using System.Threading.Tasks;
using MongoDB.Driver;
using Domain.Models.GeneralModel.ResumesModel;
using Domain.Models.Enum;

namespace ApplicationLogic.Resumes
{
    public class ResumesBusinessLogic : IResumesBusinessLogic
    {
        private readonly IMongoDbRepository _mongoDbRepository;
        public ResumesBusinessLogic(IMapper mapper,
           IMongoDbRepository mongoDbRepository)
        {
            _mongoDbRepository = mongoDbRepository;
        }

        public async Task<string> CreateResumes(ResumeRequest resumeStep1)
        {
            Resume resumeModel = new Resume();
            resumeModel = Mapper.Map<ResumeRequest,Resume>(resumeStep1);
            resumeModel.Id = IdGeneratorHelper.IdGenerator();
            await _mongoDbRepository.Create(resumeModel);
            return resumeModel.Id;
        }

        public async Task UpdateResumes(ResumeRequest resume)
        {
            Resume resumeModel = new Resume();
            resumeModel = Mapper.Map<ResumeRequest, Resume>(resume);
            await _mongoDbRepository.Replace<Resume>(resumeModel);
        }

        public async Task<List<ResumeRequest>> GetResumesByUser(string email)
        {
            var filter = Builders<Resume>.Filter.Where(x => x.Email == email);
            var listResume =await _mongoDbRepository.Find(filter);
            return Mapper.Map<List<Resume>,List<ResumeRequest>>(listResume.ToList());
        }

        public async Task<ResumeRequest> GetResumesById(string id)
        {
            var resumes = await _mongoDbRepository.Get<Resume>(id);
            return Mapper.Map<Resume, ResumeRequest>(resumes);
        }

        public async Task<List<ResumeRequest>> SearchResume(SearchResumeRequest searchRequest)
        {
            var filter = Builders<Resume>.Filter.Empty;
            if (!string.IsNullOrEmpty(searchRequest.Title))
            {
                filter = filter & Builders<Resume>.Filter.Where(x => x.Title.ToLower().Contains(searchRequest.Title.ToLower()));
            }
            if (!string.IsNullOrEmpty(searchRequest.Level))
            {
                filter = filter & Builders<Resume>.Filter.Where(x => x.Purpose.LevelOfWork.Id.Equals(searchRequest.Level));
            }
            if (searchRequest.LocationIds != null &&  searchRequest.LocationIds.Length > 0)
            {
                foreach (string locationId in searchRequest.LocationIds)
                {
                    filter = filter & Builders<Resume>.Filter.Where(x => x.ContactInfo.City.Id.Contains(locationId));
                }
            }
            if (searchRequest.CategoryIds != null && searchRequest.CategoryIds.Length > 0)
            {
                foreach (string categoryId in searchRequest.CategoryIds)
                {
                    filter = filter & Builders<Resume>.Filter.Where(x => x.Purpose.Categorys.Any(c => c.Id.Contains(categoryId)));
                }
            }
            filter = filter & Builders<Resume>.Filter.Where(x => x.Status == ResumeStatus.Published);
            var listResume = await _mongoDbRepository.Find(filter);
            var listResumeRequest = Mapper.Map<List<ResumeRequest>>(listResume);

            return listResumeRequest;
        }
    }
}
