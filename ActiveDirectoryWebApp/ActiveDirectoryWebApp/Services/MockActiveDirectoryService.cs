using ActiveDirectoryWebApp.Entities;


namespace ActiveDirectoryWebApp.Services
{
    public class MockActiveDirectoryService : IActiveDirectoryService
    {

        public async Task<GeneralInfo?> GetGeneralData(string username)
        {
            try
            {

               var userInfo = new GeneralInfo() { 
                    DisplayName= "Carlos Alberto Aguero Fallas",                   
                    Mobile = "(506)-1234-5678",
                    TelephoneNumber = "(506)-2234-5678",
                };

                return userInfo;


            }
            catch (Exception ex)
            {
                throw ex; // Handle exceptions appropriately
            }
        }

        public async Task<OrganizationInfo?> GetOrganizationData(string username)
        {
            try
            {

                var organizationInfo = new OrganizationInfo()
                {
                   Department = "IT",
                   DirectReport = "Mi Manager",
                   Title = "IT Manager",
                };

                return organizationInfo;


            }
            catch (Exception ex)
            {
                throw ex; // Handle exceptions appropriately
            }
        }

        public async Task<AddressInfo?> GetAddressStructureData(string username)
        {
            try
            {

                var addressInfo = new AddressInfo()
                {
                   Country = "CostaRica"
                };

                return addressInfo;


            }
            catch (Exception ex)
            {
                throw ex; // Handle exceptions appropriately
            }
        }
    }
}
