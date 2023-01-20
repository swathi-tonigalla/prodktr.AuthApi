namespace prodktr.AuthApi.Models
{
    public class Client
    {
        public int id { get; set; }
        public string? c_id { get; set; }
        public string? name { get; set; }
        public string? remark { get; set; }
        public int? is_deleted { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
//    id bigint UN AI PK
//unique_id varchar(255)
//username varchar(255)
//project_name varchar(255)
//destination_name varchar(255)
//client_name varchar(255)
//mappedInstrumentCount bigint
//description varchar(255)
//user_id varchar(255)
//created_at timestamp
//updated_at timesta
    public class Client_Project
    {
        public long id { get; set; }
        public string? unique_id { get; set; }
        public string? username { get; set; }
        public string? project_name { get; set; }
        public string? destination_name { get; set; }
        public string? client_name { get; set; }
        public long? mappedInstrumentCount { get; set; }
        public string? description { get; set; }
        public string? user_id { get; set; }

    }
}
