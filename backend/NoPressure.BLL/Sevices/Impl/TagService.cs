using System.Globalization;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.DTO;
using NoPressure.Common.Enums;
using NoPressure.Common.Models.Activity;
using NoPressure.Common.Models.Requests;
using NoPressure.Common.Models.Schedule;
using NoPressure.Common.Models.Tag;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Unit.Abstract;

namespace NoPressure.BLL.Sevices.Impl
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public TagService (IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<TagDTO>> GetBestUserTags(int userId)
        {
            var tagsEntity = await _uow.TagRepository.GetTagsWithActivities(userId);
            if(!tagsEntity.Any())
            {
                return null;
            }
            var sortedTags = tagsEntity.OrderByDescending(tag => tag.Activities.Count).ToList();
            var bestTags = new List<TagDTO>();
            var sortedTagsCount = sortedTags.Count >= 5 ? 5 : sortedTags.Count;
            for(int i = 0; i <= sortedTagsCount-1; i++)
            {
                bestTags.Add(_mapper.Map<TagDTO>(sortedTags[i]));
            }

            return bestTags;
        }

        public async Task<List<TagInfoDTO>> GetUsersTagInfo(int userId)
        {
            var tags = await _uow.TagRepository.FindAllTagsByUserId(userId);
            
            var tagsInfo = tags.Select(tag => new TagInfoDTO {
                Name = tag.Name,
                Color = tag.Color
            }).ToList();

            return tagsInfo;
        }

        public async Task UpdateTag(UpdateTagDTO updateTag)
        {
            var tagEntity = await _uow.TagRepository.FindAsync(updateTag.Id);

            tagEntity.Name = updateTag.Name;
            tagEntity.Color = updateTag.Color;

            _uow.TagRepository.Update(tagEntity);
        }
    }
}
