using Dal;
using DomainModel;
using Microsoft.EntityFrameworkCore;

public class CellarCategoryRepository : ICellarCategoryRepository
{
    private readonly CellarContext _context;

    public CellarCategoryRepository(CellarContext context)
    {
        _context = context;
    }

    public async Task<List<CellarCategory>> GetCellarCategoriesByUserIdAsync(string userId)
    {
        return await _context.CellarCategories
            .Where(cc => cc.UserId == userId)
            .ToListAsync();
    }

    public async Task<CellarCategory> GetCellarCategoryByIdAndUserIdAsync(int id, string userId)
    {
        return await _context.CellarCategories
            .FirstOrDefaultAsync(cc => cc.CellarCategoryId == id && cc.UserId == userId);
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
