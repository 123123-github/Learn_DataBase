# -*- coding: utf-8 -*-

# Define here the models for your scraped items
#
# See documentation in:
# https://doc.scrapy.org/en/latest/topics/items.html

import scrapy


# 测试类
class DoubanspiderItem(scrapy.Item):
    # define the fields for your item here like:
    # name = scrapy.Field()
    title = scrapy.Field()
    authors = scrapy.Field()


# 书籍信息
class BookItem(scrapy.Item):
    # 基本信息
    isbn = scrapy.Field()
    title = scrapy.Field()
    authors = scrapy.Field()
    press = scrapy.Field()
    pdate = scrapy.Field()
    page = scrapy.Field()
    price = scrapy.Field()
    # 详情部分
    author_info = scrapy.Field()
    content_info = scrapy.Field()


# 用户信息
class UserItem(scrapy.Item):
    uid = scrapy.Field()
    account = scrapy.Field()
    name = scrapy.Field()
    place = scrapy.Field()
    udate = scrapy.Field()
    # 密码直接自动生成
    password = scrapy.Field()


# 评论信息
class CommentItem(scrapy.Item):
    isbn = scrapy.Field()
    uid = scrapy.Field()
    cdate = scrapy.Field()
    score = scrapy.Field()
    content = scrapy.Field()