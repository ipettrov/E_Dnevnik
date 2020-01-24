namespace E_Dnevnik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StidentteacherModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
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
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
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
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentSubjects", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentSubjects", "StudentId", "dbo.Teachers");
            DropForeignKey("dbo.TeacherSubjects", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.TeacherSubjects", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.StudentSubjects", "SubjectId", "dbo.Subjects");
            DropIndex("dbo.TeacherSubjects", new[] { "SubjectId" });
            DropIndex("dbo.TeacherSubjects", new[] { "TeacherId" });
            DropIndex("dbo.StudentSubjects", new[] { "SubjectId" });
            DropIndex("dbo.StudentSubjects", new[] { "StudentId" });
            DropTable("dbo.Subjects");
            DropTable("dbo.TeacherSubjects");
            DropTable("dbo.Teachers");
            DropTable("dbo.StudentSubjects");
            DropTable("dbo.Students");
        }
    }
}
