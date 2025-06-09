
using API.Middleware;
using Application.Interfaces;
using Application.Mapping;
using Application.Services;
using Domain;
using Domain.MongoModels;
using Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HelsiTeskTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<MongoDbSettings>(
            builder.Configuration.GetSection("MongoDb"));

            // Зареєструвати MongoClient як singleton (щоб не створювати новий кожного разу)
            builder.Services.AddSingleton<IMongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                return new MongoClient(settings.ConnectionString);
            });

            // Зареєструвати IMongoDatabase для інжекції
            builder.Services.AddScoped(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(settings.Database);
            });

            builder.Services.AddScoped<IMongoCollection<MongoUser>>(sp =>
            {
                var database = sp.GetRequiredService<IMongoDatabase>();
                return database.GetCollection<MongoUser>("Users");
            });

            builder.Services.AddScoped<IMongoCollection<MongoTaskList>>(sp =>
            {
                var database = sp.GetRequiredService<IMongoDatabase>();
                return database.GetCollection<MongoTaskList>("TaskLists");
            });




            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<ITaskListRepository, TaskListRepository>();
            builder.Services.AddScoped<ITaskListService, TaskListService>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //middleware
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
