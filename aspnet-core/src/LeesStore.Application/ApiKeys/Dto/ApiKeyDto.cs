using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using System.ComponentModel.DataAnnotations;

namespace LeesStore.ApiKeys.Dto
{
    public class ApiKeyDto : EntityDto<long>
    {
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string ApiKey { get; set; }
    }
}