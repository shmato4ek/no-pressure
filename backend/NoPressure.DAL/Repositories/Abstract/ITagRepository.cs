using NoPressure.DAL.Entities;

namespace NoPressure.DAL.Repositories.Abstract
{
    public interface ITagRepository : IRepository<Tag, int>
    {
        Task<List<Tag>> GetAllTagsActivitiesAsync(int userId);
        Task<Tag> FindByNameAsync(string tagName);
        Task<List<Tag>> FindAllTagsByUserId(int userId);
    }
}
