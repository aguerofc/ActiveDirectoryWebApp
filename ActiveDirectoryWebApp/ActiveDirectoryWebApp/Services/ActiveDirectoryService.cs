
using ActiveDirectoryWebApp.Entities;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace ActiveDirectoryWebApp.Services
{
    public class ActiveDirectoryService : IActiveDirectoryService
    {
        private PrincipalContext _context;


        public ActiveDirectoryService(string domain)
        {
            _context = new PrincipalContext(ContextType.Domain, domain);
        }

        public ActiveDirectoryService() { }


        public async Task<GeneralInfo?> GetGeneralData(string username)
        {
            using (var searcher = new DirectorySearcher(_context.ConnectedServer))
            {
                // Define the LDAP filter to find the user by username (samAccountName)
                searcher.Filter = $"(&(objectClass=user)(samAccountName={username}))";

                // Set the properties to retrieve
                searcher.PropertiesToLoad.Add("displayName");
                searcher.PropertiesToLoad.Add("mail");
                searcher.PropertiesToLoad.Add("telephoneNumber");
                // Add more properties as needed

                // Execute the search
                SearchResult result = searcher.FindOne();

                if (result != null)
                {
                    return new GeneralInfo()
                    {
                        DisplayName = GetPropertyValue(result, "displayName"),
                        Mobile = GetPropertyValue(result, "OtherTelephone"),
                        TelephoneNumber = GetPropertyValue(result, "telephoneNumber"),
                        // Add more properties as needed
                    };
                }

                return null; // User not found
            }
        }


        public async Task<OrganizationInfo> GetOrganizationData(string organizationOU)
        {
            using (DirectorySearcher searcher = new DirectorySearcher(_context.ConnectedServer))
            {
                // Define the LDAP filter to target a specific organizational unit (OU)
                searcher.Filter = $"(&(objectCategory=organizationalUnit)(distinguishedName={organizationOU}))";

                // Execute the search
                SearchResult result = searcher.FindOne();

                if (result != null)
                {
                    DirectoryEntry organizationEntry = result.GetDirectoryEntry();
                    return new OrganizationInfo
                    {
                        Department = organizationEntry.Properties["department"].Value?.ToString(),
                        DirectReport = organizationEntry.Properties["directReports"].Value?.ToString(),
                        Title = organizationEntry.Properties["title"].Value?.ToString()
                    };
                }

                return null; // Organization not found
            }
        }

        public async Task<AddressInfo> GetAddressStructureData(string organizationOU)
        {
            using (DirectorySearcher searcher = new DirectorySearcher(_context.ConnectedServer))
            {
                // Define the LDAP filter to target a specific organizational unit (OU)
                searcher.Filter = $"(&(objectCategory=organizationalUnit)(distinguishedName={organizationOU}))";

                // Execute the search
                SearchResult result = searcher.FindOne();

                if (result != null)
                {
                    DirectoryEntry organizationEntry = result.GetDirectoryEntry();
                    return new AddressInfo
                    {
                        //Street = organizationEntry.Properties["street"].Value?.ToString(),
                        //City = organizationEntry.Properties["l"].Value?.ToString(),
                        //State = organizationEntry.Properties["st"].Value?.ToString(),
                        //PostalCode = organizationEntry.Properties["postalCode"].Value?.ToString()
                        Country = organizationEntry.Properties["co"].Value?.ToString()
                    };
                }

                return null; // Organization not found
            }
        }

        private string GetPropertyValue(SearchResult result, string propertyName)
        {
            if (result.Properties.Contains(propertyName))
            {
                return result.Properties[propertyName][0].ToString();
            }

            return null;
        }
    }
}
  

