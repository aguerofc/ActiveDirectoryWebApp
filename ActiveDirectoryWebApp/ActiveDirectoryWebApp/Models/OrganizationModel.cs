using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ActiveDirectoryWebApp.Models
{
    public class OrganizationModel

    {
        public string? Department { get; set; }            
        public string? DirectReport{ get; set; }
        public string? Title { get; set; }       

    }
}
