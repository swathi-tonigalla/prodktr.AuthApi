using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;

namespace prodktr.AuthApi.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Id
    {
        [JsonProperty("$oid")]
        public string oid { get; set; }
    }

    

   
    [BsonIgnoreExtraElements]
    public class MappedInstrumentCollection
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string projectID { get; set; }
        public string projectName { get; set; }
        public string clientName { get; set; }
        public MappedInstruments mappedInstruments { get; set; }
    }






    //public class MappedInstruments
    //{

    //    public InstumentDataMapped instumentDataMapped { get; set; } 
    //    public int mappedInstID { get; set; } 
    //    public string mappedInstrumentName { get; set; } = String.Empty;
    //}
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    
   

    public class Instrument
    {
        public int instrumentID { get; set; }
        public string instrumentName { get; set; }
        public string completed { get; set; }
        public string isChecked { get; set; }
        public bool isMasterSel { get; set; }
        public bool isInstrumentSaved { get; set; }
        public string toggleCheck { get; set; }
        public List<InstrumentsDatum> instrumentsData { get; set; }
        public string selectedInstrumentType { get; set; }
        public string sourceName { get; set; }
        public int instTypeSourceID { get; set; }
        public List<int> LDcolId { get; set; }
        public string instTypeSourceColName { get; set; }
        public string projectID { get; set; }
        public bool isMappingStartedForInstrument { get; set; }
        public string projectStatus { get; set; } = String.Empty;
    }

    public class InstrumentsDatum
    {
        public int colId { get; set; }
        public string group { get; set; }
        public string name { get; set; }
        public string mandatory { get; set; }
        public bool isMultiple { get; set; }
        public string completed { get; set; }
        public bool isDestination { get; set; }
        public bool MDisChecked { get; set; }
        public string MDisDesabled { get; set; }
        public string isInstrumentSaved { get; set; }
        public string mappedResultText { get; set; }
    }

    public class InstrumentType
    {
        public string instrumentTypename { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class projectconfigured
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string id { get; set; }
        public string destinationID { get; set; }
        public string projectName { get; set; }
        public string clientName { get; set; }
        public string userName { get; set; }
        public long createdAt { get; set; }
        public string destinationName { get; set; }
        public List<Instrument> instruments { get; set; }
        public List<InstrumentType> instrumentTypes { get; set; }
        public bool isProjectStarted { get; set; }
        public bool isProjectAutoomapped { get; set; }
        public object updatedAt { get; set; }
    }
    [BsonIgnoreExtraElements]
    public class ProjectResponse
    {
        public Id _id { get; set; }
        public string id { get; set; }
        public string userName { get; set; }
        public string destinationID { get; set; }
        public string projectName { get; set; }
        public string clientName { get; set; }
        public long createdAt { get; set; }

        public string destinationName { get; set; }
        public List<Instrument> instruments { get; set; }
        public List<MappedInstrumentCollection> MappedInstrument { get; set; }
        public List<InstrumentType> instrumentTypes { get; set; }
        public bool isProjectStarted { get; set; }
        public bool isProjectAutoomapped { get; set; }
        public object updatedAt { get; set; }
    }
}
