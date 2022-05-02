namespace IdentityServerApp.Dtos
{
    public class ErrorDtoModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
