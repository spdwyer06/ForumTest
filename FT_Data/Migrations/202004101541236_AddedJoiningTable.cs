namespace FT_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedJoiningTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostAndReplyJoins",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PostID = c.Int(nullable: false),
                        ReplyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: false)
                .ForeignKey("dbo.PostReplies", t => t.ReplyID, cascadeDelete: true)
                .Index(t => t.PostID)
                .Index(t => t.ReplyID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostAndReplyJoins", "ReplyID", "dbo.PostReplies");
            DropForeignKey("dbo.PostAndReplyJoins", "PostID", "dbo.Posts");
            DropIndex("dbo.PostAndReplyJoins", new[] { "ReplyID" });
            DropIndex("dbo.PostAndReplyJoins", new[] { "PostID" });
            DropTable("dbo.PostAndReplyJoins");
        }
    }
}
