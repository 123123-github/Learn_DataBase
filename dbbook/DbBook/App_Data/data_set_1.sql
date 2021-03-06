-- User
SET IDENTITY_INSERT [dbo].[User] ON 
INSERT [dbo].[User] ([uid], [account], [password], [name], [place], [udate]) VALUES (3, N'061600223', N'123456', N'李嘉玮', N'南京', CAST(N'2019-06-07' AS Date))
INSERT [dbo].[User] ([uid], [account], [password], [name], [place], [udate]) VALUES (5, N'061600224', N'123456', N'张海军', N'南京', CAST(N'2019-06-11' AS Date))
SET IDENTITY_INSERT [dbo].[User] OFF

-- Book
INSERT [dbo].[Book] ([isbn], [title], [authors], [press], [pdate], [page], [price]) VALUES (9787532763672, N'少年', N'[俄] 陀思妥耶夫斯基', N'上海译文出版社', NULL, 704, 69)
INSERT [dbo].[Book] ([isbn], [title], [authors], [press], [pdate], [page], [price]) VALUES (9787544258975, N'霍乱时期的爱情', N'[哥伦比亚] 加西亚·马尔克斯', N'南海出版公司', NULL, 401, 39.5)

-- BookInfo
INSERT [dbo].[BookInfo] ([isbn], [author_info], [content_info]) VALUES (9787532763672, N'陀思妥耶夫斯基（1821-1881），与大文豪列夫·托尔斯泰、屠格涅夫等人齐名，是俄国文学的卓越代表，他走的是一条极为艰辛、复杂的生活与创作道路，是俄国文学史上最复杂、最矛盾的，作家之一。他的创作独具特色，在群星灿烂的19世纪俄国文坛上独树一帜，占有着十分特殊的地位。主要作品有《穷人》、《被侮辱与被损害的》、《死屋手记》、《罪与罚》、《白痴》、《群魔》、《卡拉马佐夫兄弟》等。', N'《少年》是陀思妥耶夫斯基晚年一部颇为重要的长篇小说。小说以一个涉世未深、深受罗特希尔德思想影响的少年阿尔卡其·多尔戈鲁基的成长经历为主线，描绘了资本主义迅速发展的19世纪70年代俄国的拜金主义对当时青年一代灵魂的腐蚀。')
INSERT [dbo].[BookInfo] ([isbn], [author_info], [content_info]) VALUES (9787544258975, N'加西亚?马尔克斯（Gabriel García Márquez）

1927年出生于哥伦比亚马格达莱纳海滨小镇阿拉卡塔卡。童年与外祖父母一起生活。1936年随父母迁居苏克雷。1947年考入波哥大国立大学。1948年因内战辍学，进入报界。五十年代开始出版文学作品。六十年代初移居墨西哥。1967年《百年孤独》问世。1982年获诺贝尔文学奖。1985年出版《霍乱时期的爱情》。', N'★马尔克斯唯一正式授权，首次完整翻译
★《霍乱时期的爱情》是我最好的作品，是我发自内心的创作。——加西亚?马尔克斯
★这部光芒闪耀、令人心碎的作品是人类有史以来最伟大的爱情小说。——《纽约时报》
《霍乱时期的爱情》是加西亚?马尔克斯获得诺贝尔文学奖之后完成的第一部小说。讲述了一段跨越半个多世纪的爱情史诗，穷尽了所有爱情的可能性：忠贞的、隐秘的、粗暴的、羞怯的、柏拉图式的、放荡的、转瞬即逝的、生死相依的……再现了时光的无情流逝，被誉为“人类有史以来最伟大的爱情小说”，是20世纪最重要的经典文学巨著之一。')

-- Comment
SET IDENTITY_INSERT [dbo].[Comment] ON 
INSERT [dbo].[Comment] ([cid], [isbn], [uid], [cdate], [score], [content]) VALUES (2, 9787532763672, 3, CAST(N'2019-06-08' AS Date), 5, N'123456789')
INSERT [dbo].[Comment] ([cid], [isbn], [uid], [cdate], [score], [content]) VALUES (3, 9787544258975, 3, CAST(N'2019-06-08' AS Date), 5, N'内容内容内容内容内容内容')
INSERT [dbo].[Comment] ([cid], [isbn], [uid], [cdate], [score], [content]) VALUES (4, 9787532763672, 3, CAST(N'2019-06-08' AS Date), 5, N'nnei=adoasasda')
INSERT [dbo].[Comment] ([cid], [isbn], [uid], [cdate], [score], [content]) VALUES (5, 9787532763672, 3, CAST(N'2019-06-08' AS Date), 3, N'123')
INSERT [dbo].[Comment] ([cid], [isbn], [uid], [cdate], [score], [content]) VALUES (6, 9787532763672, 3, CAST(N'2019-06-08' AS Date), 4, N'453453543')
INSERT [dbo].[Comment] ([cid], [isbn], [uid], [cdate], [score], [content]) VALUES (7, 9787532763672, 3, CAST(N'2019-06-08' AS Date), 5, N'453453453453453435434534534354')
INSERT [dbo].[Comment] ([cid], [isbn], [uid], [cdate], [score], [content]) VALUES (10, 9787532763672, 5, CAST(N'2019-06-11' AS Date), 5, N'zzz')
SET IDENTITY_INSERT [dbo].[Comment] OFF


