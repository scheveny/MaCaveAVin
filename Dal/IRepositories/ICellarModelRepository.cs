using DomainModel;

public interface ICellarModelRepository
{
    Task<List<CellarModel>> GetCellarModelsByUserIdAsync(string userId);
    Task<CellarModel> GetCellarModelByIdAndUserIdAsync(int id, string userId);
    Task AddCellarModelAsync(CellarModel cellarModel);
    Task UpdateCellarModelAsync(CellarModel cellarModel);
    Task RemoveCellarModelAsync(CellarModel cellarModel);
}
