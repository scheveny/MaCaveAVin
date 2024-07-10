namespace MaCaveAVin.Interfaces
{
    public interface IPositionService
    {
        (int, int)? FindFirstAvailablePosition(int cellarId);
    }
}
