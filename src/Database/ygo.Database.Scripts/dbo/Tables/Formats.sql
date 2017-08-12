CREATE TABLE [dbo].[Formats] (
    [Id]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (255) NOT NULL,
    [Acronym] NVARCHAR (255) NOT NULL,
    [Created] DATETIME       NOT NULL,
    [Updated] DATETIME       NOT NULL,
    CONSTRAINT [PK_Formats] PRIMARY KEY CLUSTERED ([Id] ASC)
);