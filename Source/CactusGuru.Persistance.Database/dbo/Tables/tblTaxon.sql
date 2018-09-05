CREATE TABLE [dbo].[tblTaxon] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [tblGenusId] UNIQUEIDENTIFIER NOT NULL,
    [Species]    NVARCHAR (50)    NOT NULL,
    [Variety]    NVARCHAR (50)    NOT NULL DEFAULT '',
    [SubSpecies] NVARCHAR (50)    NOT NULL DEFAULT '',
    [Forma]      NVARCHAR (50)    NOT NULL DEFAULT '',
    [Cultivar]   NVARCHAR (50)    NOT NULL DEFAULT '',
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tblTaxon_tblGenus] FOREIGN KEY ([tblGenusId]) REFERENCES [dbo].[tblGenus] ([Id])
);

