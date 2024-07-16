using DomainModel;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Cellar>> SearchCellarsByNameAsync(string userId, string name)
        {
            return await _context.Cellars
                .Where(c => c.UserId == userId && c.CellarName.Contains(name))
                .ToListAsync();
        }

        public async Task<List<Cellar>> GetCellarsByModelAsync(string userId, int modelId)
        {
            return await _context.Cellars
                .Where(c => c.UserId == userId && c.CellarModelId == modelId)
                .ToListAsync();
        }

        public async Task<List<Cellar>> GetCellarsByCategoryAsync(string userId, int categoryId)
        {
            return await _context.Cellars
                .Where(c => c.UserId == userId && c.CellarCategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<List<Cellar>> GetCellarsByUserIdAsync(string userId)
        {
            return await _context.Cellars
                .Where(c => c.UserId == userId)
                .ToListAsync();
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

        public async Task<CellarCategory> GetCellarCategoryByIdAsync(int categoryId)
        {
            return await _context.CellarCategories.FindAsync(categoryId);
        }

        public async Task<CellarModel> GetCellarModelByIdAsync(int modelId)
        {
            return await _context.CellarModels.FindAsync(modelId);
        }
    }

}
