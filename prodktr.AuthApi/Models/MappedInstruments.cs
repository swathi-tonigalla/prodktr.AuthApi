using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace prodktr.AuthApi.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
  

    public class InstumentDataMapped
    {
        
        public string mpColName { get; set; }
        public string mappedFile { get; set; }
        public string mappedSourceName { get; set; }
        public string mappedInstrumentName { get; set; }
        public string mpgroup { get; set; }
        public int instID { get; set; }
        public int mappedInstID { get; set; }
        public int mpColID { get; set; }
        public object mpcolData { get; set; }
        public bool mappedColDataCheckbox { get; set; }
        

    }
    public class MappedInstruments
    {
        public InstumentDataMapped instumentDataMapped { get; set; }
        public int mappedInstID { get; set; }
        public string mappedInstrumentName { get; set; }
    }

    




}
