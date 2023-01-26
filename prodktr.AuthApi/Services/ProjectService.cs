using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using prodktr.AuthApi.Models;
using prodktr.AuthApi.Services.Interfaces;
using Ubiety.Dns.Core;

namespace prodktr.AuthApi.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IMongoCollection<projectconfigured> _projectconfigured;
        private readonly IMongoCollection<MappedInstrumentCollection> _mappedInstruments_Collection;
      
        public ProjectService(IProdktrsegueDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _projectconfigured = database.GetCollection<projectconfigured>(settings.ProjectConfiguredCollectionName);
            _mappedInstruments_Collection = database.GetCollection<MappedInstrumentCollection>(settings.MappedInstruments_CollectionName);
        }
        public async Task<List<ProjectResponse>> GetAllConfiguredProjects()
        {
            try
            {
                var projectCollection = _projectconfigured.Find(student => true).ToList();
                var response = new List<ProjectResponse>();
                foreach (var item in projectCollection)
                {
                    response.Add(new ProjectResponse
                    {
                        id = item.id,
                        projectName = item.projectName,
                        clientName = item.clientName,
                        userName = item.userName,
                        createdAt = item.createdAt,
                        destinationName = item.destinationName,
                        instruments = item.instruments.Distinct().ToList(),
                        isProjectAutoomapped = item.isProjectAutoomapped,
                        mappedInstruments = await GetAllMappings(item.projectName),
                    });
                }
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
        public async Task<List<MappedInstruments>> GetAllMappings(string projectName)
        {
            try
            {
                var result= await _mappedInstruments_Collection.FindAsync(c => c.projectName.Equals(projectName));

                var collection = result.ToList().Select(x=>x.mappedInstruments).ToList();
                return collection;
            }
            catch (Exception ex)
            {

                throw;
            }
         
        }
    }
}
