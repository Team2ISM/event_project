CREATE TABLE [dbo].[Comment] (
    [Id]              NVARCHAR (50) NOT NULL,
    [EventId]           NVARCHAR (50) NULL,
    [AuthorName]     NVARCHAR (50) NULL,
    [Text] TEXT          NULL,
    [PostingDate]        DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);