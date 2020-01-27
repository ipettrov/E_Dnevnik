namespace E_Dnevnik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeacherStudentSubject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Oddelenies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentOddelenies",
                c => new
                    {
                        StudentId = c.Int(nullable: false),
                        OddelenieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.OddelenieId })
                .ForeignKey("dbo.Oddelenies", t => t.OddelenieId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.OddelenieId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserIdKey = c.Int(nullable: false),
                        Name = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentSubjects",
                c => new
                    {
                        StudentId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        FirstGrade = c.Int(nullable: false),
                        SecondGrade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.SubjectId })
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeacherSubjects",
                c => new
                    {
                        TeacherId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TeacherId, t.SubjectId })
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserIdKey = c.Int(nullable: false),
                        Name = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeacherOddelenies",
                c => new
                    {
                        TeacherId = c.Int(nullable: false),
                        OddelenieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TeacherId, t.OddelenieId })
                .ForeignKey("dbo.Oddelenies", t => t.OddelenieId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId)
                .Index(t => t.OddelenieId);
            
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 160),
                        LastName = c.String(nullable: false, maxLength: 160),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        IsTeacher = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.TeacherOddelenies", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.TeacherOddelenies", "OddelenieId", "dbo.Oddelenies");
            DropForeignKey("dbo.StudentOddelenies", "StudentId", "dbo.Students");
            DropForeignKey("dbo.TeacherSubjects", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.TeacherSubjects", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.StudentSubjects", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.StudentSubjects", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentOddelenies", "OddelenieId", "dbo.Oddelenies");
            DropIndex("dbo.TeacherOddelenies", new[] { "OddelenieId" });
            DropIndex("dbo.TeacherOddelenies", new[] { "TeacherId" });
            DropIndex("dbo.TeacherSubjects", new[] { "SubjectId" });
            DropIndex("dbo.TeacherSubjects", new[] { "TeacherId" });
            DropIndex("dbo.StudentSubjects", new[] { "SubjectId" });
            DropIndex("dbo.StudentSubjects", new[] { "StudentId" });
            DropIndex("dbo.StudentOddelenies", new[] { "OddelenieId" });
            DropIndex("dbo.StudentOddelenies", new[] { "StudentId" });
            DropTable("dbo.TeacherOddelenies");
            DropTable("dbo.Teachers");
            DropTable("dbo.TeacherSubjects");
            DropTable("dbo.Subjects");
            DropTable("dbo.StudentSubjects");
            DropTable("dbo.Students");
            DropTable("dbo.StudentOddelenies");
            DropTable("dbo.Oddelenies");
        }
    }
}
