if not exists (select * from sys.databases where name = 'StudentManager')
begin
	create database StudentManager
end
go

use StudentManager

create table users (
  id int identity(1,1) primary key,

  extra_code varchar(100) NOT NULL,
  first_name varchar(60) NOT NULL,
  last_name varchar(60) NOT NULL,
  full_name varchar(60) NOT NULL,
  email varchar(60) NOT NULL,
  phone varchar(20) NOT NULL,
  address varchar(200) NOT NULL,
  birthday date NOT NULL,
  gender tinyint NOT NULL DEFAULT 1,
  avatar varchar(200) DEFAULT NULL,
  information text DEFAULT NULL,
  status tinyint NOT NULL DEFAULT 1,
  created_at datetime DEFAULT NULL,
  updated_at datetime DEFAULT NULL,
  deleted_at datetime DEFAULT NULL,
)
go

CREATE TABLE accounts (
  id int identity(1,1) primary key,
  role_id int NOT NULL,
  user_id int NOT NULL,
  username varchar(100) NOT NULL,
  password varchar(100) NOT NULL,
  status tinyint NOT NULL DEFAULT 1,
  ip_client varchar(100) DEFAULT NULL,
  last_login datetime DEFAULT NULL,
  last_logout datetime DEFAULT NULL,
  created_at datetime DEFAULT NULL,
  updated_at datetime DEFAULT NULL,
  deleted_at datetime DEFAULT NULL
)
go

 CREATE TABLE courses (
  id int identity(1,1) primary key,
  name varchar(100) NOT NULL,
  slug varchar(150) NOT NULL,
  department_id int NOT NULL,
  status tinyint NOT NULL DEFAULT 1,
  created_at datetime DEFAULT NULL,
  updated_at datetime DEFAULT NULL,
  deleted_at datetime DEFAULT NULL
)
go

CREATE TABLE departments (
  id int identity(1,1) primary key,
  name varchar(150) NOT NULL,
  slug varchar(200) NOT NULL,
  leader_id int NOT NULL,
  date_beginning date DEFAULT NULL,
  status tinyint NOT NULL DEFAULT 1,
  logo varchar(200) DEFAULT NULL,
  created_at datetime DEFAULT NULL,
  updated_at datetime DEFAULT NULL,
  deleted_at datetime DEFAULT NULL
)
go

CREATE TABLE groups (
  id int identity(1,1) primary key,
  department_id int NOT NULL,
  term_id int NOT NULL,
  name varchar(100) NOT NULL,
  slug varchar(150) NOT NULL,
  student_numbers int NOT NULL,
  teacher_id int not null,
  captain_id int not NULL,
  status tinyint NOT NULL DEFAULT 1,
  created_at datetime DEFAULT NULL,
  updated_at datetime DEFAULT NULL,
  deleted_at datetime DEFAULT NULL
)
go

CREATE TABLE group_student (
  id int identity(1,1) primary key,
  group_id int NOT NULL,
  student_id int NOT NULL,
  course_id int NOT NULL,
  teacher_id int NOT NULL,
  absent tinyint NOT NULL DEFAULT 1,
  present tinyint NOT NULL DEFAULT 0,
  learning_date date DEFAULT NULL,
  created_at datetime DEFAULT NULL,
  updated_at datetime DEFAULT NULL,
  deleted_at datetime DEFAULT NULL
)
go

CREATE TABLE roles (
  id int identity(1,1) primary key,
  name varchar(30) NOT NULL,
  description varchar(200) DEFAULT NULL,
  slug varchar(200) NOT NULL,
  status tinyint NOT NULL DEFAULT 1,
  created_at datetime DEFAULT NULL,
  updated_at datetime DEFAULT NULL,
  deleted_at datetime DEFAULT NULL
)
go

CREATE TABLE terms (
  id int identity(1,1) primary key,
  name varchar(100) NOT NULL,
  slug varchar(150) NOT NULL,
  year int NOT NULL,
  status tinyint NOT NULL DEFAULT 1,
  created_at datetime DEFAULT NULL,
  updated_at datetime DEFAULT NULL,
  deleted_at datetime DEFAULT NULL
)
go

create table schedule(
id int identity(1,1) primary key,
course_id int not null,
group_id int not null,
description varchar(200),
location varchar(150),
teacher_id int not null,
start_date date not null,
end_date date not null,
repeats int not null,
created_at datetime DEFAULT NULL,
updated_at datetime DEFAULT NULL,
deleted_at datetime DEFAULT NULL
)
go

create table schedule_user(
id int identity(1,1) primary key,
schedule_id int not null,
date date not null,
start_time time not null,
end_time time not null
)
go

alter table accounts
add constraint fk_role_id
foreign key (role_id)

references roles(id)
go

alter table accounts
add constraint fk_user_id
foreign key(user_id)
references users(id)
go

alter table courses
add constraint fk_department_id
foreign key (department_id)
references departments(id)
go

alter table groups
add constraint fk_department_id_groups
foreign key (department_id)
references departments(id)
go

alter table groups
add constraint fk_teacher_id_groups
foreign key (teacher_id)

references accounts(id)
go

alter table groups
add constraint fk_term_id
foreign key(term_id)
references terms(id)
go

alter table groups
add constraint fk_groups_captain_id
foreign key(captain_id)
references accounts(id)
go


alter table group_student
add constraint fk_group_id
foreign key (group_id)

references groups(id)
go

alter table group_student
add constraint fk_student_id
foreign key(student_id)
references accounts(id)
go

alter table group_student
add constraint fk_teacher_id
foreign key(teacher_id)
references accounts(id)
go

alter table group_student
add constraint fk_course_id
foreign key (course_id)
references courses(id)
go

alter table schedule
add constraint fk_course_schedule_id
foreign key (course_id)
references courses(id)
go

alter table schedule
add constraint fk_group_schedule_id
foreign key (group_id)
references groups(id)
go

alter table schedule
add constraint fk_teacher_schedule_id
foreign key (teacher_id)

references accounts(id)
go

alter table schedule_user
add constraint fk_schedule_id
foreign key (schedule_id)
references schedule(id)
go

alter table departments
add constraint fk_department_account_id
foreign key (leader_id)

references accounts(id)
go
