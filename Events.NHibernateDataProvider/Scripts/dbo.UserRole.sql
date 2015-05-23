CREATE TABLE  [DB_9C4C3F_team2project].[dbo].[UserRole](
    [Id] [int] NOT NULL IDENTITY(1,1),
    [UserId] [varchar](50) NOT NULL,
    [RoleId] [int] NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
)

GO

ALTER TABLE  [DB_9C4C3F_team2project].[dbo].[UserRole]
   WITH CHECK ADD  CONSTRAINT [FK_UserRole_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO

ALTER TABLE  [DB_9C4C3F_team2project].[dbo].[UserRole]
     WITH CHECK ADD  CONSTRAINT [FK_UserRole_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO

INSERT INTO  [DB_9C4C3F_team2project].[dbo].[UserRole] (UserId, RoleId) VALUES('af5e6fad-af59-47e1-a115-559ca960c855', 1);
GO