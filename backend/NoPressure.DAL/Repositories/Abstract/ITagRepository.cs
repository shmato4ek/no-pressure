using NoPressure.DAL.Entities;

namespace NoPressure.DAL.Repositories.Abstract
{
    public interface ITagRepository : IRepository<Tag, int>
    {
        Task<List<Tag>> GetAllTagsActivitiesAsync(int userId);
        Task<Tag> FindByNameAsync(string tagName, int userId);
        Task<List<Tag>> FindAllTagsByUserId(int userId);
        Task<List<Tag>> FindAllTagsByTeamId(int teamId);
        Task<List<Tag>> GetTagsWithActivities(int userId);
        Task<Tag> FindTeamTag(string tagName, int teamId);
    }
}
