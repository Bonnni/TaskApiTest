using AutoMapper;
using TaskManager.Services.DTOs;
using TaskManager.Services.Enums;
using TaskManagerApi.Model.Entities;

namespace TaskManager.Services.Profiles;

/// <summary>
/// Provides mapper configuration
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    /// Create profile
    /// </summary>
    public MappingProfile()
    {
        CreateMap<TaskDescEntity, TaskDescStatusDto>()
            .ForCtorParam(nameof(TaskDescStatusDto.Status),
                opt => opt.MapFrom(src => ((TaskDescEnitityStatus) src.Status).ToString()))
            .ForCtorParam(nameof(TaskDescStatusDto.TimeStamp),
                opt => opt.MapFrom(src => src.UpdateTime.ToString("yyyy-MM-ddTHH:mm:ssZ")));
    }
}