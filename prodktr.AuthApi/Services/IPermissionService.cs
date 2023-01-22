namespace prodktr.AuthApi.Services
{
    public interface IPermissionService
    {
        Task<UserPermissions> GetNetUserPermission(string uniqueId);
    }
}