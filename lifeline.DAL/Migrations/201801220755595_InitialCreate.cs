namespace lifeline.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        memberId = c.Int(nullable: false, identity: true),
                        firstName = c.String(),
                        lastName = c.String(),
                        email = c.String(),
                        password = c.String(),
                        profilePicture = c.String(),
                        gender = c.String(),
                        weight = c.String(),
                        height = c.String(),
                        age = c.String(),
                        contactNumber = c.String(),
                        facebookId = c.String(),
                        instagramId = c.String(),
                        joiningDate = c.String(),
                    })
                .PrimaryKey(t => t.memberId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Members");
        }
    }
}
