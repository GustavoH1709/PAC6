
using dotenv.net;
using PAC6.API.Application;
using PAC6.API.Interfaces;
using PAC6.API.Providers;

namespace PAC6.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            DotEnv.Load();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMvc();

            builder.Services.AddSingleton<ICreateSensorApplication, CreateSensorApplication>();
            builder.Services.AddSingleton<ICreateParametersApplication, CreateParametersApplication>();
            builder.Services.AddSingleton<ICreateEmailApplication, CreateEmailApplication>();
            builder.Services.AddSingleton<IEmailProvider, EmailProvider>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }



            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
