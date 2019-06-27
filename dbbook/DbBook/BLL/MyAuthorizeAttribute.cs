using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DbBook.BLL
{
	public class MyAuthorizeAttribute : AuthorizeAttribute
	{
		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			// 使用 UserID 验证用户的身份-
			string user_info = (string)httpContext.Session["UserID"];
			return user_info != null;
		}

		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			//处理 Url 请求
			//验证不通过,直接跳转到相应页面，注意：如果不使用以下跳转，则会继续执行Action方法 
			filterContext.Result = new RedirectResult("/Account/Login");
		}
	}
}