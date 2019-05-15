using System.Threading.Tasks;
using Abp.Application.Services;
using LeesStore.Sessions.Dto;

namespace LeesStore.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
