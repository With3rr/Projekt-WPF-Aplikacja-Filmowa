namespace DataBaseCommunication.DbModel
{
    using GlobalClassesAndInterfaces.Classes;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class CinemaDbModel : DbContext
    {
        // Your context has been configured to use a 'CinemaDbModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DataBaseCommunication.DbModel.CinemaDbModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'CinemaDbModel' 
        // connection string in the application configuration file.
        public CinemaDbModel()
            : base("name=CinemaDbModel")
        {
        }

        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }

        public virtual DbSet<WantToSeeMovie>  WantToSeeMovies { get; set; }

        public virtual DbSet<Comment> MoviesComments { get; set; }

        public virtual DbSet<News> News { get; set; }

        public virtual DbSet<Movie> Movies { get; set; }

        public virtual DbSet<MovieInLibrary> MovieInLibraries { get; set; }

        public virtual DbSet<Notification> Notifications { get; set; }

        
        public virtual DbSet<ActorInMovie> ActorInMovies { get; set; }

        public virtual DbSet<Actor> Actors { get; set; }

        public virtual DbSet<MovieReview> MovieReviews { get; set; }

        public virtual DbSet<Friend> Friends { get; set; }

        



        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}