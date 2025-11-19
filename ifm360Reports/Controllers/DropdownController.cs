using DocumentFormat.OpenXml.Office2010.Excel;
using ifm360Reports.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ifm360Reports.Controllers
{
    public class DropdownController : Controller
    {
        protected string UserId;
        private readonly string companyid;
        private readonly string locationid;
        private readonly string branchid;
        db_Utility util = new db_Utility();
        ResponseMessage _response = new ResponseMessage();
        public DropdownController( IHttpContextAccessor httpContextAccessor )
        {
         
            UserId = httpContextAccessor.HttpContext.Session.GetString("UserName");
            companyid = httpContextAccessor.HttpContext.Session.GetString("companyid");
            locationid = httpContextAccessor.HttpContext.Session.GetString("locationid");
            branchid = httpContextAccessor.HttpContext.Session.GetString("branchid");
        
        }


        public ResponseMessage GetClientbyTask(string Date)
        {


            var data = util.PopulateDropDown(@$"exec Usp_GroupLNewAppDLL 'Customerbytask',@Id2='{branchid}', @Id = '{UserId}',@Id3='{Date}'", util.strElect);

            if (data.Count > 0)
            {
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                _response.Message = "Clients retrieved successfully.";
                _response.Data = data;
            }
            else
            {
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.Message = "Clients Not Found !";
                _response.Data = data;

            }




                return _response;
        }

        public ResponseMessage ChecklistHeader(string Id)
        {


            var data = util.PopulateDropDown(@$"exec Usp_GroupLNewAppDLL 'ChecklistHeader', @Id= '{Id}'", util.strElect);

            if (data.Count > 0)
            {
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                _response.Message = "ChecklistHeader retrieved successfully.";
                _response.Data = data;
            }
            else
            {
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.Message = "ChecklistHeader Not Found !";
                _response.Data = data;

            }




                return _response;
        }

        public ResponseMessage GetSitebyTask(string Date,string Id)
        {


            var data = util.PopulateDropDown(@$"exec Usp_GroupLNewAppDLL 'Sitebytask',@Id2='{branchid}', @Id = '{UserId}',@Id3='{Date}',@Id4='{Id}'", util.strElect);

            _response.StatusCode = System.Net.HttpStatusCode.OK;
            _response.Message = "Clients retrieved successfully.";
            _response.Data = data;


            return _response;
        }

        public ResponseMessage GetBranchByRegion(string Id)
        {

            var data = util.PopulateDropDown("exec udp_GetReportPortalBranch @CompanyCode='" + companyid + "', @HrLocationCode='" + Id + "' ", util.strElect); 
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            _response.Message = "Clients retrieved successfully.";
            _response.Data = data;
            return _response;
        }

    }
}
