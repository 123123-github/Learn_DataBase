using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DbBook.ViewModels
{
	// 书籍详情页所需的信息
	public class BookDetailItem
	{
		public long isbn { get; set; }
		public string title { get; set; }
		public string authors { get; set; }
		public string press { get; set; }
		public Nullable<System.DateTime> pdate { get; set; }
		public Nullable<int> page { get; set; }
		public Nullable<double> price { get; set; }
		public string author_info { get; set; }
		public string content_info { get; set; }
	}
}