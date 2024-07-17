using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using Dal.IRepositories;

namespace Dal
{
    public class CellarModelRepository : ICellarModelRepository
    {
        private readonly CellarContext _context;

        public CellarModelRepository(CellarContext context)
        {
            _context = context;
        }

        public async Task<List<CellarModel>> GetAllCellarModelsAsync()
        {
            return await _context.CellarModels.ToListAsync();
        }

        public async Task<CellarModel> GetCellarModelByIdAsync(int id)
        {
            return await _context.CellarModels.FindAsync(id);
        }

        public async Task AddCellarModelAsync(CellarModel cellarModel)
        {
            await _context.CellarModels.AddAsync(cellarModel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCellarModelAsync(CellarModel cellarModel)
        {
            _context.CellarModels.Update(cellarModel);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCellarModelAsync(CellarModel cellarModel)
        {
            _context.CellarModels.Remove(cellarModel);
            await _context.SaveChangesAsync();
        }
    }
}

