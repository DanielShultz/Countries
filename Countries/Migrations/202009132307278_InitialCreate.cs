namespace Countries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryCode = c.String(),
                        Area = c.Double(),
                        Population = c.Int(nullable: false),
                        Name = c.String(),
                        Capital_Id = c.Int(),
                        Region_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.Capital_Id)
                .ForeignKey("dbo.Regions", t => t.Region_Id)
                .Index(t => t.Capital_Id)
                .Index(t => t.Region_Id);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Countries", "Region_Id", "dbo.Regions");
            DropForeignKey("dbo.Countries", "Capital_Id", "dbo.Cities");
            DropIndex("dbo.Countries", new[] { "Region_Id" });
            DropIndex("dbo.Countries", new[] { "Capital_Id" });
            DropTable("dbo.Regions");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
        }
    }
}
