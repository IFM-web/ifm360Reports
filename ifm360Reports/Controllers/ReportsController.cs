using DocumentFormat.OpenXml.Bibliography;
using ifm360Reports.AuthFilter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Operations;
using Newtonsoft.Json;
using NuGet.DependencyResolver;
using System.Data;
using System.Security.Policy;
using System.Text;


namespace ifm360Reports.Controllers
{
    [AuthenticationFilter]
    public class ReportsController : Controller
	{
		db_Utility util = new db_Utility();
		ClsUtility utility = new ClsUtility();
       protected string Customer = string.Empty;
       protected int Customerlevel;
       protected string UserId;

        public ReportsController(IHttpContextAccessor httpContextAccessor)
        {
           Customer= httpContextAccessor.HttpContext.Session.GetString("Customer");
           UserId= httpContextAccessor.HttpContext.Session.GetString("UserName");
           Customerlevel= Convert.ToInt16( httpContextAccessor.HttpContext.Session.GetString("CustomerLevel"));
        }


     
		public IActionResult PhotoAttendance()

		{
            

           
            var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");
			//var uname = HttpContext.Session.GetString("UserName").ToUpper();

            if (HttpContext.Session.GetString("UserName").ToUpper() == "HDBADMIN" && HttpContext.Session.GetString("password").ToUpper() == "ADMIN@123")
			{
                List<SelectListItem> ddl = new List<SelectListItem>();
                ddl.Add(new SelectListItem
                {
                    Value = "GL0072",
                    Text = "Hdb Financial Services Limited"
                });

                List<SelectListItem> ddl2 = new List<SelectListItem>();
                ddl2.Add(new SelectListItem
                {
                    Value = "North",
                    Text = "North"
                });
                ViewBag.client = ddl;
                ViewBag.Region = ddl2;
               // ViewBag.Region = util.PopulateDropDown("exec udp_GetRegion @CompanyCode='" + companyid + "'", util.strElect);
                ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);

            }
			else
			{
                ViewBag.client = CustomerDropDown();


                ViewBag.Region = RegionDropDown();
                ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);
            }
			
			

			

			return View();
		}


     
        public IActionResult PhotoAttendanceNew()

        {


            var companyid = HttpContext.Session.GetString("companyid");
            var locationid = HttpContext.Session.GetString("locationid");
            var branchid = HttpContext.Session.GetString("branchid");
            //var uname = HttpContext.Session.GetString("UserName").ToUpper();

            if (HttpContext.Session.GetString("UserName").ToUpper() == "HDBADMIN" && HttpContext.Session.GetString("password").ToUpper() == "ADMIN@123")
            {
                List<SelectListItem> ddl = new List<SelectListItem>();
                ddl.Add(new SelectListItem
                {
                    Value = "GL0072",
                    Text = "Hdb Financial Services Limited"
                });

                List<SelectListItem> ddl2 = new List<SelectListItem>();
                ddl2.Add(new SelectListItem
                {
                    Value = "North",
                    Text = "North"
                });
                ViewBag.client = ddl;
                ViewBag.Region = ddl2;
                // ViewBag.Region = util.PopulateDropDown("exec udp_GetRegion @CompanyCode='" + companyid + "'", util.strElect);
                ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);

            }
            else
            {
               // ViewBag.client = CustomerDropDown();


                ViewBag.client = util.PopulateDropDown("exec Usp_GroupLNewAppDLL 'CustomerMap', @Id = '" +UserId + "'", util.strElect);
                ViewBag.Region = util.PopulateDropDown("exec Usp_GroupLNewAppDLL 'RegionMap', @Id = '" + UserId + "'", util.strElect);
                ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);
            }





            return View();
        }
     
        public IActionResult AttendanceSuper()
		{

           
            var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");

            if (HttpContext.Session.GetString("UserName").ToUpper() == "HDBADMIN" && HttpContext.Session.GetString("password").ToUpper() == "ADMIN@123")
            {
                List<SelectListItem> ddl = new List<SelectListItem>();
                ddl.Add(new SelectListItem
                {
                    Value = "GL0072",
                    Text = "Hdb Financial Services Limited"
                });
                ViewBag.client = ddl;
                ViewBag.Region = util.PopulateDropDown("exec udp_GetRegion @CompanyCode='" + companyid + "'", util.strElect);
                ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);

            }
            else
            {
                ViewBag.client = CustomerDropDown();
                ViewBag.Region = util.PopulateDropDown("exec udp_GetRegion @CompanyCode='" + companyid + "'", util.strElect);
                ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);
            }
            

			return View();
		}

     
        public IActionResult AttendanceSuperNew()
        {

            var companyid = HttpContext.Session.GetString("companyid");
            var locationid = HttpContext.Session.GetString("locationid");
            var branchid = HttpContext.Session.GetString("branchid");

            
                ViewBag.Region = util.PopulateDropDown("exec 'CustomerMap' @CompanyCode='" + companyid + "'", util.strElect);

                ViewBag.client = util.PopulateDropDown("exec Usp_GroupLNewAppDLL 'CustomerMap', @Id = '" + HttpContext.Session.GetString("UserName") + "'", util.strElect);
                ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);
            


            return View();
        }
     
        public IActionResult AttendanceSuperWithOutImage()
        {

            var companyid = HttpContext.Session.GetString("companyid");
            var locationid = HttpContext.Session.GetString("locationid");
            var branchid = HttpContext.Session.GetString("branchid");

            if (HttpContext.Session.GetString("UserName").ToUpper() == "HDBADMIN" && HttpContext.Session.GetString("password").ToUpper() == "ADMIN@123")
            {
                List<SelectListItem> ddl = new List<SelectListItem>();
                ddl.Add(new SelectListItem
                {
                    Value = "GL0072",
                    Text = "Hdb Financial Services Limited"
                });
                ViewBag.client = ddl;
                ViewBag.Region = util.PopulateDropDown("exec udp_GetRegion @CompanyCode='" + companyid + "'", util.strElect);
                ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);

            }
            else
            {
                ViewBag.client = CustomerDropDown();
                ViewBag.Region = util.PopulateDropDown("exec 'CustomerMap' @CompanyCode='" + companyid + "'", util.strElect);

                
                ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);
            }


            return View();
        }
        
     
        public IActionResult AttendanceSuperWithOutImageNew()
        {

            var companyid = HttpContext.Session.GetString("companyid");
            var locationid = HttpContext.Session.GetString("locationid");
            var branchid = HttpContext.Session.GetString("branchid");

          
                //ViewBag.client = CustomerDropDown();
                ViewBag.Region = util.PopulateDropDown("exec 'CustomerMap' @CompanyCode='" + companyid + "'", util.strElect);

                ViewBag.client = util.PopulateDropDown("exec Usp_GroupLNewAppDLL 'CustomerMap', @Id = '" + UserId + "'", util.strElect);
                ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);
            


            return View();
        }

     
        public IActionResult VisitReport(string date, string client, string AsmtId, string vtype, string empid,string LocautoId)
		{
			var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");
			DataTable dt = new DataTable();
			DataTable dt1 = new DataTable();
			DataTable dt2 = new DataTable();

			var ds = util.Fill("exec udp_GetVisitReportUpdated @LocationAutoID ='" + LocautoId + "',@FromDate='" + date + "',@AsmtID='" + AsmtId + "',@ClientCode='" + client + "',@VisitType='" + vtype + "',@Empid='" + empid + "'", util.strElect);
			ViewBag.dt = ds.Tables[0];
			ViewBag.dt1 = ds.Tables[1];
			ViewBag.dt2 = ds.Tables[2];

			return View();
		}

     
        public IActionResult VisittopReport(string date, string client, string AsmtId, string vtype, string empid, string LocautoId)
        {
            var companyid = HttpContext.Session.GetString("companyid");
            var locationid = HttpContext.Session.GetString("locationid");
            var branchid = HttpContext.Session.GetString("branchid");
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            var ds = util.Fill("exec udp_GetVisitReportUpdated @LocationAutoID ='" + LocautoId + "',@FromDate='" + date + "',@AsmtID='" + AsmtId + "',@ClientCode='" + client + "',@VisitType='" + vtype + "',@Empid='" + empid + "'", util.strElect);
            ViewBag.dt = ds.Tables[0];
            ViewBag.dt1 = ds.Tables[1];
            ViewBag.dt2 = ds.Tables[2];

            return View();
        }
     
        public IActionResult ConsolidatedVisitReport()
		{
          
            var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");

            if (HttpContext.Session.GetString("UserName").ToUpper() == "HDBADMIN" && HttpContext.Session.GetString("password").ToUpper() == "ADMIN@123")
            {
                List<SelectListItem> ddl = new List<SelectListItem>();
                ddl.Add(new SelectListItem
                {
                    Value = "GL0072",
                    Text = "Hdb Financial Services Limited"
                });
                List<SelectListItem> ddl2 = new List<SelectListItem>();
                ddl2.Add(new SelectListItem
                {
                    Value = "North",
                    Text = "North"
                });
                ViewBag.customer = ddl;
                ViewBag.Region = ddl2;

            }
			else
			{
                //ViewBag.customer = util.PopulateDropDown("exec udpMstSale_Client_Get @LocationAutoID ='" + branchid + "'", util.strElect);
                ViewBag.customer = CustomerDropDown();

                ViewBag.Region = RegionDropDown();
            }

           
           
			return View();
		}
     
        public IActionResult ConsolidatedVisittoReport()
        {
            
            var companyid = HttpContext.Session.GetString("companyid");
            var locationid = HttpContext.Session.GetString("locationid");
            var branchid = HttpContext.Session.GetString("branchid");

            if (HttpContext.Session.GetString("UserName").ToUpper() == "HDBADMIN" && HttpContext.Session.GetString("password").ToUpper() == "ADMIN@123")
            {
                List<SelectListItem> ddl = new List<SelectListItem>();
                ddl.Add(new SelectListItem
                {
                    Value = "GL0072",
                    Text = "Hdb Financial Services Limited"
                });
                List<SelectListItem> ddl2 = new List<SelectListItem>();
                ddl2.Add(new SelectListItem
                {
                    Value = "North",
                    Text = "North"
                });
                ViewBag.customer = ddl;
                ViewBag.Region = ddl2;

            }
            else
            {
                ViewBag.customer = util.PopulateDropDown("exec udpMstSale_Client_Get @LocationAutoID ='" + branchid + "'", util.strElect);

                ViewBag.Region = util.PopulateDropDown("exec udp_GetRegion @CompanyCode='" + companyid + "'", util.strElect);
            }



            return View();
        }


        public JsonResult GetConsolidatedVisitReport(string todate, string fromdate, string clientcode ,string Regid)
		{

			DataTable dt = new DataTable();
			var branchid = HttpContext.Session.GetString("branchid");
			var ds = util.Fill("exec udp_GetCustomerFeedback2 @LocationAutoId ='" + branchid + "',@FromDate='" + fromdate + "',@ToDate='" + todate + "',@ClientCode='" + clientcode + "', @Region='"+Regid+"',@UserId='"+UserId+"' ", util.strElect);

			dt = ds.Tables[0];

			return Json(JsonConvert.SerializeObject(dt));
		}



		[HttpPost]
		public JsonResult GetSearchPhotoAttendance(string todate, string Region, string Empno, string client, string shift,string fromdate)
		{
			var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");
			var UserName = HttpContext.Session.GetString("UserName");
			string div = "";
			DataTable dt = new DataTable();

			var ds = util.Fill("exec udp_GetEmployeePhotoAttendanceGroupL2 @LocationAutoId ='" + branchid + "',@FromDate='" + fromdate + "',@EmployeeNumber='" + Empno + "',@CompanyCode='" + companyid + "',@Region='" + Region + "',@ClientCode='" + client + "',@Shift='" + shift + "',@ToDate='" + todate + "' ,@UserId='"+ UserName + "'", util.strElect);
			dt = ds.Tables[0];

			return Json(JsonConvert.SerializeObject(dt));
		}

		[HttpPost]

		public JsonResult GetSearchAttendanceSuper(string date, string site, string Empno, string client, string shift,string fromdate)
		{


			var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");

			string div = "";
			DataTable dt = new DataTable();


			var ds = util.Fill("exec udp_GetEmployeeSuperAttendanceGroupL @LocationAutoId ='" + branchid + "',@FromDate='" + fromdate + "',@EmployeeNumber='" + Empno + "',@AsmtCode='" + site + "',@ClientCode='" + client + "',@ShiftCode='" + shift + "',@ToDate='" + date + "' ", util.strElect);
			dt = ds.Tables[0];
			
			return Json(JsonConvert.SerializeObject(dt));
		}

		[HttpPost]

		public JsonResult GetSearchAttendanceSuperwithoutImage(string date, string site, string Empno, string client, string shift, string fromdate)
		{


			var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");
			var UserName = HttpContext.Session.GetString("UserName");

			string div = "";
			DataTable dt = new DataTable();


			var ds = util.Fill("exec udp_GetEmployeeSuperAttendanceGroupL_WithoutImage @LocationAutoId ='" + branchid + "',@FromDate='" + fromdate + "',@EmployeeNumber='" + Empno + "',@AsmtCode='" + site + "',@ClientCode='" + client + "',@ShiftCode='" + shift + "',@ToDate='" + date + "',@UserId='"+ UserName + "' ", util.strElect);
			dt = ds.Tables[0];

			return Json(JsonConvert.SerializeObject(dt));
		}
		[HttpPost]

		public JsonResult bindSite(string siteid)
		{
            
            var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");
			DataSet ds = util.Fill("exec udp_GetSiteListGroupLNew @LocationAutoID ='" + branchid + "',@BaseCompanyCode ='" + companyid + "',@ClientCode='" + siteid + "' ", util.strElect);
			DataTable dt = ds.Tables[0];
			var data = JsonConvert.SerializeObject(dt);
			return Json(data);
		}


		[HttpPost]

		public JsonResult bindclient(string custid)
		{
			var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");


            if (HttpContext.Session.GetString("UserName").ToUpper() == "HDBADMIN" && HttpContext.Session.GetString("password").ToUpper() == "ADMIN@123")
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("ClientCode", typeof(string));
                dt.Columns.Add("ClientName", typeof(string));

                dt.Rows.Add("GL0072", "Hdb Financial Services Limited");

                var data = JsonConvert.SerializeObject(dt);
                return Json(data);

            }
            else
			{
                DataSet ds = util.Fill("exec udpMstSale_Client_Asmt_Get @LocationAutoID ='" + branchid + "',@ClientCode ='" + custid + "'", util.strElect);
                DataTable dt = ds.Tables[0];
                var data = JsonConvert.SerializeObject(dt);

                return Json(data);
            }
          
		}
		[HttpPost]
		public JsonResult bindvisittype(string custid)
		{
			var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");
			DataSet ds = util.Fill("exec udpGetVisitType @LocationAutoID ='" + branchid + "',@ClientCode ='" + custid + "'", util.strElect);
			DataTable dt = ds.Tables[0];
			var data = JsonConvert.SerializeObject(dt);
			return Json(data);
		}


		[HttpPost]
		public JsonResult GetVisitReport(string date, string custid, string client, string visittype, string EmpId)
		{


			var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");

			string div = "";
			DataTable dt = new DataTable();
			DataTable dt1 = new DataTable();
			DataTable dt2 = new DataTable();


			var ds = util.Fill("exec udp_GetVisitReportUpdated @LocationAutoID ='" + branchid + "',@FromDate='" + date + "',@AsmtID='" + client + "',@ClientCode='" + custid + "',@VisitType='" + visittype + "',@Empid='" + EmpId + "' ", util.strElect);
			dt = ds.Tables[0];
			dt1 = ds.Tables[1];
			dt2 = ds.Tables[2];
			var data = new { dt = JsonConvert.SerializeObject(dt), dt1 = JsonConvert.SerializeObject(dt1), dt2 = JsonConvert.SerializeObject(dt2) };

			return Json(data);
		}

        #region Shortage Report 
     
        public IActionResult ShortageReport()
		{
          
            var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");
			ViewBag.Region = RegionDropDown();

            if (HttpContext.Session.GetString("UserName").ToUpper() == "HDBADMIN" && HttpContext.Session.GetString("password").ToUpper() == "ADMIN@123")
            {
                List<SelectListItem> ddl = new List<SelectListItem>();
                ddl.Add(new SelectListItem
                {
                    Value = "GL0072",
                    Text = "Hdb Financial Services Limited"
                });
                ViewBag.client = ddl;


			}
			else
            {
                ViewBag.client = CustomerDropDown();

            }
        

			return View();
		}
		public JsonResult BindBranchtoReg(string regid)
		{
			var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");

            if (HttpContext.Session.GetString("UserName").ToUpper() == "HDBADMIN" && HttpContext.Session.GetString("password").ToUpper() == "ADMIN@123")
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("LocationAutoID", typeof(string));
                dt.Columns.Add("LocationCode", typeof(string));

                dt.Rows.Add("20909", "Lucknow");

                var data = JsonConvert.SerializeObject(dt);
                return Json(data);

            }
			else {

                DataSet ds = util.Fill("exec Usp_GroupLNewAppDLL 'GetBranchFromRegion', @Id2 ='" + companyid + "',@Id3 ='" + regid + "',@Id='"+UserId +"'", util.strElect);
                DataTable dt = ds.Tables[0];
                var data = JsonConvert.SerializeObject(dt);
                return Json(data);
            }
		}
		public JsonResult BindSitetoClient(string clietnid)
		{
			var companyid = HttpContext.Session.GetString("companyid");
			var locationid = HttpContext.Session.GetString("locationid");
			var branchid = HttpContext.Session.GetString("branchid");
			DataSet ds = util.Fill("exec udp_GetSiteListGroupLNew @BaseCompanyCode ='" + companyid + "',@ClientCode ='" + clietnid + "', @LocationAutoID='" + branchid + "'", util.strElect);
			DataTable dt = ds.Tables[0];
			var data = JsonConvert.SerializeObject(dt);
			return Json(data);
		}
		[HttpPost]
		public JsonResult GetSearchShortageReport(string date, string Regionid, string branchid, string emptype, string clientcode, string sitecode)
		{
			var companyid = HttpContext.Session.GetString("companyid");
			//var locationid = HttpContext.Session.GetString("locationid");

			DataSet ds = util.Fill("exec udp_GetShortageReportGrouplNewApp2 @ComanyCode ='" + companyid + "',@DDate ='" + date + "', @Region='" + Regionid + "', @Branch='" + branchid + "',@employeetype='" + emptype + "',@ClientCode='" + clientcode + "',@SiteCode='" + sitecode + "',@UserId='"+UserId+"'", util.strElect);
			DataTable dt = ds.Tables[0];
			DataTable dt1 = ds.Tables[1];
			DataTable dt2 = ds.Tables[2];
			DataTable dt3 = ds.Tables[3];
			DataTable dt4 = ds.Tables[4];
			DataTable dt5 = ds.Tables[5];
			DataTable dt6 = ds.Tables[6];
			DataTable dt7 = ds.Tables[7];
			var data = new
			{
				dt = JsonConvert.SerializeObject(dt),
				dt1 = JsonConvert.SerializeObject(dt1),
				dt2 = JsonConvert.SerializeObject(dt2),
				dt3 = JsonConvert.SerializeObject(dt3),
				dt4 = JsonConvert.SerializeObject(dt4),
				dt5 = JsonConvert.SerializeObject(dt5),
				dt6 = JsonConvert.SerializeObject(dt6),
				dt7 = JsonConvert.SerializeObject(dt7)

			};

			return Json(data);
		}

        #endregion

        #region Shortage Report Client Wise
     
        public IActionResult ShortageReportClientWise()
		{
            
            var companyid = HttpContext.Session.GetString("companyid");
            var locationid = HttpContext.Session.GetString("locationid");
            var branchid = HttpContext.Session.GetString("branchid");
            if (HttpContext.Session.GetString("UserName").ToUpper() == "HDBADMIN" && HttpContext.Session.GetString("password").ToUpper() == "ADMIN@123")
            {
                List<SelectListItem> ddl = new List<SelectListItem>();
                ddl.Add(new SelectListItem
                {
                    Value = "GL0072",
                    Text = "Hdb Financial Services Limited"
                });
                ViewBag.client = ddl;
                ViewBag.Region = util.PopulateDropDown("exec udp_GetRegion @CompanyCode='" + companyid + "'", util.strElect);
                ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);

            }
            else
            {
                ViewBag.client = CustomerDropDown();
                ViewBag.Region = RegionDropDown();
                ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" + branchid + "'", util.strElect);
            }
            return View();
		}

		public JsonResult GetSearchShortageReportclientWise(string date, string Regionid, string Clientid, string shiftid )
        {
            var companyid = HttpContext.Session.GetString("companyid");
            DataSet ds = util.Fill("exec udp_ShortageReportClientWise4 @CompanyCode ='" + companyid + "',@Date ='" + date + "', @Region='" + Regionid + "',@ClientCode='" + Clientid + "',@Shift='" + shiftid + "',@UserId='"+UserId+"'", util.strElect);

            DataTable dt = ds.Tables[0];
            DataTable dt1 = ds.Tables[1];
            DataTable dt2 = ds.Tables[2];
            DataTable dt3 = ds.Tables[3];

			var data = new
			{
				dt = JsonConvert.SerializeObject(dt),
				dt1 = JsonConvert.SerializeObject(dt1),
				dt2 = JsonConvert.SerializeObject(dt2),
				dt3 = JsonConvert.SerializeObject(dt3)
			};

            
            return Json(data);
        }
        #endregion

        #region Out Of Range
     
        public IActionResult OutOfRange()
		{
            
            var companyid = HttpContext.Session.GetString("companyid");
            var locationid = HttpContext.Session.GetString("locationid");
            var branchid = HttpContext.Session.GetString("branchid");
            ViewBag.Region = RegionDropDown();
          
            return View();
		}

		[HttpPost]
		public  JsonResult bindclienttobranch(string branchid)
		{
            var companyid = HttpContext.Session.GetString("companyid");
            var locationid = HttpContext.Session.GetString("locationid");
       
            if(branchid== "20909")
            {

                DataTable dt = new DataTable();

                dt.Columns.Add("ClientCode", typeof(string));
                dt.Columns.Add("ClientName", typeof(string));

                dt.Rows.Add("GL0072", "Hdb Financial Services Limited");

                var data = JsonConvert.SerializeObject(dt);
                return Json(data);
            }
            else
            {
                DataSet ds = util.Fill("exec [Usp_GroupLNewAppDLL] 'GetClientFromBranch', @Id2 ='" + branchid + "',@Id3 ='" + companyid + "',@Id='"+UserId+"' ", util.strElect);
                DataTable dt = ds.Tables[0];
                var data = JsonConvert.SerializeObject(dt);
                return Json(data);

            }
          
        }

		public JsonResult SearchGetOutOfRange(string fromdate,string todate,string Region, string Branch,string client ,string site)
		{
            var companyid = HttpContext.Session.GetString("companyid");
            var locationid = HttpContext.Session.GetString("locationid");
            var branchid = HttpContext.Session.GetString("branchid");
            string div = "";
            DataTable dt = new DataTable();
            var ds = util.Fill("exec udp_GetOutOfRangeReportGrouplNewApp2 @ComanyCode ='" + companyid + "',@DDate='" + fromdate + "',@Region='" + Region + "',@Branch='" + Branch + "',@ClientCode='" + client + "',@SiteCode='" + site + "',@ToDate='" + todate + "',@UserId='"+UserId+"' ", util.strElect);
            dt = ds.Tables[0];
           
            return Json(JsonConvert.SerializeObject(dt));
        }


        public JsonResult bindSiteToOutrange(string siteid)
        {

            var companyid = HttpContext.Session.GetString("companyid");
            var locationid = HttpContext.Session.GetString("locationid");
            var branchid = HttpContext.Session.GetString("branchid");
            DataSet ds = util.Fill("exec udp_GetSiteListGroupLNew @LocationAutoID ='All',@BaseCompanyCode ='" + companyid + "',@ClientCode='" + siteid + "' ", util.strElect);
            DataTable dt = ds.Tables[0];
            var data = JsonConvert.SerializeObject(dt);
            return Json(data);
        }








        #endregion
        #region AuditStatusReport Report
     
        public IActionResult AuditStatusReport()
        {
         
            var branchid = HttpContext.Session.GetString("branchid");
            ViewBag.customer = CustomerDropDown();

            return View();
		}

        public JsonResult GetAudioStatusReport(string todate, string fromdate, string clientcode,string Regid)
        {
            var branchid = HttpContext.Session.GetString("branchid");

            string div = "";
            DataTable dt = new DataTable();
            

            var ds = util.Fill("exec udp_GetAuditReportWithFilters2 @FromDate='" + fromdate + "',@ToDate='" + todate + "', @LocationAutoId ='" + branchid + "',@ClientCode='" + clientcode + "',@Region='All' ,@UserId='"+UserId+"' ", util.strElect);
            dt = ds.Tables[0];
            return Json(JsonConvert.SerializeObject(dt));
        }


        #endregion

        public JsonResult GetAppointmentLetterAcceptance(string CompanyCodeid)
        {

            var ds = util.Fill("udp_GetAppointmentLetterAccepted2 @CompanyCode ='" + CompanyCodeid + "',@UserId='"+UserId+"' ", util.strElect);
            DataTable dt = ds.Tables[0];
            return Json(JsonConvert.SerializeObject(dt));
        }
     
        public IActionResult AppointmentLetterAcceptance(DataSet data)
        {
            ViewBag.Region = util.PopulateDropDown("exec udp_GetReportPortalCompany", util.strElect);
            var companyListJson = HttpContext.Session.GetString("CompanyList");
            List<SelectListItem> companyList = null;
            if (!string.IsNullOrEmpty(companyListJson))
            {
                companyList = JsonConvert.DeserializeObject<List<SelectListItem>>(companyListJson);
            }
            ViewBag.Company = util.PopulateDropDown("Usp_GroupLNewAppDLL 'CompanyLogin',@Id='" + UserId + "'", util.strElect); ;
            return View();
        }

        #region Leave Date Report
     
        public IActionResult LeaveDate()
        {
            return View();
        }

        public JsonResult GetLeaveDateData(string Year,string Month)
        {
            var ds = util.Fill("exec udp_GetGroupLLeaveData @Year ='" + Year + "',@Month='"+Month+"' ", util.strElect);
            DataTable dt = ds.Tables[0];
            return Json(JsonConvert.SerializeObject(dt));
        }
        [HttpPost]
        public JsonResult DeleteAndstatusUpdate(string type, int AutoId,int status)
        {
            string Message = string.Empty;
            if(type == "StatusUpdate")
            {
                string Mes = util.execQuery("Update GroupLNewAppLeaveMaster set ApprovalStatus='"+status+"'  where AutoId='" + AutoId+"'", util.strElect);
                if (Mes == "Successfull")
                {
                    Message = "Leave Record Status Updated Successfully";
                }
                else
                {
                    Message = "Error in Updating Leave Record Status";
                }

            }
            else if(type == "Delete")
            {
                string Mes = util.execQuery("Delete from GroupLNewAppLeaveMaster where AutoId='"+AutoId+"'", util.strElect);

                if (Mes == "Successfull")
                {
                    Message= "Leave Record Deleted Successfully";
                }
               else
                {
                    Message = "Error in Deleting Leave Record";
                }


            }
            return Json(JsonConvert.SerializeObject(new { Message = Message }));
        }


        #endregion


        #region Regularization Report
     
        public IActionResult Regularization()
        {
            return View();
        }

        public JsonResult GetRegularization(string Year, string Month)
        {
            var ds = util.Fill("exec udp_GetGroupLRegualrizationData @Year ='" + Year + "',@Month='" + Month + "' ", util.strElect);
            DataTable dt = ds.Tables[0];
            return Json(JsonConvert.SerializeObject(dt));
        }
        [HttpPost]
        public JsonResult RegularizationDeleteAndstatusUpdate(string type, int AutoId, int status)
        {
            string Message = string.Empty;
            if (type == "StatusUpdate")
            {
                string Mes = "";
                if (status == 1)
                {
                     Mes = util.execQuery("Update GroupLNewAppAttendanceRegularization set ApprovalStatus='" + status + "'  ,ApprovedByID='"+HttpContext.Session.GetString("UserName") +"',ApprovedByName='" + HttpContext.Session.GetString("UserName") +"',ApprovalDate=getdate() where AutoId='" + AutoId + "'", util.strElect);
                }
                else
                {
                    Mes = util.execQuery("Update GroupLNewAppAttendanceRegularization set ApprovalStatus='" + status + "'  ,ApprovedByID='',ApprovedByName='',ApprovalDate=NULL where AutoId='" + AutoId + "'", util.strElect);
                }
               
                if (Mes == "Successfull")
                {
                    Message = "Regularization  Record Status Updated Successfully";
                }
                else
                {
                    Message = "Error in Updating Regularization  Record Status";
                }

            }
            else if (type == "Delete")
            {
                string Mes = util.execQuery("Delete from GroupLNewAppAttendanceRegularization where AutoId='" + AutoId + "'", util.strElect);

                if (Mes == "Successfull")
                {
                    Message = "Regularization  Record Deleted Successfully";
                }
                else
                {
                    Message = "Error in Deleting Regularization  Record";
                }


            }
            return Json(JsonConvert.SerializeObject(new { Message = Message }));
        }

        #endregion

        #region MonthlyOutDuty
        public IActionResult MonthlyOutDuty()
        {
            return View();
        }

        public JsonResult GetMonthlyOutDuty(string Year, string Month)
        {
            var ds = util.Fill("exec udp_GetGroupLOutDutyData @Year ='" + Year + "',@Month='" + Month + "' ", util.strElect);
            DataTable dt = ds.Tables[0];
            return Json(JsonConvert.SerializeObject(dt));
        }


        [HttpPost]
        public JsonResult MonthlyOutDutyDeleteAndstatusUpdate(string type, int AutoId, int status)
        {
            string Message = string.Empty;
            if (type == "StatusUpdate")
            {
                string Mes = "";
                if (status == 1)
                {
                    Mes = util.execQuery("Update GroupLNewAppOutDuty set ApprovalStatus='" + status + "'  ,ApprovedByID='" + HttpContext.Session.GetString("UserName") + "',ApprovedByName='" + HttpContext.Session.GetString("UserName") + "',ApprovalDate=getdate() where AutoId='" + AutoId + "'", util.strElect);
                }
                else
                {
                    Mes = util.execQuery("Update GroupLNewAppOutDuty set ApprovalStatus='" + status + "'  ,ApprovedByID='',ApprovedByName='',ApprovalDate=NULL where AutoId='" + AutoId + "'", util.strElect);
                }

                if (Mes == "Successfull")
                {
                    Message = "Monthly Out Duty Status Updated Successfully";
                }
                else
                {
                    Message = "Error in Updating Monthly Out Duty Status";
                }

            }
            else if (type == "Delete")
            {
                string Mes = util.execQuery("Delete from GroupLNewAppOutDuty where AutoId='" + AutoId + "'", util.strElect);

                if (Mes == "Successfull")
                {
                    Message = "Monthly Out Duty Deleted Successfully";
                }
                else
                {
                    Message = "Error in Deleting Monthly Out Duty";
                }


            }
            return Json(JsonConvert.SerializeObject(new { Message = Message }));
        }


        #endregion

        public List<SelectListItem> CustomerDropDown()
        {
          
                return util.PopulateDropDown("exec Usp_GroupLNewAppDLL 'CustomerMap', @Id = '" + HttpContext.Session.GetString("UserName") + "'", util.strElect);
            
        }
        public  List<SelectListItem> RegionDropDown()
        {
          
                return util.PopulateDropDown("exec Usp_GroupLNewAppDLL 'RegionMap', @Id = '" + HttpContext.Session.GetString("UserName") + "',@Id2='"+ HttpContext.Session.GetString("companyid") + "'", util.strElect);
            
        }
     
        public IActionResult PendingVisitReport()
        {

            var companyid = HttpContext.Session.GetString("companyid");
            var locationid = HttpContext.Session.GetString("locationid");
            var branchid = HttpContext.Session.GetString("branchid");

            if (HttpContext.Session.GetString("UserName").ToUpper() == "HDBADMIN" && HttpContext.Session.GetString("password").ToUpper() == "ADMIN@123")
            {
                List<SelectListItem> ddl = new List<SelectListItem>();
                ddl.Add(new SelectListItem
                {
                    Value = "GL0072",
                    Text = "Hdb Financial Services Limited"
                });
                List<SelectListItem> ddl2 = new List<SelectListItem>();
                ddl2.Add(new SelectListItem
                {
                    Value = "North",
                    Text = "North"
                });
                ViewBag.customer = ddl;
                ViewBag.Region = ddl2;

            }
            else
            {
                ViewBag.customer = CustomerDropDown();

                ViewBag.Region = RegionDropDown();
            }
            return View();
        }

     
        public JsonResult GetPendingVisitReport(string ClientCode,string Region,string Duration)
        {
            var branchid = HttpContext.Session.GetString("branchid");
            var ds = util.Fill(@$"exec udp_GetCustomerFeedbackUnDoneReports2 @LocationAutoID='{branchid}',@ClientCode='{ClientCode}',@Region='{Region}',@Duration='{Duration}',@UserId='{UserId}' ", util.strElect);

            return Json(JsonConvert.SerializeObject(ds.Tables[0]));

        }

        public class Attendance
        {
            public string EmpCode { get; set; }
            public string Name { get; set; }
            public string Designation { get; set; }
            public string Department { get; set; }
            public string Location { get; set; }
            public Dictionary<int, string> Days { get; set; } = new Dictionary<int, string>();
        }

        #region AttendanceMuster

        public IActionResult AttendanceMuster()
        {
            
 

            return View();
        }
      
        public JsonResult GetAttendanceMuster(int Year,int Month)
        {
            var branchid = HttpContext.Session.GetString("branchid");
            var ds = util.Fill(@$"exec udp_GetAttendanceMusterWithShiftGroupLWithoutclient2 @LocationAutoId='{branchid}',@Year='{Year}',@Month='{Month}' ", util.strElect);




            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }

        #endregion

        #region AttendanceMusterClientWise
        public IActionResult AttendanceMusterClientWise()
        {
          ViewBag.Client=  util.PopulateDropDown("exec Usp_GroupLNewAppDLL 'CustomerMap', @Id = '" + HttpContext.Session.GetString("UserName") + "'", util.strElect);


            return View();
        }

        public JsonResult GetAttendanceMusterClientWise(int Year, int Month,string Client,string Site)
        {
            var branchid = HttpContext.Session.GetString("branchid");
            var ds = util.Fill(@$"exec udp_GetAttendanceMusterWithShiftGroupL @LocationAutoId='{branchid}',@Year='{Year}',@Month='{Month}',@ClientCode='{Client}',@AsmtID='{Site}' ", util.strElect);



            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }

        #endregion

        #region Materialreport
        public IActionResult Materialreport()
        {
            return View();
        }

        public JsonResult GetAllMaterialreport(string Month,string Year)
        {
            var companyid = HttpContext.Session.GetString("companyid");
            string query = "Usp_GroupLNewAppMaterialReport @UserId='"+UserId+ "',@CompanyCode='"+ companyid + "',@Month='"+Month+"',@Year='"+Year+"'";
            DataSet ds = util.Fill(query, util.strElect);
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        #endregion


        public IActionResult GetReports( string  Id)
        {
        
            var branchid = HttpContext.Session.GetString("branchid");
            string[] Pramsname = { "From date", "To date","Company","Region", "Branch", "Client","Site","Shift" };
            ViewBag.Pramsname = Pramsname;         

            ViewBag.ReportName = Id;
            ViewBag.Type = Id == "Client Details Report" ? "1": Id=="Site Details Report"?"2": Id == "My Task Report" ? "3":Id== "Resignation Details HR"?"4":"";
            ViewBag.Company = util.PopulateDropDown("Usp_GroupLNewAppDLL 'CompanyLogin',@Id='" + UserId + "'", util.strElect); 
            ViewBag.customer = CustomerDropDown();
            ViewBag.Shift = util.PopulateDropDown("exec udp_GetStandardShifts @LocationAutoId='" +  branchid+ "'", util.strElect);
            ViewBag.ChecklistClient = util.PopulateDropDown("exec udp_GetClientListGroupL @LocationAutoId='" +  branchid+ "'", util.strElect);
         
            return View();
        }

     

        public JsonResult GetReport(string Type,string ClientCode,string Site, string fromdate,string ChecklistClient,string ChecklistSite)
       {
            var branchid = HttpContext.Session.GetString("branchid");
            var companyid = HttpContext.Session.GetString("companyid");
            var region = HttpContext.Session.GetString("locationid");
            string query = @$"Usp_GetGroupLReports @Type='{Type}',@UserId='{UserId}',@CompanyCode='{companyid}',@LocationAutoId='{branchid}',@Region='{region}',@CleintCode='{ClientCode}',@Site='{Site}',@FDate='{fromdate}',
@id1='{ChecklistClient}',@id2='{ChecklistSite}'
";
            DataSet ds = util.Fill(query, util.strElect);
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }


        public JsonResult getClientListSite(string Id)
        {
            var branchid = HttpContext.Session.GetString("branchid");
            var companyid = HttpContext.Session.GetString("companyid");
            var region = HttpContext.Session.GetString("locationid");
            string query = $"udp_GetSiteListGroupLNew  @ClientCode='{Id}' ,@BaseCompanyCode='{companyid}',@LocationAutoId='{branchid}'";
            DataSet ds = util.Fill(query, util.strElect);
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }

    }
}

