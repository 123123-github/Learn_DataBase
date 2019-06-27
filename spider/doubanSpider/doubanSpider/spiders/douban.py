# -*- coding: utf-8 -*-
import scrapy
import datetime
import random
from scrapy.linkextractors import LinkExtractor
from scrapy.spiders import CrawlSpider, Rule
from doubanSpider.items import *

# tags = ['成长','生活','旅行','科普','互联网','编程','科学',
#         '青春','历史','心理学','哲学','传记','爱情',
#         '经典', '日本文学', '散文', '漫画', '推理', '绘本',
#         '小说','外国文学','文学','随笔','中国文学']

tags = ['陀思妥耶夫斯基','加缪','黑塞','波拉尼奥','马尔克斯','鲁迅']

class DoubanSpider(CrawlSpider):
    name = 'douban'
    allowed_domains = ['douban.com']
    start_urls = ['https://book.douban.com/tag/' + tag  for tag in tags]

    rules = (
        Rule(LinkExtractor(restrict_xpaths=('//*[@class="nbg"]')), callback='parse_item'),
        #Rule(LinkExtractor(restrict_xpaths=('//*[@class="tagCol"]')), follow=True)
    )

    # def parse_item(self, response):
    #     item = {}
    #
    #     link_list = response.xpath('//*[@class="subject-item"]/*/h2/a/@href').extract()
    #     title_list = response.xpath('//*[@class="subject-item"]/*/h2/a/@title').extract()
    #
    #     for link, title in zip(link_list, title_list):
    #         item['title'] = title
    #         item['link'] = link
    #         item['id'] = link.split('/')[-2]
    #         yield item


    def parse_item(self, response):
        item = BookItem()
        item['title'] = ''
        item['press'] = ''
        item['authors'] = ''
        item['pdate'] = '1990-01-01'
        item['page'] = 0
        item['price'] = 0.0
        item['isbn'] = random.randint(1,100000000)
        item['content_info'] = ''
        item['author_info'] = ''

        keys = response.xpath('//*[@id="info"]').xpath('string(.)').extract_first().split()
        item['title'] = response.xpath('//*[@id="wrapper"]/h1/span/text()').get()

        for i in range(0, len(keys), 2):
            if keys[i] == '出版社:':
                item['press'] = keys[i+1]
            if keys[i] == '作者:':
                item['authors'] = keys[i+1]
            elif keys[i] == '出版年:':
                pdate = keys[i+1]
                if pdate.count('-') != 2: continue
                item['pdate'] = pdate
            elif keys[i] == '页数:':
                item['page'] = keys[i+1]
            elif keys[i] == '定价:':
                item['price'] = keys[i+1][0:-1]
            elif keys[i] == 'ISBN:':
                item['isbn'] = keys[i+1]

        info = response.xpath('//*[@class="intro"]').xpath('string(.)').extract()
        item['content_info'] = info[0].strip()
        item['author_info'] = info[1].strip()

        yield item