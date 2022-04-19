IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224715_CreateDb')
BEGIN
    CREATE TABLE [AboutOptions] (
        [Id] int NOT NULL IDENTITY,
        [Option] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_AboutOptions] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224715_CreateDb')
BEGIN
    CREATE TABLE [Abouts] (
        [Id] int NOT NULL IDENTITY,
        [Head] nvarchar(100) NOT NULL,
        [NormalContent] nvarchar(200) NOT NULL,
        [ItalicContent] nvarchar(100) NULL,
        [NormalContent2] nvarchar(255) NULL,
        [Image] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Abouts] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224715_CreateDb')
BEGIN
    CREATE TABLE [Categories] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224715_CreateDb')
BEGIN
    CREATE TABLE [ChooseRestaurants] (
        [Id] int NOT NULL IDENTITY,
        [Number] nvarchar(max) NOT NULL DEFAULT N'0',
        [CardHead] nvarchar(20) NOT NULL,
        [CardContent] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_ChooseRestaurants] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224715_CreateDb')
BEGIN
    CREATE TABLE [HomeIntros] (
        [Id] int NOT NULL IDENTITY,
        [Head] nvarchar(50) NOT NULL,
        [Content] nvarchar(255) NOT NULL,
        CONSTRAINT [PK_HomeIntros] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224715_CreateDb')
BEGIN
    CREATE TABLE [MenuImages] (
        [Id] int NOT NULL IDENTITY,
        [Image] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_MenuImages] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224715_CreateDb')
BEGIN
    CREATE TABLE [PizzaPrices] (
        [Id] int NOT NULL IDENTITY,
        [Value] int NOT NULL DEFAULT 0,
        [Content] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_PizzaPrices] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224715_CreateDb')
BEGIN
    CREATE TABLE [Positions] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Positions] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224715_CreateDb')
BEGIN
    CREATE TABLE [Reservations] (
        [Id] int NOT NULL IDENTITY,
        [FullName] nvarchar(50) NOT NULL,
        [Email] nvarchar(255) NULL,
        [Number] nvarchar(14) NOT NULL,
        [Date] nvarchar(max) NOT NULL,
        [Time] nvarchar(max) NOT NULL,
        [PeopleCount] int NOT NULL DEFAULT 0,
        [Message] nvarchar(255) NULL,
        [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
        CONSTRAINT [PK_Reservations] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224715_CreateDb')
BEGIN
    CREATE TABLE [RestaurantPhotos] (
        [Id] int NOT NULL IDENTITY,
        [Image] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_RestaurantPhotos] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224715_CreateDb')
BEGIN
    CREATE TABLE [SectionHeads] (
        [Id] int NOT NULL IDENTITY,
        [Head] nvarchar(50) NOT NULL,
        [Content] nvarchar(255) NOT NULL,
        CONSTRAINT [PK_SectionHeads] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224715_CreateDb')
BEGIN
    CREATE TABLE [Sliders] (
        [Id] int NOT NULL IDENTITY,
        [Image] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Sliders] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224715_CreateDb')
BEGIN
    CREATE TABLE [Subscribes] (
        [Id] int NOT NULL IDENTITY,
        [Email] nvarchar(255) NOT NULL,
        CONSTRAINT [PK_Subscribes] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224715_CreateDb')
BEGIN
    CREATE TABLE [Prouducts] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        [Price] decimal(18,2) NOT NULL DEFAULT 0.0,
        [MenuImageId] int NULL,
        [CategoryId] int NULL,
        [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
        CONSTRAINT [PK_Prouducts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Prouducts_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Prouducts_MenuImages_MenuImageId] FOREIGN KEY ([MenuImageId]) REFERENCES [MenuImages] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224715_CreateDb')
BEGIN
    CREATE TABLE [Specials] (
        [Id] int NOT NULL IDENTITY,
        [FoodName] nvarchar(50) NOT NULL,
        [PropHead] nvarchar(50) NOT NULL,
        [PropContent] nvarchar(255) NOT NULL,
        [MenuImageId] int NULL,
        CONSTRAINT [PK_Specials] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Specials_MenuImages_MenuImageId] FOREIGN KEY ([MenuImageId]) REFERENCES [MenuImages] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224715_CreateDb')
BEGIN
    CREATE TABLE [Feedbacks] (
        [Id] int NOT NULL IDENTITY,
        [Image] nvarchar(max) NOT NULL,
        [FullName] nvarchar(50) NOT NULL,
        [PositionId] int NOT NULL,
        [Comment] nvarchar(255) NOT NULL,
        CONSTRAINT [PK_Feedbacks] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Feedbacks_Positions_PositionId] FOREIGN KEY ([PositionId]) REFERENCES [Positions] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224715_CreateDb')
BEGIN
    CREATE TABLE [Teams] (
        [Id] int NOT NULL IDENTITY,
        [FullName] nvarchar(255) NOT NULL,
        [Image] nvarchar(max) NOT NULL,
        [PositionId] int NULL,
        CONSTRAINT [PK_Teams] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Teams_Positions_PositionId] FOREIGN KEY ([PositionId]) REFERENCES [Positions] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224715_CreateDb')
BEGIN
    CREATE INDEX [IX_Feedbacks_PositionId] ON [Feedbacks] ([PositionId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224715_CreateDb')
BEGIN
    CREATE INDEX [IX_Prouducts_CategoryId] ON [Prouducts] ([CategoryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224715_CreateDb')
BEGIN
    CREATE INDEX [IX_Prouducts_MenuImageId] ON [Prouducts] ([MenuImageId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224715_CreateDb')
BEGIN
    CREATE INDEX [IX_Specials_MenuImageId] ON [Specials] ([MenuImageId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224715_CreateDb')
BEGIN
    CREATE INDEX [IX_Teams_PositionId] ON [Teams] ([PositionId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224715_CreateDb')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220226224715_CreateDb', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224954_AlterFeedbackTable')
BEGIN
    ALTER TABLE [Feedbacks] DROP CONSTRAINT [FK_Feedbacks_Positions_PositionId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224954_AlterFeedbackTable')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Feedbacks]') AND [c].[name] = N'PositionId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Feedbacks] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Feedbacks] ALTER COLUMN [PositionId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224954_AlterFeedbackTable')
BEGIN
    ALTER TABLE [Feedbacks] ADD CONSTRAINT [FK_Feedbacks_Positions_PositionId] FOREIGN KEY ([PositionId]) REFERENCES [Positions] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226224954_AlterFeedbackTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220226224954_AlterFeedbackTable', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227090610_AlterConfigurationTable')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SectionHeads]') AND [c].[name] = N'Head');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [SectionHeads] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [SectionHeads] ALTER COLUMN [Head] nvarchar(100) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227090610_AlterConfigurationTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220227090610_AlterConfigurationTable', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227091119_AlterChooseRestaurantConfigurationTable')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ChooseRestaurants]') AND [c].[name] = N'CardContent');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [ChooseRestaurants] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [ChooseRestaurants] ALTER COLUMN [CardContent] nvarchar(255) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227091119_AlterChooseRestaurantConfigurationTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220227091119_AlterChooseRestaurantConfigurationTable', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227092443_AlterAboutConfigurationTable')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Abouts]') AND [c].[name] = N'ItalicContent');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Abouts] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Abouts] ALTER COLUMN [ItalicContent] nvarchar(200) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227092443_AlterAboutConfigurationTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220227092443_AlterAboutConfigurationTable', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227093624_AlterSectionHeadConfigurationTable')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SectionHeads]') AND [c].[name] = N'Content');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [SectionHeads] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [SectionHeads] ALTER COLUMN [Content] nvarchar(255) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227093624_AlterSectionHeadConfigurationTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220227093624_AlterSectionHeadConfigurationTable', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227105707_CreateTable')
BEGIN
    CREATE TABLE [SectionContent] (
        [Id] int NOT NULL IDENTITY,
        [Content] nvarchar(255) NULL,
        CONSTRAINT [PK_SectionContent] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227105707_CreateTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220227105707_CreateTable', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227110022_AlterTableHead')
BEGIN
    ALTER TABLE [SectionContent] DROP CONSTRAINT [PK_SectionContent];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227110022_AlterTableHead')
BEGIN
    EXEC sp_rename N'[SectionContent]', N'SectionContents';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227110022_AlterTableHead')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SectionHeads]') AND [c].[name] = N'Content');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [SectionHeads] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [SectionHeads] ALTER COLUMN [Content] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227110022_AlterTableHead')
BEGIN
    ALTER TABLE [SectionHeads] ADD [Key] nvarchar(max) NOT NULL DEFAULT N'';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227110022_AlterTableHead')
BEGIN
    ALTER TABLE [SectionContents] ADD [Key] nvarchar(max) NOT NULL DEFAULT N'';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227110022_AlterTableHead')
BEGIN
    ALTER TABLE [SectionContents] ADD CONSTRAINT [PK_SectionContents] PRIMARY KEY ([Id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227110022_AlterTableHead')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220227110022_AlterTableHead', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227110512_DeleteColumnTable')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SectionHeads]') AND [c].[name] = N'Content');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [SectionHeads] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [SectionHeads] DROP COLUMN [Content];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227110512_DeleteColumnTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220227110512_DeleteColumnTable', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227112400_DeleteTable')
BEGIN
    DROP TABLE [Sliders];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227112400_DeleteTable')
BEGIN
    ALTER TABLE [HomeIntros] ADD [Image] nvarchar(max) NOT NULL DEFAULT N'';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227112400_DeleteTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220227112400_DeleteTable', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227114511_AboutTableAddedColumn')
BEGIN
    ALTER TABLE [Abouts] ADD [Link] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227114511_AboutTableAddedColumn')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220227114511_AboutTableAddedColumn', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227200111_DeleteSectionTables')
BEGIN
    DROP TABLE [SectionContents];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227200111_DeleteSectionTables')
BEGIN
    DROP TABLE [SectionHeads];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227200111_DeleteSectionTables')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220227200111_DeleteSectionTables', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227202447_AddNewColumnSpecialTable')
BEGIN
    ALTER TABLE [Specials] ADD [PropContentItalic] nvarchar(255) NOT NULL DEFAULT N'';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227202447_AddNewColumnSpecialTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220227202447_AddNewColumnSpecialTable', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227203023_AlterCOnfigurationTableSql')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Specials]') AND [c].[name] = N'PropContent');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Specials] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [Specials] ALTER COLUMN [PropContent] nvarchar(500) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227203023_AlterCOnfigurationTableSql')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220227203023_AlterCOnfigurationTableSql', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227203656_ALterColumnSpecialTable')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Specials]') AND [c].[name] = N'PropHead');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Specials] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Specials] ALTER COLUMN [PropHead] nvarchar(100) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227203656_ALterColumnSpecialTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220227203656_ALterColumnSpecialTable', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227213138_CreateContactUsTable')
BEGIN
    CREATE TABLE [ContactUs] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        [Email] nvarchar(255) NOT NULL,
        [Subject] nvarchar(20) NULL,
        [Message] nvarchar(255) NOT NULL,
        [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
        CONSTRAINT [PK_ContactUs] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220227213138_CreateContactUsTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220227213138_CreateContactUsTable', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220228213423_AlterProductTable')
BEGIN
    ALTER TABLE [Prouducts] ADD [Description] nvarchar(255) NOT NULL DEFAULT N'';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220228213423_AlterProductTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220228213423_AlterProductTable', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220228222448_ProductPriceRelation')
BEGIN
    DROP TABLE [PizzaPrices];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220228222448_ProductPriceRelation')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Prouducts]') AND [c].[name] = N'Price');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Prouducts] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Prouducts] DROP COLUMN [Price];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220228222448_ProductPriceRelation')
BEGIN
    CREATE TABLE [Prices] (
        [Id] int NOT NULL IDENTITY,
        [Value] int NOT NULL,
        [Content] nvarchar(max) NULL,
        CONSTRAINT [PK_Prices] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220228222448_ProductPriceRelation')
BEGIN
    CREATE TABLE [ProductPrices] (
        [Id] int NOT NULL IDENTITY,
        [ProductId] int NOT NULL,
        [PriceId] int NOT NULL,
        CONSTRAINT [PK_ProductPrices] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductPrices_Prices_PriceId] FOREIGN KEY ([PriceId]) REFERENCES [Prices] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ProductPrices_Prouducts_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Prouducts] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220228222448_ProductPriceRelation')
BEGIN
    CREATE INDEX [IX_ProductPrices_PriceId] ON [ProductPrices] ([PriceId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220228222448_ProductPriceRelation')
BEGIN
    CREATE INDEX [IX_ProductPrices_ProductId] ON [ProductPrices] ([ProductId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220228222448_ProductPriceRelation')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220228222448_ProductPriceRelation', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301170714_DeleteTablePrice')
BEGIN
    DROP TABLE [ProductPrices];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301170714_DeleteTablePrice')
BEGIN
    DROP TABLE [Prices];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301170714_DeleteTablePrice')
BEGIN
    ALTER TABLE [Prouducts] ADD [Price] float NOT NULL DEFAULT 0.0E0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301170714_DeleteTablePrice')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220301170714_DeleteTablePrice', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301175318_AlterColumnProductTable')
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Prouducts]') AND [c].[name] = N'Price');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Prouducts] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [Prouducts] ALTER COLUMN [Price] decimal(18,2) NOT NULL;
    ALTER TABLE [Prouducts] ADD DEFAULT 0.0 FOR [Price];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301175318_AlterColumnProductTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220301175318_AlterColumnProductTable', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301191532_update')
BEGIN
    ALTER TABLE [Prouducts] DROP CONSTRAINT [FK_Prouducts_Categories_CategoryId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301191532_update')
BEGIN
    DROP INDEX [IX_Prouducts_CategoryId] ON [Prouducts];
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Prouducts]') AND [c].[name] = N'CategoryId');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Prouducts] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [Prouducts] ALTER COLUMN [CategoryId] int NOT NULL;
    CREATE INDEX [IX_Prouducts_CategoryId] ON [Prouducts] ([CategoryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301191532_update')
BEGIN
    ALTER TABLE [Prouducts] ADD CONSTRAINT [FK_Prouducts_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301191532_update')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220301191532_update', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220302094820_ALterColumnReservation')
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Reservations]') AND [c].[name] = N'Number');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Reservations] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [Reservations] ALTER COLUMN [Number] int NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220302094820_ALterColumnReservation')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220302094820_ALterColumnReservation', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220302193623_updateDatabaseReservationTable')
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Reservations]') AND [c].[name] = N'Number');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Reservations] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [Reservations] ALTER COLUMN [Number] nvarchar(14) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220302193623_updateDatabaseReservationTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220302193623_updateDatabaseReservationTable', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220302195601_alterCOlumnReservationTable')
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Reservations]') AND [c].[name] = N'Time');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Reservations] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [Reservations] DROP COLUMN [Time];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220302195601_alterCOlumnReservationTable')
BEGIN
    DECLARE @var15 sysname;
    SELECT @var15 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Reservations]') AND [c].[name] = N'Date');
    IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Reservations] DROP CONSTRAINT [' + @var15 + '];');
    ALTER TABLE [Reservations] ALTER COLUMN [Date] datetime2 NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220302195601_alterCOlumnReservationTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220302195601_alterCOlumnReservationTable', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220303091920_ChangeTableNameProduct')
BEGIN
    ALTER TABLE [Prouducts] DROP CONSTRAINT [FK_Prouducts_Categories_CategoryId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220303091920_ChangeTableNameProduct')
BEGIN
    ALTER TABLE [Prouducts] DROP CONSTRAINT [FK_Prouducts_MenuImages_MenuImageId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220303091920_ChangeTableNameProduct')
BEGIN
    ALTER TABLE [Prouducts] DROP CONSTRAINT [PK_Prouducts];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220303091920_ChangeTableNameProduct')
BEGIN
    EXEC sp_rename N'[Prouducts]', N'Products';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220303091920_ChangeTableNameProduct')
BEGIN
    EXEC sp_rename N'[Products].[IX_Prouducts_MenuImageId]', N'IX_Products_MenuImageId', N'INDEX';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220303091920_ChangeTableNameProduct')
BEGIN
    EXEC sp_rename N'[Products].[IX_Prouducts_CategoryId]', N'IX_Products_CategoryId', N'INDEX';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220303091920_ChangeTableNameProduct')
BEGIN
    ALTER TABLE [Products] ADD CONSTRAINT [PK_Products] PRIMARY KEY ([Id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220303091920_ChangeTableNameProduct')
BEGIN
    ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220303091920_ChangeTableNameProduct')
BEGIN
    ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_MenuImages_MenuImageId] FOREIGN KEY ([MenuImageId]) REFERENCES [MenuImages] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220303091920_ChangeTableNameProduct')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220303091920_ChangeTableNameProduct', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220307230100_addedteamColumn')
BEGIN
    ALTER TABLE [Teams] ADD [About] nvarchar(255) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220307230100_addedteamColumn')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220307230100_addedteamColumn', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220308213029_AddNewColumnReservationTable')
BEGIN
    ALTER TABLE [Products] DROP CONSTRAINT [FK_Products_MenuImages_MenuImageId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220308213029_AddNewColumnReservationTable')
BEGIN
    ALTER TABLE [Teams] DROP CONSTRAINT [FK_Teams_Positions_PositionId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220308213029_AddNewColumnReservationTable')
BEGIN
    DECLARE @var16 sysname;
    SELECT @var16 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Reservations]') AND [c].[name] = N'IsDeleted');
    IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Reservations] DROP CONSTRAINT [' + @var16 + '];');
    ALTER TABLE [Reservations] DROP COLUMN [IsDeleted];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220308213029_AddNewColumnReservationTable')
BEGIN
    DROP INDEX [IX_Teams_PositionId] ON [Teams];
    DECLARE @var17 sysname;
    SELECT @var17 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Teams]') AND [c].[name] = N'PositionId');
    IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Teams] DROP CONSTRAINT [' + @var17 + '];');
    ALTER TABLE [Teams] ALTER COLUMN [PositionId] int NOT NULL;
    CREATE INDEX [IX_Teams_PositionId] ON [Teams] ([PositionId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220308213029_AddNewColumnReservationTable')
BEGIN
    ALTER TABLE [Reservations] ADD [IsCheck] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220308213029_AddNewColumnReservationTable')
BEGIN
    ALTER TABLE [Reservations] ADD [IsClose] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220308213029_AddNewColumnReservationTable')
BEGIN
    DROP INDEX [IX_Products_MenuImageId] ON [Products];
    DECLARE @var18 sysname;
    SELECT @var18 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'MenuImageId');
    IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var18 + '];');
    ALTER TABLE [Products] ALTER COLUMN [MenuImageId] int NOT NULL;
    CREATE INDEX [IX_Products_MenuImageId] ON [Products] ([MenuImageId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220308213029_AddNewColumnReservationTable')
BEGIN
    ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_MenuImages_MenuImageId] FOREIGN KEY ([MenuImageId]) REFERENCES [MenuImages] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220308213029_AddNewColumnReservationTable')
BEGIN
    ALTER TABLE [Teams] ADD CONSTRAINT [FK_Teams_Positions_PositionId] FOREIGN KEY ([PositionId]) REFERENCES [Positions] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220308213029_AddNewColumnReservationTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220308213029_AddNewColumnReservationTable', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220308220911_ALterReservationTable')
BEGIN
    ALTER TABLE [ContactUs] ADD [SentDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220308220911_ALterReservationTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220308220911_ALterReservationTable', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220309090930_AddNewColumnSubscribeTable')
BEGIN
    ALTER TABLE [Subscribes] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220309090930_AddNewColumnSubscribeTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220309090930_AddNewColumnSubscribeTable', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220310084452_ChooseRestaurantDeleteColumn')
BEGIN
    DECLARE @var19 sysname;
    SELECT @var19 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ChooseRestaurants]') AND [c].[name] = N'Number');
    IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [ChooseRestaurants] DROP CONSTRAINT [' + @var19 + '];');
    ALTER TABLE [ChooseRestaurants] DROP COLUMN [Number];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220310084452_ChooseRestaurantDeleteColumn')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220310084452_ChooseRestaurantDeleteColumn', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311205116_SpecialTableColumnNameChanged')
BEGIN
    ALTER TABLE [Feedbacks] DROP CONSTRAINT [FK_Feedbacks_Positions_PositionId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311205116_SpecialTableColumnNameChanged')
BEGIN
    ALTER TABLE [Specials] DROP CONSTRAINT [FK_Specials_MenuImages_MenuImageId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311205116_SpecialTableColumnNameChanged')
BEGIN
    DECLARE @var20 sysname;
    SELECT @var20 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Specials]') AND [c].[name] = N'PropContent');
    IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [Specials] DROP CONSTRAINT [' + @var20 + '];');
    ALTER TABLE [Specials] DROP COLUMN [PropContent];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311205116_SpecialTableColumnNameChanged')
BEGIN
    DECLARE @var21 sysname;
    SELECT @var21 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Specials]') AND [c].[name] = N'PropContentItalic');
    IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [Specials] DROP CONSTRAINT [' + @var21 + '];');
    ALTER TABLE [Specials] DROP COLUMN [PropContentItalic];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311205116_SpecialTableColumnNameChanged')
BEGIN
    DECLARE @var22 sysname;
    SELECT @var22 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Specials]') AND [c].[name] = N'PropHead');
    IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [Specials] DROP CONSTRAINT [' + @var22 + '];');
    ALTER TABLE [Specials] DROP COLUMN [PropHead];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311205116_SpecialTableColumnNameChanged')
BEGIN
    DROP INDEX [IX_Specials_MenuImageId] ON [Specials];
    DECLARE @var23 sysname;
    SELECT @var23 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Specials]') AND [c].[name] = N'MenuImageId');
    IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [Specials] DROP CONSTRAINT [' + @var23 + '];');
    ALTER TABLE [Specials] ALTER COLUMN [MenuImageId] int NOT NULL;
    CREATE INDEX [IX_Specials_MenuImageId] ON [Specials] ([MenuImageId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311205116_SpecialTableColumnNameChanged')
BEGIN
    ALTER TABLE [Specials] ADD [InformationTabContent] nvarchar(500) NOT NULL DEFAULT N'';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311205116_SpecialTableColumnNameChanged')
BEGIN
    ALTER TABLE [Specials] ADD [InformationTabHead] nvarchar(100) NOT NULL DEFAULT N'';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311205116_SpecialTableColumnNameChanged')
BEGIN
    ALTER TABLE [Specials] ADD [InformationTabItalicContent] nvarchar(255) NOT NULL DEFAULT N'';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311205116_SpecialTableColumnNameChanged')
BEGIN
    DROP INDEX [IX_Feedbacks_PositionId] ON [Feedbacks];
    DECLARE @var24 sysname;
    SELECT @var24 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Feedbacks]') AND [c].[name] = N'PositionId');
    IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [Feedbacks] DROP CONSTRAINT [' + @var24 + '];');
    ALTER TABLE [Feedbacks] ALTER COLUMN [PositionId] int NOT NULL;
    CREATE INDEX [IX_Feedbacks_PositionId] ON [Feedbacks] ([PositionId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311205116_SpecialTableColumnNameChanged')
BEGIN
    ALTER TABLE [Feedbacks] ADD CONSTRAINT [FK_Feedbacks_Positions_PositionId] FOREIGN KEY ([PositionId]) REFERENCES [Positions] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311205116_SpecialTableColumnNameChanged')
BEGIN
    ALTER TABLE [Specials] ADD CONSTRAINT [FK_Specials_MenuImages_MenuImageId] FOREIGN KEY ([MenuImageId]) REFERENCES [MenuImages] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311205116_SpecialTableColumnNameChanged')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220311205116_SpecialTableColumnNameChanged', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220312142759_CreateSettingTable')
BEGIN
    CREATE TABLE [Settings] (
        [Id] int NOT NULL IDENTITY,
        [Key] nvarchar(max) NOT NULL,
        [Value] nvarchar(max) NOT NULL,
        [Type] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Settings] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220312142759_CreateSettingTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220312142759_CreateSettingTable', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220312223550_AddedSomeTablesIsDeletedColumn')
BEGIN
    ALTER TABLE [Teams] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220312223550_AddedSomeTablesIsDeletedColumn')
BEGIN
    ALTER TABLE [Specials] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220312223550_AddedSomeTablesIsDeletedColumn')
BEGIN
    ALTER TABLE [Positions] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220312223550_AddedSomeTablesIsDeletedColumn')
BEGIN
    ALTER TABLE [MenuImages] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220312223550_AddedSomeTablesIsDeletedColumn')
BEGIN
    ALTER TABLE [HomeIntros] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220312223550_AddedSomeTablesIsDeletedColumn')
BEGIN
    ALTER TABLE [Feedbacks] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220312223550_AddedSomeTablesIsDeletedColumn')
BEGIN
    ALTER TABLE [ChooseRestaurants] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220312223550_AddedSomeTablesIsDeletedColumn')
BEGIN
    ALTER TABLE [Categories] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220312223550_AddedSomeTablesIsDeletedColumn')
BEGIN
    ALTER TABLE [AboutOptions] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220312223550_AddedSomeTablesIsDeletedColumn')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220312223550_AddedSomeTablesIsDeletedColumn', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313155001_CreateIdentityTables')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313155001_CreateIdentityTables')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        [FullName] nvarchar(max) NULL,
        [IsActivated] bit NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313155001_CreateIdentityTables')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313155001_CreateIdentityTables')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313155001_CreateIdentityTables')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313155001_CreateIdentityTables')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313155001_CreateIdentityTables')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313155001_CreateIdentityTables')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313155001_CreateIdentityTables')
BEGIN
    CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313155001_CreateIdentityTables')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313155001_CreateIdentityTables')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313155001_CreateIdentityTables')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313155001_CreateIdentityTables')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313155001_CreateIdentityTables')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313155001_CreateIdentityTables')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220313155001_CreateIdentityTables', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313223249_CreateBasketItemTable')
BEGIN
    CREATE TABLE [BasketItems] (
        [Id] int NOT NULL IDENTITY,
        [ProductId] int NOT NULL,
        [Count] int NOT NULL,
        [Size] nvarchar(max) NULL,
        [AppUserId] nvarchar(450) NULL,
        CONSTRAINT [PK_BasketItems] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_BasketItems_AspNetUsers_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_BasketItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313223249_CreateBasketItemTable')
BEGIN
    CREATE INDEX [IX_BasketItems_AppUserId] ON [BasketItems] ([AppUserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313223249_CreateBasketItemTable')
BEGIN
    CREATE INDEX [IX_BasketItems_ProductId] ON [BasketItems] ([ProductId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313223249_CreateBasketItemTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220313223249_CreateBasketItemTable', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313224307_AdddedNewColumnBasketItemsTable')
BEGIN
    ALTER TABLE [BasketItems] ADD [Price] float NOT NULL DEFAULT 0.0E0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313224307_AdddedNewColumnBasketItemsTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220313224307_AdddedNewColumnBasketItemsTable', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220314185838_CreateTypeAndTokenBlackListTable')
BEGIN
    DECLARE @var25 sysname;
    SELECT @var25 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[BasketItems]') AND [c].[name] = N'Size');
    IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [BasketItems] DROP CONSTRAINT [' + @var25 + '];');
    ALTER TABLE [BasketItems] DROP COLUMN [Size];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220314185838_CreateTypeAndTokenBlackListTable')
BEGIN
    ALTER TABLE [BasketItems] ADD [TypeId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220314185838_CreateTypeAndTokenBlackListTable')
BEGIN
    CREATE TABLE [TokenBlackList] (
        [Id] int NOT NULL IDENTITY,
        [Token] nvarchar(max) NULL,
        CONSTRAINT [PK_TokenBlackList] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220314185838_CreateTypeAndTokenBlackListTable')
BEGIN
    CREATE TABLE [Types] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(10) NOT NULL,
        CONSTRAINT [PK_Types] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220314185838_CreateTypeAndTokenBlackListTable')
BEGIN
    CREATE INDEX [IX_BasketItems_TypeId] ON [BasketItems] ([TypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220314185838_CreateTypeAndTokenBlackListTable')
BEGIN
    ALTER TABLE [BasketItems] ADD CONSTRAINT [FK_BasketItems_Types_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [Types] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220314185838_CreateTypeAndTokenBlackListTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220314185838_CreateTypeAndTokenBlackListTable', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315051544_AddedColumnAppUser')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315051544_AddedColumnAppUser')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220315051544_AddedColumnAppUser', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201123_CreateAdressAndOrderAndFullOrderTablesAndConfigurations')
BEGIN
    CREATE TABLE [BillingAdresses] (
        [Id] int NOT NULL IDENTITY,
        [Adress] nvarchar(70) NOT NULL,
        [Phone] nvarchar(14) NOT NULL,
        [AppUserId] int NOT NULL,
        [AppUserId1] nvarchar(450) NULL,
        CONSTRAINT [PK_BillingAdresses] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_BillingAdresses_AspNetUsers_AppUserId1] FOREIGN KEY ([AppUserId1]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201123_CreateAdressAndOrderAndFullOrderTablesAndConfigurations')
BEGIN
    CREATE TABLE [FullOrders] (
        [Id] int NOT NULL IDENTITY,
        [AppUserId] int NOT NULL,
        [AppUserId1] nvarchar(450) NULL,
        [Total] float NOT NULL,
        [BillingAdressId] int NOT NULL,
        [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_FullOrders] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_FullOrders_AspNetUsers_AppUserId1] FOREIGN KEY ([AppUserId1]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_FullOrders_BillingAdresses_BillingAdressId] FOREIGN KEY ([BillingAdressId]) REFERENCES [BillingAdresses] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201123_CreateAdressAndOrderAndFullOrderTablesAndConfigurations')
BEGIN
    CREATE TABLE [Orders] (
        [Id] int NOT NULL IDENTITY,
        [ProductId] int NOT NULL,
        [Price] float NOT NULL,
        [Count] int NOT NULL,
        [TypeId] int NOT NULL,
        [FullOrderId] int NOT NULL,
        [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
        CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Orders_FullOrders_FullOrderId] FOREIGN KEY ([FullOrderId]) REFERENCES [FullOrders] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Orders_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Orders_Types_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [Types] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201123_CreateAdressAndOrderAndFullOrderTablesAndConfigurations')
BEGIN
    CREATE INDEX [IX_BillingAdresses_AppUserId1] ON [BillingAdresses] ([AppUserId1]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201123_CreateAdressAndOrderAndFullOrderTablesAndConfigurations')
BEGIN
    CREATE INDEX [IX_FullOrders_AppUserId1] ON [FullOrders] ([AppUserId1]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201123_CreateAdressAndOrderAndFullOrderTablesAndConfigurations')
BEGIN
    CREATE INDEX [IX_FullOrders_BillingAdressId] ON [FullOrders] ([BillingAdressId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201123_CreateAdressAndOrderAndFullOrderTablesAndConfigurations')
BEGIN
    CREATE INDEX [IX_Orders_FullOrderId] ON [Orders] ([FullOrderId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201123_CreateAdressAndOrderAndFullOrderTablesAndConfigurations')
BEGIN
    CREATE INDEX [IX_Orders_ProductId] ON [Orders] ([ProductId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201123_CreateAdressAndOrderAndFullOrderTablesAndConfigurations')
BEGIN
    CREATE INDEX [IX_Orders_TypeId] ON [Orders] ([TypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201123_CreateAdressAndOrderAndFullOrderTablesAndConfigurations')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220315201123_CreateAdressAndOrderAndFullOrderTablesAndConfigurations', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201850_AlterColumnTwoTable')
BEGIN
    ALTER TABLE [BillingAdresses] DROP CONSTRAINT [FK_BillingAdresses_AspNetUsers_AppUserId1];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201850_AlterColumnTwoTable')
BEGIN
    ALTER TABLE [FullOrders] DROP CONSTRAINT [FK_FullOrders_AspNetUsers_AppUserId1];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201850_AlterColumnTwoTable')
BEGIN
    ALTER TABLE [Orders] DROP CONSTRAINT [FK_Orders_Products_ProductId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201850_AlterColumnTwoTable')
BEGIN
    DROP INDEX [IX_Orders_ProductId] ON [Orders];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201850_AlterColumnTwoTable')
BEGIN
    DROP INDEX [IX_FullOrders_AppUserId1] ON [FullOrders];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201850_AlterColumnTwoTable')
BEGIN
    DROP INDEX [IX_BillingAdresses_AppUserId1] ON [BillingAdresses];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201850_AlterColumnTwoTable')
BEGIN
    DECLARE @var26 sysname;
    SELECT @var26 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FullOrders]') AND [c].[name] = N'AppUserId1');
    IF @var26 IS NOT NULL EXEC(N'ALTER TABLE [FullOrders] DROP CONSTRAINT [' + @var26 + '];');
    ALTER TABLE [FullOrders] DROP COLUMN [AppUserId1];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201850_AlterColumnTwoTable')
BEGIN
    DECLARE @var27 sysname;
    SELECT @var27 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[BillingAdresses]') AND [c].[name] = N'AppUserId1');
    IF @var27 IS NOT NULL EXEC(N'ALTER TABLE [BillingAdresses] DROP CONSTRAINT [' + @var27 + '];');
    ALTER TABLE [BillingAdresses] DROP COLUMN [AppUserId1];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201850_AlterColumnTwoTable')
BEGIN
    DECLARE @var28 sysname;
    SELECT @var28 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Orders]') AND [c].[name] = N'ProductId');
    IF @var28 IS NOT NULL EXEC(N'ALTER TABLE [Orders] DROP CONSTRAINT [' + @var28 + '];');
    ALTER TABLE [Orders] ALTER COLUMN [ProductId] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201850_AlterColumnTwoTable')
BEGIN
    ALTER TABLE [Orders] ADD [ProductId1] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201850_AlterColumnTwoTable')
BEGIN
    DECLARE @var29 sysname;
    SELECT @var29 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FullOrders]') AND [c].[name] = N'AppUserId');
    IF @var29 IS NOT NULL EXEC(N'ALTER TABLE [FullOrders] DROP CONSTRAINT [' + @var29 + '];');
    ALTER TABLE [FullOrders] ALTER COLUMN [AppUserId] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201850_AlterColumnTwoTable')
BEGIN
    DECLARE @var30 sysname;
    SELECT @var30 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[BillingAdresses]') AND [c].[name] = N'AppUserId');
    IF @var30 IS NOT NULL EXEC(N'ALTER TABLE [BillingAdresses] DROP CONSTRAINT [' + @var30 + '];');
    ALTER TABLE [BillingAdresses] ALTER COLUMN [AppUserId] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201850_AlterColumnTwoTable')
BEGIN
    CREATE INDEX [IX_Orders_ProductId1] ON [Orders] ([ProductId1]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201850_AlterColumnTwoTable')
BEGIN
    CREATE INDEX [IX_FullOrders_AppUserId] ON [FullOrders] ([AppUserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201850_AlterColumnTwoTable')
BEGIN
    CREATE INDEX [IX_BillingAdresses_AppUserId] ON [BillingAdresses] ([AppUserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201850_AlterColumnTwoTable')
BEGIN
    ALTER TABLE [BillingAdresses] ADD CONSTRAINT [FK_BillingAdresses_AspNetUsers_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201850_AlterColumnTwoTable')
BEGIN
    ALTER TABLE [FullOrders] ADD CONSTRAINT [FK_FullOrders_AspNetUsers_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201850_AlterColumnTwoTable')
BEGIN
    ALTER TABLE [Orders] ADD CONSTRAINT [FK_Orders_Products_ProductId1] FOREIGN KEY ([ProductId1]) REFERENCES [Products] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315201850_AlterColumnTwoTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220315201850_AlterColumnTwoTable', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315202835_DeleteColumnAdressTable')
BEGIN
    DECLARE @var31 sysname;
    SELECT @var31 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[BillingAdresses]') AND [c].[name] = N'Phone');
    IF @var31 IS NOT NULL EXEC(N'ALTER TABLE [BillingAdresses] DROP CONSTRAINT [' + @var31 + '];');
    ALTER TABLE [BillingAdresses] DROP COLUMN [Phone];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315202835_DeleteColumnAdressTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220315202835_DeleteColumnAdressTable', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315213146_ChangeType')
BEGIN
    ALTER TABLE [Orders] DROP CONSTRAINT [FK_Orders_Products_ProductId1];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315213146_ChangeType')
BEGIN
    DROP INDEX [IX_Orders_ProductId1] ON [Orders];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315213146_ChangeType')
BEGIN
    DECLARE @var32 sysname;
    SELECT @var32 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Orders]') AND [c].[name] = N'ProductId1');
    IF @var32 IS NOT NULL EXEC(N'ALTER TABLE [Orders] DROP CONSTRAINT [' + @var32 + '];');
    ALTER TABLE [Orders] DROP COLUMN [ProductId1];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315213146_ChangeType')
BEGIN
    DECLARE @var33 sysname;
    SELECT @var33 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Orders]') AND [c].[name] = N'ProductId');
    IF @var33 IS NOT NULL EXEC(N'ALTER TABLE [Orders] DROP CONSTRAINT [' + @var33 + '];');
    ALTER TABLE [Orders] ALTER COLUMN [ProductId] int NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315213146_ChangeType')
BEGIN
    CREATE INDEX [IX_Orders_ProductId] ON [Orders] ([ProductId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315213146_ChangeType')
BEGIN
    ALTER TABLE [Orders] ADD CONSTRAINT [FK_Orders_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315213146_ChangeType')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220315213146_ChangeType', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315233818_DeleteColumnFullOrderTable')
BEGIN
    ALTER TABLE [FullOrders] DROP CONSTRAINT [FK_FullOrders_AspNetUsers_AppUserId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315233818_DeleteColumnFullOrderTable')
BEGIN
    DROP INDEX [IX_FullOrders_AppUserId] ON [FullOrders];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315233818_DeleteColumnFullOrderTable')
BEGIN
    DECLARE @var34 sysname;
    SELECT @var34 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FullOrders]') AND [c].[name] = N'AppUserId');
    IF @var34 IS NOT NULL EXEC(N'ALTER TABLE [FullOrders] DROP CONSTRAINT [' + @var34 + '];');
    ALTER TABLE [FullOrders] DROP COLUMN [AppUserId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220315233818_DeleteColumnFullOrderTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220315233818_DeleteColumnFullOrderTable', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220318161638_ChangeCOlumnDatabase')
BEGIN
    ALTER TABLE [FullOrders] DROP CONSTRAINT [FK_FullOrders_BillingAdresses_BillingAdressId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220318161638_ChangeCOlumnDatabase')
BEGIN
    DROP TABLE [BillingAdresses];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220318161638_ChangeCOlumnDatabase')
BEGIN
    DROP INDEX [IX_FullOrders_BillingAdressId] ON [FullOrders];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220318161638_ChangeCOlumnDatabase')
BEGIN
    DECLARE @var35 sysname;
    SELECT @var35 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FullOrders]') AND [c].[name] = N'BillingAdressId');
    IF @var35 IS NOT NULL EXEC(N'ALTER TABLE [FullOrders] DROP CONSTRAINT [' + @var35 + '];');
    ALTER TABLE [FullOrders] DROP COLUMN [BillingAdressId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220318161638_ChangeCOlumnDatabase')
BEGIN
    ALTER TABLE [FullOrders] ADD [AppUserId] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220318161638_ChangeCOlumnDatabase')
BEGIN
    ALTER TABLE [FullOrders] ADD [BillingAdress] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220318161638_ChangeCOlumnDatabase')
BEGIN
    CREATE INDEX [IX_FullOrders_AppUserId] ON [FullOrders] ([AppUserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220318161638_ChangeCOlumnDatabase')
BEGIN
    ALTER TABLE [FullOrders] ADD CONSTRAINT [FK_FullOrders_AspNetUsers_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220318161638_ChangeCOlumnDatabase')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220318161638_ChangeCOlumnDatabase', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220318192218_addedNewColumnDbFullOrder')
BEGIN
    ALTER TABLE [FullOrders] ADD [Status] nvarchar(max) NOT NULL DEFAULT N'';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220318192218_addedNewColumnDbFullOrder')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220318192218_addedNewColumnDbFullOrder', N'3.1.0');
END;

GO

