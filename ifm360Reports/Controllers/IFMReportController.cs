using DocumentFormat.OpenXml.Spreadsheet;
using ifm360Reports.AuthFilter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ifm360Reports.Controllers
{
	[AuthenticationFilter]
    public class IFMReportController : Controller
    {

		db_Utility util = new db_Utility();
		ClsUtility utility = new ClsUtility();
		#region Uniform Accepten Report

		public IActionResult UniformAccpten()
        {
			var companyListJson = HttpContext.Session.GetString("CompanyList");
			List<SelectListItem> companyList = null;
			if (!string.IsNullOrEmpty(companyListJson))
			{
				companyList = JsonConvert.DeserializeObject<List<SelectListItem>>(companyListJson);
			}
			ViewBag.Company = util.PopulateDropDown("Usp_GroupLNewAppDLL 'CompanyLogin',@Id='" + HttpContext.Session.GetString("UserName") + "'", util.strElect); 
			return View();
        }

		public JsonResult getUnifromAccetance(string Company,string Region,string Branch)
		{
			var ds = util.Fill("exec Udp_EnployeeUniformDetails @company='"+ Company + "',@Region='"+Region+"',@Branch='"+Branch+"'", util.strElect);
			var dt = ds.Tables[0];
			
			return Json(JsonConvert.SerializeObject(dt));
		}
		public JsonResult getUnifromDetails(string LocId,string EmpCode)
		{
			var ds = util.Fill("exec Udp_EnployeeUniform_ItemDetails @Emp_Code='" + EmpCode + "'", util.strElect);
			var dt = ds.Tables[0];
			
			return Json(JsonConvert.SerializeObject(dt));
		}

	#endregion

	public IActionResult ConsolidatedUniform()
	{
            var companyListJson = HttpContext.Session.GetString("CompanyList");
            List<SelectListItem> companyList = null;
            if (!string.IsNullOrEmpty(companyListJson))
            {
                companyList = JsonConvert.DeserializeObject<List<SelectListItem>>(companyListJson);
            }
            ViewBag.Company = util.PopulateDropDown("Usp_GroupLNewAppDLL 'CompanyLogin',@Id='" + HttpContext.Session.GetString("UserName") + "'", util.strElect); 
            return View();
    }

    public JsonResult getConsolidatedUniform(string Company, string Region, string Branch)
	{

        var ds = util.Fill("exec Udp_EmployeeConsolidatedUniform @Company='" + Company + "',@UserId='"+ HttpContext.Session.GetString("UserName") + "',@Region='"+Region+"',@Branch='"+Branch+"'", util.strElect);
        var dt = ds.Tables[0];

        return Json(JsonConvert.SerializeObject(dt));
    }
    }
}
