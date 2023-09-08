using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ActiveDirectoryWebApp.Models
{
    public class UserViewModel
    {

        public GeneralModel? GeneralData { get; set; }
        public OrganizationModel? OrganizationData { get; set; }
        public AddressStructureModel? AddressData { get; set; }



    }
}
