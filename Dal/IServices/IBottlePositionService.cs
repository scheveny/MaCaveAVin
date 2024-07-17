namespace Dal.Services
{
    public interface IBottlePositionService
    {
        (int, int)? FindFirstAvailablePosition(int cellarId);
    }
}
