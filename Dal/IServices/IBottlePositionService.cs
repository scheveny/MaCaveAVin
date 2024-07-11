namespace Dal.IServices
{
    public interface IPositionService
    {
        (int, int)? FindFirstAvailablePosition(int cellarId);
    }
}
