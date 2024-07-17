using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChristianBookClub.Migrations.Migrations.Views
{
    /// <inheritdoc />
    public partial class UserHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
			CREATE VIEW UsersHistoric AS
			WITH RawData AS (
				SELECT
					anu.Id as 'UserId',
					s.Id as 'SeminarId',
					s.Name,
					anu.FirstName,
					anu.SureName,
					ss.MeetingDate,
					CASE 
						WHEN DATETIME('now') > DATETIME(ss.MeetingDate) THEN 1
						ELSE 0
					END as 'Finished',
					CASE 
						WHEN ((sa.Id IS NOT NULL) AND (DATETIME('now') >= DATETIME(ss.MeetingDate) )) THEN 1
						ELSE 0
					END as 'Present'
					FROM
						Seminars s
					JOIN SeminarSchedules ss ON
						ss.SeminarId = s.Id
					JOIN SeminarRegisters sr ON
						sr.SeminarId = s.Id
					JOIN AspNetUsers anu ON
						sr.UserId = anu.Id
					LEFT JOIN SeminarAttendances sa ON
						sa.SeminarRegisterId = sr.Id
						AND sa.SeminarScheduleId = ss.Id
				)
				SELECT
					rd.UserId,
					rd.SeminarId,
					rd.Name,
					rd.FirstName,
					rd.SureName,
					SUM(rd.Finished) as 'FinishedMeetings',
					COUNT(rd.SeminarId) as 'TotalMeetings',
					CAST(((SUM(rd.Present)* 100)/ SUM(rd.SeminarId)) as FLOAT) as 'PresenceRate'
				FROM
					RawData rd
				GROUP BY
					rd.SeminarId,
					rd.UserId;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP VIEW UserHistory;");
		}
    }
}
