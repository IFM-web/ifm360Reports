using DocumentFormat.OpenXml.Office2016.Excel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace ifm360Reports.Models
{
    public static class BulkInsert
    {
      
        public static DataTable BulkSave(string data,string Clientcode,string BranchId, string type,string ConnectionString )
        {
            
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(data);
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand("Usp_GroupLNew_FlgUpdateSiteEmp", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Type", type);
                cmd.Parameters.AddWithValue("@Clientcode", Clientcode);
                cmd.Parameters.AddWithValue("@BranchId", BranchId);
                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@data", dt);
                tvpParam.SqlDbType = SqlDbType.Structured;
                tvpParam.TypeName = "FlagIsActive";

                DataTable result = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(result);

                }
                return result;
            }
        }
    }
}
