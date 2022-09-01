using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BookmarkManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            DriveServices driveService = new DriveServices();
            driveService.GetDataBase();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
