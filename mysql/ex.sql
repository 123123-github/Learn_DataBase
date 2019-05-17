drop database if exists dbcourse;
-- 实验一：SQL 定义功能、数据插入 1 学时
-- 创建学生数据库
create database dbcourse character set utf8 collate utf8_general_ci;
-- 建表
-- S 学生
create table S (
    Sno char(9) primary key,
    Sname char(20),
    Ssex char(2),
    Sage smallint,
    Sdept char(20)
);

-- C 课程
create table C (
    Cno char(4) primary key,
    Cname char(40) not null,
    Cpno char(4),
    Ccredit smallint,
    foreign key (Cpno) references C(Cno)
);

-- SC 选课
create table SC (
    Sno char(9),
    Cno char(4),
    Grade smallint,
    primary key (Sno, Cno),
    foreign key (Sno) references S(Sno),
    foreign key (Cno) references C(Cno)
);

-- 插入数据
-- S
insert into S values ('201215121', '李勇', '男', 20, 'CS');
insert into S values ('201215122', '刘晨', '女', 19, 'CS');
insert into S values ('201215123', '王敏', '女', 18, 'MA');
insert into S values ('201215125', '张立', '男', 20, 'IS');
-- C
insert into C values ('2', '数学', null, 2);
insert into C values ('6', '数据处理', null, 2);
insert into C values ('7', 'PASCAL语言', '6', 4);
insert into C values ('4', '操作系统', '6', 3);
insert into C values ('5', '数据结构', '7', 4);
insert into C values ('1', '数据库', '5', 4);
insert into C values ('3', '信息系统', '1', 4);
-- SC
insert into SC values ('201215121', '1', 92);
insert into SC values ('201215121', '2', 85);
insert into SC values ('201215121', '3', 88);
insert into SC values ('201215122', '2', 90);
insert into SC values ('201215122', '6', 80);
insert into SC values ('201215122', '7', 90);
insert into SC values ('201215122', '5', 90);
insert into SC values ('201215122', '1', 90);


-- 实验二：数据查询 2 学时
-- 1．查询选修 1 号课程的学生学号与姓名。
select s.sname, s.sno 
from s, sc
where s.sno = sc.sno and sc.cno = 1;

-- 2．查询选修课程名为数据结构的学生学号与姓名。
select s.sname, s.sno 
from s, c, sc
where s.sno = sc.sno and c.cno = sc.cno and c.cname = '数据结构';
-- 课程信息系统 
select s.sname, s.sno 
from s, c, sc
where s.sno = sc.sno and c.cno = sc.cno and c.cname = '信息系统';

-- 3．查询不选 1 号课程的学生学号与姓名。
select s.sno, s.sname
from s
where s.sno not in (
    select sc.sno
    from sc
    where sc.cno = '1'
);

-- 4．查询学习全部课程学生姓名。(1 2 3课程)
select s.sname
from s
where not exists (
    select *
    from c
    where c.cno in ('1', '2', '3') and not exists (
        select *
        from sc
        where sc.sno = s.sno and sc.cno = c.cno
    )
);

-- 5．查询所有学生除了选修 1 号课程外所有成绩均及格的学生的学号和平均成绩，其结果按平均成绩的降序排列。
select x.sno, avg(x.Grade)
from sc x
where x.sno in (
    select y.sno
    from sc y
    where not exists (
        select *
        from sc z
        where z.sno = y.sno and z.cno != 1 and z.Grade < 60 
    )
)
group by x.sno
order by avg(x.Grade) desc;

-- 6．查询选修数据库原理成绩第 2 名的学生姓名。 （换成数学课）
select s.sname
from s, sc, c
where s.sno = sc.sno and sc.cno = c.cno and c.cname = '数学'
order by sc.Grade desc
limit 1, 1;

-- 7. 查询所有 3(改成4) 个学分课程中有 3 门以上（含 3 门）课程获 80 分以上（含 80 分）的学生的姓名。
select s.sname
from s
where s.sno in (
    select sc.sno
    from c, sc
    where sc.cno = c.cno and c.Ccredit = 4 and sc.Grade >= 80
    group by sc.sno
    having count(sc.sno) >= 3
);

-- 8. 查询选课门数唯一的学生的学号。
create view sc_count(sno, c_count)
as
select sc.sno, count(sc.cno)
from sc
group by sc.sno;

select x.sno
from sc_count x
where x.sno not in (
    select y.sno
    from sc_count y
    where y.sno != x.sno and y.c_count = x.c_count
);

-- 9．SELECT 语句中各种查询条件的实验。


-- 实验三：数据修改、删除 1 学时
-- 1．把 1 号课程的非空成绩提高 10％。
update sc
set sc.Grade = sc.Grade * 1.1
where sc.cno = '1' and sc.Grade is not null;

-- 2．在 SC 表中删除课程名为数据结构的成绩的元组。
delete from sc
where sc.cno in (
    select c.cno
    from c
    where c.cname = '数据结构'
);

-- 3．在 S 和 SC 表中删除学号为 201215122 的所有数据。
delete from sc
where sc.sno = '201215122';
delete from s
where s.sno = '201215122';


-- 实验四：视图的操作 1 学时
-- 1．建立男学生的视图，属性包括学号、姓名、选修课程名和成绩。
create view student_male
as
select s.sno, s.sname, c.cname, sc.Grade
from s, sc, c
where s.sno = sc.sno and sc.cno = c.cno and s.Ssex = '男';

-- 2．在男学生视图中查询平均成绩大于 80 分的学生学号与姓名。
select m.sno, m.sname
from student_male m
group by m.sno
having avg(m.Grade) > 80;


-- 实验五：库函数，授权控制 1 学时
-- 1．计算每个学生有成绩的课程门数、平均成绩。
select sc.sno, count(sc.cno), avg(sc.Grade)
from sc
group by sc.sno;

-- 2．使用 GRANT 语句，把对基本表 S、SC、C 的使用权限授给其它用户。
/* 查看用户 */
select host,user, plugin from mysql.user;
/* 创建用户 - 只需要在 mysql 数据库中的 user 表添加新用户即可。 */
grant usage 
on *.* to 'guest'@'localhost' identified by '123456' 
with grant option;
/* 授权 */
grant select 
on *.* 
to guest@"localhost";

-- 3．实验完成后，撤消建立的基本表和视图。
drop view if exists student_male;
drop view if exists sc_count;
drop table if exists S;
drop table if exists C;
drop table if exists SC;