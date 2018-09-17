CREATE TABLE [dbo].[tblCollectionItemFullImage]
(
	[Id] uniqueidentifier NOT NULL PRIMARY KEY, 
    [Image] VARBINARY(MAX) NOT NULL, 
    CONSTRAINT [FK_tblCollectionItemFullImage_To_tblCollectionItemImage] FOREIGN KEY (Id) REFERENCES tblCollectionItemImage(Id) ON DELETE CASCADE
)
