using CloudTrixApp.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudTrixApp.Controllers
{
    public class LayoutController : Controller
    {
        //
        // GET: /Layout/
        public ActionResult _Layout()
        {   //Role Permission
            string RemoveTab = "";
            string ShowTab = "";
           int EmployeeID = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
           DataTable dtRolePermission = ClientData.Role_Permission(EmployeeID, 0);
            DataRow[] rows = dtRolePermission.Select();
            if (dtRolePermission != null)
            {
                foreach (DataRow item in dtRolePermission.Rows)
                {
                    if (item["AddPermission"].ToString() == "False" && item["UpdatePermission"].ToString() == "False" && item["DeletePermission"].ToString() == "False" && item["ViewPermission"].ToString() == "False")
                    {
                        RemoveTab += item["FormID"].ToString() + "|";
                    }
                    else
                    {
                        ShowTab += item["FormID"].ToString() + "|";
                    }
                }
            }
            TempData["RemoveTab"] = RemoveTab;
            TempData["ShowTab"] = ShowTab;
            return View();
        }
	}
}