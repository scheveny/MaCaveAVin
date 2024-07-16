using DomainModel;

public interface ICellarRepository
{
    Task<List<Cellar>> GetAllCellarsAsync();
    Task<Cellar> GetCellarByIdAsync(int id);
    Task<List<Cellar>> SearchCellarsByNameAsync(string userId, string name);
    Task<List<Cellar>> GetCellarsByModelAsync(string userId, int modelId);
    Task<List<Cellar>> GetCellarsByCategoryAsync(string userId, int categoryId);
    Task<List<Cellar>> GetCellarsByUserIdAsync(string userId);
    Task AddCellarAsync(Cellar cellar);
    Task UpdateCellarAsync(Cellar cellar);
    Task RemoveCellarAsync(Cellar cellar);
    Task<CellarCategory> GetCellarCategoryByIdAsync(int categoryId);
    Task<CellarModel> GetCellarModelByIdAsync(int modelId);
}