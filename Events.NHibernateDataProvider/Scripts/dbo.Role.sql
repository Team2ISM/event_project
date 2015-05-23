CREATE TABLE  [DB_9C4C3F_team2project].[dbo].[Roles](
    [Id] [smallint] NOT NULL IDENTITY(1,1),
    [Name] [varchar](50) NOT NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO

INSERT INTO [DB_9C4C3F_team2project].[dbo].[Roles] (Name) VALUES('Admin');
INSERT INTO [DB_9C4C3F_team2project].[dbo].[Roles] (Name) VALUES('User');