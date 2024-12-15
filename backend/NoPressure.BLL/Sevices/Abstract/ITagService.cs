using NoPressure.Common.DTO;
using NoPressure.Common.Models.Requests;
using NoPressure.Common.Models.Schedule;
using NoPressure.Common.Models.Tag;

namespace NoPressure.BLL.Sevices.Abstract
{
    public interface ITagService
    {
        Task<List<TagInfoDTO>> GetUsersTagInfo(int userId);
        Task<List<TagInfoDTO>> GetTeamTagInfo(int teamId);
        Task UpdateTag(UpdateTagDTO updateTag);
        Task<List<TagDTO>> GetBestUserTags(int userId);
    }
}
