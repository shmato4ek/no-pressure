using Microsoft.EntityFrameworkCore;
using NoPressure.DAL.Context;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Repositories.Abstract;

namespace NoPressure.DAL.Repositories.Impl
{
    public class TagRepository : Repository<Tag, int>, ITagRepository
    {
        public TagRepository(NoPressureDbContext context) : base(context) { }

        public async Task<List<Tag>> FindAllTagsByTeamId(int teamId)
        {
            var tags = await _context
                .Tags
                .Include(t => t.Team)
                .Where(t => t.Team.Id == teamId)
                .ToListAsync();

            return tags;
        }

        public async Task<List<Tag>> FindAllTagsByUserId(int userId)
        {
            var tags = await _context
                .Tags
                .Where(tag => tag.UserId == userId)
                .ToListAsync();

            return tags;
        }

        public async Task<Tag> FindByNameAsync(string tagName, int userId)
        {
            var tag = await _context
                .Tags
                .Where(t => t.Name == tagName)
                .FirstOrDefaultAsync(t => t.UserId == userId);

            return tag;
        }

        public async Task<Tag> FindTeamTag(string tagName, int teamId)
        {
            var tag = await _context
                .Tags
                .Include(t => t.Team)
                .Where(t => t.Name == tagName)
                .Where(t => t.Team.Id == teamId)
                .FirstOrDefaultAsync();

            return tag;
        }

        public async Task<List<Tag>> GetAllTagsActivitiesAsync(int userId)
        {

            var tags = await _context
                .Tags
                .Include(tag => tag.Activities)
                .Where(tag => tag.UserId == userId)
                .Select(tag => new Tag
                {
                    Id = tag.Id,
                    Color = tag.Color,
                    UserId = tag.UserId,
                    Name = tag.Name,
                    Activities = tag.Activities
                        .Where(activity => !activity.IsScheduled || activity.IsRepeatable)
                        .ToList()
                })
                .Where(tag => tag.Activities.Any())
                .ToListAsync();

            return tags;
        }

        public async Task<List<Tag>> GetTagsWithActivities(int userId)
        {
            var tags = await _context
                .Tags
                .Include(tag => tag.Activities)
                .Where(tag => tag.UserId == userId)
                .ToListAsync();

            return tags;
        }
    }
}
