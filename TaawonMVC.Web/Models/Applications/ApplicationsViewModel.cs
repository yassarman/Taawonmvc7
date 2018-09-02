using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaawonMVC.Applications.DTO;
using TaawonMVC.Buildings.DTO;
using TaawonMVC.InterventionType.DTO;
using TaawonMVC.PropertyOwnership.DTO;

namespace TaawonMVC.Web.Models.Applications
{
public class ApplicationsViewModel
    {
        public IEnumerable<GetApplicationsOutput> Applications { get; set; }
        public string[] fullNameArray { get; set; }
        public GetBuildingsOutput buildingOutput { get; set; }
        public GetApplicationsOutput applicationsOutput { get; set; }
        public SelectList YesOrNo { get; set; }
        public IEnumerable<GetPropertyOwnershipOutput> PropertyOwnerShips { get; set; }
        public IEnumerable<GetInterventionTypeOutput>  InterventionTypes { get; set; }
    }
}