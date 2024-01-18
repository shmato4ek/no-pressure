using System.Globalization;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using NoPressure.BLL.Helpers;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.DTO;
using NoPressure.Common.Enums;
using NoPressure.Common.Models.Activity;
using NoPressure.Common.Models.Notifications;
using NoPressure.Common.Models.Requests;
using NoPressure.Common.Models.Schedule;
using NoPressure.Common.Models.Tag;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Unit.Abstract;

namespace NoPressure.BLL.Sevices.Impl
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public NotificationService (IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task CheckNotification(int id)
        {
            var notification = await _uow.NotificationRepository.FindAsync(id);

            notification.IsRead = true;

            _uow.NotificationRepository.Update(notification);
        }

        public async Task<bool> CheckNotifications(int userId)
        {
            var isChecked = await _uow.NotificationRepository.CheckNotifications(userId);

            return isChecked;
        }

        public async Task<List<NotificationDTO>> GetUserNotifications(int userId)
        {
            var notificationsEntity = await _uow.NotificationRepository.GetAllUserNotifications(userId);
            
            var notifications = new List<NotificationDTO>();

            if(notificationsEntity.Count != 0)
            {
                foreach(var notification in notificationsEntity)
                {
                    var newNotification = NotificationHelper.CreateNotification(notification);

                    newNotification.IsRead = notification.IsRead;

                    newNotification.Date = notification.Date.ToString("dd/MM/yy H:mm:ss");

                    notifications.Add(newNotification);
                }
            }

            return notifications;
        }

        
    }
}
