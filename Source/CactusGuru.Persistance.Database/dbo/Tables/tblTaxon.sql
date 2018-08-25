CREATE TABLE [dbo].[tblTaxon] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [tblGenusId] UNIQUEIDENTIFIER NOT NULL,
    [Species]    NVARCHAR (50)    NOT NULL,
    [Variety]    NVARCHAR (50)    NULL,
    [SubSpecies] NVARCHAR (50)    NULL,
    [Forma]      NVARCHAR (50)    NULL,
    [Cultivar]   NVARCHAR (50)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tblTaxon_tblGenus] FOREIGN KEY ([tblGenusId]) REFERENCES [dbo].[tblGenus] ([Id])
);

