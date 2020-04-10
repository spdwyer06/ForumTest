namespace FT_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDataTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        ThreadID = c.Int(nullable: false),
                        PostCreator = c.Guid(nullable: false),
                        PostContent = c.String(),
                        PostCreated = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.PostID)
                .ForeignKey("dbo.Threads", t => t.ThreadID, cascadeDelete: true)
                .Index(t => t.ThreadID);
            
            CreateTable(
                "dbo.Threads",
                c => new
                    {
                        ThreadID = c.Int(nullable: false, identity: true),
                        ThreadCreator = c.Guid(nullable: false),
                        ThreadTitle = c.String(),
                        ThreadDescription = c.String(),
                        ThreadCreated = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.ThreadID);
            
            CreateTable(
                "dbo.PostReplies",
                c => new
                    {
                        ReplyID = c.Int(nullable: false, identity: true),
                        PostID = c.Int(nullable: false),
                        ReplyCreator = c.Guid(nullable: false),
                        ReplyContent = c.String(),
                        ReplyCreated = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.ReplyID)
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: true)
                .Index(t => t.PostID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostReplies", "PostID", "dbo.Posts");
            DropForeignKey("dbo.Posts", "ThreadID", "dbo.Threads");
            DropIndex("dbo.PostReplies", new[] { "PostID" });
            DropIndex("dbo.Posts", new[] { "ThreadID" });
            DropTable("dbo.PostReplies");
            DropTable("dbo.Threads");
            DropTable("dbo.Posts");
        }
    }
}
