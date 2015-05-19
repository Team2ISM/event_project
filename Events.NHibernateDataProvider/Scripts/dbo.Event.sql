CREATE TABLE [dbo].[Event] (
    [Id]              NVARCHAR (50) NOT NULL,
    [Title]           NVARCHAR (50) NULL,
    [Description]     NVARCHAR (50) NULL,
    [LongDescription] TEXT          NULL,
    [FromDate]        DATETIME      NULL,
    [ToDate]          DATETIME      NULL,
    [Location]        NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);