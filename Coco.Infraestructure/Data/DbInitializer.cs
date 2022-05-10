using Coco.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Infraestructure.Data
{
    public class DbInitializer
    {
        private readonly ModelBuilder modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            modelBuilder.Entity<Category>().HasData(
            new Category() { Code = "CAT01", Description = "Sodas" },
            new Category() { Code = "CAT02", Description = "Cleaning" },
            new Category() { Code = "CAT03", Description = "Food" },
            new Category() { Code = "CAT04", Description = "Bathroom" }
            );

            modelBuilder.Entity<Product>().HasData(
            new Product() { Code = "COD01", Description = "Cold Ice Tea", Amount = 100, Categories = new List<Category>() { new Category() { Code = "CAT01", Description = "Sodas" } } },
            new Product() { Code = "COD02", Description = "Coffee flavoured milk", Amount = 50, Categories = new List<Category>() { new Category() { Code = "CAT01", Description = "Sodas" } } },
            new Product() { Code = "COD03", Description = "Nuke-Cola", Amount = 70, Categories = new List<Category>() { new Category() { Code = "CAT01", Description = "Sodas" } } },
            new Product() { Code = "COD04", Description = "Sprute", Amount = 40, Categories = new List<Category>() { new Category() { Code = "CAT01", Description = "Sodas" } } },
            new Product() { Code = "COD05", Description = "Slurm", Amount = 90, Categories = new List<Category>() { new Category() { Code = "CAT01", Description = "Sodas" } } },
            new Product() { Code = "COD06", Description = "Diet Slurm", Amount = 110, Categories = new List<Category>() { new Category() { Code = "CAT01", Description = "Sodas" } } },

            new Product() { Code = "COD07", Description = "Salsa Cookies", Amount = 100, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } },
            new Product() { Code = "COD08", Description = "Windmill Cookies", Amount = 50, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } },
            new Product() { Code = "COD09", Description = "Garlic-o-bread 2000", Amount = 70, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } },
            new Product() { Code = "COD10", Description = "LACTEL bread", Amount = 40, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } },
            new Product() { Code = "COD11", Description = "Ravioloches x12", Amount = 90, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } },
            new Product() { Code = "COD12", Description = "Ravioloches x48", Amount = 110, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } },
            new Product() { Code = "COD13", Description = "Milanga ganga", Amount = 110, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } },
            new Product() { Code = "COD14", Description = "Milanga ganga napo", Amount = 110, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } },

            new Product() { Code = "COD15", Description = "Atlantis detergent", Amount = 100, Categories = new List<Category>() { new Category() { Code = "CAT02", Description = "Cleaning" } } },
            new Product() { Code = "COD16", Description = "Virulanita", Amount = 50, Categories = new List<Category>() { new Category() { Code = "CAT02", Description = "Cleaning" } } },
            new Product() { Code = "COD17", Description = "Sponge, Bob", Amount = 70, Categories = new List<Category>() { new Category() { Code = "CAT02", Description = "Cleaning" } } },
            new Product() { Code = "COD18", Description = "Generic mop", Amount = 40, Categories = new List<Category>() { new Category() { Code = "CAT02", Description = "Cleaning" } } },

            new Product() { Code = "COD19", Description = "Pure steel toilet paper", Amount = 100, Categories = new List<Category>() { new Category() { Code = "CAT04", Description = "Bathroom" } } },
            new Product() { Code = "COD20", Description = "Generic soap", Amount = 50, Categories = new List<Category>() { new Category() { Code = "CAT04", Description = "Bathroom" } } },
            new Product() { Code = "COD21", Description = "PANTONE shampoo", Amount = 70, Categories = new List<Category>() { new Category() { Code = "CAT04", Description = "Bathroom" } } },
            new Product() { Code = "COD22", Description = "Cabbagegate toothpaste", Amount = 40, Categories = new List<Category>() { new Category() { Code = "CAT04", Description = "Bathroom" } } }
            );

            modelBuilder.Entity<Store>().HasData(
            new Store()
            {
                Name = "COCO Downtown",
                Address = "La Plata Calle 13",
                Phone = "2214693454",
                Stocks = new List<Stock>() {
                    new Stock() { CurrentStock= 5, Product = new Product() { Code = "COD01", Description = "Cold Ice Tea", Amount = 100, Categories = new List<Category>() { new Category() { Code = "CAT01", Description = "Sodas" } } }} ,
                    new Stock() { CurrentStock= 7, Product = new Product() { Code = "COD02", Description = "Coffee flavoured milk", Amount = 50, Categories = new List<Category>() { new Category() { Code = "CAT01", Description = "Sodas" } } }},
                    new Stock() { CurrentStock= 3, Product = new Product() { Code = "COD03", Description = "Nuke-Cola", Amount = 70, Categories = new List<Category>() { new Category() { Code = "CAT01", Description = "Sodas" } } }},
                    new Stock() { CurrentStock= 2, Product = new Product() { Code = "COD06", Description = "Diet Slurm", Amount = 110, Categories = new List<Category>() { new Category() { Code = "CAT01", Description = "Sodas" } } }},
                    new Stock() { CurrentStock= 6, Product = new Product() { Code = "COD07", Description = "Salsa Cookies", Amount = 100, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } }},
                    new Stock() { CurrentStock= 1, Product = new Product() { Code = "COD08", Description = "Windmill Cookies", Amount = 50, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } }},
                    new Stock() { CurrentStock= 8, Product = new Product() { Code = "COD09", Description = "Garlic-o-bread 2000", Amount = 70, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } }},
                    new Stock() { CurrentStock= 9, Product = new Product() { Code = "COD10", Description = "LACTEL bread", Amount = 40, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } }},
                    new Stock() { CurrentStock= 4, Product = new Product() { Code = "COD11", Description = "Ravioloches x12", Amount = 90, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } }},
                    new Stock() { CurrentStock= 7, Product = new Product() { Code = "COD12", Description = "Ravioloches x48", Amount = 110, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } }},
                    new Stock() { CurrentStock= 2, Product = new Product() { Code = "COD13", Description = "Milanga ganga", Amount = 110, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } }},
                    new Stock() { CurrentStock= 15, Product = new Product() { Code = "COD14", Description = "Milanga ganga napo", Amount = 110, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } }},
                    new Stock() { CurrentStock= 12, Product = new Product() { Code = "COD20", Description = "Generic soap", Amount = 50, Categories = new List<Category>() { new Category() { Code = "CAT04", Description = "Bathroom" } } }},
                    new Stock() { CurrentStock= 11, Product = new Product() { Code = "COD21", Description = "PANTONE shampoo", Amount = 70, Categories = new List<Category>() { new Category() { Code = "CAT04", Description = "Bathroom" } } }},
                    new Stock() { CurrentStock= 10, Product = new Product() { Code = "COD22", Description = "Cabbagegate toothpaste", Amount = 40, Categories = new List<Category>() { new Category() { Code = "CAT04", Description = "Bathroom" } } }}
                },
                WorkingDay = new WorkingDays() { Monday = true, Tuesday = true, Wednesday = true, Thursday = true, Friday = true, Saturday = false, Sunday = false, TimeFrom = new TimeOnly(8, 0), TimeTo = new TimeOnly(20, 0) }
            },
             new Store()
             {
                 Name = "COCO Bay",
                 Address = "La Plata Calle 2",
                 Phone = "2214693454",
                 Stocks = new List<Stock>() {
                    new Stock() { CurrentStock= 5, Product = new Product() { Code = "COD01", Description = "Cold Ice Tea", Amount = 100, Categories = new List<Category>() { new Category() { Code = "CAT01", Description = "Sodas" } } }} ,
                    new Stock() { CurrentStock= 7, Product = new Product() { Code = "COD02", Description = "Coffee flavoured milk", Amount = 50, Categories = new List<Category>() { new Category() { Code = "CAT01", Description = "Sodas" } } }},
                    new Stock() { CurrentStock= 3, Product = new Product() { Code = "COD03", Description = "Nuke-Cola", Amount = 70, Categories = new List<Category>() { new Category() { Code = "CAT01", Description = "Sodas" } } }},
                    new Stock() { CurrentStock= 2, Product = new Product() { Code = "COD04", Description = "Sprute", Amount = 40, Categories = new List<Category>() { new Category() { Code = "CAT01", Description = "Sodas" } } }},
                    new Stock() { CurrentStock= 2, Product = new Product() { Code = "COD05", Description = "Slurm", Amount = 90, Categories = new List<Category>() { new Category() { Code = "CAT01", Description = "Sodas" } } }},
                    new Stock() { CurrentStock= 6, Product = new Product() { Code = "COD07", Description = "Salsa Cookies", Amount = 100, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } }},
                    new Stock() { CurrentStock= 1, Product = new Product() { Code = "COD08", Description = "Windmill Cookies", Amount = 50, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } }},
                    new Stock() { CurrentStock= 8, Product = new Product() { Code = "COD09", Description = "Garlic-o-bread 2000", Amount = 70, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } }},
                    new Stock() { CurrentStock= 9, Product = new Product() { Code = "COD10", Description = "LACTEL bread", Amount = 40, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } }},
                    new Stock() { CurrentStock= 4, Product = new Product() { Code = "COD11", Description = "Ravioloches x12", Amount = 90, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } }},
                    new Stock() { CurrentStock= 7, Product = new Product() { Code = "COD12", Description = "Ravioloches x48", Amount = 110, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } }},
                    new Stock() { CurrentStock= 2, Product = new Product() { Code = "COD13", Description = "Milanga ganga", Amount = 110, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } }},
                    new Stock() { CurrentStock= 15, Product = new Product() { Code = "COD14", Description = "Milanga ganga napo", Amount = 110, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } }},
                    new Stock() { CurrentStock= 12, Product = new Product() { Code = "COD15", Description = "Atlantis detergent", Amount = 100, Categories = new List<Category>() { new Category() { Code = "CAT02", Description = "Cleaning" } } }},
                    new Stock() { CurrentStock= 12, Product = new Product() { Code = "COD16", Description = "Virulanita", Amount = 50, Categories = new List<Category>() { new Category() { Code = "CAT02", Description = "Cleaning" } } }},
                    new Stock() { CurrentStock= 12, Product = new Product() { Code = "COD17", Description = "Sponge, Bob", Amount = 70, Categories = new List<Category>() { new Category() { Code = "CAT02", Description = "Cleaning" } } }},
                    new Stock() { CurrentStock= 12, Product = new Product() { Code = "COD18", Description = "Generic mop", Amount = 40, Categories = new List<Category>() { new Category() { Code = "CAT02", Description = "Cleaning" } } }},
                },
                 WorkingDay = new WorkingDays() { Monday = true, Tuesday = true, Wednesday = true, Thursday = true, Friday = true, Saturday = false, Sunday = true, TimeFrom = new TimeOnly(8, 0), TimeTo = new TimeOnly(20, 0) }
             },
             new Store()
             {
                 Name = "COCO Mail",
                 Address = "La Plata Calle 7",
                 Phone = "2214693454",
                 Stocks = new List<Stock>() {
                    new Stock() { CurrentStock= 5, Product = new Product() { Code = "COD01", Description = "Cold Ice Tea", Amount = 100, Categories = new List<Category>() { new Category() { Code = "CAT01", Description = "Sodas" } } }} ,
                    new Stock() { CurrentStock= 7, Product = new Product() { Code = "COD02", Description = "Coffee flavoured milk", Amount = 50, Categories = new List<Category>() { new Category() { Code = "CAT01", Description = "Sodas" } } }},
                    new Stock() { CurrentStock= 3, Product = new Product() { Code = "COD03", Description = "Nuke-Cola", Amount = 70, Categories = new List<Category>() { new Category() { Code = "CAT01", Description = "Sodas" } } }},
                    new Stock() { CurrentStock= 2, Product = new Product() { Code = "COD04", Description = "Sprute", Amount = 40, Categories = new List<Category>() { new Category() { Code = "CAT01", Description = "Sodas" } } }},
                    new Stock() { CurrentStock= 2, Product = new Product() { Code = "COD05", Description = "Slurm", Amount = 90, Categories = new List<Category>() { new Category() { Code = "CAT01", Description = "Sodas" } } }},
                    new Stock() { CurrentStock= 2, Product = new Product() { Code = "COD06", Description = "Diet Slurm", Amount = 110, Categories = new List<Category>() { new Category() { Code = "CAT01", Description = "Sodas" } } }},
                    new Stock() { CurrentStock= 6, Product = new Product() { Code = "COD07", Description = "Salsa Cookies", Amount = 100, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } }},
                    new Stock() { CurrentStock= 1, Product = new Product() { Code = "COD08", Description = "Windmill Cookies", Amount = 50, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } }},
                    new Stock() { CurrentStock= 8, Product = new Product() { Code = "COD09", Description = "Garlic-o-bread 2000", Amount = 70, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } }},
                    new Stock() { CurrentStock= 9, Product = new Product() { Code = "COD10", Description = "LACTEL bread", Amount = 40, Categories = new List<Category>() { new Category() { Code = "CAT03", Description = "Food" } } }},
                    new Stock() { CurrentStock= 12, Product = new Product() { Code = "COD19", Description = "Pure steel toilet paper", Amount = 100, Categories = new List<Category>() { new Category() { Code = "CAT04", Description = "Bathroom" } } }},
                    new Stock() { CurrentStock= 12, Product = new Product() { Code = "COD20", Description = "Generic soap", Amount = 50, Categories = new List<Category>() { new Category() { Code = "CAT04", Description = "Bathroom" } } }},
                    new Stock() { CurrentStock= 11, Product = new Product() { Code = "COD21", Description = "PANTONE shampoo", Amount = 70, Categories = new List<Category>() { new Category() { Code = "CAT04", Description = "Bathroom" } } }},
                    new Stock() { CurrentStock= 10, Product = new Product() { Code = "COD22", Description = "Cabbagegate toothpaste", Amount = 40, Categories = new List<Category>() { new Category() { Code = "CAT04", Description = "Bathroom" } } }}
                },
                 WorkingDay = new WorkingDays() { Monday = true, Tuesday = true, Wednesday = true, Thursday = true, Friday = false, Saturday = false, Sunday = false, TimeFrom = new TimeOnly(9, 0), TimeTo = new TimeOnly(17, 0) }
             }
            );
        }
    }
}
