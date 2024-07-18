using Dal;
using DomainModel;
using DomainModel.DTO.cellarCat;
using Microsoft.EntityFrameworkCore;

public class CellarCategoryRepository : ICellarCategoryRepository
{
    private readonly CellarContext _context;

    public CellarCategoryRepository(CellarContext context)
    {
        _context = context;
    }

    public async Task <List<CellarCategory>> GetCellarCategoriesAsync()
    {
        return await _context.CellarCategories.ToListAsync();
    }

    public async Task <CellarCategory> GetCellarCategoryAsync(int Id)
    {
        return await _context.CellarCategories.FindAsync(Id);
    }

    public async Task<CellarCategory> PostCellarCategoryAsync(CellarCategory cellarCategory)
    {
        _context.CellarCategories.Add(cellarCategory);
        await _context.SaveChangesAsync();

        return cellarCategory;
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
