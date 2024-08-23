namespace UserServices.Dtos
{
    public class AuthenResponse
    {
        public string Token { get; set; } = string.Empty;
        public bool IsSuccess { get; set; } 
        public string Message { get; set; } = string.Empty;
    }
}
