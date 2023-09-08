using ActiveDirectoryWebApp.Common;
using ActiveDirectoryWebApp.Models;
using ActiveDirectoryWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Sockets;
using System.Net.WebSockets;

namespace ActiveDirectoryWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ActiveDirectoryService _adService;
        private readonly FeatureFlags _featureFlags;

        public UserController(ActiveDirectoryService adService, FeatureFlags featureFlags)
        {
            _adService = adService;
            _featureFlags = featureFlags;
        }

        public async Task<IActionResult> GetUserLoginInfo()
        {
            UserViewModel userViewModel = new UserViewModel();
            try
            {
                if (_featureFlags.UseActiveDirectory)
                {
                    var userInfo = await _adService.GetGeneralData(User.Identity.Name.Split('\\')[1]);

                    if (userInfo != null)
                    {
                        userViewModel.GeneralData = new GeneralModel()
                        {
                            DisplayName = userInfo.DisplayName,
                            Mobile = userInfo.Mobile,
                            TelephoneNumber = userInfo.TelephoneNumber
                        };
                    }
                    else
                    {
                        return NotFound();
                    }
                    
                    // carga organization
                   
                    var organizationInfo = await _adService.GetOrganizationData(User.Identity.Name.Split('\\')[1]);
                    if (organizationInfo != null)
                    {
                        userViewModel.OrganizationData = new OrganizationModel()
                        {
                            Department = organizationInfo.Department,
                            DirectReport = organizationInfo.DirectReport,
                            Title = organizationInfo.Title
                        };
                    }
                    else
                    {
                        return NotFound();
                    }

                    // carga address
                    var addressInfo = await _adService.GetAddressStructureData(User.Identity.Name.Split('\\')[1]);
                    if (addressInfo != null)
                    {
                        userViewModel.AddressData = new AddressStructureModel()
                        {
                            Country = addressInfo.Country
                        };
                    }
                    else
                    {
                        return NotFound();
                    }

                    return View(userViewModel);                   
                }
                else 
                {
                    var mock = new MockActiveDirectoryService();
                    var userInfo = await mock.GetGeneralData(User.Identity.Name.Split('\\')[1]);

                    if (userInfo != null)
                    {
                        userViewModel.GeneralData = new GeneralModel()
                        {
                            DisplayName = userInfo.DisplayName,
                            Mobile = userInfo.Mobile,
                            TelephoneNumber = userInfo.TelephoneNumber
                        };
                    }
                    else
                    {
                        return NotFound();
                    }

                    // carga organization

                    var organizationInfo = await mock.GetOrganizationData(User.Identity.Name.Split('\\')[1]);
                    if (organizationInfo != null)
                    {
                        userViewModel.OrganizationData = new OrganizationModel()
                        {
                            Department = organizationInfo.Department,
                            DirectReport = organizationInfo.DirectReport,
                            Title = organizationInfo.Title
                        };
                    }
                    else
                    {
                        return NotFound();
                    }

                    // carga address
                    var addressInfo = await mock.GetAddressStructureData(User.Identity.Name.Split('\\')[1]);
                    if (addressInfo != null)
                    {
                        userViewModel.AddressData = new AddressStructureModel()
                        {
                            Country = addressInfo.Country
                        };
                    }
                    else
                    {
                        return NotFound();
                    }

                    return View(userViewModel);
                }
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel
                {
                    RequestId= HttpContext.TraceIdentifier,
                    Message = ex.Message,
                };
                return View("Error", errorViewModel); // StatusCode(500, ex.Message); // Handle exceptions
            }
        }

        [HttpPost]
        public IActionResult EditProfile(UserViewModel userViewModel)
        {

            if (ModelState.IsValid)
            {
                ViewBag.SuccessMessage = "User information saved successfully.";
                // Redirect back to the user profile page
                //return RedirectToAction("GetUserLoginInfo", new { username = User.Identity.Name.Split('\\')[1] });
                return View(userViewModel);
            }
            else 
            {
                return View(userViewModel);
            }
        

        }

        public IActionResult EditProfile(string SerializedModel)
        {

            if (string.IsNullOrEmpty(SerializedModel))
            {
                return NotFound(); // User not found
            }
                // Deserialize the model from JSON format
            var user = Newtonsoft.Json.JsonConvert.DeserializeObject<UserViewModel>(SerializedModel);
            
            return View(user);
        }      
       
    }

}
