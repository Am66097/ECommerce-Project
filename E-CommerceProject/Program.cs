
using E_Commerce.Domain.Contracts;
using E_Commerce.Persistance.Data.Contexts;
using E_Commerce.Persistance.Data.DataSeed;
using E_Commerce.Persistance.Repositories;
using E_Commerce.Services;
using E_Commerce.Services.Abstraction;
using E_Commerce.Services.MappingProfiels;
using E_CommerceProject.Extentions;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Reflection;
using System.Threading.Tasks;

namespace E_CommerceProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDataInitializer, DataInitializer>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            //builder.Services.AddAutoMapper(x=>x.AddProfile<ProductProfile>());
            builder.Services.AddAutoMapper(typeof(ServiceAssemblyReferance).Assembly);

            builder.Services.AddTransient<ProductPictureUrlResolver>();

            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!);
            });
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();

            #endregion

            var app = builder.Build();

            #region Data Seeding - Pending Migations 

            await app.MigrateDatabaseAsync(); // Method In Extention Folder In E-Commerce.Web
            await app.SeedDatabaseAsync();    // Method In Extention Folder In E-Commerce.Web

            #endregion

            #region Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
