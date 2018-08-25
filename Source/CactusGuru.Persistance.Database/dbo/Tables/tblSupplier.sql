CREATE TABLE [dbo].[tblSupplier] (
    [Id]      UNIQUEIDENTIFIER NOT NULL,
    [Title]   NVARCHAR (250)   NOT NULL,
    [Acronym] NVARCHAR (10)    NULL,
    [WebSite] NVARCHAR (50)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

