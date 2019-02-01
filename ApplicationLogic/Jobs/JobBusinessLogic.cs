
using ApplicationLogic.Jobs.Messages;
using AutoMapper;
using Domain.Models;
using Domain.Models.Enum;
using MongoDB.Driver;
using Repository;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationLogic.Jobs
{
    public class JobBusinessLogic : IJobBusinessLogic
    {
        private readonly IMongoDbRepository _mongoDbRepository;
        public JobBusinessLogic(IMapper mapper,
           IMongoDbRepository mongoDbRepository)
        {
            _mongoDbRepository = mongoDbRepository;
        }

        public async Task CreateJob(CreateJobRequest job)
        {
            var filter = Builders<Company>.Filter.Where(x => x.Email == job.Email);
            var resultCompany = await _mongoDbRepository.Find<Company>(filter);
            var company  = resultCompany.FirstOrDefault();
            var jobModel = Mapper.Map<Job>(job);
            jobModel.StartDay = jobModel.StartDay.Date;
            jobModel.ExpirationDate = jobModel.ExpirationDate.Date;
            jobModel.Id = IdGeneratorHelper.IdGenerator();
            jobModel.CompanyId = company.Id;
 
            var task = Task.Run(async () => await _mongoDbRepository.Create(jobModel));
            task.Wait();
            if(task.IsCompletedSuccessfully)
            {
               await UpdateStatusJob(jobModel.Id);
            }
        }

        public async Task<GetJobReponseForUpdate> GetJobById(string id)
        {
            var job = await _mongoDbRepository.Get<Job>(id);
            var locationFilter = Builders<City>.Filter.Where(x => x.Id == job.LocationId);
            var locations = await _mongoDbRepository.Find(locationFilter);

            var categoryFilter = Builders<Category>.Filter.Where(x => x.Id == job.CategoryId);
            var categories = await _mongoDbRepository.Find(categoryFilter);
            var jobReponse = Mapper.Map<GetJobReponseForUpdate>(job);
            jobReponse.CategoryName = categories.FirstOrDefault().Name;
            jobReponse.Location = locations.FirstOrDefault().Name;
            return jobReponse;
        }

        public async Task UpdateJob(UpdateJobRequest job)
        {
            var filter = Builders<Company>.Filter.Where(x => x.Email == job.Email);
            var resultCompany = await _mongoDbRepository.Find<Company>(filter);
            var company = resultCompany.FirstOrDefault();
            var jobModel = Mapper.Map<Job>(job);
            jobModel.StartDay = jobModel.StartDay.Date;
            jobModel.ExpirationDate = jobModel.ExpirationDate.Date;
            jobModel.CompanyId = company.Id;
            jobModel = changeStatusJob(jobModel);
            await _mongoDbRepository.Replace(jobModel);
        }

        public async Task DeleteJob(string jobId)
        {
            await _mongoDbRepository.Delete<Job>(jobId);
        }

        public async Task<List<GetJobReponse>> GetJobs(GetJobsRequest request)
        {
            var jobFilter = Builders<Job>.Filter.Empty;
            if (request.IsActive)
            {
                var currentTime = DateTime.Now;
                jobFilter = jobFilter & Builders<Job>.Filter.Where(x => x.Status == JobStatus.Published && x.StartDay <= currentTime && x.ExpirationDate > currentTime);
            }
            if (!string.IsNullOrEmpty(request.SearchText))
            {
                jobFilter = jobFilter & Builders<Job>.Filter.Where(x => x.Name.ToLower().Contains(request.SearchText.ToLower()));
            }
            if (request.JobType != null)
            {
                jobFilter = jobFilter & Builders<Job>.Filter.Where(x => x.JobType == request.JobType);
            }
            if (request.LocationId != null)
            {
                jobFilter = jobFilter & Builders<Job>.Filter.Where(x => x.LocationId == request.LocationId);
            }
            if (request.CategoryId != null)
            {
                jobFilter = jobFilter & Builders<Job>.Filter.Where(x => x.CategoryId == request.CategoryId);
            }

            var listJob = (await _mongoDbRepository.Find(jobFilter))
                .OrderByDescending(s => s.StartDay)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize);

            var locationIds = listJob.Select(s => s.LocationId);
            var locationFilter = Builders<City>.Filter.Where(x => locationIds.Contains(x.Id));
            var locations = await _mongoDbRepository.Find(locationFilter);

            var companyIds = listJob.Select(s => s.CompanyId);
            var companyFilter = Builders<Company>.Filter.Where(x => companyIds.Contains(x.Id));
            var companies = await _mongoDbRepository.Find(companyFilter);

            var categoryIds = listJob.Select(s => s.CategoryId);
            var categoryFilter = Builders<Category>.Filter.Where(x => categoryIds.Contains(x.Id));
            var categories = await _mongoDbRepository.Find(categoryFilter);

            return listJob.Select(s =>
            new GetJobReponse
            {
                Id = s.Id,
                CategoryId = s.CategoryId,
                CategoryName = categories.FirstOrDefault(f => f.Id == s.CategoryId)?.Name,
                CompanyId = s.CompanyId,
                CompanyName = companies.FirstOrDefault(f => f.Id == s.CompanyId)?.Name,
                Description = s.Description,
                ExpirationDate = s.ExpirationDate,
                JobType = s.JobType,
                LocationId = s.LocationId,
                Location = locations.FirstOrDefault(f => f.Id == s.LocationId)?.Name,
                Name = s.Name,
                Salary = s.Salary,
                Status = s.Status,
                Summary = s.Summary,
                Vacancies = s.Vacancies
            }).ToList();
        }

        public async Task<List<GetJobReponse>> GetJobsByCompanyId(string companyId)
        {
            var filter = Builders<Job>.Filter.Where(x => x.CompanyId == companyId);
            var listJob = await _mongoDbRepository.Find(filter);

            var locationIds = listJob.Select(s => s.LocationId);
            var locationFilter = Builders<City>.Filter.Where(x => locationIds.Contains(x.Id));
            var locations = await _mongoDbRepository.Find(locationFilter);


            return listJob.Select(s =>
            new GetJobReponse
            {
                Id = s.Id,
                CategoryId = s.CategoryId,
                CompanyId = s.CompanyId,
                Description = s.Description,
                ExpirationDate = s.ExpirationDate,
                JobType = s.JobType,
                Location = locations.FirstOrDefault(f => f.Id == s.LocationId)?.Name,
                Name = s.Name,
                Salary = s.Salary,
                Status = s.Status,
                Summary = s.Summary,
                Vacancies = s.Vacancies,
            }).ToList();
        }

        public async Task<List<GetJobLocationsResponse>> GetJobLocations()
        {
            var currentTime = DateTime.Now;
            var jobFilter = Builders<Job>.Filter.Where(x => x.Status == JobStatus.Published && x.StartDay <= currentTime && x.ExpirationDate > currentTime);
            var listJob = await _mongoDbRepository.Find(jobFilter);
            var jobsByLocation = listJob.GroupBy(g => g.LocationId)
                .OrderByDescending(o => o.Count())
                .Take(7)
                .ToList();

            var locationIds = jobsByLocation.Select(s => s.Key).ToList();
            var locationFilters = Builders<City>.Filter.Where(x => locationIds.Contains(x.Id));
            var locations = await _mongoDbRepository.Find(locationFilters);

            return jobsByLocation.Select(s => new GetJobLocationsResponse()
            {
                LocationId = s.Key,
                Quantity = s.Count(),
                LocationName = locations.FirstOrDefault(f => f.Id == s.Key)?.Name
            }).ToList();
        }

        public async Task<List<GetJobCategoriesResponse>> GetJobCategories()
        {
            var currentTime = DateTime.Now;
            var jobFilter = Builders<Job>.Filter.Where(x => x.Status == JobStatus.Published && x.StartDay <= currentTime && x.ExpirationDate > currentTime);
            var listJob = await _mongoDbRepository.Find(jobFilter);
            var jobsByCategory = listJob.GroupBy(g => g.CategoryId)
                .OrderByDescending(o => o.Count())
                .Take(7)
                .ToList();

            var categoryIds = jobsByCategory.Select(s => s.Key).ToList();
            var categoryFilters = Builders<Category>.Filter.Where(x => categoryIds.Contains(x.Id));
            var categories = await _mongoDbRepository.Find(categoryFilters);

            return jobsByCategory.Select(s => new GetJobCategoriesResponse()
            {
                CategoryId = s.Key,
                Quantity = s.Count(),
                CategoryName = categories.FirstOrDefault(f => f.Id == s.Key)?.Name
            }).ToList();
        }

        public async Task UpdateStatusJob(string jobId)
        {
            var jobFilter = Builders<Job>.Filter.Empty;
            if(!string.IsNullOrEmpty(jobId))
            {
                jobFilter =  Builders<Job>.Filter.Where(x => x.Id == jobId);
                var listJob = await _mongoDbRepository.Find(jobFilter);
                var job = listJob.FirstOrDefault();
                await _mongoDbRepository.Replace<Job>(changeStatusJob(job));
            }
            else
            {
                jobFilter = Builders<Job>.Filter.Where(x => x.Status != JobStatus.Closed && (x.StartDay == DateTime.Now || x.ExpirationDate == DateTime.Now));
                var listJob = await _mongoDbRepository.Find(jobFilter);
                foreach (Job job in listJob.ToList())
                {
                    await _mongoDbRepository.Replace<Job>(changeStatusJob(job));
                }
            }
        }

        public async Task<IList<GetLocationResponse>> GetLocations()
        {
            var locationsFilter = Builders<City>.Filter.Empty;
            var locations = await _mongoDbRepository.Find(locationsFilter);
            return locations.Select(s =>
            {
                return new GetLocationResponse()
                {
                    Id = s.Id,
                    Name = s.Name
                };
            }).ToList();
        }

        public async Task<IList<GetCategoryResponse>> GetCategories()
        {
            var categoryFilter = Builders<Category>.Filter.Empty;
            var categories = await _mongoDbRepository.Find(categoryFilter);
            return categories.Select(s =>
            {
                return new GetCategoryResponse()
                {
                    Id = s.Id,
                    Name = s.Name
                };
            }).ToList();
        }

        private Job changeStatusJob(Job job)
        {
            var today = DateTime.Now.ToUniversalTime();
            if (job.StartDay.Year == today.Year && job.StartDay.Month == today.Month && job.StartDay.Day == today.Day)
            {
                job.Status = JobStatus.Published;
            }
            if (job.ExpirationDate.Year == today.Year && job.ExpirationDate.Month == today.Month && job.ExpirationDate.Day == today.Day)
            {
                job.Status = JobStatus.Closed;
            }
            return job;
        }
    }
}
