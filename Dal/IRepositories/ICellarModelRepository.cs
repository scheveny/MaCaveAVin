using DomainModel;


namespace Dal.IRepositories
{
    public interface ICellarModelRepository
    {
        Task<CellarModel> PostAsync(CellarModel cellarModel);
        Task<List<CellarModel>> GetAllCellarModelsAsync();
        Task<CellarModel> GetCellarModelByIdAsync(int id);
        Task AddCellarModelAsync(CellarModel cellarModel);
        Task UpdateCellarModelAsync(CellarModel cellarModel);
        Task RemoveCellarModelAsync(CellarModel cellarModel);
    }
}
