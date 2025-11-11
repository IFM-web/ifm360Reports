using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using ifm360Reports.Models;
using ifm360Reports; 

public class MenuViewComponent : ViewComponent
{
    private readonly IConfiguration _configuration;

    public MenuViewComponent(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        List<MenuItem> menuItems = new List<MenuItem>();

        db_Utility connectionString = new  db_Utility();

        using (SqlConnection con = new SqlConnection(connectionString.strElect))
        {
            string query = "exec Usp_GroupL_ShowMenuList @UserId='"+ HttpContext.Session.GetString("UserName") + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader rdr = await cmd.ExecuteReaderAsync();

            while (await rdr.ReadAsync())
            {
                menuItems.Add(new MenuItem
                {
                    MenuId = rdr.GetInt32(0),
                    MenuName = rdr.GetString(1),
                    SubMenuName = rdr.IsDBNull(2) ? null : rdr.GetString(2),
                    ProfileId = 0,
                    PrintOrder = rdr.GetDecimal(4),

					MenuUrl = rdr.IsDBNull(3) ? null : rdr.GetString(3),
				});
            }
        }

        // Group by MenuName
        var groupedMenus = menuItems
            .GroupBy(m => m.MenuName)
			.Select(g => new MenuViewModel
			{
				MenuName = g.Key,
				SubMenus = g
		.Where(x => !string.IsNullOrEmpty(x.SubMenuName))
		.Select(x => new SubMenuItem
		{
			SubMenuName = x.SubMenuName,
			MenuUrl = x.MenuUrl
		}).ToList()
			}).ToList();


		return View(groupedMenus);
    }
}
