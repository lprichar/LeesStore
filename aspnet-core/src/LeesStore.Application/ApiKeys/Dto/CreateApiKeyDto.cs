using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using LeesStore.Authorization.ApiKeys;

namespace LeesStore.ApiKeys.Dto
{
    [AutoMapFrom(typeof(ApiKey))]
    public class CreateApiKeyDto : ApiKeyDto
    {
        [Required]
        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        public string Secret { get; set; }
    }
}