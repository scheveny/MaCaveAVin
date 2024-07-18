using DomainModel;

public interface ICellarCategoryRepository
{
    Task<List<CellarCategory>> GetAllCellarCategoriesAsync();
    Task<CellarCategory> GetCellarCategoryByIdAsync(int id);
    Task AddCellarCategoryAsync(CellarCategory cellarCategory);
    Task UpdateCellarCategoryAsync(CellarCategory cellarCategory);
    Task RemoveCellarCategoryAsync(CellarCategory cellarCategory);
}