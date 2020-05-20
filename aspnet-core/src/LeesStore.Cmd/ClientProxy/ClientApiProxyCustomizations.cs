using System.Net.Http.Headers;

namespace LeesStore.Cmd.ClientProxy
{
    partial class ClientApiProxy
    {
        public string AccessToken { get; set; }

        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url)
        {
            if (AccessToken != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            }
        }
    }
}
