namespace prodktr.AuthApi.Services.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectResponse>> GetAllConfiguredProjects();
        Task<List<MappedInstruments>> GetAllMappings(string projectName);
    }
    public interface IProdktrsegueDatabaseSettings
    {
        string ProjectConfiguredCollectionName { get; set; }
        string MappedInstruments_CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
    public class ProdktrsegueDatabaseSettings : IProdktrsegueDatabaseSettings
    {
        public string ProjectConfiguredCollectionName { get; set; } = String.Empty;
        public string MappedInstruments_CollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}
