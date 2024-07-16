using DomainModel;

public interface ICellarCategoryRepository
{
    Task<List<CellarCategory>> GetAllCellarCategoriesAsync();
    Task<CellarCategory> GetCellarCategoryByIdAsync(int id);
    Task AddCellarCategoryAsync(CellarCategory cellarCategory);
    Task UpdateCellarCategoryAsync(CellarCategory cellarCategory);
    Task RemoveCellarCategoryAsync(CellarCategory cellarCategory);
    Task<List<CellarCategory>> GetCellarCategoriesByUserIdAsync(string userId);
    Task<CellarCategory> GetCellarCategoryByIdAndUserIdAsync(int id, string userId);
}
