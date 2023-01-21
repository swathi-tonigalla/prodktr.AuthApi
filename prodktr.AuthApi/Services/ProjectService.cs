using MongoDB.Driver;
using prodktr.AuthApi.Services.Interfaces;

namespace prodktr.AuthApi.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IMongoCollection<projectconfigured> _projectconfigured;

        public ProjectService(IProdktrsegueDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _projectconfigured = database.GetCollection<projectconfigured>(settings.ProjectConfiguredCollectionName);
        }
        public async Task<List<projectconfigured>> GetAllConfiguredProjects()
        {
            try
            {
                var response = _projectconfigured.Find(student => true).ToList();
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
    }
}
