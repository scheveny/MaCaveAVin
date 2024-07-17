using DomainModel;

public interface ICellarCategoryRepository
{
    Task<List<CellarCategory>> GetCellarCategoriesByUserIdAsync(string userId);
    Task<CellarCategory> GetCellarCategoryByIdAndUserIdAsync(int id, string userId);
    Task AddCellarCategoryAsync(CellarCategory cellarCategory);
    Task UpdateCellarCategoryAsync(CellarCategory cellarCategory);
    Task RemoveCellarCategoryAsync(CellarCategory cellarCategory);
}
