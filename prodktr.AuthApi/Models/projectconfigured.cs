using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Diagnostics.Metrics;

namespace prodktr.AuthApi.Models
{
    [BsonIgnoreExtraElements]
    public class projectconfigured
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        [BsonElement("projectName")]
        public string projectName { get; set; } = String.Empty;
        [BsonElement("clientName")]
        public string clientName { get; set; } = String.Empty;
        [BsonElement("userName")]
        public string userName { get; set; } = String.Empty;
        [BsonElement("createdAt")]
        public object createdAt { get; set; } = String.Empty;
        [BsonElement("destinationName")]
        public string destinationName { get; set; } = String.Empty;
        [BsonElement("isProjectAutoomapped")]
        public bool isProjectAutoomapped { get; set; } = false;
        [BsonElement("updatedAt")]
        public string updatedAt { get; set; } = String.Empty;
    }
}
