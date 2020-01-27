namespace E_Dnevnik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddKey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.TeacherSubjects");
            AddColumn("dbo.TeacherSubjects", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.TeacherSubjects", new[] { "Id", "TeacherId", "SubjectId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.TeacherSubjects");
            DropColumn("dbo.TeacherSubjects", "Id");
            AddPrimaryKey("dbo.TeacherSubjects", new[] { "TeacherId", "SubjectId" });
        }
    }
}
