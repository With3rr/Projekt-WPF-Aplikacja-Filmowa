namespace DataBaseCommunication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                        Rank = c.Int(nullable: false),
                        Interests = c.String(),
                        Picture = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MovieInLibraries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account_Id = c.Int(),
                        Movie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .ForeignKey("dbo.Movies", t => t.Movie_Id)
                .Index(t => t.Account_Id)
                .Index(t => t.Movie_Id);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Length = c.Int(nullable: false),
                        Director = c.String(),
                        BoxOffice = c.Int(nullable: false),
                        Name = c.String(),
                        Producer = c.String(),
                        AgeCategory = c.Int(nullable: false),
                        Budget = c.Int(nullable: false),
                        Country = c.String(),
                        Rating = c.Double(nullable: false),
                        Type = c.String(),
                        MainActor = c.String(),
                        SecondPlanActor = c.String(),
                        Plot = c.String(),
                        Picture = c.String(),
                        Link1 = c.String(),
                        Link2 = c.String(),
                        Language = c.String(),
                        PremiereDate = c.DateTime(nullable: false),
                        Trailer = c.String(),
                        Scenario = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ActorInMovies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Role = c.String(),
                        Actor_Id = c.Int(),
                        Movie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Actors", t => t.Actor_Id)
                .ForeignKey("dbo.Movies", t => t.Movie_Id)
                .Index(t => t.Actor_Id)
                .Index(t => t.Movie_Id);
            
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PicturePath = c.String(),
                        Age = c.String(),
                        Growth = c.Int(nullable: false),
                        Biography = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        PlaceOfBirth = c.String(),
                        Rating = c.Double(nullable: false),
                        Gender = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false),
                        Content = c.String(),
                        Account_Id = c.Int(),
                        Actor_Id = c.Int(),
                        Friend_Id = c.Int(),
                        Movie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .ForeignKey("dbo.Actors", t => t.Actor_Id)
                .ForeignKey("dbo.Accounts", t => t.Friend_Id)
                .ForeignKey("dbo.Movies", t => t.Movie_Id)
                .Index(t => t.Account_Id)
                .Index(t => t.Actor_Id)
                .Index(t => t.Friend_Id)
                .Index(t => t.Movie_Id);
            
            CreateTable(
                "dbo.MovieReviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rate = c.Int(nullable: false),
                        Opinion = c.String(),
                        Account_Id = c.Int(),
                        Movie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .ForeignKey("dbo.Movies", t => t.Movie_Id)
                .Index(t => t.Account_Id)
                .Index(t => t.Movie_Id);
            
            CreateTable(
                "dbo.WantToSeeMovies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account_Id = c.Int(),
                        Movie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .ForeignKey("dbo.Movies", t => t.Movie_Id)
                .Index(t => t.Account_Id)
                .Index(t => t.Movie_Id);
            
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IsActive = c.Boolean(nullable: false),
                        Account1_Id = c.Int(),
                        Account2_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accounts", t => t.Account1_Id)
                .ForeignKey("dbo.Accounts", t => t.Account2_Id)
                .Index(t => t.Account1_Id)
                .Index(t => t.Account2_Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Content = c.String(),
                        Account1_Sender_Id = c.Int(),
                        Account2_Receiver_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account1_Sender_Id)
                .ForeignKey("dbo.Accounts", t => t.Account2_Receiver_Id)
                .Index(t => t.Account1_Sender_Id)
                .Index(t => t.Account2_Receiver_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Account2_Receiver_Id", "dbo.Accounts");
            DropForeignKey("dbo.Messages", "Account1_Sender_Id", "dbo.Accounts");
            DropForeignKey("dbo.Friends", "Account2_Id", "dbo.Accounts");
            DropForeignKey("dbo.Friends", "Account1_Id", "dbo.Accounts");
            DropForeignKey("dbo.WantToSeeMovies", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.WantToSeeMovies", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.MovieReviews", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.MovieReviews", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.MovieInLibraries", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.Comments", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.Comments", "Friend_Id", "dbo.Accounts");
            DropForeignKey("dbo.Comments", "Actor_Id", "dbo.Actors");
            DropForeignKey("dbo.Comments", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.ActorInMovies", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.ActorInMovies", "Actor_Id", "dbo.Actors");
            DropForeignKey("dbo.MovieInLibraries", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.Messages", new[] { "Account2_Receiver_Id" });
            DropIndex("dbo.Messages", new[] { "Account1_Sender_Id" });
            DropIndex("dbo.Friends", new[] { "Account2_Id" });
            DropIndex("dbo.Friends", new[] { "Account1_Id" });
            DropIndex("dbo.WantToSeeMovies", new[] { "Movie_Id" });
            DropIndex("dbo.WantToSeeMovies", new[] { "Account_Id" });
            DropIndex("dbo.MovieReviews", new[] { "Movie_Id" });
            DropIndex("dbo.MovieReviews", new[] { "Account_Id" });
            DropIndex("dbo.Comments", new[] { "Movie_Id" });
            DropIndex("dbo.Comments", new[] { "Friend_Id" });
            DropIndex("dbo.Comments", new[] { "Actor_Id" });
            DropIndex("dbo.Comments", new[] { "Account_Id" });
            DropIndex("dbo.ActorInMovies", new[] { "Movie_Id" });
            DropIndex("dbo.ActorInMovies", new[] { "Actor_Id" });
            DropIndex("dbo.MovieInLibraries", new[] { "Movie_Id" });
            DropIndex("dbo.MovieInLibraries", new[] { "Account_Id" });
            DropTable("dbo.Messages");
            DropTable("dbo.Friends");
            DropTable("dbo.WantToSeeMovies");
            DropTable("dbo.MovieReviews");
            DropTable("dbo.Comments");
            DropTable("dbo.Actors");
            DropTable("dbo.ActorInMovies");
            DropTable("dbo.Movies");
            DropTable("dbo.MovieInLibraries");
            DropTable("dbo.Accounts");
        }
    }
}
