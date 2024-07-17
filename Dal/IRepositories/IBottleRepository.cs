using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.IRepositories
{
    public interface IBottleRepository
    {
        Task<List<Bottle>> GetAllAsync();
        Task<Bottle> GetByIdAsync(int id);
        Task<List<Bottle>> GetBottlesByCellarIdAsync(int cellarId);
        Task<List<Bottle>> GetBottlesByUserIdAsync(string userId);
        Task<Bottle> PostAsync(Bottle bottle);
        Task<Bottle> UpdateAsync(Bottle bottle);
        Task<Bottle> RemoveAsync(int id);
    }
}
