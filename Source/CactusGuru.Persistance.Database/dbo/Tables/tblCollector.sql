CREATE TABLE [dbo].[tblCollector] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [Title]        NVARCHAR (250)   NOT NULL,
    [FieldAcronym] NVARCHAR (20)    NOT NULL DEFAULT '',
    [WebSite]      NVARCHAR (50)    NOT NULL DEFAULT '',
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

