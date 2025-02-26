using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppShareOn.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hashtags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Tag = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    PlatformHashtagId = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hashtags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Platforms",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    AppId = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    ApiEndpoint = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platforms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Walls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Walls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    Likes = table.Column<int>(type: "INTEGER", nullable: true),
                    Comments = table.Column<int>(type: "INTEGER", nullable: true),
                    PlatformId = table.Column<string>(type: "TEXT", nullable: false),
                    PlatformPostId = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    PostedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Platforms_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PlatformId = table.Column<string>(type: "TEXT", nullable: false),
                    ProfileHandle = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    PlatformUserId = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    Token = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Platforms_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WallsToHashtagsMapping",
                columns: table => new
                {
                    HashtagsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    WallsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WallsToHashtagsMapping", x => new { x.HashtagsId, x.WallsId });
                    table.ForeignKey(
                        name: "FK_WallsToHashtagsMapping_Hashtags_HashtagsId",
                        column: x => x.HashtagsId,
                        principalTable: "Hashtags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WallsToHashtagsMapping_Walls_WallsId",
                        column: x => x.WallsId,
                        principalTable: "Walls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HashtagsToPostsMapping",
                columns: table => new
                {
                    HashtagsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PostsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HashtagsToPostsMapping", x => new { x.HashtagsId, x.PostsId });
                    table.ForeignKey(
                        name: "FK_HashtagsToPostsMapping_Hashtags_HashtagsId",
                        column: x => x.HashtagsId,
                        principalTable: "Hashtags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HashtagsToPostsMapping_Posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WallsToProfilesMapping",
                columns: table => new
                {
                    ProfilesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    WallsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WallsToProfilesMapping", x => new { x.ProfilesId, x.WallsId });
                    table.ForeignKey(
                        name: "FK_WallsToProfilesMapping_Profiles_ProfilesId",
                        column: x => x.ProfilesId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WallsToProfilesMapping_Walls_WallsId",
                        column: x => x.WallsId,
                        principalTable: "Walls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HashtagsToPostsMapping_PostsId",
                table: "HashtagsToPostsMapping",
                column: "PostsId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PlatformId",
                table: "Posts",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_PlatformId",
                table: "Profiles",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_WallsToHashtagsMapping_WallsId",
                table: "WallsToHashtagsMapping",
                column: "WallsId");

            migrationBuilder.CreateIndex(
                name: "IX_WallsToProfilesMapping_WallsId",
                table: "WallsToProfilesMapping",
                column: "WallsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HashtagsToPostsMapping");

            migrationBuilder.DropTable(
                name: "WallsToHashtagsMapping");

            migrationBuilder.DropTable(
                name: "WallsToProfilesMapping");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Hashtags");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Walls");

            migrationBuilder.DropTable(
                name: "Platforms");
        }
    }
}
