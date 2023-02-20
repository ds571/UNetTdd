using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Processors;
using RoomBookingApp.Persistence;
using RoomBookingApp.Persistence.Repositories;

namespace RoomBookingApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /**************** Add services to the container *****************************************/
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RoomBookingApp.Api", Version = "v1" });
            });

            // Setup Database
            var connString = "Datasource=:memory:";
            var conn = new SqliteConnection(connString);
            conn.Open();

            builder.Services.AddDbContext<RoomBookingAppDbContext>(opt => opt.UseSqlite(conn));

            EnsureDatabaseCreated(conn);

            builder.Services.AddScoped<IRoomBookingService, RoomBookingService>();
            builder.Services.AddScoped<IRoomBookingRequestProcessor, RoomBookingRequestProcessor>();
            /****************************************************************************************/


            #region Configure
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
            #endregion

            app.Run();
        }

        private static void EnsureDatabaseCreated(SqliteConnection conn)
        {
            var builder = new DbContextOptionsBuilder<RoomBookingAppDbContext>();
            builder.UseSqlite(conn);

            using var context = new RoomBookingAppDbContext(builder.Options);
            context.Database.EnsureCreated();
        }

    }
}