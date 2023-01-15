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
//    id bigint UN AI PK
//    unique_id varchar(255)
//name varchar(255)
//email varchar(255)
//email_verified_at varchar(255)
//password varchar(255)
//password_confirmation varchar(255)
//primary_role varchar(255)
//remember_token varchar(100)
//created_at timestamp
//updated_at timestamp
//is_deleted tinyin
}
