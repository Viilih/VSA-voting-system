IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Candidates] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Candidates] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Votes] (
    [Id] uniqueidentifier NOT NULL,
    [CandidateId] int NOT NULL,
    CONSTRAINT [PK_Votes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Votes_Candidates_CandidateId] FOREIGN KEY ([CandidateId]) REFERENCES [Candidates] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Votes_CandidateId] ON [Votes] ([CandidateId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250218162846_initialCreate', N'8.0.11');
GO

COMMIT;
GO

