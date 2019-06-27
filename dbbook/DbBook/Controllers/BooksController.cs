using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DbBook.BLL;
using DbBook.Models;
using DbBook.ViewModels;

namespace DbBook.Controllers
{
	public class BooksController : Controller
	{
		private dbbookEntities db = new dbbookEntities();

		// GET: Books
		// 查看系统所有书籍
		public ActionResult Index(int page = 1, int page_size = 10, string tag = "", string content = "", string sort = "")
		{
			int totalItems;
			List<Book> books;
			if (tag == "" || content == "")
			{
				books = Service.GetBooks(db, page, page_size);
				totalItems = db.Book.Count();
			}
			else
			{
				string sql_format = @"select * from Book where {0} like '{1}' order by {0} {2};";
				string sql = string.Format(sql_format, tag, content, sort);
				books = Service.GetBooksBySQL(db, page, page_size, sql);
				totalItems = db.Database.SqlQuery<Book>(sql).Count();
			}

			PagingInfo pagingInfo = new PagingInfo
			{
				CurrentPage = page,
				ItemsPerPage = page_size,
				TotalItems = totalItems
			};

			ViewBag.PagingInfo = pagingInfo;
			ViewBag.SearchBar = new SearchBar("Index", "Books");
			return View(books);
		}

        // GET: Books/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.isbn = new SelectList(db.BookInfo, "isbn", "author_info");
            return View();
        }

        // POST: Books/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "isbn,title,authors,press,pdate,page,price,BookInfo")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Book.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.isbn = new SelectList(db.BookInfo, "isbn", "author_info", book.isbn);
            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.isbn = new SelectList(db.BookInfo, "isbn", "author_info", book.isbn);
            return View(book);
        }

        // POST: Books/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "isbn,title,authors,press,pdate,page,price,BookInfo")] Book book)
        {
            if (ModelState.IsValid)
            {
				book.BookInfo.isbn = book.isbn;
                db.Entry(book).State = EntityState.Modified;
				db.Entry(book.BookInfo).State = EntityState.Modified;
				db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.isbn = new SelectList(db.BookInfo, "isbn", "author_info", book.isbn);
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Book book = db.Book.Find(id);
            db.Book.Remove(book);
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
