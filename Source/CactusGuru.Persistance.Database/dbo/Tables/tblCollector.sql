CREATE TABLE [dbo].[tblCollector] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [Title]        NVARCHAR (250)   NOT NULL,
    [FieldAcronym] NVARCHAR (20)    NULL,
    [WebSite]      NVARCHAR (50)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

