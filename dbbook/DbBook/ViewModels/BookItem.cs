using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DbBook.ViewModels
{
	public class BookItem
	{
		public long isbn { get; set; }
		public string title { get; set; }
		public string authors { get; set; }
	}
}