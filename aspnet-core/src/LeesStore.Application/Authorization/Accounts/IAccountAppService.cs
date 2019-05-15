using System.Threading.Tasks;
using Abp.Application.Services;
using LeesStore.Authorization.Accounts.Dto;

namespace LeesStore.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
