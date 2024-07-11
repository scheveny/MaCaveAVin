using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.IRepositories
{
    public interface ICellarCategoryRepository
    {
        Task<List<CellarCategory>> GetAllCellarCategoriesAsync();
        Task<CellarCategory> GetCellarCategoryByIdAsync(int id);
        Task AddCellarCategoryAsync(CellarCategory cellarCategory);
        Task UpdateCellarCategoryAsync(CellarCategory cellarCategory);
        Task RemoveCellarCategoryAsync(CellarCategory cellarCategory);
    }
}
