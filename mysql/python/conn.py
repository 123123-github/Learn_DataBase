# python 连接数据库 - 基本操作 select
import pymysql

# 打开数据库连接
db = pymysql.connect(host = 'localhost',
                     user = 'root', password = '061600223',
                     db = 'dbcourse', charset = 'utf8')

# 使用cursor()方法获取操作游标
cursor = db.cursor()

# SQL 查询语句
sql = 'select * from s'

try:
    # 执行SQL语句
    cursor.execute(sql)
    # 获取所有记录列表
    rows = cursor.fetchall()
    for row in rows:
        print(row)
except:
    print("Error: unable to fetch data")

# 关闭数据库连接
db.close()


# 注：在 PyCharm 虚拟环境，windows 下
# 连接用户名使用 user 时 user = ‘root’
# root 会替换为 windows 下的当前用户名