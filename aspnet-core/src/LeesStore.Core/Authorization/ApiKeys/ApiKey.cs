using LeesStore.Authorization.Users;

namespace LeesStore.Authorization.ApiKeys
{
    /// <summary>
    /// Api Keys are like regular users in that they:
    /// * Are granted and restricted access via AbpAuthorize attributes
    /// * Have permissions (see ShieldedAlphaAuthorizationProvider)
    /// * Can hit API Endpoints that they have permission for
    ///
    /// Api Users are unlike regular users in that they:
    /// * Don't have a (real) first name, last name, e-mail etc
    /// * Don't generally appear along side regular users in the UI
    /// * Don't have the permission required to log into the web front-end
    /// * Do have permission to log into SFTP
    /// * Do have permission to hit externally facing API's to e.g. get status for a batch job
    /// * Do have a Username, but it is called an ApiKey
    /// * Do have a Password, but it is called an ApiSecret
    /// </summary>
    public class ApiKey : User
    {

    }
}
