-- 创建学生数据库
create database dbcourse;

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
insert into C values ('1', '数据库', '5', 4);
insert into C values ('2', '数学', null, 2);
insert into C values ('3', '信息系统', '1', 4);
insert into C values ('4', '操作系统', '6', 3);
insert into C values ('5', '数据结构', '7', 4);
insert into C values ('6', '数据处理', null, 2);
insert into C values ('7', 'PASCAL语言', '6', 4);
-- SC
insert into SC values ('201215121', '1', 92);
insert into SC values ('201215121', '2', 85);
insert into SC values ('201215121', '3', 88);
insert into SC values ('201215122', '2', 90);
insert into SC values ('201215122', '3', 80);

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

-- 4．查询学习全部课程学生姓名。



-- 5．查询所有学生除了选修 1 号课程外所有成绩均及格的学生的学号和平均成绩，其结果按平均成绩的降序排列。


-- 6．查询选修数据库原理成绩第 2 名的学生姓名。


-- 7. 查询所有 3 个学分课程中有 3 门以上（含 3 门）课程获 80 分以上（含 80 分）的学生的姓名。


-- 8. 查询选课门数唯一的学生的学号。


-- 9．SELECT 语句中各种查询条件的实验。