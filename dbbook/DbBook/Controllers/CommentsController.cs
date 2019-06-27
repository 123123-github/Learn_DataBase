using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DbBook.Models;
using DbBook.ViewModels;
using DbBook.BLL;

namespace DbBook.Controllers
{
    public class CommentsController : Controller
    {
        private dbbookEntities db = new dbbookEntities();

		// GET: Comments
		// 查看系统所有评论
		public ActionResult Index(int page = 1, int page_size = 10, string tag = "", string content = "", string sort = "")
		{
			int totalItems;
			List<Comment> comments;
			if (tag == "" || content == "")
			{
				comments = Service.GetComments(db, page, page_size);
				totalItems = db.Comment.Count();
			}
			else
			{
				string sql_format = @"select distinct * from [Comment], [Book] where [Comment].isbn = [Book].isbn and {0} like '{1}' order by {0} {2};";
				string sql = string.Format(sql_format, tag, content, sort);
				comments = Service.GetCommentsBySQL(db, page, page_size, sql);
				totalItems = db.Database.SqlQuery<Comment>(sql).Count();
			}

			PagingInfo pagingInfo = new PagingInfo
			{
				CurrentPage = page,
				ItemsPerPage = page_size,
				TotalItems = totalItems
			};

			ViewBag.PagingInfo = pagingInfo;
			ViewBag.SearchBar = new SearchBar("Index", "Comments");
            return View(comments);
        }

		//// POST: Books - 首页查询显示
		//[HttpPost]
		//public ActionResult Index(string tag, string content, string sort)
		//{
		//	if (tag == "" || content == "")
		//	{
		//		return RedirectToAction("Index");
		//	}

		//	string sql_format = @"select * from Book where {0} like '{1}' order by {0} {2};";
		//	string sql = String.Format(sql_format, tag, content, sort);

		//	ViewBag.SearchBar = new SearchBar("Index", "Books");
		//	return View(db.Database.SqlQuery<Book>(sql).ToList());
		//}

		// GET: Comments/Details/5
		public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            ViewBag.isbn = new SelectList(db.Book, "isbn", "title");
            ViewBag.uid = new SelectList(db.User, "uid", "account");
            return View();
        }

        // POST: Comments/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cid,isbn,uid,cdate,score,content")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comment.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.isbn = new SelectList(db.Book, "isbn", "title", comment.isbn);
            ViewBag.uid = new SelectList(db.User, "uid", "account", comment.uid);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.isbn = new SelectList(db.Book, "isbn", "title", comment.isbn);
            ViewBag.uid = new SelectList(db.User, "uid", "account", comment.uid);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cid,isbn,uid,cdate,score,content")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.isbn = new SelectList(db.Book, "isbn", "title", comment.isbn);
            ViewBag.uid = new SelectList(db.User, "uid", "account", comment.uid);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comment.Find(id);
            db.Comment.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
