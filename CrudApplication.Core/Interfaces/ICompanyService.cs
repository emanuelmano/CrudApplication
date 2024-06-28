using CrudApplication.Models;
using CrudApplication.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudApplication.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<List<CompanyModel>> GetAllCompaniesAsync(int skip, int take);
        Task<CompanyModel> GetCompanyByIdAsync(int id);
        Task<int> CreateCompanyAsync(CompanyModel companyModel);
        Task<int> UpdateCompanyAsync(CompanyModel companyModel);
        Task<int> DeleteCompanyAsync(int id);
    }
}
