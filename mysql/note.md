# MySQL


## 基本命令

登录

mysql -u * -p

显示

show database;  
show tables;  

连接指定数据库  

use 数据库名

## 基本管理

user <数据库名>  
show databases  
show tables  
show columns from <表>  
show index from <表>  
show table status from <数据库> like <表达式>[\G打印信息]  

## 数据库操作

create database <数据库名>;  
create table <表名> (...);  
drop table <表名>;

## 数据库备份

-- mysqldump 命令  
mysqldump --column-statistics=0 -h localhost -u root -p --databases dbcourse > dbcourse.sql  
-- mysql 中恢复  
source <文件>