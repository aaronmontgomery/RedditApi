using Models;

namespace Services
{
    public interface IRedditService
    {
        Task<PopularModel?> GetSubRedditAsync();
    }
}
