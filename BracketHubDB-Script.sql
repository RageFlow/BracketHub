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
CREATE TABLE [Games] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Type] nvarchar(10) NOT NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_Games] PRIMARY KEY ([Id])
);

CREATE TABLE [Members] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Nickname] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Members] PRIMARY KEY ([Id])
);

CREATE TABLE [Tournaments] (
    [Id] int NOT NULL IDENTITY,
    [Type] nvarchar(10) NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Banner] nvarchar(max) NULL,
    [Date] datetime2 NULL,
    [IsPublic] bit NOT NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_Tournaments] PRIMARY KEY ([Id])
);

CREATE TABLE [Matches] (
    [Id] int NOT NULL IDENTITY,
    [Status] nvarchar(max) NOT NULL,
    [Round] int NULL,
    [MatchNumber] int NULL,
    [Winner] int NULL,
    [ChildMatchId] int NULL,
    [TournamentId] int NULL,
    CONSTRAINT [PK_Matches] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Matches_Matches_ChildMatchId] FOREIGN KEY ([ChildMatchId]) REFERENCES [Matches] ([Id]),
    CONSTRAINT [FK_Matches_Tournaments_TournamentId] FOREIGN KEY ([TournamentId]) REFERENCES [Tournaments] ([Id])
);

CREATE TABLE [MemberTournament] (
    [MembersId] int NOT NULL,
    [TournamentsId] int NOT NULL,
    CONSTRAINT [PK_MemberTournament] PRIMARY KEY ([MembersId], [TournamentsId]),
    CONSTRAINT [FK_MemberTournament_Members_MembersId] FOREIGN KEY ([MembersId]) REFERENCES [Members] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MemberTournament_Tournaments_TournamentsId] FOREIGN KEY ([TournamentsId]) REFERENCES [Tournaments] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [MatchMember] (
    [MatchesId] int NOT NULL,
    [MembersId] int NOT NULL,
    CONSTRAINT [PK_MatchMember] PRIMARY KEY ([MatchesId], [MembersId]),
    CONSTRAINT [FK_MatchMember_Matches_MatchesId] FOREIGN KEY ([MatchesId]) REFERENCES [Matches] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MatchMember_Members_MembersId] FOREIGN KEY ([MembersId]) REFERENCES [Members] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Matches_ChildMatchId] ON [Matches] ([ChildMatchId]);

CREATE INDEX [IX_Matches_TournamentId] ON [Matches] ([TournamentId]);

CREATE INDEX [IX_MatchMember_MembersId] ON [MatchMember] ([MembersId]);

CREATE INDEX [IX_MemberTournament_TournamentsId] ON [MemberTournament] ([TournamentsId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250925205811_InitialCreate', N'9.0.9');

COMMIT;
GO

