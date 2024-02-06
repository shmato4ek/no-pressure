using NoPressure.Common.DTO;
using NoPressure.Common.Enums;
using NoPressure.DAL.Entities;

namespace NoPressure.BLL.Helpers
{
    public class NotificationHelper
    {
        public static NotificationDTO CreateNotification(Notification notification)
        {
            var newNotification = new NotificationDTO();
            newNotification.Id = notification.Id;

            switch(notification.Title)
            {
                case NotificationTitle.NewSubscription:
                {
                    newNotification.Title = "New Subsctiption!";
                    newNotification.Text = $"{notification.Data.SecondUserName} has just subscribed to you!";
                    newNotification.Link = notification.Data.Link;
                }
                break;
                
                case NotificationTitle.NewJointRequest:
                {
                    newNotification.Title = "New joint request!";
                    newNotification.Text = $"{notification.Data.SecondUserName} has just sent a request to join the {notification.Data.GoalName} goal!";
                    newNotification.Link = notification.Data.Link;
                }
                break;

                case NotificationTitle.ApprovedJointRequest:
                {
                    newNotification.Title = "Approved joint request!";
                    newNotification.Text = $"{notification.Data.SecondUserName} has just approved your request to join the {notification.Data.GoalName} goal!";
                    newNotification.Link = notification.Data.Link;
                }
                break;

                case NotificationTitle.NewJointInvitation:
                {
                    newNotification.Title = "New joint invitation!";
                    newNotification.Text = $"{notification.Data.SecondUserName} has just send you in invitation to join the {notification.Data.GoalName} goal!";
                    newNotification.Link = notification.Data.Link;
                }
                break;

                case NotificationTitle.NewTeamInvitation:
                {
                    newNotification.Title = "New team invitation!";
                    newNotification.Text = $"{notification.Data.SecondUserName} has just invited you to join {notification.Data.TeamName}!";
                    newNotification.Link = notification.Data.Link;
                }
                break;

                case NotificationTitle.NewTeamJoin:
                {
                    newNotification.Title = "New team invitation!";
                    newNotification.Text = $"{notification.Data.SecondUserName} has just approved your invitation to join {notification.Data.TeamName}!";
                    newNotification.Link = notification.Data.Link;
                }
                break;
            }

            return newNotification;
        }
    }
}
