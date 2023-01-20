namespace prodktr.AuthApi.Models
{
    public class UserDto
    {
        public string? email { get; set; } = string.Empty;
        public string? password { get; set; } = string.Empty;
    }
         
    public class UserResponseDto
    {
        public string? id { get; set; }
        public string? unique_id { get; set; }
        public string? userName { get; set; }
        public string? email_id { get; set; }
        public string? email_verified_at { get; set; }
        public string? password { get; set; }
        public string? password_confirmation { get; set; }
        public string? roles { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedat { get; set; }
    }
    public class InstrumentConfuguredByYouDto
    {
        public int instrumentConfuguredByYou { get; set; }
    }
    public class InstrumentResponseDto
    {
        public int AllUserInstrumentConfigured { get; set; }
    }
}
