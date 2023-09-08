using ActiveDirectoryWebApp.Entities;

namespace ActiveDirectoryWebApp.Services
{
    public interface IActiveDirectoryService
    {
         Task<GeneralInfo> GetGeneralData(string username);
        Task<OrganizationInfo> GetOrganizationData(string username);
        Task<AddressInfo> GetAddressStructureData(string username);
    }
}
