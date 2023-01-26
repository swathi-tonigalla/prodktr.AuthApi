using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using Newtonsoft.Json;
using prodktr.AuthApi.Models;
using prodktr.AuthApi.Services.Interfaces;
using System.Diagnostics.Metrics;
using Ubiety.Dns.Core;
using Instrument = prodktr.AuthApi.Models.Instrument;

namespace prodktr.AuthApi.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IMongoCollection<projectconfigured> _projectconfigured;
        private readonly IMongoCollection<MappedInstrumentCollection> _mappedInstruments_Collection;
        private readonly IProdktrsegueDatabaseSettings _settings;
        private readonly IMongoClient _mongoClient;

        public ProjectService(IProdktrsegueDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _projectconfigured = database.GetCollection<projectconfigured>(settings.ProjectConfiguredCollectionName);
            _mappedInstruments_Collection = database.GetCollection<MappedInstrumentCollection>(settings.MappedInstruments_CollectionName);
            this._settings = settings;
            this._mongoClient = mongoClient;
        }
        public async Task<List<Project>> GetAllConfiguredProjects()
        {
            try
            {
                var projectCollection = _projectconfigured.Find(student => true).ToList();
                var response = new List<Project>();
                foreach (var item in projectCollection)
                {
                    response.Add(new Project
                    {
                        id = item.id,
                        projectName = item.projectName,
                        clientName = item.clientName,
                        userName = item.userName,
                        createdAt = item.createdAt,
                        destinationName = item.destinationName,
                        instruments = item.instruments,
                        isProjectAutoomapped = item.isProjectAutoomapped,
                        mappedInstruments = await GetMappedInstrument(item.projectName,item.instruments),
                    });
                }
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
        public async Task<string> LoadJson()
        {
            var test = string.Empty;
            using (StreamReader r = new StreamReader(@"C:\Swathi\Core\prodktr.AuthApi\prodktr.AuthApi\Test.json"))
            {
                string json = r.ReadToEnd();
                dynamic array = JsonConvert.DeserializeObject(json);
                foreach (var item in array)
                {
                    Console.WriteLine("{0} {1}", item.instruments, item.mappedInstruments);
                }
                test = json;
            }
            return test;
        }
        public async Task<List<MappedInstruments>> GetMappedInstrument(string projectName, List<Instrument> instruments)
        {
            try
            {
                var distinctcollection = new List<MappedInstruments>();
                foreach (var item in instruments)
                {
                    var builder = Builders<MappedInstrumentCollection>.Filter;
                    var filter = builder.Eq("projectName", projectName) & builder.Eq("mappedInstruments.mappedInstID", item.instrumentID);
                    var result = await _mappedInstruments_Collection.Find(filter).ToListAsync();
                    if(result.Count>0)
                    {
                        var mappedInstrument = result.Select(x => x.mappedInstruments).ToList();
                        distinctcollection.AddRange(mappedInstrument);
                    }
                }
                
                return distinctcollection;
            }
            catch (Exception ex)
            {

                throw ex;
            }
         
        }
    }
}
