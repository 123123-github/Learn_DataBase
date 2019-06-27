# # -*- coding: utf-8 -*-
# import scrapy
# from scrapy.linkextractors import LinkExtractor
# from scrapy.spiders import CrawlSpider, Rule
# from doubanSpider.items import *
#
# class DoubanSpider(CrawlSpider):
#     name = 'douban'
#     allowed_domains = ['douban.com']
#     start_urls = ['https://book.douban.com/tag/']
#
#     rules = (
#         Rule(LinkExtractor(restrict_xpaths=('//*[@class="tagCol"]')), callback='parse_item'),
#     )
#
#     def parse_item(self, response):
#         item = DoubanspiderItem()
#         #item['domain_id'] = response.xpath('//input[@id="sid"]/@value').get()
#         #item['name'] = response.xpath('//div[@id="name"]').get()
#         #item['description'] = response.xpath('//div[@id="description"]').get()
#         tlist = response.xpath('//*[@class="subject-item"]/*/h2/a/@title').extract()
#         alist = response.xpath('//*[@class="pub"]/text()').extract()
#
#         for title, author in zip(tlist, alist):
#             item['title'] = title
#             item['authors'] = author.strip()
#             yield item
#
#         # return item