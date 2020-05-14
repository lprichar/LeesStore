using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using LeesStore.Authorization.ApiKeys;

namespace LeesStore.ApiKeys.Dto
{
    [AutoMapFrom(typeof(ApiKey))]
    public class ApiKeyDto : EntityDto<long>
    {
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string ApiKey { get; set; }
    }
}