using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DbBook.ViewModels
{
	public class SearchBar
	{
		List<string> BooksList = new List<string>() { "", "isbn", "title", "authors" };
		List<string> UsersList = new List<string>() { "", "name", "uid", "place", "account" };
		List<string> CommentsList = new List<string>() { "", "score", "content", "title", "account" };

		public string actionName { get; set; }
		public string controllerName { get; set; }
		public List<string> tagList { get; set; }
		public List<string> sortList { get; set; }

		public SearchBar(string actionName, string controllerName)
		{
			this.actionName = actionName;
			this.controllerName = controllerName;
			switch (controllerName)
			{
				case "Books":
					tagList = BooksList;
					break;
				case "Users":
					tagList = UsersList;
					break;
				case "Comments":
					tagList = CommentsList;
					break;
				case "User":
					if (actionName == "ViewBooks") tagList = BooksList;
					else if (actionName == "ViewUsers") tagList = UsersList;
					break;
				default:
					break;
			}
			sortList = new List<string>() { "asc", "desc" };
		}
	}
}