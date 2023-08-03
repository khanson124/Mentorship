using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mentorship.Migrations
{
    /// <inheritdoc />
    public partial class Mirgations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    first_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    last_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    profile_picture = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    registration_date = table.Column<DateTime>(type: "date", nullable: true),
                    status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('active')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__B9BE370FA5FAF7C5", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Mentees",
                columns: table => new
                {
                    mentee_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    bio = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    interests = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    preferred_mentor_gender = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    preferred_mentor_age_range = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Mentees__C079F297FE4B1A9F", x => x.mentee_id);
                    table.ForeignKey(
                        name: "FK__Mentees__user_id__3B75D760",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Mentors",
                columns: table => new
                {
                    mentor_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    bio = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    area_of_expertise = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    hourly_rate = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    availability = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Mentors__E5D27EF34F0AB75A", x => x.mentor_id);
                    table.ForeignKey(
                        name: "FK__Mentors__user_id__38996AB5",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    message_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sender_id = table.Column<int>(type: "int", nullable: true),
                    receiver_id = table.Column<int>(type: "int", nullable: true),
                    message_content = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    timestamp = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Messages__0BBF6EE6F5AF8166", x => x.message_id);
                    table.ForeignKey(
                        name: "FK__Messages__receiv__4316F928",
                        column: x => x.receiver_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK__Messages__sender__4222D4EF",
                        column: x => x.sender_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "MentorMenteeAssignments",
                columns: table => new
                {
                    assignment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mentor_id = table.Column<int>(type: "int", nullable: true),
                    mentee_id = table.Column<int>(type: "int", nullable: true),
                    assigned_date = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MentorMe__DA89181451D260B3", x => x.assignment_id);
                    table.ForeignKey(
                        name: "FK__MentorMen__mente__4AB81AF0",
                        column: x => x.mentee_id,
                        principalTable: "Mentees",
                        principalColumn: "mentee_id");
                    table.ForeignKey(
                        name: "FK__MentorMen__mento__49C3F6B7",
                        column: x => x.mentor_id,
                        principalTable: "Mentors",
                        principalColumn: "mentor_id");
                });

            migrationBuilder.CreateTable(
                name: "MentorshipSessions",
                columns: table => new
                {
                    session_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mentor_id = table.Column<int>(type: "int", nullable: true),
                    mentee_id = table.Column<int>(type: "int", nullable: true),
                    start_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    end_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    session_notes = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    session_rating = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Mentorsh__69B13FDCDE92B0FA", x => x.session_id);
                    table.ForeignKey(
                        name: "FK__Mentorshi__mente__3F466844",
                        column: x => x.mentee_id,
                        principalTable: "Mentees",
                        principalColumn: "mentee_id");
                    table.ForeignKey(
                        name: "FK__Mentorshi__mento__3E52440B",
                        column: x => x.mentor_id,
                        principalTable: "Mentors",
                        principalColumn: "mentor_id");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    review_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mentor_id = table.Column<int>(type: "int", nullable: true),
                    mentee_id = table.Column<int>(type: "int", nullable: true),
                    rating = table.Column<int>(type: "int", nullable: true),
                    review_text = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    review_date = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reviews__60883D90801932CA", x => x.review_id);
                    table.ForeignKey(
                        name: "FK__Reviews__mentee___46E78A0C",
                        column: x => x.mentee_id,
                        principalTable: "Mentees",
                        principalColumn: "mentee_id");
                    table.ForeignKey(
                        name: "FK__Reviews__mentor___45F365D3",
                        column: x => x.mentor_id,
                        principalTable: "Mentors",
                        principalColumn: "mentor_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mentees_user_id",
                table: "Mentees",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_MentorMenteeAssignments_mentee_id",
                table: "MentorMenteeAssignments",
                column: "mentee_id");

            migrationBuilder.CreateIndex(
                name: "IX_MentorMenteeAssignments_mentor_id",
                table: "MentorMenteeAssignments",
                column: "mentor_id");

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
                name: "MentorMenteeAssignments");

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
