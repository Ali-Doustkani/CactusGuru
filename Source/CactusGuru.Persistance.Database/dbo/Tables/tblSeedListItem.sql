CREATE TABLE [dbo].[tblSeedListItem] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [tblSeedListId]       UNIQUEIDENTIFIER NOT NULL,
    [Type]                INT              NOT NULL,
    [Code]                NVARCHAR (10)    NOT NULL,
    [StandardPocketCount] INT              NOT NULL,
    [StandardPocketPrice] INT              NOT NULL,
    [Pocket100sPrice]     INT              NULL,
    [Pocket500sPrice]     INT              NULL,
    [Pocket1000sPrice]    INT              NULL,
    [tblCollectionItemId] UNIQUEIDENTIFIER NULL,
    [tblTaxonId]          UNIQUEIDENTIFIER NULL,
    [tblSupplierId]       UNIQUEIDENTIFIER NULL,
    [SupplierCode]        NVARCHAR (20)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tblSeedListItem_tblCollectionItem] FOREIGN KEY ([tblCollectionItemId]) REFERENCES [dbo].[tblCollectionItem] ([Id]),
    CONSTRAINT [FK_tblSeedListItem_tblSeedList] FOREIGN KEY ([tblSeedListId]) REFERENCES [dbo].[tblSeedList] ([Id]),
    CONSTRAINT [FK_tblSeedListItem_tblSupplier] FOREIGN KEY ([tblSupplierId]) REFERENCES [dbo].[tblSupplier] ([Id]),
    CONSTRAINT [FK_tblSeedListItem_tblTaxon] FOREIGN KEY ([tblTaxonId]) REFERENCES [dbo].[tblTaxon] ([Id])
);

