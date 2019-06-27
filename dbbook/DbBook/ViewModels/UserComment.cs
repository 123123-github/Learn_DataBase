using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DbBook.ViewModels
{
	public class UserComment
	{
		// book
		public long isbn { get; set; }
		// comment
		public Nullable<System.DateTime> cdate { get; set; }
		public Nullable<byte> score { get; set; }
		public string content { get; set; }
	}
}