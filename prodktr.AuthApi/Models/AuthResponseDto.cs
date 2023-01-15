namespace prodktr.AuthApi.Models
{
    public class AuthResponseDto
    {
        public string? unique_id { get; set; } = string.Empty;
        public string? name { get; set; } = string.Empty;
        public string? roles { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenExpires { get; set; }
        public long id { get; set; }
        public Permission? permission { get; set; }
    }
    public class Permission
    {
        public int id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string? user_id { get; set; }
        public string? user_unique_id { get; set; }
        public string? user_name { get; set; }
        public string? selectedClient { get; set; }
        public string? instrument_destination { get; set; }
        public string? audit_report { get; set; }
        public string? load_project { get; set; }
    }
}
