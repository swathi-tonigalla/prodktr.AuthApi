using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Diagnostics.Metrics;

namespace prodktr.AuthApi.Models
{
    [BsonIgnoreExtraElements]
    //public class projectconfigured
    //{
    //    [BsonId]
    //    [BsonRepresentation(BsonType.ObjectId)]
    //    public string Id { get; set; } = String.Empty; = String.Empty;

    //    [BsonElement("projectName")]
    //    public string projectName { get; set; } = String.Empty; = String.Empty;
    //    [BsonElement("clientName")]
    //    public string clientName { get; set; } = String.Empty; = String.Empty;
    //    [BsonElement("userName")]
    //    public string userName { get; set; } = String.Empty; = String.Empty;
    //    [BsonElement("createdAt")]
    //    public object createdAt { get; set; } = String.Empty; = String.Empty;
    //    [BsonElement("destinationName")]
    //    public string destinationName { get; set; } = String.Empty; = String.Empty;
    //    [BsonElement("isProjectAutoomapped")]
    //    public bool isProjectAutoomapped { get; set; } = String.Empty; = false;
    //    [BsonElement("updatedAt")]
    //    public string updatedAt { get; set; } = String.Empty; = String.Empty;
    //}

    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class Instrument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;
        public int instrumentID { get; set; } 
        public string instrumentName { get; set; } = String.Empty;
        public string completed { get; set; } = String.Empty;
        public string isChecked { get; set; } = String.Empty;
        public bool isMasterSel { get; set; } 
        public bool isInstrumentSaved { get; set; } 
        public string toggleCheck { get; set; } = String.Empty;
        public List<InstrumentsData> instrumentsData { get; set; }
        public string selectedInstrumentType { get; set; } = String.Empty;
        public string sourceName { get; set; } = String.Empty;
        public int instTypeSourceID { get; set; } 
        public List<int> LDcolId { get; set; } 
        public string instTypeSourceColName { get; set; } = String.Empty;
        public string projectID { get; set; } = String.Empty;
        public bool isMappingStartedForInstrument { get; set; } 
        public string projectStatus { get; set; } = String.Empty;
    }

    public class InstrumentsData
    {
        
        public string Id { get; set; } = String.Empty;
        public int colId { get; set; }
        public string group { get; set; } = String.Empty;
        public string name { get; set; } = String.Empty;
        public string mandatory { get; set; } = String.Empty;
        public bool isMultiple { get; set; } 
        public string completed { get; set; } = String.Empty;
        public bool isDestination { get; set; } 
        public bool MDisChecked { get; set; } 
        public string MDisDesabled { get; set; } = String.Empty;
        public string isInstrumentSaved { get; set; } = String.Empty;
        public string mappedResultText { get; set; } = String.Empty;
    }

    public class InstumentDataMapped
    {
        
        public string mpColName { get; set; } = String.Empty;
        public string mpgroup { get; set; } = String.Empty;
        public int instID { get; set; } 
        public int mpColID { get; set; } 
        public object mpcolData { get; set; } = String.Empty;
        public bool mappedColDataCheckbox { get; set; } 
        public string mappedFile { get; set; } = String.Empty;
        public string mappedSourceName { get; set; } = String.Empty;
    }

   
    [BsonIgnoreExtraElements]
    public class projectconfigured
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;
        public string projectName { get; set; } = String.Empty;
        public string clientName { get; set; } = String.Empty;
        public string userName { get; set; } = String.Empty;
        public object createdAt { get; set; } = String.Empty;
        public string destinationName { get; set; } = String.Empty;
        public List<Instrument> instruments { get; set; } 
        public bool isProjectAutoomapped { get; set; } 
        public List<MappedInstruments_Collection> MappedInstruments_Collection { get; set; } 
        public string updatedAt { get; set; } = String.Empty;
    }
   

    public class MappedInstruments
    {
        
        public InstumentDataMapped instumentDataMapped { get; set; }
        public int mappedInstID { get; set; }
        public string mappedInstrumentName { get; set; }
    }
    [BsonIgnoreExtraElements]
    public class MappedInstruments_Collection
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;
        public string projectID { get; set; } = String.Empty;
        public string projectName { get; set; } = String.Empty;
        public string clientName { get; set; } = String.Empty;
        public MappedInstruments mappedInstruments { get; set; }
    }
    public class ProjectResponse
    {
        public string id { get; set; }
        public string userName { get; set; }
        public string projectName { get; set; }
        public string clientName { get; set; }
        public int createdAt { get; set; }

        public string destinationName { get; set; }
        public List<Instrument> instruments { get; set; }
        public bool isProjectAutoomapped { get; set; }
        public List<MappedInstruments_Collection> mappedInstruments { get; set; }
    }
}
