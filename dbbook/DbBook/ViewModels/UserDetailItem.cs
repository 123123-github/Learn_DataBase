using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DbBook.ViewModels
{
	public class UserDetailItem
	{
		public int uid { get; set; }
		public string account { get; set; }
		public string name { get; set; }
		public string place { get; set; }
		public Nullable<System.DateTime> udate { get; set; }
	}
}