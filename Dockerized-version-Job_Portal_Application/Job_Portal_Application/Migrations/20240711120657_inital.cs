using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Job_Portal_Application.Migrations
{
    /// <inheritdoc />
    public partial class inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Credential",
                columns: table => new
                {
                    CredentialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    HasCode = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credential", x => x.CredentialId);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillId);
                });

            migrationBuilder.CreateTable(
                name: "Titles",
                columns: table => new
                {
                    TitleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titles", x => x.TitleId);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyDescription = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    CredentialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanySize = table.Column<int>(type: "int", nullable: true),
                    CompanyWebsite = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_Companies_Credential_CredentialId",
                        column: x => x.CredentialId,
                        principalTable: "Credential",
                        principalColumn: "CredentialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AboutMe = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    CredentialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Dob = table.Column<DateOnly>(type: "date", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PortfolioLink = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phonenumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResumeUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Credential_CredentialId",
                        column: x => x.CredentialId,
                        principalTable: "Credential",
                        principalColumn: "CredentialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExperienceRequired = table.Column<float>(type: "real", nullable: true),
                    Lpa = table.Column<float>(type: "real", nullable: true),
                    DatePosted = table.Column<DateOnly>(type: "date", nullable: false),
                    JobDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.JobId);
                    table.ForeignKey(
                        name: "FK_Jobs_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jobs_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "TitleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AreasOfInterests",
                columns: table => new
                {
                    AreasOfInterestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Lpa = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreasOfInterests", x => x.AreasOfInterestId);
                    table.ForeignKey(
                        name: "FK_AreasOfInterests_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "TitleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AreasOfInterests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    EducationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartYear = table.Column<DateOnly>(type: "date", nullable: false),
                    EndYear = table.Column<DateOnly>(type: "date", nullable: true),
                    Percentage = table.Column<float>(type: "real", nullable: false),
                    InstitutionName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsCurrentlyStudying = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.EducationId);
                    table.ForeignKey(
                        name: "FK_Educations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    ExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TitleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartYear = table.Column<DateOnly>(type: "date", nullable: false),
                    EndYear = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.ExperienceId);
                    table.ForeignKey(
                        name: "FK_Experiences_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "TitleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Experiences_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSkills",
                columns: table => new
                {
                    UserSkillsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSkills", x => x.UserSkillsId);
                    table.ForeignKey(
                        name: "FK_UserSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSkills_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobActivities",
                columns: table => new
                {
                    JobApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResumeViewed = table.Column<bool>(type: "bit", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppliedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobActivities", x => x.JobApplicationId);
                    table.ForeignKey(
                        name: "FK_JobActivities_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobActivities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSkills",
                columns: table => new
                {
                    JobSkillsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSkills", x => x.JobSkillsId);
                    table.ForeignKey(
                        name: "FK_JobSkills_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Credential",
                columns: new[] { "CredentialId", "HasCode", "Password", "Role" },
                values: new object[] { new Guid("bf0e4d0f-f8d4-4bb5-839e-2f34d9f6c6a4"), new byte[] { 199, 169, 68, 243, 93, 188, 147, 138, 101, 130, 72, 82, 48, 246, 37, 31, 223, 224, 206, 80, 121, 169, 139, 127, 47, 62, 177, 54, 143, 215, 23, 212, 20, 169, 16, 26, 81, 116, 195, 123, 55, 30, 27, 220, 45, 96, 231, 110, 77, 17, 145, 231, 93, 33, 154, 123, 134, 136, 69, 95, 179, 106, 149, 93, 126, 219, 208, 19, 222, 240, 12, 94, 163, 207, 136, 20, 237, 83, 19, 21, 175, 9, 44, 180, 107, 60, 141, 188, 182, 168, 65, 92, 236, 43, 163, 169, 244, 98, 228, 208, 164, 250, 223, 190, 29, 12, 184, 106, 210, 13, 48, 224, 241, 191, 135, 178, 198, 102, 224, 219, 178, 96, 253, 115, 142, 5, 48, 7 }, new byte[] { 134, 175, 38, 11, 2, 167, 225, 82, 225, 193, 25, 153, 228, 204, 18, 163, 84, 161, 34, 59, 15, 188, 155, 183, 39, 94, 140, 210, 58, 20, 46, 67, 129, 185, 133, 31, 217, 166, 38, 24, 244, 166, 253, 103, 193, 114, 70, 71, 138, 251, 161, 225, 16, 89, 120, 63, 212, 237, 215, 15, 136, 98, 55, 236 }, "Admin" });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "SkillId", "SkillName" },
                values: new object[,]
                {
                    { new Guid("08d72cc1-1ae2-4648-86ad-3adabc19c591"), "Objective-C" },
                    { new Guid("0eef01ea-2f47-48fb-a23e-acf42fe3c367"), "Puppet" },
                    { new Guid("1f0925ad-4897-4be4-a78e-63b9aac5fe02"), "AWS" },
                    { new Guid("21f984db-b474-41ef-861d-edb490d85991"), "Customer Service" },
                    { new Guid("26dd9ab7-bdcd-40e9-b47e-0875a733996e"), "Express" },
                    { new Guid("272bf54a-9557-456b-bf69-b59505d40554"), "Angular" },
                    { new Guid("290ce652-9058-4c39-b2c2-2da9a4f8c5b9"), "Adaptability" },
                    { new Guid("2b89ed39-4a32-402d-8544-f3770fb3964f"), "Rails" },
                    { new Guid("2e283682-b22d-4f84-9b9e-e7a129afd59f"), "Attention to Detail" },
                    { new Guid("2f010977-665f-42d8-9b4b-385e387eb488"), "SQL" },
                    { new Guid("2f5c89da-f94a-4158-96b7-6b8a85f1ad8b"), "Redux" },
                    { new Guid("33a1bdb7-382b-4e21-a656-dfe55fa2423d"), "Rust" },
                    { new Guid("3887993f-4914-40c0-bb82-0ceb408ed842"), "Time Management" },
                    { new Guid("39ba5f0a-0afb-47eb-959e-6af9bedcff94"), "Cucumber" },
                    { new Guid("3c6b7582-f0a9-434d-91f0-ec69fd136703"), "Kubernetes" },
                    { new Guid("3cf4ec48-f487-4b72-8508-e23588be7b03"), "Vue" },
                    { new Guid("42dac450-619a-4191-9ef5-5abe5ad3232f"), "PowerShell" },
                    { new Guid("44a9b731-72b2-4cf9-8f84-9d4a250c704e"), "Terraform" },
                    { new Guid("4500f23c-8c15-477d-83a0-316c5efa5b21"), "Babel" },
                    { new Guid("46560cbc-ce1a-401b-bbde-851c00f3e066"), "Chef" },
                    { new Guid("465df5a5-b3c0-4197-87b3-d593c9d20dde"), "Ruby" },
                    { new Guid("48f24c77-8a9a-4340-bfb7-aaf99f4089e8"), "C++" },
                    { new Guid("4c8d4d51-73d0-4a81-bbb5-ec88fd707d5c"), "Django" },
                    { new Guid("4dc9ab64-9344-44fc-a843-c807f8b9ca92"), "Leadership" },
                    { new Guid("51a34456-5f21-4211-8f82-73f531989268"), "Subversion" },
                    { new Guid("55972392-507a-44ec-98a6-fc5d826528b9"), "C" },
                    { new Guid("5a3c9b0f-fb39-4d64-aab1-923931762b08"), "Jest" },
                    { new Guid("5df32bce-0f27-45be-8927-b4c4c5580645"), "LESS" },
                    { new Guid("5e67dc71-9cdb-420c-aa38-275efc8311a2"), "Teamwork" },
                    { new Guid("625ab532-ebca-4b03-ad7f-98f9345577c3"), "HTML" },
                    { new Guid("626a9ace-6dc1-4b0d-ba4a-03b4e844a870"), "Flask" },
                    { new Guid("64ad8798-0131-4280-8a07-dad82b00ceba"), "Swift" },
                    { new Guid("683d8bd3-2226-498f-9f6b-5e37f3e1ea6c"), "Mercurial" },
                    { new Guid("707f6128-094c-4a8d-96a8-b122fa1e7fb4"), "Perl" },
                    { new Guid("729f5973-0dc2-4547-a571-5ed26aa9ffef"), "Creativity" },
                    { new Guid("74734376-54d0-4a0d-a2fd-ba480e00ff19"), "Ansible" },
                    { new Guid("7a55a442-c8d3-4a70-a889-2eca7cd5e302"), "TypeScript" },
                    { new Guid("812ade95-7da8-4c00-8e7c-93603a8b7efa"), "Kotlin" },
                    { new Guid("8db9bfc6-2218-494d-8067-766d39a1387a"), "Chai" },
                    { new Guid("8e81710f-cacf-41b8-8195-63ecf8ea60fa"), "JavaScript" },
                    { new Guid("8fad9c4b-b629-41cc-9cd1-07039603d63f"), "Travis CI" },
                    { new Guid("99395646-971c-44ed-b304-d897622109d2"), "Azure" },
                    { new Guid("99ee399d-af03-42cf-927c-9db221c3a22f"), "Problem-Solving" },
                    { new Guid("9d2a8c5c-2547-4a29-8b71-17f3d0556934"), "Jenkins" },
                    { new Guid("a4dc887b-1d5d-495e-a27f-ae5e3a84d2c5"), "Scrum" },
                    { new Guid("a5df2f64-7237-46fd-ab6a-a0d9317c84d5"), "Git" },
                    { new Guid("a72968e2-2895-4824-820d-d2e3d4240cc9"), "CircleCI" },
                    { new Guid("aae70555-78ec-415d-b5f3-42afc862d29d"), "Negotiation" },
                    { new Guid("b15edd52-bbad-4492-bd53-4606abcd2eae"), "Prometheus" },
                    { new Guid("b17bf8fc-8ea0-4379-9075-e79743d5953e"), "React" },
                    { new Guid("b5466c47-e190-4b4e-b0ea-b9f5b959ef64"), "Spring" },
                    { new Guid("b7936cf7-ec0d-4ce0-9733-f0e9f1235b5a"), "Node.js" },
                    { new Guid("b8dd36b5-eeb5-43ac-9bcd-b16533475258"), "Java" },
                    { new Guid("b9150130-5db7-4e76-9cd6-7959ca3be7ec"), "MobX" },
                    { new Guid("bae23ea9-5982-44d2-834d-5323bc77b8b2"), "CSS" },
                    { new Guid("c1afa4d7-b212-4b22-b885-14739115dc5d"), "Critical Thinking" },
                    { new Guid("c7252508-3c2b-47cb-a925-2cf07b056107"), "PHP" },
                    { new Guid("ca10ea06-72e3-439e-b659-c9748ba0ff62"), "Mocha" },
                    { new Guid("cba06e99-5a11-4f0f-a8a6-26ae35fcf3be"), "Splunk" },
                    { new Guid("cfa91f61-c2bd-4d4d-85e3-ba976fd50ed7"), "Communication" },
                    { new Guid("d607d5c7-1761-4bc6-85eb-2cfaf977e900"), "Shell Scripting" },
                    { new Guid("d69d35f8-41d4-400f-a490-dfabd0d67d51"), "Python" },
                    { new Guid("d87d6309-278e-4a4e-a8e1-2b3312917579"), "SASS" },
                    { new Guid("d93a9f84-b86b-4323-926f-edab2967ecb2"), "GCP" },
                    { new Guid("d9d6bf94-f11a-4447-afa9-97b2a013dc33"), "NoSQL" },
                    { new Guid("e121d852-5a9a-4607-bbbd-586630d7dc85"), "Grafana" },
                    { new Guid("e4271448-d0fc-4c55-938c-1bd838e27030"), "Project Management" },
                    { new Guid("e7503659-82db-42b4-afd5-5a4dc82dad33"), "Webpack" },
                    { new Guid("e8afb451-acdb-403a-8adc-d784b2e1309a"), "Conflict Resolution" },
                    { new Guid("ecc4e664-10cf-4944-9a0c-f70487108470"), "GraphQL" },
                    { new Guid("eec65160-80f1-4310-beee-9fe3eb9e0006"), "ELK Stack" },
                    { new Guid("f2d48105-cbce-444b-b2c6-d49417c3d00e"), "Go" },
                    { new Guid("f5209920-d986-482e-9a51-92de4e22a3d0"), "Docker" },
                    { new Guid("f69de935-2135-473a-9865-11bbd3155a62"), "Kanban" },
                    { new Guid("f6b620ad-9b20-4fb6-ae61-f50cd04a8dea"), "C#" },
                    { new Guid("f72a8417-c796-4728-b601-90c87d031e6e"), "Agile Methodology" },
                    { new Guid("f8fc1147-22ad-4072-9095-ca12d489fc5c"), "ASP.NET" }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "TitleId", "TitleName" },
                values: new object[,]
                {
                    { new Guid("02234fd3-23e8-4ae8-a1c8-36d110492232"), "Machine Learning Engineer" },
                    { new Guid("02c4d41a-47b8-4f06-90dc-891c76b908ce"), "IT Project Manager" },
                    { new Guid("03551ca9-f4f0-4b3c-b440-75645b3e5636"), "Security Engineer" },
                    { new Guid("0652c58d-33ed-420a-8d17-e638c095c335"), "Big Data Engineer" },
                    { new Guid("06a3aec8-1b3b-4ca0-9dba-7488f579ce53"), "Hadoop Developer" },
                    { new Guid("09097183-5edb-4e2b-bd5b-89d1197dc97d"), "IT Director" },
                    { new Guid("0e26d9df-fe99-43b3-ad9a-efb5aea4406a"), "Technical Lead" },
                    { new Guid("0ffb7ba9-34ea-4274-af7f-a6c74d6f50cd"), "UX Researcher" },
                    { new Guid("11cf87ad-e59c-4c96-8882-b078b71376d4"), "Mobile Developer" },
                    { new Guid("1253117d-c1dd-4e07-b970-383f741148eb"), "Chief Information Officer" },
                    { new Guid("12eb90b8-5d6c-457b-9906-4ba820efb28f"), "Bioinformatics Specialist" },
                    { new Guid("1a2b81dc-eb29-4b5f-a5c8-af1467f14960"), "Blockchain Developer" },
                    { new Guid("201d4509-a9c8-4fd4-97c1-8c27b388bac0"), "Embedded Systems Engineer" },
                    { new Guid("22faaa14-baf4-42f4-8382-e6ec74de4c4e"), "Scrum Master" },
                    { new Guid("234a5a98-6b7a-4897-bd15-80a29ab4156f"), "Security Analyst" },
                    { new Guid("2c42e7e9-57a3-4f79-ab43-c48ef1b8b8fb"), "Data Engineer" },
                    { new Guid("2f17b38b-fdae-4dbd-8ff6-4a7e5f4ac77c"), "IT Director" },
                    { new Guid("328607f7-7355-4ae1-b5a2-591e4abdfa18"), "IoT Engineer" },
                    { new Guid("340ab93c-4a0c-4899-8c96-dc56ae26c09f"), "Enterprise Systems Manager" },
                    { new Guid("347da339-c387-4220-b2e3-5ba09da5c192"), "Technical Support Engineer" },
                    { new Guid("359ba66a-1759-45a3-85c7-9f795e90a87c"), "UI/UX Designer" },
                    { new Guid("394da39c-3511-4f2d-a42c-2dd41f11cb68"), "Chief Information Officer" },
                    { new Guid("3bdd160e-9a98-4ce4-8432-7f4bd071fc9a"), "Agile Coach" },
                    { new Guid("3f25f95d-cbb9-4ae2-83fd-c1c6a599e693"), "IT Operations Manager" },
                    { new Guid("402c823d-a968-42ae-978d-0a0a20eb9dd8"), "Help Desk Technician" },
                    { new Guid("42ddc4b1-d3fb-486b-9604-220457955a27"), "Data Analyst" },
                    { new Guid("44acd124-2635-421e-8d1f-48de814ed77b"), "Natural Language Processing Engineer" },
                    { new Guid("450f99fc-ed12-4be0-9e98-fb1f9244e97d"), "Back End Developer" },
                    { new Guid("460d89b4-db80-4617-bca2-f513e439369c"), "Database Administrator" },
                    { new Guid("499b4652-7fc4-4d31-8318-7b4abec586be"), "Software Architect" },
                    { new Guid("4d2629f5-df58-4530-bde0-e277629dbc1b"), "Network Engineer" },
                    { new Guid("55b4ecc7-29a2-493d-b010-96f1cc499055"), "Product Manager" },
                    { new Guid("56ebadd3-e410-4a12-8c03-dca29a1ebc2c"), "Cloud Engineer" },
                    { new Guid("57434753-8ad0-4fed-9b61-90f8a6575090"), "IT Manager" },
                    { new Guid("57cca890-24c5-44ea-a6f0-0bc907fdc374"), "Database Developer" },
                    { new Guid("5937378f-aee6-4db0-913f-1add2289d292"), "Business Systems Analyst" },
                    { new Guid("5a2c5b99-4c24-4e07-a485-cfe2175aba89"), "Digital Forensics Specialist" },
                    { new Guid("5bba5873-a5e8-4952-8b49-3b0c38eb02d4"), "Systems Analyst" },
                    { new Guid("5c7ad542-f866-4dbf-a987-17efbe36cf15"), "DevOps Engineer" },
                    { new Guid("61c0e3f2-c785-4c1b-b29e-61bc15e1a6a9"), "Ethical Hacker" },
                    { new Guid("62a41323-7693-4eba-b6e2-dc9a1f92e0fa"), "Full Stack Developer" },
                    { new Guid("6fea5776-381f-4c67-b513-0bd08bff5db0"), "Data Scientist" },
                    { new Guid("74798d5d-55d1-4277-b02a-a2c4b4f606c0"), "Site Reliability Engineer" },
                    { new Guid("757fada1-34ac-485e-938b-6c98d1d4e20c"), "Data Privacy Officer" },
                    { new Guid("759ac35b-adc6-40ef-bb9f-0bea5bcd5d56"), "Chief Data Officer" },
                    { new Guid("77229507-45d7-4c22-8cc1-1596e157dc90"), "Hardware Engineer" },
                    { new Guid("7c0ab400-d1ec-49a2-b533-484be6bfa8c1"), "Game Developer" },
                    { new Guid("7c22670b-a233-4452-ba4f-68787ef3d185"), "Build Engineer" },
                    { new Guid("7e9b8c2c-1ac9-48b1-9ad2-f1dfbe630908"), "VR/AR Developer" },
                    { new Guid("8020a6cb-489c-4a7b-9576-6632a15e7913"), "Software Engineer" },
                    { new Guid("82e2c5b1-1cae-40ce-a5bd-0df048a82a95"), "Business Analyst" },
                    { new Guid("8ba56c3f-4e68-448c-8538-32a66e5aa07f"), "Robotics Engineer" },
                    { new Guid("8bf99458-5447-4ff6-806d-e7e0f9f3e6eb"), "Information Technology Specialist" },
                    { new Guid("8da1213b-cb51-48c7-b1fb-6d4a45616e65"), "Enterprise Architect" },
                    { new Guid("911d4aa9-52c3-4995-9e19-f31c800efe5e"), "IT Procurement Manager" },
                    { new Guid("931ace33-da72-43b4-ae30-b877572c5cf1"), "IT Project Coordinator" },
                    { new Guid("95219e34-d316-4a94-8003-67585e27bc72"), "ERP Consultant" },
                    { new Guid("99706fbc-89c9-458c-9396-cc7d16f64ff6"), "Computer Vision Engineer" },
                    { new Guid("9ad9a565-b662-4ea6-982a-3631928411c3"), "Project Manager" },
                    { new Guid("9d3ddbe6-ccdb-41bd-99f7-ac876296a783"), "Chief Information Security Officer" },
                    { new Guid("a271982f-4f51-4680-8704-99e9476b02a4"), "Penetration Tester" },
                    { new Guid("a2cbb931-e64b-48d3-b52a-2e5803ec725f"), "Systems Integrator" },
                    { new Guid("a58b298e-bd0e-4a82-8aa4-74cd3894d9af"), "Site Engineer" },
                    { new Guid("a73edaf1-d1a1-4de0-85e5-ef833a26d181"), "Solution Architect" },
                    { new Guid("acb8b242-4183-4a4c-9db5-33f900f5f4e1"), "IT Auditor" },
                    { new Guid("c12752c1-a83a-4ec2-800c-d54ffaf69bb4"), "Configuration Manager" },
                    { new Guid("c7f99d8b-24f0-4f24-8609-0fbea045892e"), "Artificial Intelligence Engineer" },
                    { new Guid("ce9c3d65-0a30-4231-ba58-ac61e503e563"), "Quality Assurance Manager" },
                    { new Guid("d49a1ec3-8602-4c65-9de3-7b26766a32e2"), "QA Engineer" },
                    { new Guid("d6f02ea4-bd0d-4710-a454-b38403294fc8"), "Release Manager" },
                    { new Guid("d9ac57da-772e-4505-b0e6-91b803c71860"), "Compliance Analyst" },
                    { new Guid("ddccaf80-4aec-4f7e-a51c-75560ad7c209"), "CRM Developer" },
                    { new Guid("ddcd1966-dc1d-4fa6-9ce5-d9dc959a8773"), "Firmware Engineer" },
                    { new Guid("de2af90b-ca85-4c26-8b54-f73b2a8fc5aa"), "IT Support Specialist" },
                    { new Guid("e014b4f5-2f41-40a2-9d11-a4028f8d9e35"), "Cloud Architect" },
                    { new Guid("e259ef42-5184-4b54-be4d-f4e0bcf9f2bc"), "Application Support Engineer" },
                    { new Guid("e3b6a6ec-9f47-4c50-9b2b-af4b072e1884"), "Technical Writer" },
                    { new Guid("e4cc1e13-9d4d-4530-8e97-68d47eb99d66"), "Cryptographer" },
                    { new Guid("e4fb5696-a239-43bb-87f5-a8f2e9b13441"), "DevSecOps Engineer" },
                    { new Guid("e611bfb4-3152-41c1-a7bc-b97874847d91"), "Telecommunications Specialist" },
                    { new Guid("e6812a2c-4c66-42f3-97fa-b729ffe94efb"), "Software Development Manager" },
                    { new Guid("e7eb25a5-17f7-49df-ba36-4297b6758425"), "IT Consultant" },
                    { new Guid("e8f66c9b-597e-4954-8ff2-0d6ad19c4f46"), "Network Architect" },
                    { new Guid("ec62eda3-8d8d-465d-9cec-08a233439d60"), "Engineering Manager" },
                    { new Guid("f167b948-7861-4091-81ea-7c9cb2b12e62"), "Business Intelligence Developer" },
                    { new Guid("f2a7d6c6-6e8c-4320-a793-948b00b31d66"), "Incident Response Analyst" },
                    { new Guid("f74604db-11db-46a6-8475-fbcdc3aa2d4c"), "IT Trainer" },
                    { new Guid("f83f8e5c-7dc7-457c-9dd5-4d3beb716a89"), "Front End Developer" },
                    { new Guid("f842b94c-7c63-4a58-b9a0-f864e603921f"), "Cloud Security Engineer" },
                    { new Guid("f88e2c5f-2ffa-4b04-9df6-cd05e3ccdd07"), "Chief Technology Officer" },
                    { new Guid("f8d4eb69-f4ce-4d1c-a107-5f093014b786"), "Systems Administrator" },
                    { new Guid("fd842360-4729-4888-bd48-19b270b389f4"), "Information Security Analyst" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AboutMe", "Address", "City", "CredentialId", "Dob", "Email", "Name", "Phonenumber", "PortfolioLink", "ProfilePictureUrl", "ResumeUrl" },
                values: new object[] { new Guid("415fec10-1fe1-4334-86fe-810a925df1c6"), null, null, null, new Guid("bf0e4d0f-f8d4-4bb5-839e-2f34d9f6c6a4"), new DateOnly(1, 1, 1), "Admin@jobportal.com", "Admin", null, null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_AreasOfInterests_TitleId",
                table: "AreasOfInterests",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_AreasOfInterests_UserId",
                table: "AreasOfInterests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CredentialId",
                table: "Companies",
                column: "CredentialId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Educations_UserId",
                table: "Educations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_TitleId",
                table: "Experiences",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_UserId",
                table: "Experiences",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobActivities_JobId",
                table: "JobActivities",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobActivities_UserId",
                table: "JobActivities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CompanyId",
                table: "Jobs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_TitleId",
                table: "Jobs",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkills_JobId",
                table: "JobSkills",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkills_SkillId",
                table: "JobSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CredentialId",
                table: "Users",
                column: "CredentialId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSkills_SkillId",
                table: "UserSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSkills_UserId",
                table: "UserSkills",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AreasOfInterests");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "JobActivities");

            migrationBuilder.DropTable(
                name: "JobSkills");

            migrationBuilder.DropTable(
                name: "UserSkills");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Titles");

            migrationBuilder.DropTable(
                name: "Credential");
        }
    }
}
