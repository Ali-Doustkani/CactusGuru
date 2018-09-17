CREATE TABLE [dbo].[tblCollectionItemImage] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [tblCollectionItemId] UNIQUEIDENTIFIER NOT NULL,
    [Description]         NVARCHAR (500)   NULL,
    [DateAdded]           DATETIME         NOT NULL,
    [Image]               VARBINARY (MAX)  NOT NULL,
    [SharedOnInstagram] BIT NOT NULL DEFAULT 0, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tblCollectionItemImage_tblCollectionItem] FOREIGN KEY ([tblCollectionItemId]) REFERENCES [dbo].[tblCollectionItem] ([Id]) ON DELETE CASCADE
);

