using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ifm360Reports.Controllers
{
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
			ViewBag.Company = companyList;
			return View();
        }

		public JsonResult getUnifromAccetance(string Company)
		{
			var ds = util.Fill("exec Udp_EnployeeUniformDetails @company='"+ Company + "'", util.strElect);
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
            ViewBag.Company = companyList;
            return View();
        }

	
    

    public JsonResult getConsolidatedUniform(string Company)
	{

        var ds = util.Fill("exec Udp_EmployeeConsolidatedUniform @Company='" + Company + "'", util.strElect);
        var dt = ds.Tables[0];

        return Json(JsonConvert.SerializeObject(dt));
    }
    }
}
