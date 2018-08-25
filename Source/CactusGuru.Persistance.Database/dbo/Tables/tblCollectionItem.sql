CREATE TABLE [dbo].[tblCollectionItem] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [tblTaxonId]     UNIQUEIDENTIFIER NOT NULL,
    [tblCollectorId] UNIQUEIDENTIFIER NULL,
    [tblSupplierId]  UNIQUEIDENTIFIER NULL,
    [Code]           NVARCHAR (10)    NOT NULL,
    [SupplierCode]   NVARCHAR (20)    NULL,
    [Count]          INT              NULL,
    [FieldNumber]    NVARCHAR (20)    NULL,
    [Locality]       NVARCHAR (200)   NULL,
    [IncomeDate]     DATETIME         NULL,
    [IncomeType]     TINYINT          DEFAULT ((0)) NOT NULL,
    [Description]    NVARCHAR (300)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tblCollectionItem_tblCollector] FOREIGN KEY ([tblCollectorId]) REFERENCES [dbo].[tblCollector] ([Id]),
    CONSTRAINT [FK_tblCollectionItem_tblSupplier] FOREIGN KEY ([tblSupplierId]) REFERENCES [dbo].[tblSupplier] ([Id]),
    CONSTRAINT [FK_tblCollectionItem_tblTaxon] FOREIGN KEY ([tblTaxonId]) REFERENCES [dbo].[tblTaxon] ([Id])
);

