using ApplicationLogic.Companys.Messages;
using System.Threading.Tasks;

namespace ApplicationLogic.Companys
{
    public interface ICompanyBusinessLogic
    {
        Task CreateCompany(CreateCompanyRequest company);

        Task UpdateCompany(UpdateCompanyRequest company);

        Task<GetCompanyReponse> GetCompanyByEmail(string email);

        Task<GetCompanyReponse> GetCompanyById(string id);
    }
}
