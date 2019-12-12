namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeNullableUpdateDeleteDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Suppliers", "UpdateDate", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Suppliers", "DeleteDate", c => c.DateTimeOffset(precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Suppliers", "DeleteDate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Suppliers", "UpdateDate", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
    }
}
