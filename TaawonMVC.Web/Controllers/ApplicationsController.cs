using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaawonMVC.Applications;
using TaawonMVC.Applications.DTO;
using TaawonMVC.Buildings;
using TaawonMVC.Buildings.DTO;
using TaawonMVC.BuildingUnits;
using TaawonMVC.InterventionType;
using TaawonMVC.PropertyOwnership;
using TaawonMVC.Web.Models.Applications;

namespace TaawonMVC.Web.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly IApplicationsAppService _applicationsAppService;
        private readonly IBuildingsAppService _buildingsAppService;
        private readonly IBuildingUnitsAppService _buildingUnitsAppService;
        private readonly IPropertyOwnershipAppService _propertyOwnershipAppService;
        private readonly IInterventionTypeAppService _interventionTypeAppService;

        public ApplicationsController(IApplicationsAppService applicationsAppServic,IBuildingsAppService buildingsAppService, IBuildingUnitsAppService buildingUnitsAppService, IPropertyOwnershipAppService propertyOwnershipAppService, IInterventionTypeAppService interventionTypeAppService)
        {
            _applicationsAppService = applicationsAppServic;
            _buildingsAppService = buildingsAppService;
            _buildingUnitsAppService = buildingUnitsAppService;
            _propertyOwnershipAppService = propertyOwnershipAppService;
            _interventionTypeAppService = interventionTypeAppService;

        }
        // GET: Applications
        public ActionResult Index()
        {
            var applications = _applicationsAppService.getAllApplications();
            var applicationsViewModel = new ApplicationsViewModel()
            {
             Applications = applications
            };
            return View("Applications", applicationsViewModel);
        }

        public ActionResult ApplicationForm()
        {
            // get all of intervention types 
            var interventionTypes = _interventionTypeAppService.getAllInterventionTypes();
            // get all applications 
            var applications = _applicationsAppService.getAllApplications().ToList();
            // get all property ownerships 
            var propertyOwnerships = _propertyOwnershipAppService.getAllPropertyOwnerships();
            // populate yes no drop down list 
            var yesOrNo = new List<string>
            {
                "True",
                "False"
            };
            var fullNameList = new List<string>();
            foreach(var application in applications)
            {
                if (!String.IsNullOrWhiteSpace(application.fullName))
                {
                    fullNameList.Add(application.fullName);
                }
            }
            var fullNameArray = fullNameList.Distinct().ToArray();
            var applicationsViewModel = new ApplicationsViewModel()
            {
                fullNameArray = fullNameArray,
                buildingOutput = new GetBuildingsOutput(),
                PropertyOwnerShips= propertyOwnerships,
                YesOrNo= new SelectList(yesOrNo),
                InterventionTypes= interventionTypes

            };

            return View("ApplicationForm", applicationsViewModel);
        }

        public ActionResult PopulateApplicationForm(int buildingId)
        { 
            
            
            //instantiate object GetBuidlingsInput to get the building entity with given id 
            var getBuildingInput = new GetBuidlingsInput()
            {
              Id= buildingId
            };
            // retrieve the building with givin id 
            var building = _buildingsAppService.getBuildingsById(getBuildingInput);
            // declare viewmodel object to pass data to view 
            var applicationViewModel = new ApplicationsViewModel()
            {
                 buildingOutput = building
            };

             return Json(applicationViewModel, JsonRequestBehavior.AllowGet);
          //  return View("ApplicationForm", applicationViewModel);
        }

        public ActionResult CreateApplication(CreateApplicationsInput model )
        {
            var application = new CreateApplicationsInput();
            application.fullName = model.fullName;
             application.isThereFundingOrPreviousRestoration = model.isThereFundingOrPreviousRestoration;
            var YourRadioButton = Request["RB1"];

            return null;

        }
    }
}