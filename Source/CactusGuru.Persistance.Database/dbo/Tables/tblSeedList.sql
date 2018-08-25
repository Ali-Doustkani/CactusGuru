CREATE TABLE [dbo].[tblSeedList] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (50)    NOT NULL,
    [PublishDate] DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

