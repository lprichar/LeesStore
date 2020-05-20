namespace LeesStore.Authorization
{
    public class ApiKeyAuthenticationResult
    {
        public bool Success { get; }
        public int TenantId { get; }

        public ApiKeyAuthenticationResult(bool success, int? tenantId = null)
        {
            Success = success;
            TenantId = tenantId ?? 0;
        }
    }
}