namespace SorveteriaEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Correcao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sorvetes", "Tamanho", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sorvetes", "Tamanho");
        }
    }
}
