using AutoMapper;
using ParcelTracker.Application.Features.Notifications.Models;
using ParcelTracker.Application.Features.Rules.Models;
using ParcelTracker.Core.Notifications;
using ParcelTracker.Core.Rules;
using ParcelTracker.Infrastructure.Entities;

namespace ParcelTracker.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Notification, NotificationModel>().ReverseMap();
        CreateMap<Notification, NotificationEntity>().ReverseMap();
        CreateMap<NotificationModel, NotificationEntity>().ReverseMap();
        CreateMap<Rule, RuleModel>().ReverseMap();
        CreateMap<Rule, RuleEntity>().ReverseMap();
        CreateMap<RuleEntity, Rule>().ReverseMap();
        CreateMap<RuleRequest, Rule>().ReverseMap();
    }
}