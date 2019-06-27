# -*- coding: utf-8 -*-

# Define your item pipelines here
#
# Don't forget to add your pipeline to the ITEM_PIPELINES setting
# See: https://doc.scrapy.org/en/latest/topics/item-pipeline.html
import pymysql

# sql_insert = '''
# insert into book_link(id, title, link)
# values(%s, '%s', '%s')
# '''

sql_insert_book = '''
insert into book(isbn, title, authors, press, pdate, page, price)
values(%s, '%s', '%s', '%s', '%s', %s, %s)
'''

sql_insert_book_info = '''
insert into bookinfo(isbn, author_info, content_info)
values(%s, '%s', '%s') 
'''

class DoubanspiderPipeline(object):
    def open_spider(self, spider):
        self.conn = pymysql.connect(host='127.0.0.1', user='root', password='061600223', db='spider')
        self.cursor = self.conn.cursor()

    def process_item(self, item, sider):
        try:
            self.cursor.execute(sql_insert_book % (item['isbn'], item['title'], item['authors'],
                                              item['press'], item['pdate'], item['page'], item['price']))
            self.cursor.execute(sql_insert_book_info % (item['isbn'], item['author_info'], item['content_info']))
            self.conn.commit()
            print('success INSERT')
        except:
            raise
            self.conn.rollback()
            print('insert error')

        return item

    def close_spider(self, spider):
        self.conn.close()
