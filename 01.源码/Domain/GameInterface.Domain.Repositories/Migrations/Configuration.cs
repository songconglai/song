namespace GameInterface.Domain.Repositories.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using GameInterface.Domain.Enum;
    using GameInterface.Domain.Model;

    internal sealed class Configuration : DbMigrationsConfiguration<GameInterface.Domain.Repositories.Repositories.GameInterfaceDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GameInterface.Domain.Repositories.Repositories.GameInterfaceDbContext context)
        {
            string password = "e10adc3949ba59abbe56e057f20f883e";
            context.LoginUsers.AddOrUpdate(
                p => p.UserName,
                new LoginUser { UserName = "song", Password = password, Email = "songconglai@qq.com", Type = UserType.管理员.ToString(), Status = UserStatus.正常.ToString() }
                );
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
