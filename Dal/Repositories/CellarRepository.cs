using DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Repositories
{
    public class CellarRepository : ICellarRepository
    {
        private readonly CellarContext _context;

        public CellarRepository(CellarContext context)
        {
            _context = context;
        }

        public async Task<List<Cellar>> GetAllCellarsAsync()
        {
            return await _context.Cellars.ToListAsync();
        }

        public async Task<Cellar> GetCellarByIdAsync(int id)
        {
            return await _context.Cellars.FindAsync(id);
        }

        public async Task<List<Cellar>> SearchCellarsByNameAsync(string name)
        {
            return await _context.Cellars
                .Where(c => c.CellarName.Contains(name))
                .ToListAsync();
        }

        public async Task<List<Cellar>> GetCellarsByModelAsync(int modelId)
        {
            return await _context.Cellars.Where(c => c.CellarModelId == modelId).ToListAsync();
        }

        public async Task<List<Cellar>> GetCellarsByCategoryAsync(int categoryId)
        {
            return await _context.Cellars.Where(c => c.CellarCategoryId == categoryId).ToListAsync();
        }

        public async Task AddCellarAsync(Cellar cellar)
        {
            await _context.Cellars.AddAsync(cellar);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCellarAsync(Cellar cellar)
        {
            _context.Cellars.Update(cellar);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCellarAsync(Cellar cellar)
        {
            _context.Cellars.Remove(cellar);
            await _context.SaveChangesAsync();
        }
    }
}
