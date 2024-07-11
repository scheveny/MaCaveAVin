using Dal.IRepositories;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Repositories
{
    public class CellarCategoryRepository : ICellarCategoryRepository
    {
        private readonly CellarContext _context;

        public CellarCategoryRepository(CellarContext context)
        {
            _context = context;
        }

        public async Task<List<CellarCategory>> GetAllCellarCategoriesAsync()
        {
            return await _context.CellarCategories.ToListAsync();
        }

        public async Task<CellarCategory> GetCellarCategoryByIdAsync(int id)
        {
            return await _context.CellarCategories.FindAsync(id);
        }

        public async Task AddCellarCategoryAsync(CellarCategory cellarCategory)
        {
            await _context.CellarCategories.AddAsync(cellarCategory);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCellarCategoryAsync(CellarCategory cellarCategory)
        {
            _context.CellarCategories.Update(cellarCategory);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCellarCategoryAsync(CellarCategory cellarCategory)
        {
            _context.CellarCategories.Remove(cellarCategory);
            await _context.SaveChangesAsync();
        }
    }
}
