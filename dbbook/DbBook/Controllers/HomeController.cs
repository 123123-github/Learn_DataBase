using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DbBook.Models;

namespace DbBook.Controllers
{
	public class HomeController : Controller
	{
		private dbbookEntities db = new dbbookEntities();

		const string sql_clear = @"delete from [Comment];delete from [User];delete from [Book];";
		const string data_path = "/App_Data/data_set_{0}.sql";

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult GenData(int id)
		{
			string sql_file = Server.MapPath("/") + String.Format(data_path, id);
			string sql_data = System.IO.File.ReadAllText(sql_file, System.Text.Encoding.UTF8);

			db.Database.ExecuteSqlCommand(sql_data);

			return RedirectToAction("Index");
		}

		public ContentResult ClearData()
		{
			db.Database.ExecuteSqlCommand(sql_clear);
			var script = String.Format("<script>alert('成功清除');location.href='{0}'</script>", Url.Action("index"));
			return Content(script, "text/html");
		}
	}
}