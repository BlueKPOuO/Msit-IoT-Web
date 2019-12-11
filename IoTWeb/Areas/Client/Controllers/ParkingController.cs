using IoTWeb.Areas.Admin.Hubs;
using IoTWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IoTWeb.Areas.Client.Controllers
{
    public class ParkingController : Controller
    {
        Buliding_ManagementEntities db = new Buliding_ManagementEntities();
        // GET: Admin/Parking
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SignalrConnection"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [ParkingNum],[EnterTime] FROM [dbo].[ParkingManagement]  WHERE [QuitTime] is NULL", connection))
                {
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += Dependency_OnChange;

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    var listParking = reader.Cast<IDataRecord>().Select(x => new
                    {
                        ParkingNum = (int)x["ParkingNum"],
                        EnterTime = ((DateTime)x["EnterTime"]).ToString()
                    }).ToList();

                    return Json(new { listParking = listParking }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        private void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            ParkingHub.ShowStatus();
        }

        public void IdleCar(int Days)
        {
            //var Idlecars = db
        }
    }
}