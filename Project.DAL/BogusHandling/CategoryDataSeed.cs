using Microsoft.EntityFrameworkCore;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus.DataSets;
namespace Project.DAL.BogusHandling
{
    public static class CategoryDataSeed
    {
        public static void SeedCategories(ModelBuilder modelBuilder)
        {
            //Bu seviyede DAL katmanındayız
            List<Category> categories = new();

            for (int i = 1; i < 11; i++)
            {
                Category c = new()
                {
                    Id = i,
                    CategoryName = new Commerce("tr").Categories(1)[0],
                    Description = new Lorem("tr").Sentence(10),
                    CreatedDate = DateTime.Now,
                    Status = ENTITIES.Enums.DataStatus.Inserted
                };
                categories.Add(c);
            }

            modelBuilder.Entity<Category>().HasData(categories);
        }
    }
}
