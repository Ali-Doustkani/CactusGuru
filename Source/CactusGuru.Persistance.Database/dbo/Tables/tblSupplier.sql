CREATE TABLE [dbo].[tblSupplier] (
    [Id]      UNIQUEIDENTIFIER NOT NULL,
    [Title]   NVARCHAR (250)   NOT NULL,
    [Acronym] NVARCHAR (10)    NOT NULL DEFAULT '',
    [WebSite] NVARCHAR (50)    NOT NULL DEFAULT '',
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

