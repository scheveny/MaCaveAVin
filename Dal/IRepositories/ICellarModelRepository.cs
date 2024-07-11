using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.IRepositories
{
    public interface ICellarModelRepository
    {
        Task<List<CellarModel>> GetAllCellarModelsAsync();
        Task<CellarModel> GetCellarModelByIdAsync(int id);
        Task AddCellarModelAsync(CellarModel cellarModel);
        Task UpdateCellarModelAsync(CellarModel cellarModel);
        Task RemoveCellarModelAsync(CellarModel cellarModel);
    }
}
