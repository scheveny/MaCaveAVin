using DomainModel;

public interface ICellarRepository
{
    Task<List<Cellar>> GetAllCellarsAsync();
    Task<Cellar> GetCellarByIdAsync(int id);
    Task<List<Cellar>> SearchCellarsByNameAsync(string name);
    Task<List<Cellar>> GetCellarsByModelAsync(int modelId);
    Task<List<Cellar>> GetCellarsByCategoryAsync(int categoryId);
    Task AddCellarAsync(Cellar cellar);
    Task UpdateCellarAsync(Cellar cellar);
    Task RemoveCellarAsync(Cellar cellar);
}