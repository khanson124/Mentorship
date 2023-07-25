using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mentorship.Migrations
{
    /// <inheritdoc />
    public partial class Migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    username = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    profile_picture = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    registration_date = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__B9BE370FF27E283D", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Mentees",
                columns: table => new
                {
                    mentee_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    bio = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    interests = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    preferred_mentor_gender = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    preferred_mentor_age_range = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Mentees__C079F297B66B7ADD", x => x.mentee_id);
                    table.ForeignKey(
                        name: "FK__Mentees__user_id__29572725",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Mentors",
                columns: table => new
                {
                    mentor_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    bio = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    area_of_expertise = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    hourly_rate = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    availability = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Mentors__E5D27EF3521379FF", x => x.mentor_id);
                    table.ForeignKey(
                        name: "FK__Mentors__user_id__267ABA7A",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    message_id = table.Column<int>(type: "int", nullable: false),
                    sender_id = table.Column<int>(type: "int", nullable: true),
                    receiver_id = table.Column<int>(type: "int", nullable: true),
                    message_content = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    timestamp = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Messages__0BBF6EE642E1AD51", x => x.message_id);
                    table.ForeignKey(
                        name: "FK__Messages__receiv__30F848ED",
                        column: x => x.receiver_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK__Messages__sender__300424B4",
                        column: x => x.sender_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "MentorshipSessions",
                columns: table => new
                {
                    session_id = table.Column<int>(type: "int", nullable: false),
                    mentor_id = table.Column<int>(type: "int", nullable: true),
                    mentee_id = table.Column<int>(type: "int", nullable: true),
                    start_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    end_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    session_notes = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    session_rating = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Mentorsh__69B13FDCEE44AE1E", x => x.session_id);
                    table.ForeignKey(
                        name: "FK__Mentorshi__mente__2D27B809",
                        column: x => x.mentee_id,
                        principalTable: "Mentees",
                        principalColumn: "mentee_id");
                    table.ForeignKey(
                        name: "FK__Mentorshi__mento__2C3393D0",
                        column: x => x.mentor_id,
                        principalTable: "Mentors",
                        principalColumn: "mentor_id");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    review_id = table.Column<int>(type: "int", nullable: false),
                    mentor_id = table.Column<int>(type: "int", nullable: true),
                    mentee_id = table.Column<int>(type: "int", nullable: true),
                    rating = table.Column<int>(type: "int", nullable: true),
                    review_text = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    review_date = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reviews__60883D90458EA0C0", x => x.review_id);
                    table.ForeignKey(
                        name: "FK__Reviews__mentee___34C8D9D1",
                        column: x => x.mentee_id,
                        principalTable: "Mentees",
                        principalColumn: "mentee_id");
                    table.ForeignKey(
                        name: "FK__Reviews__mentor___33D4B598",
                        column: x => x.mentor_id,
                        principalTable: "Mentors",
                        principalColumn: "mentor_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mentees_user_id",
                table: "Mentees",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Mentors_user_id",
                table: "Mentors",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_MentorshipSessions_mentee_id",
                table: "MentorshipSessions",
                column: "mentee_id");

            migrationBuilder.CreateIndex(
                name: "IX_MentorshipSessions_mentor_id",
                table: "MentorshipSessions",
                column: "mentor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_receiver_id",
                table: "Messages",
                column: "receiver_id");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_sender_id",
                table: "Messages",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_mentee_id",
                table: "Reviews",
                column: "mentee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_mentor_id",
                table: "Reviews",
                column: "mentor_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MentorshipSessions");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Mentees");

            migrationBuilder.DropTable(
                name: "Mentors");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
