namespace TechBlog.NewsManager.Tests.IntegrationTests.Fixtures
{
    public static class CreateDatabase
    {
        public const string Script = @"
            CREATE TABLE IF NOT EXISTS ""__EFMigrationsHistory"" (
                ""MigrationId"" TEXT NOT NULL CONSTRAINT ""PK___EFMigrationsHistory"" PRIMARY KEY,
                ""ProductVersion"" TEXT NOT NULL
            );
            
            BEGIN TRANSACTION;
            
            CREATE TABLE ""AspNetRoles"" (
                ""Id"" varchar(300) NOT NULL CONSTRAINT ""PK_AspNetRoles"" PRIMARY KEY,
                ""Name"" varchar(300) NULL,
                ""NormalizedName"" varchar(300) NULL,
                ""ConcurrencyStamp"" varchar(300) NULL
            );
            
            CREATE TABLE ""AspNetUsers"" (
                ""Id"" varchar(300) NOT NULL CONSTRAINT ""PK_AspNetUsers"" PRIMARY KEY,
                ""Name"" varchar(300) NOT NULL,
                ""BlogUserType"" TEXT NOT NULL,
                ""Enabled"" INTEGER NOT NULL DEFAULT 1,
                ""CreatedAt"" TEXT NOT NULL DEFAULT '2023-11-02 00:24:23.0959662',
                ""LastUpdateAt"" TEXT NOT NULL DEFAULT '2023-11-02 00:24:23.0959879',
                ""UserName"" varchar(300) NOT NULL,
                ""NormalizedUserName"" varchar(300) NULL,
                ""Email"" varchar(300) NOT NULL,
                ""NormalizedEmail"" varchar(300) NULL,
                ""EmailConfirmed"" INTEGER NOT NULL DEFAULT 1,
                ""PasswordHash"" varchar(300) NULL,
                ""SecurityStamp"" varchar(300) NULL,
                ""ConcurrencyStamp"" varchar(300) NULL,
                ""PhoneNumber"" varchar(300) NULL,
                ""PhoneNumberConfirmed"" INTEGER NOT NULL,
                ""TwoFactorEnabled"" INTEGER NOT NULL,
                ""LockoutEnd"" TEXT NULL,
                ""LockoutEnabled"" INTEGER NOT NULL,
                ""AccessFailedCount"" INTEGER NOT NULL
            );
            
            CREATE TABLE ""AspNetRoleClaims"" (
                ""Id"" INTEGER NOT NULL CONSTRAINT ""PK_AspNetRoleClaims"" PRIMARY KEY AUTOINCREMENT,
                ""RoleId"" varchar(300) NOT NULL,
                ""ClaimType"" varchar(300) NULL,
                ""ClaimValue"" varchar(300) NULL,
                CONSTRAINT ""FK_AspNetRoleClaims_AspNetRoles_RoleId"" FOREIGN KEY (""RoleId"") REFERENCES ""AspNetRoles"" (""Id"") ON DELETE CASCADE
            );
            
            CREATE TABLE ""AspNetUserClaims"" (
                ""Id"" INTEGER NOT NULL CONSTRAINT ""PK_AspNetUserClaims"" PRIMARY KEY AUTOINCREMENT,
                ""UserId"" varchar(300) NOT NULL,
                ""ClaimType"" varchar(300) NULL,
                ""ClaimValue"" varchar(300) NULL,
                CONSTRAINT ""FK_AspNetUserClaims_AspNetUsers_UserId"" FOREIGN KEY (""UserId"") REFERENCES ""AspNetUsers"" (""Id"") ON DELETE CASCADE
            );
            
            CREATE TABLE ""AspNetUserLogins"" (
                ""LoginProvider"" varchar(300) NOT NULL,
                ""ProviderKey"" varchar(300) NOT NULL,
                ""ProviderDisplayName"" varchar(300) NULL,
                ""UserId"" varchar(300) NOT NULL,
                CONSTRAINT ""PK_AspNetUserLogins"" PRIMARY KEY (""LoginProvider"", ""ProviderKey""),
                CONSTRAINT ""FK_AspNetUserLogins_AspNetUsers_UserId"" FOREIGN KEY (""UserId"") REFERENCES ""AspNetUsers"" (""Id"") ON DELETE CASCADE
            );
            
            CREATE TABLE ""AspNetUserRoles"" (
                ""UserId"" varchar(300) NOT NULL,
                ""RoleId"" varchar(300) NOT NULL,
                CONSTRAINT ""PK_AspNetUserRoles"" PRIMARY KEY (""UserId"", ""RoleId""),
                CONSTRAINT ""FK_AspNetUserRoles_AspNetRoles_RoleId"" FOREIGN KEY (""RoleId"") REFERENCES ""AspNetRoles"" (""Id"") ON DELETE CASCADE,
                CONSTRAINT ""FK_AspNetUserRoles_AspNetUsers_UserId"" FOREIGN KEY (""UserId"") REFERENCES ""AspNetUsers"" (""Id"") ON DELETE CASCADE
            );
            
            CREATE TABLE ""AspNetUserTokens"" (
                ""UserId"" varchar(300) NOT NULL,
                ""LoginProvider"" varchar(300) NOT NULL,
                ""Name"" varchar(300) NOT NULL,
                ""Value"" varchar(300) NULL,
                CONSTRAINT ""PK_AspNetUserTokens"" PRIMARY KEY (""UserId"", ""LoginProvider"", ""Name""),
                CONSTRAINT ""FK_AspNetUserTokens_AspNetUsers_UserId"" FOREIGN KEY (""UserId"") REFERENCES ""AspNetUsers"" (""Id"") ON DELETE CASCADE
            );
            
            CREATE TABLE ""BlogNew"" (
                ""Id"" TEXT NOT NULL CONSTRAINT ""PK_BlogNew"" PRIMARY KEY,
                ""Title"" varchar(300) NOT NULL,
                ""Description"" varchar(300) NOT NULL,
                ""Body"" varchar(300) NOT NULL,
                ""Tags"" varchar(300) NULL,
                ""Enabled"" INTEGER NOT NULL DEFAULT 1,
                ""BlogUserId"" varchar(300) NOT NULL,
                ""CreatedAt"" TEXT NOT NULL DEFAULT '2023-11-02 00:24:23.0962701',
                ""LastUpdateAt"" TEXT NOT NULL DEFAULT '2023-11-02 00:24:23.0962965',
                CONSTRAINT ""FK_BlogNew_AspNetUsers_BlogUserId"" FOREIGN KEY (""BlogUserId"") REFERENCES ""AspNetUsers"" (""Id"")
            );
            
            CREATE INDEX ""IX_AspNetRoleClaims_RoleId"" ON ""AspNetRoleClaims"" (""RoleId"");
            
            CREATE UNIQUE INDEX ""RoleNameIndex"" ON ""AspNetRoles"" (""NormalizedName"");
            
            CREATE INDEX ""IX_AspNetUserClaims_UserId"" ON ""AspNetUserClaims"" (""UserId"");
            
            CREATE INDEX ""IX_AspNetUserLogins_UserId"" ON ""AspNetUserLogins"" (""UserId"");
            
            CREATE INDEX ""IX_AspNetUserRoles_RoleId"" ON ""AspNetUserRoles"" (""RoleId"");
            
            CREATE INDEX ""EmailIndex"" ON ""AspNetUsers"" (""NormalizedEmail"");
            
            CREATE UNIQUE INDEX ""UserNameIndex"" ON ""AspNetUsers"" (""NormalizedUserName"");
            
            CREATE INDEX ""IX_BlogNew_BlogUserId"" ON ""BlogNew"" (""BlogUserId"");
            
            INSERT INTO ""__EFMigrationsHistory"" (""MigrationId"", ""ProductVersion"")
            VALUES ('20231102032423_Test_Database', '7.0.13');
            
            COMMIT;
            ";
    }
}
