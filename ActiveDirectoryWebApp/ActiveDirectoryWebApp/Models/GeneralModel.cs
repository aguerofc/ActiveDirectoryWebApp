using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ActiveDirectoryWebApp.Models
{
    public class GeneralModel
    {

        public string? DisplayName { get; set; }       
        public string? Mobile { get; set; }
        public string? TelephoneNumber { get; set; }

    }
}
