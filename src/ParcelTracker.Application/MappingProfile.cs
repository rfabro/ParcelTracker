using AutoMapper;
using ParcelTracker.Application.Features.Notifications.Models;
using ParcelTracker.Core.Notifications;
using ParcelTracker.Infrastructure.Entities;

namespace ParcelTracker.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Notification, NotificationModel>().ReverseMap();
        CreateMap<Notification, NotificationEntity>().ReverseMap();
        CreateMap<NotificationModel, NotificationEntity>().ReverseMap();
    }
}