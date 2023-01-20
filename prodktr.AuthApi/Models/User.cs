using System.Xml.Linq;

namespace prodktr.AuthApi.Models
{
    public class User
    {
        public long id { get; set; }
        public string? unique_id { get; set; } 
        public string? name { get; set; } 
        public string? email { get; set; } 
        public string? email_verified_at { get; set; } 
        public string? password { get; set; } 
        public string? password_confirmation { get; set; } 
        public string? primary_role { get; set; } 
        public string? remember_token { get; set; } 
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public byte? is_deleted { get; set; } = 0;

    }
        
    }
