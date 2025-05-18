using Microsoft.Extensions.DependencyInjection;
using Project.DAL.Repositories.Abstracts;
using Project.DAL.Repositories.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DependencyResolvers
{
    public static class RepositoryResolver
    {
        public static void AddRepositoryService(this IServiceCollection services)
        {
            /*
             AddSingleton : Sadece bir instance alınarak tüm program boyunca o instance kullanılır

             AddScoped : Bir Request icerisinde aynı tiplerde farklı tanımlamalar varsa o spesifik request icin o tipten sadece 1 instance alınır...

             AddTransient : Tip kac kez tanımlandıysa her biri icin ayrı instance alınır...



             */

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped<IOrderRepository,OrderRepository>();
            services.AddScoped<IOrderDetailRepository,OrderDetailRepository>(); 
            services.AddScoped<IAppUserRepository,AppUserRepository>(); 
            services.AddScoped<IAppUserProfileRepository,AppUserProfileRepository>();   
        }
    }
}
