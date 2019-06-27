using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DbBook.ViewModels
{
	public static class PagingHelpers
	{
		public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
		{
			int cur = pagingInfo.CurrentPage;
			int tot = pagingInfo.TotalPage;

			StringBuilder result = new StringBuilder();
			// 前一页
			TagBuilder prePage = new TagBuilder("a");
			prePage.InnerHtml = "Previous";
			prePage.AddCssClass("btn btn-light");
			prePage.MergeAttribute("href", pageUrl(Math.Max(cur - 1, 1)));
			result.Append(prePage.ToString());

			// 中间页
			int i = 1;
			while (i <= tot)
			{
				TagBuilder tag = new TagBuilder("a");

				if (i <= 2 || i >= tot - 1 || (cur - 2 <= i && i <= cur + 2))
				{
					// 需要显示
					tag.MergeAttribute("href", pageUrl(i));
					tag.InnerHtml = i.ToString();
					if (i == cur)
					{
						tag.AddCssClass("btn btn-primary");
					}
					else
					{
						tag.AddCssClass("btn btn-light");
					}
					result.Append(tag.ToString());
					i++;
				}
				else
				{
					// 不在需要显示的范围
					tag.InnerHtml = "...";
					tag.AddCssClass("btn");
					result.Append(tag.ToString());
					i = i < cur ? cur - 2 : tot - 1;
				}
			}

			// 后一页
			TagBuilder nextPage = new TagBuilder("a");
			nextPage.InnerHtml = "Next";
			nextPage.AddCssClass("btn btn-light");
			nextPage.MergeAttribute("href", pageUrl(Math.Min(cur + 1, tot)));
			result.Append(nextPage.ToString());

			return MvcHtmlString.Create("<div class=\"btn-group\">" + result.ToString() + "</div>");
		}
	}
}