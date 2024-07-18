using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChristianBookClub.Migrations.Migrations.Views
{
    /// <inheritdoc />
    public partial class Views : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            CreatePublicUpcomingSeminarsView(migrationBuilder);
            CreatePrivateUpcomingSeminarsView(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP VIEW UpcomingSeminars;");
			migrationBuilder.Sql("DROP VIEW RegisteredUpcomingSeminars;");
		}

        private void CreatePublicUpcomingSeminarsView(MigrationBuilder migrateBuilder)
        {
			migrateBuilder.Sql(@"
				CREATE VIEW PublicUpcomingSeminars AS
                SELECT
	                s.Id as 'SeminarId',
	                s.Name,
	                MIN(ss.MeetingDate) as 'NextMeeting',
	                ss.MeetingDetails as 'Details',
	                s.CoverImage as 'CoverImage',
	                s.Description
                FROM
	                Seminars s
                INNER JOIN SeminarSchedules ss ON
	                ss.SeminarId = s.Id
                WHERE
	                DATETIME('now', 'localtime') <= DATETIME(ss.MeetingDate)
                GROUP BY
	                1
                ORDER BY
	                3;");
		}

		private void CreatePrivateUpcomingSeminarsView(MigrationBuilder migrateBuilder)
		{
			migrateBuilder.Sql(@"
				CREATE VIEW RegisteredUpcomingSeminars AS SELECT
                    s.Id as 'SeminarId',
                    s.Name,
                    sr.UserId,
                    DATETIME(ss.MeetingDate) as 'NextMeeting',
                    ss.MeetingDetails as 'Details',
                    ss.Id as 'ScheduleId',
                    ss.RoomId as 'RoomId',
                    s.CoverImage as 'CoverImage',
                    sr.Id as 'RegisterId',
                    s.Description
                FROM
	                Seminars s
                INNER JOIN SeminarSchedules ss ON
	                ss.SeminarId = s.Id
                INNER JOIN SeminarRegisters sr ON
	                s.Id = sr.SeminarId
                GROUP BY 1,3,4
                ORDER BY 4;");
		}
	}
}
