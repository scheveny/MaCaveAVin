using DomainModel;

public interface ICellarCategoryRepository
{
    
    Task<CellarCategory> PostCellarCategoryAsync(CellarCategory cellarCategory);
    Task <List<CellarCategory>> GetCellarCategoriesAsync();
    Task <CellarCategory> GetCellarCategoryAsync(int Id);
    Task AddCellarCategoryAsync(CellarCategory cellarCategory);
    Task UpdateCellarCategoryAsync(CellarCategory cellarCategory);
    Task RemoveCellarCategoryAsync(CellarCategory cellarCategory);
    
}
