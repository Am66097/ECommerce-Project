using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.OrderModule;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Persistance.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistance.Data.DataSeed
{

    public class DataInitializer : IDataInitializer
    {
        private readonly StoreDbContext _dbContext;

        public DataInitializer(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task InitializeAsync()
        {
            try
            {
                var HasProudcts = await _dbContext.Products.AnyAsync();
                var HasBrands = await _dbContext.ProductBrands.AnyAsync();
                var HasTypes = await _dbContext.ProductTypes.AnyAsync();
                var HasDeliveryMethods = await _dbContext.Set<DeliveryMethod>().AnyAsync();


                if (HasProudcts && HasBrands && HasTypes && HasDeliveryMethods) return;

                if (!HasBrands)
                    await seedDataFromJsonFilesAsync<ProductBrand, int>("brands.json", _dbContext.ProductBrands);

                if (!HasTypes)
                    await seedDataFromJsonFilesAsync<ProductType, int>("types.json", _dbContext.ProductTypes);
                await _dbContext.SaveChangesAsync();
                if (!HasProudcts)
                    await seedDataFromJsonFilesAsync<Product, int>("products.json", _dbContext.Products);
                if (!HasDeliveryMethods) // الي الان لم يتم وضع ملف ال (json الخاص بطرق التوصيل)
                    await seedDataFromJsonFilesAsync<DeliveryMethod, int>("delivery.json", _dbContext.Set<DeliveryMethod>());


                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed Seeding : {ex} ");

            }
        }


        //Helper Method
        private async Task seedDataFromJsonFilesAsync<T, TKey>(string fileName, DbSet<T> dbset) where T : BaseEntity<TKey>
        {
            //D:\Route\7.API\API Project For Course\E-CommerceSolution\E-Commerce.Persistance\Data\DataSeed\JSONFiles\brands.json

            var FilePath = @"..\E-Commerce.Persistance\Data\DataSeed\JSONFiles\" + fileName;
            if (!File.Exists(FilePath)) throw new FileNotFoundException($"File {fileName} is no Exists");

            try
            {
                using var dataStream = File.OpenRead(FilePath);

                var data = await JsonSerializer.DeserializeAsync<List<T>>(dataStream, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
                if (data is not null)
                {
                    await dbset.AddRangeAsync(data);  // async هنا ملهاش لازمة محطوطة عشان الكود يبقي كلوا ماشي async
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Reading Json File : {ex}");

            }


        }
    }
}
