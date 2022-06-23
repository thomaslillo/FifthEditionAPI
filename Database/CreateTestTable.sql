USE [DND5e]
GO

-- remove the table if it already exists
--DROP TABLE [dbo].[Test_Table]

-- create the table
CREATE TABLE [dbo].[Test_Table](
	[Id] [int] NOT NULL,
	[Numbers] [int] NULL,
	[Words] [varchar](50) NULL
)
GO

-- truncate the table first if it exists
--Truncate Table [dbo].[Test_Table]

INSERT INTO  [dbo].[Test_Table] (Id, Numbers, Words)
VALUES (1, 34, 'hello'); 
GO