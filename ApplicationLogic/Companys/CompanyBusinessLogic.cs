
using ApplicationLogic.Companys.Messages;
using AutoMapper;
using Domain.Models;
using MongoDB.Driver;
using Repository;
using Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationLogic.Companys
{
    public class CompanyBusinessLogic : ICompanyBusinessLogic
    {
        private readonly IMongoDbRepository _mongoDbRepository;
        public CompanyBusinessLogic(IMapper mapper,
           IMongoDbRepository mongoDbRepository)
        {
            _mongoDbRepository = mongoDbRepository;
        }

        public async Task CreateCompany(CreateCompanyRequest company)
        {
            var companyModel = Mapper.Map<Company>(company);
            companyModel.Id = IdGeneratorHelper.IdGenerator();
            await _mongoDbRepository.Create(companyModel);
        }

        public async Task<GetCompanyReponse> GetCompanyByEmail(string email)
        {
            var filter = Builders<Company>.Filter.Where(x => x.Email == email);
            var result = await _mongoDbRepository.Find<Company>(filter);
            return Mapper.Map<Company,GetCompanyReponse>(result.FirstOrDefault());
        }

        public async Task UpdateCompany(UpdateCompanyRequest company)
        {
            var companyModel = Mapper.Map<Company>(company);
            await _mongoDbRepository.Replace(companyModel);
        }

        public async Task<GetCompanyReponse> GetCompanyById(string id)
        {
            var result = await _mongoDbRepository.Get<Company>(id);
            return Mapper.Map<Company, GetCompanyReponse>(result);
        }
    }
}
