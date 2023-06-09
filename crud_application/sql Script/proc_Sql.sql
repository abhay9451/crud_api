USE [model]
GO
/****** Object:  Table [dbo].[employee]    Script Date: 11-05-2023 15:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](40) NULL,
	[email] [varchar](30) NULL,
	[gender] [varchar](10) NULL,
	[mobile] [varchar](20) NULL,
	[salary] [money] NULL,
	[address] [varchar](100) NULL,
	[emp_code]  AS ('INF'+right(CONVERT([varchar](5),[id]),(5))) PERSISTED,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblAdmin]    Script Date: 11-05-2023 15:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAdmin](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](20) NULL,
	[email] [varchar](30) NULL,
	[password] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[login_user]    Script Date: 11-05-2023 15:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[login_user]
@email varchar(100),
@password varchar(100)
as
begin
if(EXISTS(select email from tblAdmin where email=@email))
begin
select 1 as isauthenticated
end 
else
begin
select 0 isauthenticated
end
end
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_employee]    Script Date: 11-05-2023 15:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 create proc [dbo].[sp_delete_employee]
@id int
as
begin
delete employee where id = @id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_get_employee]    Script Date: 11-05-2023 15:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_get_employee]
as 
begin
select Id, name,email, gender, mobile,salary,address, emp_code from employee;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_employee]    Script Date: 11-05-2023 15:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_insert_employee]
@name varchar(40),
@email varchar(30),
@gender varchar(10),
@mobile varchar(12),
@salary money,
@address varchar(50)

as 
begin
declare @c int
select @c= count(*) from employee where email = @email
if(@c>0)
begin
 select 0 as created
 end 
 else 
 begin
 insert into employee(name,email, gender, mobile,salary,address)
 values (@name, @email,@gender,@mobile,@salary,@address)
 select 1 as created
 end
 end
GO
/****** Object:  StoredProcedure [dbo].[sp_update_employee]    Script Date: 11-05-2023 15:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_update_employee]
@id int,
@name varchar(20),
@email varchar(30),
@gender varchar(10),
@mobile varchar(20),
@salary money,
@address varchar(50)
as 
begin
update employee  set Name=@name, email= @email, gender=@gender, mobile=@mobile, salary=@salary, address= @address
where id=@id
select 1 as updated
end
GO
