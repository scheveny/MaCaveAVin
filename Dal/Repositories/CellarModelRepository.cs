using Dal;
using DomainModel;
using Microsoft.EntityFrameworkCore;

public class CellarModelRepository : ICellarModelRepository
{
    private readonly CellarContext _context;

    public CellarModelRepository(CellarContext context)
    {
        _context = context;
    }

    public async Task<List<CellarModel>> GetCellarModelsByUserIdAsync(string userId)
    {
        return await _context.CellarModels
            .Where(cm => cm.UserId == userId)
            .ToListAsync();
    }

    public async Task<CellarModel> GetCellarModelByIdAndUserIdAsync(int id, string userId)
    {
        return await _context.CellarModels
            .FirstOrDefaultAsync(cm => cm.CellarModelId == id && cm.UserId == userId);
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
