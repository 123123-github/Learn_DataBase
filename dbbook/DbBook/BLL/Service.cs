using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.Script.Serialization;
using DbBook.ViewModels;
using DbBook.Models;
using System.Data.SqlClient;

namespace DbBook.BLL
{
	public class Service
	{
		// ------------------------------------------------------------
		// ------------ Book ------------------------------------------
		// 查询: 所有书籍基本信息
		internal static List<BookItem> GetBookItems(dbbookEntities db)
		{
			const string sql = "select isbn, title, authors from book";
			var bookItems = db.Database.SqlQuery<BookItem>(sql);
			return bookItems.ToList();
		}

		internal static List<Book> GetBooks(dbbookEntities db, int page, int page_size)
		{
			return db.Book.OrderBy(x => x.isbn)
				.Skip((page - 1) * page_size).Take(page_size).ToList();
		}

		internal static List<Comment> GetCommentsBySQL(dbbookEntities db, int page, int page_size, string sql)
		{
			return db.Database.SqlQuery<Comment>(sql)
				.Skip((page - 1) * page_size).Take(page_size).ToList();
		}

		internal static List<Book> GetBooksBySQL(dbbookEntities db, int page, int page_size, string sql)
		{
			return db.Database.SqlQuery<Book>(sql)
				.Skip((page - 1) * page_size).Take(page_size).ToList();
		}

		internal static List<User> GetUsersBySQL(dbbookEntities db, int page, int page_size, string sql)
		{
			return db.Database.SqlQuery<User>(sql)
				.Skip((page - 1) * page_size).Take(page_size).ToList();
		}


		// 查询: 指定书籍详细信息
		internal static BookDetailItem GetBookDetailItems(dbbookEntities db, long isbn)
		{
			// 用户提供的参数
			var userSupplied_isbn = new SqlParameter("@isbn", isbn);
			// 查询的 SQL 语句
			const string sql =
				@"select book.*, bookinfo.content_info, bookinfo.author_info
				from book, bookinfo
				where book.isbn = @isbn and bookinfo.isbn = @isbn";
			// 避免 SQL 注入
			var bookDetailItem = db.Database.SqlQuery<BookDetailItem>(sql, userSupplied_isbn);

			return bookDetailItem.FirstOrDefault();
		}

		// 插入: 用户评论
		internal static void AddUserComment(dbbookEntities db, long isbn, int uid, byte score, string content)
		{
			db.Comment.Add(new Comment {
				isbn = isbn,
				uid = uid,
				cdate = DateTime.Now,
				score = score,
				content = content
			});
			db.SaveChanges();
		}

		// 查询: 指定书籍评分
		internal static int[] GetCommentScore(dbbookEntities db, long isbn)
		{
			int[] cs = new int[6];
			db.Comment.Where(x => x.isbn == isbn)
				.GroupBy(x => x.score).Select(x => new { score = (byte)x.Key, count = x.Count() })
				.ToList().ForEach(x => cs[x.score] = x.count);
			return cs;
		}

		// ------------------------------------------------------------
		// ------------ User ------------------------------------------
		// 查询: 所有用户信息
		internal static List<User> GetUsers(dbbookEntities db, int page, int page_size)
		{
			return db.User.OrderBy(x => x.uid)
				.Skip((page - 1) * page_size).Take(page_size).ToList();
		}

		// 查询: 所有用户基本信息
		internal static List<UserItem> GetUserItems(dbbookEntities db)
		{
			const string sql = "select uid, account, name, place from [user]";
			var userItems = db.Database.SqlQuery<UserItem>(sql);
			return userItems.ToList();
		}

		// 查询: 指定用户详细信息
		internal static UserDetailItem GetUserDetailItems(dbbookEntities db, int uid)
		{
			// 用户提供的参数
			var userSupplied_uid = new SqlParameter("@uid", uid);
			// 查询的 SQL 语句
			const string sql =
				@"select uid, account, name, place, udate
				from [user]
				where uid = @uid";
			// 避免 SQL 注入
			var userDetailItem = db.Database.SqlQuery<UserDetailItem>(sql, userSupplied_uid);

			return userDetailItem.FirstOrDefault();
		}

		// ------------------------------------------------------------
		// ------------ Comment ---------------------------------------
		// 查询: 系统所有评论信息 （取部分数据）
		internal static List<Comment> GetComments(dbbookEntities db, int page, int page_size)
		{
			return db.Comment.OrderBy(x => x.cid)
				.Skip((page - 1) * page_size).Take(page_size).ToList();
		}

		// 查询: 指定书籍的评论信息 （取部分数据）
		internal static List<CommentItem> GetCommentItemsByISBN(dbbookEntities db, long isbn, int page = 1, int page_size = 10)
		{
			return db.Comment.Where(x => x.isbn == isbn)
			.Join(db.User, cmt => cmt.uid, usr => usr.uid, (cmt, usr) => new CommentItem
			{
				uid = usr.uid,
				account = usr.account,
				name = usr.name,
				isbn = cmt.isbn,
				score = cmt.score,
				cdate = cmt.cdate,
				content = cmt.content
			}).OrderBy(x => x.cdate)
			.Skip((page - 1) * page_size).Take(page_size).ToList();
		}

		// 查询: 指定用户的评论信息 （取部分数据）
		internal static List<CommentItem> GetCommentItemsByUid(dbbookEntities db, int uid, int page, int page_size)
		{
			return db.Comment.Where(x => x.uid == uid)
			.Join(db.User, cmt => cmt.uid, usr => usr.uid, (cmt, usr) => new CommentItem
			{
				uid = usr.uid,
				account = usr.account,
				name = usr.name,
				isbn = cmt.isbn,
				score = cmt.score,
				cdate = cmt.cdate,
				content = cmt.content
			}).OrderBy(x => x.cdate)
			.Skip((page - 1) * page_size).Take(page_size).ToList();
		}

		// ------------------------------------------------------------
		// ------------ Search ----------------------------------------
		// ------------ 综合查询部分, 提供更多的查询功能 -----------------


	}
}