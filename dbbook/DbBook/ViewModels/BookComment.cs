using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DbBook.ViewModels
{
	public class CommentItem
	{
		// book
		public long isbn { get; set; }
		// user
		public int uid { get; set; }
		public string account { get; set; }
		public string name { get; set; }
		// comment
		public Nullable<System.DateTime> cdate { get; set; }
		public Nullable<byte> score { get; set; }
		public string content { get; set; }
	}
}