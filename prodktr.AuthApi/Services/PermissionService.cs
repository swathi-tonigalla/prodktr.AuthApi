using prodktr.AuthApi.Data.Interfaces;

namespace prodktr.AuthApi.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IUsersRepository _usersRepository;

        public PermissionService(IUsersRepository usersRepository)
        {
            this._usersRepository = usersRepository;
        }

        public Task<UserPermissions> GetNetUserPermission(string uniqueId)
        {
            var response = _usersRepository.GetNetUserPermission(uniqueId);
            return response;
        }
    }
}
