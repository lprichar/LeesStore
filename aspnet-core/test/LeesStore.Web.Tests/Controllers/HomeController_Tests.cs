using System.Threading.Tasks;
using LeesStore.Models.TokenAuth;
using LeesStore.Web.Controllers;
using Shouldly;
using Xunit;

namespace LeesStore.Web.Tests.Controllers
{
    public class HomeController_Tests: LeesStoreWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}