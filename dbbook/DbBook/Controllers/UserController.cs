using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DbBook.BLL;
using DbBook.Models;
using DbBook.ViewModels;

namespace DbBook.Controllers
{
    public class UserController : Controller
    {
		private dbbookEntities db = new dbbookEntities();

		// Books
		// 查看系统的所有书籍
		public ActionResult ViewBooks()
		{
			List<BookItem> bookItems = Service.GetBookItems(db);
			return View(bookItems);
		}

		public ActionResult ViewBook(long isbn)
		{
			// 书籍基本信息
			BookDetailItem bookDetailItem = Service.GetBookDetailItems(db, isbn);
			// 书籍相关信息
			ViewBag.CommentsNum = db.Comment.Where(x => x.isbn == isbn).Count();
			ViewBag.PartComments = Service.GetCommentItemsByISBN(db, isbn, 1, 3);
			// 书籍统计信息
			ViewBag.CommentScore = Service.GetCommentScore(db, isbn);
			return View(bookDetailItem);
		}

		// POST:
		// 接受用户提交的评论数据
		[HttpPost]
		public ActionResult ViewBook(long isbn, byte score, string content)
		{
			// if (Session["Uid"] == null) Redirect("/login.cshtml");
			// 存储评论结果
			// int uid = int.Parse(Session["Uid"].ToString());
			int uid = db.User.Where(x => x.name == "李嘉玮").FirstOrDefault().uid;
			Service.AddUserComment(db, isbn, uid, score, content);
			return RedirectToAction("ViewBook", new { isbn });
		}


		// Users
		// 查看系统的所有用户
		public ActionResult ViewUsers()
		{
			List<UserItem> userItems = Service.GetUserItems(db);
			return View(userItems);
		}

		public ActionResult ViewUser(int uid)
		{
			// 用户基本信息
			UserDetailItem user = Service.GetUserDetailItems(db, uid);
			// 用户相关信息
			ViewBag.CommentsNum = db.Comment.Where(x => x.uid == uid).Count();
			ViewBag.PartComments = Service.GetCommentItemsByUid(db, uid, 1, 3);
			return View(user);
		}


		// BookComment
		// Comment about one book
		public ActionResult ViewBookComments(long isbn, int page = 1, int page_size = 10)
		{
			// 书名
			ViewBag.BookTitle = db.Book.Where(x => x.isbn == isbn)
				.Select(x => x.title).FirstOrDefault();
			// 总的评论数
			ViewBag.CommentsNum = db.Comment.Where(x => x.isbn == isbn).Count();
			// CommentItem 列表
			List<CommentItem> commentItems = Service.GetCommentItemsByISBN(db, isbn, page, page_size);
			return View(commentItems);
		}

		// UserComment
		// Comment about one user
		public ActionResult ViewUserComments(int uid, int page = 1, int page_size = 10)
		{
			// 用户名称
			ViewBag.UserName = db.User.Where(x => x.uid == uid)
				.Select(x => x.name).FirstOrDefault();
			// 总的评论数
			ViewBag.CommentsNum = db.Comment.Where(x => x.uid == uid).Count();
			// CommentItem 列表
			List<CommentItem> commentItems = Service.GetCommentItemsByUid(db, uid, page, page_size);
			return View(commentItems);
		}

	}
}