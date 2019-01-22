using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Altkom.DotnetCore.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // add package Microsoft.AspNetCore.Hosting
            // add package Microsoft.AspNetCore.Server.Kestrel
            IWebHost host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .UseUrls("http://0.0.0.0:5010", "https://0.0.0.0:5011")
                .Build();

            host.Run();
        }
    }

    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            // middleware za pomocą wyrażenia lambda
            app.Use(async (context, next) =>
            {
                Console.WriteLine("before A");

                await next();

                Console.WriteLine("after A");
            });
            

            // middleware z użyciem własnej metody
            app.Use(Process);
            
            // middleware z użyciem własnej klasy
       //     app.UseMiddleware<MyMiddleware>();

            // middleware z użyciem metody rozszerzającej
            app.UseMyMiddleware();

            app.UseDasboard();
            
            app.Run(async context => await context.Response.WriteAsync("Hello .NET Core"));
        }

        private async Task Process(HttpContext context, Func<Task> next)
        {
            Console.WriteLine("before B");

            await next();

            Console.WriteLine("after B");
        }


    }

    public class MyMiddleware
    {
        private readonly RequestDelegate next;

        public MyMiddleware(RequestDelegate next)
        {
            this.next = next;
        }


        // Metoda musi się nazywać Invoke lub InvokeAsync
        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("Site", "test");

            await next.Invoke(context);
        }
    }

    public class MyDashboard
    {
        private readonly RequestDelegate next;

        public MyDashboard(RequestDelegate next)
        {
            this.next = next;

        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync("Hello Dashboard");

            await next.Invoke(context);
        }


    }

    public static class RequestMyMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddleware>();
        }
    }

    public static class RequestMyDashboardExtensions
    {
        public static IApplicationBuilder UseDasboard(this IApplicationBuilder builder, string path = "/dashboard")
        {
            return builder.Map(new PathString(path), 
                a => a.UseMiddleware<MyDashboard>());
        }
    }

}
