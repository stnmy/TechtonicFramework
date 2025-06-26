using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TechtonicFramework.Data;
using TechtonicFramework.Models.ProductModels;
using TechtonicFramework.Models.Users;

namespace TechtonicFramework
{
    public static class DbSeeder
    {
        public static void Seed()
        {
            using (var context = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(context));

                var userManager = new UserManager<User>(
                    new UserStore<User>(context));

                string[] roles = { "Customer", "Moderator", "Admin" };

                foreach (var role in roles)
                {
                    if (!roleManager.RoleExists(role))
                        roleManager.Create(new IdentityRole(role));
                }

                // Seed users
                if (!userManager.Users.Any())
                {
                    var customer = new User { UserName = "customer@gmail.com", Email = "customer@gmail.com" };
                    userManager.Create(customer, "Pa$$w0rd");
                    userManager.AddToRole(customer.Id, "Customer");

                    var moderator = new User { UserName = "moderator@gmail.com", Email = "moderator@gmail.com" };
                    userManager.Create(moderator, "Pa$$w0rd");
                    userManager.AddToRoles(moderator.Id, "Customer", "Moderator");

                    var admin = new User { UserName = "admin@gmail.com", Email = "admin@gmail.com" };
                    userManager.Create(admin, "Pa$$w0rd");
                    userManager.AddToRoles(admin.Id, "Customer", "Moderator", "Admin");
                }
                if (context.Categories.Any() || context.Products.Any())
                {
                    return;
                }


                var brands = new List<Brand>
            {
                new Brand { Name = "Asus", Slug = "asus" },
                new Brand { Name = "Lenovo", Slug = "lenovo" },
                new Brand { Name = "Acer", Slug = "acer" },
                new Brand { Name = "Huawei", Slug = "huawei" },
                new Brand { Name = "Apple", Slug = "apple" },
                new Brand { Name = "Xiaomi", Slug = "xiaomi" },
                new Brand { Name = "MSI", Slug = "msi" },
                new Brand { Name = "DELL", Slug = "dell" },
                new Brand { Name = "HP", Slug = "hp" },
                new Brand { Name = "Microsoft", Slug = "microsoft" }
            };

                FilterAttributeValue favCatUltrabook, favCatGaming, favCatConsumer, favCatOffice;

                FilterAttributeValue favProcI7, favProcRyzen9, favProcRyzen5, favProcI5, favProcI3,
                                     favProcRyzen7, favProcRyzen3, favProcAppleM1Pro;

                FilterAttributeValue favGenIntel11, favGenIntel12, favGenIntel13, favGenIntel14, favGenIntel10,
                                     favGenRyzen5000, favGenRyzen3000, favGenRyzen4000, favGenRyzen6000,
                                     favGenRyzen7000, favGenRyzen8000;

                FilterAttributeValue favDisplayIps, favDisplayTouch, favDisplayOled, favDisplayTn, favDisplayVa;

                FilterAttributeValue favDisplaySize14, favDisplaySize15_6, favDisplaySize13_3,
                                     favDisplaySize16, favDisplaySize17_3; 

                FilterAttributeValue favRam16, favRam8, favRam4, favRam12, favRam32;
                FilterAttributeValue favRamDdr4, favRamDdr5;
                FilterAttributeValue favHdd500, favHdd1tb, favHdd2tb;
                FilterAttributeValue favSsd512, favSsd1tb, favSsd128, favSsd256, favSsd2tb, favSsd4tb; 

                FilterAttributeValue favGfxShared, favGfx2gb, favGfx4gb, favGfx6gb, favGfx8gb, favGfx10gb,
                                     favGfx12gb, favGfx16gb, favGfx24gb;

                FilterAttributeValue favOsWin11Pro, favOsWin11Home, favOsWin10Pro, favOsWin10Home,
                                     favOsMac, favOsChrome, favOsLinux;

                FilterAttributeValue favFeatBacklit, favFeatFingerprint, favFeatRgb, favFeatAdaptiveSync,
                                     favFeatStylus, favFeatConvertible, favFeatThinLight, favFeatHighRefresh;

                FilterAttributeValue favBrandAsus, favBrandLenovo, favBrandAcer, favBrandHuawei, favBrandApple,
                                     favBrandXiaomi, favBrandMsi, favBrandDell, favBrandHp, favBrandMicrosoft;

                var categories = new List<Category>
            {
                new Category
                {
                    Name = "Laptop",
                    Slug = "laptop",
                    Filters = new List<FilterAttribute>
                    {
                        new FilterAttribute
                            {
                                FilterName = "Brand",FilterSlug = "brand",DefaultValues = new List<FilterAttributeValue>
                                {
                                    (favBrandAsus = new FilterAttributeValue { Value = "Asus" }),
                                    (favBrandLenovo = new FilterAttributeValue { Value = "Lenovo" }),
                                    (favBrandAcer = new FilterAttributeValue { Value = "Acer" }),
                                    (favBrandHuawei = new FilterAttributeValue { Value = "Huawei" }),
                                    (favBrandApple = new FilterAttributeValue { Value = "Apple" }),
                                    (favBrandXiaomi = new FilterAttributeValue { Value = "Xiaomi" }),
                                    (favBrandMsi = new FilterAttributeValue { Value = "MSI" }),
                                    (favBrandDell = new FilterAttributeValue { Value = "DELL" }),
                                    (favBrandHp = new FilterAttributeValue { Value = "HP" }),
                                    (favBrandMicrosoft = new FilterAttributeValue { Value = "Microsoft" })
                                }
                            },
                        new FilterAttribute { FilterName = "Category", FilterSlug = "category", DefaultValues = new List<FilterAttributeValue>
                            {
                                (favCatUltrabook = new FilterAttributeValue { Value = "Ultrabook" }),
                                (favCatGaming = new FilterAttributeValue { Value = "Gaming" }),
                                (favCatConsumer = new FilterAttributeValue { Value = "Consumer" }),
                                (favCatOffice = new FilterAttributeValue { Value = "Office" })
                            }
                        },
                        new FilterAttribute { FilterName = "Processor Type", FilterSlug = "processor-type", DefaultValues = new List<FilterAttributeValue>
                            {
                                (favProcI7 = new FilterAttributeValue { Value = "Intel Core i7" }),
                                (favProcRyzen9 = new FilterAttributeValue { Value = "AMD Ryzen 9" }),
                                (favProcRyzen5 = new FilterAttributeValue { Value = "AMD Ryzen 5" }),
                                (favProcI5 = new FilterAttributeValue { Value = "Intel Core i5" }),
                                (favProcI3 = new FilterAttributeValue { Value = "Intel Core i3" }),
                                (favProcRyzen7 = new FilterAttributeValue { Value = "AMD Ryzen 7" }),
                                (favProcRyzen3 = new FilterAttributeValue { Value = "AMD Ryzen 3" }),
                                (favProcAppleM1Pro = new FilterAttributeValue { Value = "Apple M1 Pro" })
                            }
                        },
                        new FilterAttribute { FilterName = "Generation/Series", FilterSlug = "generation-series", DefaultValues = new List<FilterAttributeValue>
                            {
                                (favGenIntel11 = new FilterAttributeValue { Value = "Intel 11th Gen" }),
                                (favGenIntel12 = new FilterAttributeValue { Value = "Intel 12th Gen" }),
                                (favGenIntel13 = new FilterAttributeValue { Value = "Intel 13th Gen" }),
                                (favGenIntel14 = new FilterAttributeValue { Value = "Intel 14th Gen" }),
                                (favGenIntel10 = new FilterAttributeValue { Value = "Intel 10th Gen" }),
                                (favGenRyzen5000 = new FilterAttributeValue { Value = "Ryzen 5000 Series" }),
                                (favGenRyzen3000 = new FilterAttributeValue { Value = "Ryzen 3000 Series" }),
                                (favGenRyzen4000 = new FilterAttributeValue { Value = "Ryzen 4000 Series" }),
                                (favGenRyzen6000 = new FilterAttributeValue { Value = "Ryzen 6000 Series" }),
                                (favGenRyzen7000 = new FilterAttributeValue { Value = "Ryzen 7000 Series" }),
                                (favGenRyzen8000 = new FilterAttributeValue { Value = "Ryzen 8000 Series" })
                            }
                        },
                        new FilterAttribute { FilterName = "Display Type", FilterSlug = "display-type", DefaultValues = new List<FilterAttributeValue>
                            {
                                (favDisplayIps = new FilterAttributeValue { Value = "IPS" }),
                                (favDisplayTouch = new FilterAttributeValue { Value = "Touch Display" }),
                                (favDisplayOled = new FilterAttributeValue { Value = "OLED" }),
                                (favDisplayTn = new FilterAttributeValue { Value = "TN" }),
                                (favDisplayVa = new FilterAttributeValue { Value = "VA" })
                            }
                        },
                        new FilterAttribute { FilterName = "Display Size", FilterSlug = "display-size", DefaultValues = new List<FilterAttributeValue>
                            {
                                (favDisplaySize14 = new FilterAttributeValue { Value = "14\"" }),
                                (favDisplaySize15_6 = new FilterAttributeValue { Value = "15.6\"" }),
                                (favDisplaySize13_3 = new FilterAttributeValue { Value = "13.3\"" }),
                                (favDisplaySize16 = new FilterAttributeValue { Value = "16\"" }),
                                (favDisplaySize17_3 = new FilterAttributeValue { Value = "17.3\"" })
                            }
                        },
                        new FilterAttribute { FilterName = "RAM Size", FilterSlug = "ram-size", DefaultValues = new List<FilterAttributeValue>
                            {
                                (favRam16 = new FilterAttributeValue { Value = "16GB" }),
                                (favRam8 = new FilterAttributeValue { Value = "8GB" }),
                                (favRam4 = new FilterAttributeValue { Value = "4GB" }),
                                (favRam12 = new FilterAttributeValue { Value = "12GB" }),
                                (favRam32 = new FilterAttributeValue { Value = "32GB" })
                            }
                        },
                        new FilterAttribute { FilterName = "RAM Type", FilterSlug = "ram-type", DefaultValues = new List<FilterAttributeValue>
                            {
                                (favRamDdr4 = new FilterAttributeValue { Value = "DDR4" }),
                                (favRamDdr5 = new FilterAttributeValue { Value = "DDR5" })
                            }
                        },
                        new FilterAttribute { FilterName = "HDD", FilterSlug = "hdd", DefaultValues = new List<FilterAttributeValue>
                            {
                                (favHdd500 = new FilterAttributeValue { Value = "500GB" }),
                                (favHdd1tb = new FilterAttributeValue { Value = "1TB" }),
                                (favHdd2tb = new FilterAttributeValue { Value = "2TB" })
                            }
                        },
                        new FilterAttribute { FilterName = "SSD", FilterSlug = "ssd", DefaultValues = new List<FilterAttributeValue>
                            {
                                (favSsd512 = new FilterAttributeValue { Value = "512GB" }),
                                (favSsd1tb = new FilterAttributeValue { Value = "1TB" }),
                                (favSsd128 = new FilterAttributeValue { Value = "128GB" }),
                                (favSsd256 = new FilterAttributeValue { Value = "256GB" }),
                                (favSsd2tb = new FilterAttributeValue { Value = "2TB" }),
                                (favSsd4tb = new FilterAttributeValue { Value = "4TB" })
                            }
                        },
                        new FilterAttribute { FilterName = "Graphics", FilterSlug = "graphics-memory", DefaultValues = new List<FilterAttributeValue>
                            {
                                (favGfxShared = new FilterAttributeValue { Value = "Shared/Integrated" }),
                                (favGfx2gb = new FilterAttributeValue { Value = "Dedicated 2GB" }),
                                (favGfx4gb = new FilterAttributeValue { Value = "Dedicated 4GB" }),
                                (favGfx6gb = new FilterAttributeValue { Value = "Dedicated 6GB" }),
                                (favGfx8gb = new FilterAttributeValue { Value = "Dedicated 8GB" }),
                                (favGfx10gb = new FilterAttributeValue { Value = "Dedicated 10GB" }),
                                (favGfx12gb = new FilterAttributeValue { Value = "Dedicated 12GB" }),
                                (favGfx16gb = new FilterAttributeValue { Value = "Dedicated 16GB" }),
                                (favGfx24gb = new FilterAttributeValue { Value = "Dedicated 24GB" })
                            }
                        },
                        new FilterAttribute { FilterName = "Operating System", FilterSlug = "operating-system", DefaultValues = new List<FilterAttributeValue>
                            {
                                (favOsWin11Pro = new FilterAttributeValue { Value = "Windows 11 Pro" }),
                                (favOsWin11Home = new FilterAttributeValue { Value = "Windows 11 Home" }),
                                (favOsWin10Pro = new FilterAttributeValue { Value = "Windows 10 Pro" }),
                                (favOsWin10Home = new FilterAttributeValue { Value = "Windows 10 Home" }),
                                (favOsMac = new FilterAttributeValue { Value = "macOS" }),
                                (favOsChrome = new FilterAttributeValue { Value = "ChromeOS" }),
                                (favOsLinux = new FilterAttributeValue { Value = "Linux" })
                            }
                        },
                        new FilterAttribute { FilterName = "Special Feature", FilterSlug = "feature", DefaultValues = new List<FilterAttributeValue>
                            {
                                (favFeatBacklit = new FilterAttributeValue { Value = "Backlit Keyboard" }),
                                (favFeatFingerprint = new FilterAttributeValue { Value = "Fingerprint Reader" }),
                                (favFeatRgb = new FilterAttributeValue { Value = "RGB Keyboard" }),
                                (favFeatAdaptiveSync = new FilterAttributeValue { Value = "Adaptive Sync" }),
                                (favFeatStylus = new FilterAttributeValue { Value = "Stylus Support" }),
                                (favFeatConvertible = new FilterAttributeValue { Value = "Convertible" }),
                                (favFeatThinLight = new FilterAttributeValue { Value = "Thin and Light" }),
                                (favFeatHighRefresh = new FilterAttributeValue { Value = "High Refresh Rate Display" })
                            }
                        }
                    }
                }

            };


                var subCategories = new List<SubCategory>
            {
                // Laptop subcategories
                new SubCategory { Name = "UltraBook", Slug = "ultrabook", Category = categories[0] },
                new SubCategory { Name = "Gaming", Slug = "gaming", Category = categories[0] },
                new SubCategory { Name = "Consumer", Slug = "consumer", Category = categories[0] },
                new SubCategory { Name = "Office", Slug = "office", Category = categories[0] }
            };

                context.Brands.AddRange(brands);
                context.Categories.AddRange(categories);
                context.SubCategories.AddRange(subCategories);
                context.SaveChanges();
               
                var products = new List<Product>
            {

                new Product
                {
                    Name = "Lenovo Legion Pro 5i 16IRX8 i7-13700HX RTX 4070 16\" WQXGA 240Hz",
                    Slug = "lenovo-legion-pro-5i-16irx8-i7-rtx4070",
                    Description = "Hardcore gaming with ColdFront 5.0 cooling and RGB Light Sync. Dominate the competition with high refresh rates and powerful hardware.",
                    Price = 250000,
                    DiscountPrice = 238000,
                    Brand = brands[1], 
                    Category = categories[0],
                    SubCategory = subCategories[1],
                    StockQuantity = 12,
                    IsFeatured = true,
                    IsDealOfTheDay = false,
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "/images/products/legion-pro-5-16irx8-01-500x500.png" },
                        new ProductImage { ImageUrl = "/images/products/legion-pro-5-16irx8-02-500x500.png" },
                        new ProductImage { ImageUrl = "/images/products/legion-pro-5-16irx8-04-500x500.png" },
                        new ProductImage { ImageUrl = "/images/products/legion-pro-5-16irx8-06-500x500.png" },
                    },
                    AttributeValues = new List<ProductAttributeValue>
                    {

                        new ProductAttributeValue { Name = "Brand", Slug="brand", Value = "Lenovo", SpecificationCategory = "Brand", FilterAttributeValueId = favBrandLenovo.Id },
                        new ProductAttributeValue { Name = "Subcategory", Slug="subcategory", Value = "Gaming", SpecificationCategory = "subcategory", FilterAttributeValueId = favCatGaming.Id },
                        new ProductAttributeValue { Name = "Processor Type", Slug="processor-type", Value = "Intel Core i7", SpecificationCategory = "Processor", FilterAttributeValueId = favProcI7.Id },
                        new ProductAttributeValue { Name = "Generation/Series", Slug="generation-series", Value = "Intel 13th Gen", SpecificationCategory = "Processor", FilterAttributeValueId = favGenIntel13.Id },
                        new ProductAttributeValue { Name = "Display Size", Slug="display-size", Value = "16\"", SpecificationCategory = "Display", FilterAttributeValueId = favDisplaySize16.Id },
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type", Value = "IPS", SpecificationCategory = "Display", FilterAttributeValueId = favDisplayIps.Id },
                        new ProductAttributeValue { Name = "RAM Size", Slug="ram-size", Value = "16GB", SpecificationCategory = "Memory", FilterAttributeValueId = favRam16.Id },
                        new ProductAttributeValue { Name = "RAM Type", Slug="ram-type", Value = "DDR5", SpecificationCategory = "Memory", FilterAttributeValueId = favRamDdr5.Id },
                        new ProductAttributeValue { Name = "SSD", Slug="ssd", Value = "1TB", SpecificationCategory = "Storage", FilterAttributeValueId = favSsd1tb.Id },
                        new ProductAttributeValue { Name = "Graphics Memory", Slug="graphics-memory", Value = "Dedicated 8GB", SpecificationCategory = "Graphics", FilterAttributeValueId = favGfx8gb.Id }, // Corresponds to RTX 4070 8GB
                        new ProductAttributeValue { Name = "Operating System", Slug="operating-system", Value = "Windows 11 Home", SpecificationCategory = "Software", FilterAttributeValueId = favOsWin11Home.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-rgb", Value = "RGB Keyboard", SpecificationCategory = "Key Feature", FilterAttributeValueId = favFeatRgb.Id }, // Key Feature 1
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-high-refresh", Value = "High Refresh Rate Display", SpecificationCategory = "Key Feature", FilterAttributeValueId = favFeatHighRefresh.Id }, // Key Feature 2 - Added ID
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-adaptive-sync", Value = "Adaptive Sync", SpecificationCategory = "Display", FilterAttributeValueId = favFeatAdaptiveSync.Id }, // Related to display


                        new ProductAttributeValue { Name = "Processor Model", Slug="processor-model", Value = "Core i7-13700HX", SpecificationCategory = "Processor", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Processor Cores/Threads", Slug="processor-cores-threads", Value = "16 Cores / 24 Threads", SpecificationCategory = "Processor", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Processor Max Turbo Frequency", Slug="processor-max-turbo", Value = "5.0 GHz", SpecificationCategory = "Processor", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Processor Cache", Slug="processor-cache", Value = "30MB L3", SpecificationCategory = "Processor", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Display Resolution", Slug="display-resolution", Value = "2560x1600 (WQXGA)", SpecificationCategory = "Display", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Display Refresh Rate", Slug="display-refresh-rate", Value = "240Hz", SpecificationCategory = "Display", FilterAttributeValueId = null }, // Explicit value
                        new ProductAttributeValue { Name = "Display Brightness", Slug="display-brightness", Value = "500 nits", SpecificationCategory = "Display", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Display Color Gamut", Slug="display-color-gamut", Value = "100% sRGB", SpecificationCategory = "Display", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Memory Speed", Slug="memory-speed", Value = "4800MHz", SpecificationCategory = "Memory", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Storage Type", Slug="storage-type", Value = "NVMe PCIe 4.0 SSD", SpecificationCategory = "Storage", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Graphics Card Model", Slug="graphics-model", Value = "NVIDIA GeForce RTX 4070 Laptop GPU", SpecificationCategory = "Graphics", FilterAttributeValueId = null }, // Explicit GPU model
                        new ProductAttributeValue { Name = "Graphics TGP", Slug="graphics-tgp", Value = "140W", SpecificationCategory = "Graphics", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "MUX Switch", Slug="mux-switch", Value = "Yes, with Advanced Optimus", SpecificationCategory = "Key Feature", FilterAttributeValueId = null }, // Key Feature 3
                        new ProductAttributeValue { Name = "Wireless Connectivity", Slug="wireless-connectivity", Value = "Wi-Fi 6E, Bluetooth 5.2", SpecificationCategory = "Connectivity", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Ports", Slug="ports", Value = "1x Thunderbolt 4, 3x USB-A 3.2, 1x USB-C 3.2 (DP/PD), HDMI 2.1, Ethernet (RJ-45), Audio Jack", SpecificationCategory = "Connectivity", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Webcam", Slug="webcam", Value = "1080p FHD with E-Shutter", SpecificationCategory = "Features", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Battery", Slug="battery", Value = "80Wh", SpecificationCategory = "Power", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Weight", Slug="weight", Value = "Approx. 2.5 kg", SpecificationCategory = "Physical", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Cooling System", Slug="cooling-system", Value = "Legion ColdFront 5.0", SpecificationCategory = "Key Feature", FilterAttributeValueId = null } // Key Feature 4
                    },
                    Reviews = new List<ProductReview>
                    {
                        new ProductReview { ReviewerName = "GamerXtreme", Comment = "Absolute beast for gaming, the 240Hz screen is insane!", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-15) },
                        new ProductReview { ReviewerName = "ProductivityPro", Comment = "Handles heavy workloads well, but it's quite bulky.", Rating = 4, CreatedAt = DateTime.UtcNow.AddDays(-8) }
                    },
                    Questions = new List<ProductQuestion>
                    {
                        new ProductQuestion { Question = "Can the RAM be upgraded?", Answer = "Yes, it supports up to 32GB DDR5 RAM via two SODIMM slots.", CreatedAt = DateTime.UtcNow.AddDays(-5) }
                    },
                    Visits = new List<ProductVisit>
                    {

                    }
                },


                new Product
                {
                    Name = "Apple MacBook Air 13-inch M2 Chip 8GB RAM 256GB SSD",
                    Slug = "apple-macbook-air-13-m2-8gb-256gb",
                    Description = "Incredibly thin and light, the MacBook Air with the M2 chip delivers exceptional performance and up to 18 hours of battery life.",
                    Price = 100000,
                    DiscountPrice = null,
                    Brand = brands[4], // Apple
                    Category = categories[0],
                    SubCategory = subCategories[0], // Ultrabook
                    StockQuantity = 55,
                    IsFeatured = false,
                    IsDealOfTheDay = false,
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "/images/products/MacBook-Air-M2-13.6-inch-8256GB-space-gray-6746.png" },
                        new ProductImage { ImageUrl = "/images/products/MacBook-Air-M2-13.6-inch-8256GB-starlight-4317.png" }
                    },
                    AttributeValues = new List<ProductAttributeValue>
                    {
                        // Filter Attributes
                        new ProductAttributeValue { Name = "Brand", Slug="brand", Value = "Apple", SpecificationCategory = "Brand", FilterAttributeValueId = favBrandApple.Id },
                        new ProductAttributeValue { Name = "Subcategory", Slug="subcategory", Value = "Ultrabook", SpecificationCategory = "subcategory", FilterAttributeValueId = favCatUltrabook.Id },
                        new ProductAttributeValue { Name = "Processor Type", Slug="processor-type", Value = "Apple M2", SpecificationCategory = "Processor", FilterAttributeValueId = null }, // "Apple M2" is not in favProc* list
                        new ProductAttributeValue { Name = "Generation/Series", Slug="generation-series", Value = "Apple Silicon M2", SpecificationCategory = "Processor", FilterAttributeValueId = null }, // Not in favGen* list
                        new ProductAttributeValue { Name = "Display Size", Slug="display-size", Value = "13.6\"", SpecificationCategory = "Display", FilterAttributeValueId = null }, // "13.6\"" is not in favDisplaySize* list (favDisplaySize13_3 is "13.3\"")
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type", Value = "Liquid Retina", SpecificationCategory = "Display", FilterAttributeValueId = null }, // "Liquid Retina" is not in favDisplay* list
                        new ProductAttributeValue { Name = "RAM Size", Slug="ram-size", Value = "8GB", SpecificationCategory = "Memory", FilterAttributeValueId = favRam8.Id },
                        new ProductAttributeValue { Name = "RAM Type", Slug="ram-type", Value = "Unified Memory", SpecificationCategory = "Memory", FilterAttributeValueId = null }, // "Unified Memory" is not in favRamDdr* list
                        new ProductAttributeValue { Name = "SSD", Slug="ssd", Value = "256GB", SpecificationCategory = "Storage", FilterAttributeValueId = favSsd256.Id },
                        new ProductAttributeValue { Name = "Graphics Memory", Slug="graphics-memory", Value = "Shared/Integrated", SpecificationCategory = "Graphics", FilterAttributeValueId = favGfxShared.Id },
                        new ProductAttributeValue { Name = "Operating System", Slug="operating-system", Value = "macOS", SpecificationCategory = "Software", FilterAttributeValueId = favOsMac.Id },
                        // For "Special Feature", match based on Value if the ProductAttributeValue.Slug aligns with FilterAttribute.FilterSlug="feature"
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-fingerprint", Value = "Fingerprint Reader", SpecificationCategory = "Security", FilterAttributeValueId = favFeatFingerprint.Id }, // Touch ID
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-thin-light", Value = "Thin and Light", SpecificationCategory = "Key Feature", FilterAttributeValueId = favFeatThinLight.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-backlit-keyboard", Value = "Backlit Keyboard", SpecificationCategory = "Keyboard", FilterAttributeValueId = favFeatBacklit.Id },

                        // Detailed Attributes
                        new ProductAttributeValue { Name = "Processor Model", Slug="processor-model", Value = "Apple M2 chip", SpecificationCategory = "Processor", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Processor Cores", Slug="processor-cores", Value = "8-core CPU (4 performance, 4 efficiency)", SpecificationCategory = "Processor", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "GPU Cores", Slug="gpu-cores", Value = "8-core GPU", SpecificationCategory = "Graphics", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Neural Engine", Slug="neural-engine", Value = "16-core", SpecificationCategory = "Processor", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Display Resolution", Slug="display-resolution", Value = "2560x1664", SpecificationCategory = "Display", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Display Brightness", Slug="display-brightness", Value = "500 nits", SpecificationCategory = "Display", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Display Technology", Slug="display-technology", Value = "True Tone technology", SpecificationCategory = "Display", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Memory Bandwidth", Slug="memory-bandwidth", Value = "100GB/s", SpecificationCategory = "Memory", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Storage Type", Slug="storage-type", Value = "Superfast SSD storage", SpecificationCategory = "Storage", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Wireless Connectivity", Slug="wireless-connectivity", Value = "Wi-Fi 6 (802.11ax), Bluetooth 5.3", SpecificationCategory = "Connectivity", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Ports", Slug="ports", Value = "2x Thunderbolt / USB 4 ports, MagSafe 3 charging port, 3.5mm headphone jack", SpecificationCategory = "Connectivity", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Webcam", Slug="webcam", Value = "1080p FaceTime HD camera", SpecificationCategory = "Features", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Audio", Slug="audio", Value = "Four-speaker sound system with Spatial Audio", SpecificationCategory = "Key Feature", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Keyboard", Slug="keyboard", Value = "Magic Keyboard with Touch ID", SpecificationCategory = "Keyboard", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Trackpad", Slug="trackpad", Value = "Force Touch trackpad", SpecificationCategory = "Features", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Battery Life", Slug="battery-life", Value = "Up to 18 hours", SpecificationCategory = "Key Feature", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Weight", Slug="weight", Value = "1.24 kg (2.7 pounds)", SpecificationCategory = "Physical", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Build Material", Slug="build-material", Value = "Recycled Aluminum Enclosure", SpecificationCategory = "Key Feature", FilterAttributeValueId = null }
                    },
                    Reviews = new List<ProductReview>
                    {
                        new ProductReview { ReviewerName = "StudentLife", Comment = "Perfect for classes, battery lasts all day!", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-30) },
                        new ProductReview { ReviewerName = "CasualUser", Comment = "So fast and quiet. Love the display.", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-10) }
                    },
                    Questions = new List<ProductQuestion>
                    {
                        new ProductQuestion { Question = "Does it run Windows?", Answer = "Natively, no. macOS is the operating system. Windows can be run via virtualization software like Parallels, but it's not officially supported by Apple for M-series chips.", CreatedAt = DateTime.UtcNow.AddDays(-7) }
                    },
                    Visits = new List<ProductVisit>
                    {

                    }
                },


                new Product
                {
                    Name = "Dell XPS 15 9530 i7-13700H RTX 4050 16GB 1TB OLED Touch",
                    Slug = "dell-xps-15-9530-i7-rtx4050-oled",
                    Description = "Stunning OLED display meets powerful performance in a sleek chassis. Ideal for creators and professionals demanding quality and portability.",
                    Price = 230000,
                    DiscountPrice = 178000,
                    Brand = brands[7], // DELL
                    Category = categories[0],
                    SubCategory = subCategories[0], // Ultrabook (could also fit Consumer/Office)
                    StockQuantity = 25,
                    IsFeatured = false,
                    IsDealOfTheDay = false,
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "/images/products/XPS-15-9530-c-1421.png" },
                        new ProductImage { ImageUrl = "/images/products/XPS-15-9530-a-9440.png" },
                        new ProductImage { ImageUrl = "/images/products/XPS-15-9530-d-2553.png" },
                    },
                    // Product 3: Dell XPS 15 (Example)
                    AttributeValues = new List<ProductAttributeValue>
                    {
                        // Filter Attributes
                        new ProductAttributeValue { Name = "Brand", Slug="brand", Value = "Dell", SpecificationCategory = "Brand", FilterAttributeValueId = favBrandDell.Id },
                        new ProductAttributeValue { Name = "Subcategory", Slug="subcategory", Value = "Ultrabook", SpecificationCategory = "subcategory", FilterAttributeValueId = favCatUltrabook.Id },
                        new ProductAttributeValue { Name = "Processor Type", Slug="processor-type", Value = "Intel Core i7", SpecificationCategory = "Processor", FilterAttributeValueId = favProcI7.Id },
                        new ProductAttributeValue { Name = "Generation/Series", Slug="generation-series", Value = "Intel 13th Gen", SpecificationCategory = "Processor", FilterAttributeValueId = favGenIntel13.Id },
                        new ProductAttributeValue { Name = "Display Size", Slug="display-size", Value = "15.6\"", SpecificationCategory = "Display", FilterAttributeValueId = favDisplaySize15_6.Id },
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type", Value = "OLED", SpecificationCategory = "Key Feature", FilterAttributeValueId = favDisplayOled.Id }, // OLED Display
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type-touch", Value = "Touch Display", SpecificationCategory = "Display", FilterAttributeValueId = favDisplayTouch.Id }, // Is also touch
                        new ProductAttributeValue { Name = "RAM Size", Slug="ram-size", Value = "16GB", SpecificationCategory = "Memory", FilterAttributeValueId = favRam16.Id },
                        new ProductAttributeValue { Name = "RAM Type", Slug="ram-type", Value = "DDR5", SpecificationCategory = "Memory", FilterAttributeValueId = favRamDdr5.Id },
                        new ProductAttributeValue { Name = "SSD", Slug="ssd", Value = "1TB", SpecificationCategory = "Storage", FilterAttributeValueId = favSsd1tb.Id },
                        new ProductAttributeValue { Name = "Graphics Memory", Slug="graphics-memory", Value = "Dedicated 6GB", SpecificationCategory = "Graphics", FilterAttributeValueId = favGfx6gb.Id }, // Corresponds to RTX 4050 6GB
                        new ProductAttributeValue { Name = "Operating System", Slug="operating-system", Value = "Windows 11 Pro", SpecificationCategory = "Software", FilterAttributeValueId = favOsWin11Pro.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-fingerprint", Value = "Fingerprint Reader", SpecificationCategory = "Security", FilterAttributeValueId = favFeatFingerprint.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-backlit-keyboard", Value = "Backlit Keyboard", SpecificationCategory = "Keyboard", FilterAttributeValueId = favFeatBacklit.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-thin-light", Value = "Thin and Light", SpecificationCategory = "Physical", FilterAttributeValueId = favFeatThinLight.Id },

                        // Detailed Attributes
                        new ProductAttributeValue { Name = "Processor Model", Slug="processor-model", Value = "Core i7-13700H", SpecificationCategory = "Processor", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Processor Cores/Threads", Slug="processor-cores-threads", Value = "14 Cores (6P+8E) / 20 Threads", SpecificationCategory = "Processor", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Processor Max Turbo Frequency", Slug="processor-max-turbo", Value = "5.0 GHz", SpecificationCategory = "Processor", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Display Resolution", Slug="display-resolution", Value = "3456x2160 (3.5K)", SpecificationCategory = "Display", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Display Aspect Ratio", Slug="display-aspect-ratio", Value = "16:10", SpecificationCategory = "Display", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Display Brightness", Slug="display-brightness", Value = "400 nits", SpecificationCategory = "Display", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Display Color Gamut", Slug="display-color-gamut", Value = "100% DCI-P3", SpecificationCategory = "Display", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Memory Speed", Slug="memory-speed", Value = "4800MHz", SpecificationCategory = "Memory", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Storage Type", Slug="storage-type", Value = "NVMe PCIe 4.0 SSD", SpecificationCategory = "Storage", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Graphics Card Model", Slug="graphics-model", Value = "NVIDIA GeForce RTX 4050 Laptop GPU", SpecificationCategory = "Graphics", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Wireless Connectivity", Slug="wireless-connectivity", Value = "Intel Killer Wi-Fi 6E, Bluetooth 5.3", SpecificationCategory = "Connectivity", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Ports", Slug="ports", Value = "2x Thunderbolt 4 (DP/PD), 1x USB-C 3.2 Gen 2 (DP/PD), SD card reader, 3.5mm jack", SpecificationCategory = "Connectivity", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Webcam", Slug="webcam", Value = "720p HD with Windows Hello IR", SpecificationCategory = "Features", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Audio", Slug="audio", Value = "Quad-speaker design with Waves Nx 3D audio", SpecificationCategory = "Key Feature", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Battery", Slug="battery", Value = "86Wh", SpecificationCategory = "Power", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Weight", Slug="weight", Value = "Starting at 1.92 kg (4.23 lbs)", SpecificationCategory = "Physical", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "Build Material", Slug="build-material", Value = "CNC Machined Aluminum, Carbon Fiber Palm Rest", SpecificationCategory = "Key Feature", FilterAttributeValueId = null },
                        new ProductAttributeValue { Name = "InfinityEdge Display", Slug="infinityedge-display", Value = "Minimal Bezel Design", SpecificationCategory = "Key Feature", FilterAttributeValueId = null }
                    },
                    Reviews = new List<ProductReview>
                    {
                        new ProductReview { ReviewerName = "Designer Deb", Comment = "The OLED screen is absolutely gorgeous for photo editing!", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-45) },
                        new ProductReview { ReviewerName = "ExecOnTheGo", Comment = "Powerful, portable, and looks professional. Speakers could be louder.", Rating = 4, CreatedAt = DateTime.UtcNow.AddDays(-20) }
                    },
                    Questions = new List<ProductQuestion>
                    {
                        new ProductQuestion { Question = "Does it come with a pen?", Answer = "A stylus is not typically included with the XPS 15 but the touch display supports active pen input (sold separately).", CreatedAt = DateTime.UtcNow.AddDays(-12) }
                    },
                    Visits = new List<ProductVisit>
                    {

                    }
                },


                new Product
                {
                    Name = "HP Spectre x360 14-ef2023nr i7-1355U 16GB 1TB OLED Touch",
                    Slug = "hp-spectre-x360-14-ef2023nr-i7-oled",
                    Description = "Premium 2-in-1 convertible laptop with a stunning OLED touch display, exceptional craftsmanship, and versatile performance.",
                    Price = 160000,
                    DiscountPrice = null,
                    Brand = brands[8], // HP
                    Category = categories[0],
                    SubCategory = subCategories[0], // Ultrabook (also Convertible)
                    StockQuantity = 30,
                    IsFeatured = false,
                    IsDealOfTheDay = false,
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "/images/products/14-e2027tu-04-500x500.png" },
                        new ProductImage { ImageUrl = "/images/products/14-e2027tu-041-500x500.png" }
                    },
                    AttributeValues = new List<ProductAttributeValue>
                    {
                        // Filter Attributes
                        new ProductAttributeValue { Name = "Brand", Slug="brand", Value = "HP", SpecificationCategory = "Brand", FilterAttributeValueId = favBrandHp.Id },
                        new ProductAttributeValue { Name = "Subcategory", Slug="subcategory", Value = "Ultrabook", SpecificationCategory = "subcategory", FilterAttributeValueId = favCatUltrabook.Id },
                        new ProductAttributeValue { Name = "Processor Type", Slug="processor-type", Value = "Intel Core i7", SpecificationCategory = "Processor", FilterAttributeValueId = favProcI7.Id },
                        new ProductAttributeValue { Name = "Generation/Series", Slug="generation-series", Value = "Intel 13th Gen", SpecificationCategory = "Processor", FilterAttributeValueId = favGenIntel13.Id },
                        new ProductAttributeValue { Name = "Display Size", Slug="display-size", Value = "13.5\"", SpecificationCategory = "Display", FilterAttributeValueId = favDisplaySize13_3.Id }, // Closest match
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type-oled", Value = "OLED", SpecificationCategory = "Display", FilterAttributeValueId = favDisplayOled.Id },
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type-touch", Value = "Touch Display", SpecificationCategory = "Key Feature", FilterAttributeValueId = favDisplayTouch.Id },
                        new ProductAttributeValue { Name = "RAM Size", Slug="ram-size", Value = "16GB", SpecificationCategory = "Memory", FilterAttributeValueId = favRam16.Id },
                        new ProductAttributeValue { Name = "RAM Type", Slug="ram-type", Value = "LPDDR4x (Onboard)", SpecificationCategory = "Memory" }, // No exact match
                        new ProductAttributeValue { Name = "SSD", Slug="ssd", Value = "1TB", SpecificationCategory = "Storage", FilterAttributeValueId = favSsd1tb.Id },
                        new ProductAttributeValue { Name = "Graphics Memory", Slug="graphics-memory", Value = "Shared/Integrated", SpecificationCategory = "Graphics", FilterAttributeValueId = favGfxShared.Id },
                        new ProductAttributeValue { Name = "Operating System", Slug="operating-system", Value = "Windows 11 Home", SpecificationCategory = "Software", FilterAttributeValueId = favOsWin11Home.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-fingerprint", Value = "Fingerprint Reader", SpecificationCategory = "Security", FilterAttributeValueId = favFeatFingerprint.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-stylus", Value = "Stylus Support", SpecificationCategory = "Features", FilterAttributeValueId = favFeatStylus.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-convertible", Value = "Convertible", SpecificationCategory = "Key Feature", FilterAttributeValueId = favFeatConvertible.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-backlit-keyboard", Value = "Backlit Keyboard", SpecificationCategory = "Keyboard", FilterAttributeValueId = favFeatBacklit.Id },

                        // Detailed Attributes (remain unchanged as they don't have filter matches)
                        new ProductAttributeValue { Name = "Processor Model", Slug="processor-model", Value = "Core i7-1355U", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Processor Cores/Threads", Slug="processor-cores-threads", Value = "10 Cores (2P+8E) / 12 Threads", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Processor Max Turbo Frequency", Slug="processor-max-turbo", Value = "5.0 GHz", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Graphics Card Model", Slug="graphics-model", Value = "Intel Iris Xe Graphics", SpecificationCategory = "Graphics" },
                        new ProductAttributeValue { Name = "Display Resolution", Slug="display-resolution", Value = "3000x2000 (3K2K)", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Aspect Ratio", Slug="display-aspect-ratio", Value = "3:2", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Features", Slug="display-features", Value = "Corning Gorilla Glass NBT, Anti-reflection", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Storage Type", Slug="storage-type", Value = "NVMe PCIe 4.0 SSD", SpecificationCategory = "Storage" },
                        new ProductAttributeValue { Name = "Wireless Connectivity", Slug="wireless-connectivity", Value = "Intel Wi-Fi 6E AX211, Bluetooth 5.3", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Ports", Slug="ports", Value = "2x Thunderbolt 4 (DP/PD), 1x USB-A 3.2 Gen 2, MicroSD card reader, 3.5mm jack", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Webcam", Slug="webcam", Value = "HP True Vision 5MP IR camera with shutter", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Audio", Slug="audio", Value = "Audio by Bang & Olufsen; Quad speakers", SpecificationCategory = "Key Feature" },
                        new ProductAttributeValue { Name = "Battery", Slug="battery", Value = "66Wh", SpecificationCategory = "Power" },
                        new ProductAttributeValue { Name = "Included Accessories", Slug="included-accessories", Value = "HP Rechargeable MPP2.0 Tilt Pen", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Weight", Slug="weight", Value = "Approx. 1.36 kg (3.0 lbs)", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "Build Material", Slug="build-material", Value = "Aluminum chassis with gem-cut design", SpecificationCategory = "Key Feature" }
                    },
                    Reviews = new List<ProductReview>
                    {
                        new ProductReview { ReviewerName = "ArtistAnna", Comment = "Love the 3:2 screen and the included pen for drawing!", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-60) },
                        new ProductReview { ReviewerName = "MobileProf", Comment = "Versatile and stylish. Battery life is good for moderate use.", Rating = 4, CreatedAt = DateTime.UtcNow.AddDays(-25) }
                    },
                    Questions = new List<ProductQuestion>
                    {
                        new ProductQuestion { Question = "Is the RAM user-upgradeable?", Answer = "No, the RAM on the Spectre x360 14 series is typically soldered to the motherboard and cannot be upgraded after purchase.", CreatedAt = DateTime.UtcNow.AddDays(-18) }
                    },
                    Visits = new List<ProductVisit>
                    {

                    }
                },


                new Product
                {
                    Name = "Asus ROG Zephyrus G14 GA402XV Ryzen 9 7940HS RTX 4060 16GB 1TB QHD+ 165Hz",
                    Slug = "asus-rog-zephyrus-g14-ga402xv-r9-rtx4060",
                    Description = "The ultimate blend of portability and power. A compact 14-inch gaming laptop with high-end specs and a stunning Nebula Display.",
                    Price = 265000,
                    DiscountPrice = null,
                    Brand = brands[0], // Asus
                    Category = categories[0],
                    SubCategory = subCategories[1], // Gaming (but very portable, almost Ultrabook)
                    StockQuantity = 18,
                    IsFeatured = false,
                    IsDealOfTheDay = true,
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "/images/products/GA402XV-2020.png" },
                        new ProductImage { ImageUrl = "/images/products/GA402XV-(1)-8970.png" },
                        new ProductImage { ImageUrl = "/images/products/GA402XV-(2)-7563.png" },
                    },
                    AttributeValues = new List<ProductAttributeValue>
                    {
                        // Filter Attributes
                        new ProductAttributeValue { Name = "Brand", Slug="brand", Value = "Asus", SpecificationCategory = "Brand", FilterAttributeValueId = favBrandAsus.Id },
                        new ProductAttributeValue { Name = "Subcategory", Slug="subcategory", Value = "Gaming", SpecificationCategory = "subcategory", FilterAttributeValueId =  favCatGaming.Id},
                        new ProductAttributeValue { Name = "Processor Type", Slug="processor-type", Value = "AMD Ryzen 9", SpecificationCategory = "Processor", FilterAttributeValueId = favProcRyzen9.Id },
                        new ProductAttributeValue { Name = "Generation/Series", Slug="generation-series", Value = "Ryzen 7000 Series", SpecificationCategory = "Processor", FilterAttributeValueId = favGenRyzen7000.Id },
                        new ProductAttributeValue { Name = "Display Size", Slug="display-size", Value = "14\"", SpecificationCategory = "Display", FilterAttributeValueId = favDisplaySize14.Id },
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type", Value = "IPS", SpecificationCategory = "Display", FilterAttributeValueId = favDisplayIps.Id },
                        new ProductAttributeValue { Name = "RAM Size", Slug="ram-size", Value = "16GB", SpecificationCategory = "Memory", FilterAttributeValueId = favRam16.Id },
                        new ProductAttributeValue { Name = "RAM Type", Slug="ram-type", Value = "DDR5", SpecificationCategory = "Memory", FilterAttributeValueId = favRamDdr5.Id },
                        new ProductAttributeValue { Name = "SSD", Slug="ssd", Value = "1TB", SpecificationCategory = "Storage", FilterAttributeValueId = favSsd1tb.Id },
                        new ProductAttributeValue { Name = "Graphics Memory", Slug="graphics-memory", Value = "Dedicated 8GB", SpecificationCategory = "Graphics", FilterAttributeValueId = favGfx8gb.Id },
                        new ProductAttributeValue { Name = "Operating System", Slug="operating-system", Value = "Windows 11 Home", SpecificationCategory = "Software", FilterAttributeValueId = favOsWin11Home.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-high-refresh", Value = "High Refresh Rate Display", SpecificationCategory = "Key Feature", FilterAttributeValueId = favFeatHighRefresh.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-adaptive-sync", Value = "Adaptive Sync", SpecificationCategory = "Display", FilterAttributeValueId = favFeatAdaptiveSync.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-backlit-keyboard", Value = "Backlit Keyboard", SpecificationCategory = "Keyboard", FilterAttributeValueId = favFeatBacklit.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-thin-light", Value = "Thin and Light", SpecificationCategory = "Key Feature", FilterAttributeValueId = favFeatThinLight.Id },

                        // Detailed Attributes (remain unchanged as they don't have filter matches)
                        new ProductAttributeValue { Name = "Processor Model", Slug="processor-model", Value = "AMD Ryzen 9 7940HS", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Processor Cores/Threads", Slug="processor-cores-threads", Value = "8 Cores / 16 Threads", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Processor Max Boost Clock", Slug="processor-max-boost", Value = "Up to 5.2 GHz", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Graphics Card Model", Slug="graphics-model", Value = "NVIDIA GeForce RTX 4060 Laptop GPU", SpecificationCategory = "Graphics" },
                        new ProductAttributeValue { Name = "Graphics TGP", Slug="graphics-tgp", Value = "125W (with Dynamic Boost)", SpecificationCategory = "Graphics" },
                        new ProductAttributeValue { Name = "MUX Switch", Slug="mux-switch", Value = "Yes, with NVIDIA Advanced Optimus", SpecificationCategory = "Graphics" },
                        new ProductAttributeValue { Name = "Display Resolution", Slug="display-resolution", Value = "2560x1600 (QHD+)", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Name", Slug="display-name", Value = "ROG Nebula Display", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Refresh Rate", Slug="display-refresh-rate", Value = "165Hz", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Color Gamut", Slug="display-color-gamut", Value = "100% DCI-P3, Pantone Validated", SpecificationCategory = "Key Feature" },
                        new ProductAttributeValue { Name = "Memory Configuration", Slug="memory-config", Value = "8GB Onboard + 8GB SODIMM", SpecificationCategory = "Memory" },
                        new ProductAttributeValue { Name = "Storage Type", Slug="storage-type", Value = "NVMe PCIe 4.0 SSD", SpecificationCategory = "Storage" },
                        new ProductAttributeValue { Name = "Wireless Connectivity", Slug="wireless-connectivity", Value = "Wi-Fi 6E, Bluetooth 5.3", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Ports", Slug="ports", Value = "1x USB4 (Type-C, DP/PD), 1x USB-C 3.2 Gen 2 (DP/PD), 2x USB-A 3.2 Gen 2, HDMI 2.1, MicroSD reader, 3.5mm jack", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Webcam", Slug="webcam", Value = "1080P FHD IR Camera for Windows Hello", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Audio", Slug="audio", Value = "Quad Speakers with Dolby Atmos", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Battery", Slug="battery", Value = "76Wh", SpecificationCategory = "Power" },
                        new ProductAttributeValue { Name = "Weight", Slug="weight", Value = "Approx. 1.72 kg (3.79 lbs)", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "AniMe Matrix Display", Slug="anime-matrix", Value = "Optional LED display on lid", SpecificationCategory = "Key Feature" }
                    },
                    Reviews = new List<ProductReview>
                    {
                        new ProductReview { ReviewerName = "RoadWarriorGamer", Comment = "Incredible performance in such a small package! Runs everything I throw at it.", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-35) },
                        new ProductReview { ReviewerName = "StudentDev", Comment = "Great for both coding and gaming after class. Gets a bit hot under load.", Rating = 4, CreatedAt = DateTime.UtcNow.AddDays(-15) }
                    },
                    Questions = new List<ProductQuestion>
                    {
                        new ProductQuestion { Question = "Can I upgrade both RAM sticks?", Answer = "No, typically one RAM module is soldered onboard in the G14 series, and one SODIMM slot is available for upgrade.", CreatedAt = DateTime.UtcNow.AddDays(-10) }
                    },
                    Visits = new List<ProductVisit>
                    {

                    }
                },

                new Product
                {
                    Name = "Microsoft Surface Laptop 5 13.5\" i5-1235U 8GB 512GB Platinum",
                    Slug = "microsoft-surface-laptop-5-13-i5-8gb-512gb",
                    Description = "Sleek, ultra-portable laptop with a vibrant PixelSense touchscreen, premium finish, and all-day battery life for productivity.",
                    Price = 149000,
                    DiscountPrice = null,
                    Brand = brands[9], // Microsoft
                    Category = categories[0],
                    SubCategory = subCategories[3], // Office (or Ultrabook)
                    StockQuantity = 40,
                    IsFeatured = true,
                    IsDealOfTheDay = false,
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "/images/products/microsoft-surface-laptop-5-intel-core-i5-1235u-21683092175.png" },
                        new ProductImage { ImageUrl = "/images/products/microsoft-surface-laptop-5-intel-core-i5-1235u-11683092174.png" }
                    },
                    AttributeValues = new List<ProductAttributeValue>
                    {
                        // Filter Attributes
                        new ProductAttributeValue { Name = "Brand", Slug="brand", Value = "Microsoft", SpecificationCategory = "Brand", FilterAttributeValueId = favBrandMicrosoft.Id },
                        new ProductAttributeValue { Name = "Subcategory", Slug="subcategory", Value = "Office", SpecificationCategory = "subcategory", FilterAttributeValueId = favCatOffice.Id  },
                        new ProductAttributeValue { Name = "Processor Type", Slug="processor-type", Value = "Intel Core i5", SpecificationCategory = "Processor", FilterAttributeValueId = favProcI5.Id },
                        new ProductAttributeValue { Name = "Generation/Series", Slug="generation-series", Value = "Intel 12th Gen", SpecificationCategory = "Processor", FilterAttributeValueId = favGenIntel12.Id },
                        new ProductAttributeValue { Name = "Display Size", Slug="display-size", Value = "13.5\"", SpecificationCategory = "Display", FilterAttributeValueId = favDisplaySize13_3.Id }, // Closest match
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type-touch", Value = "Touch Display", SpecificationCategory = "Key Feature", FilterAttributeValueId = favDisplayTouch.Id },
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type-ips", Value = "IPS", SpecificationCategory = "Display", FilterAttributeValueId = favDisplayIps.Id },
                        new ProductAttributeValue { Name = "RAM Size", Slug="ram-size", Value = "8GB", SpecificationCategory = "Memory", FilterAttributeValueId = favRam8.Id },
                        new ProductAttributeValue { Name = "RAM Type", Slug="ram-type", Value = "LPDDR5x (Onboard)", SpecificationCategory = "Memory" }, // No exact match
                        new ProductAttributeValue { Name = "SSD", Slug="ssd", Value = "512GB", SpecificationCategory = "Storage", FilterAttributeValueId = favSsd512.Id },
                        new ProductAttributeValue { Name = "Graphics Memory", Slug="graphics-memory", Value = "Shared/Integrated", SpecificationCategory = "Graphics", FilterAttributeValueId = favGfxShared.Id },
                        new ProductAttributeValue { Name = "Operating System", Slug="operating-system", Value = "Windows 11 Home", SpecificationCategory = "Software", FilterAttributeValueId = favOsWin11Home.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-thin-light", Value = "Thin and Light", SpecificationCategory = "Physical", FilterAttributeValueId = favFeatThinLight.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-stylus", Value = "Stylus Support", SpecificationCategory = "Features", FilterAttributeValueId = favFeatStylus.Id },

                        // Detailed Attributes (remain unchanged as they don't have filter matches)
                        new ProductAttributeValue { Name = "Processor Model", Slug="processor-model", Value = "Core i5-1235U", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Processor Cores/Threads", Slug="processor-cores-threads", Value = "10 Cores (2P+8E) / 12 Threads", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Graphics Card Model", Slug="graphics-model", Value = "Intel Iris Xe Graphics", SpecificationCategory = "Graphics" },
                        new ProductAttributeValue { Name = "Display Resolution", Slug="display-resolution", Value = "2256 x 1504", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Name", Slug="display-name", Value = "PixelSense Display", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Aspect Ratio", Slug="display-aspect-ratio", Value = "3:2", SpecificationCategory = "Key Feature" },
                        new ProductAttributeValue { Name = "Display Protection", Slug="display-protection", Value = "Corning Gorilla Glass 5", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Storage Type", Slug="storage-type", Value = "Removable NVMe SSD", SpecificationCategory = "Storage" },
                        new ProductAttributeValue { Name = "Wireless Connectivity", Slug="wireless-connectivity", Value = "Wi-Fi 6, Bluetooth 5.1", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Ports", Slug="ports", Value = "1x USB-C with Thunderbolt 4, 1x USB-A 3.1, Surface Connect port, 3.5mm jack", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Webcam", Slug="webcam", Value = "720p HD with Windows Hello face authentication", SpecificationCategory = "Security" },
                        new ProductAttributeValue { Name = "Audio", Slug="audio", Value = "Omnisonic Speakers with Dolby Atmos", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Keyboard", Slug="keyboard", Value = "Backlit Keyboard with Alcantara or Metal finish option", SpecificationCategory = "Key Feature" },
                        new ProductAttributeValue { Name = "Battery Life", Slug="battery-life", Value = "Up to 18 hours (typical usage)", SpecificationCategory = "Power" },
                        new ProductAttributeValue { Name = "Weight", Slug="weight", Value = "Starting at 1.27 kg (2.80 lbs)", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "Build Material", Slug="build-material", Value = "Aluminum Casing", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "Security Features", Slug="security-features", Value = "Firmware TPM 2.0, Windows Hello Face Sign-in", SpecificationCategory = "Key Feature" },
                        new ProductAttributeValue { Name = "Included Software", Slug="included-software", Value = "Microsoft 365 Family 30-day trial", SpecificationCategory = "Software" },
                        new ProductAttributeValue { Name = "Color", Slug="color", Value = "Platinum", SpecificationCategory = "Physical" }
                    },
                    Reviews = new List<ProductReview>
                    {
                        new ProductReview { ReviewerName = "OfficeWorker", Comment = "Love the keyboard and the 3:2 screen for documents. Very premium feel.", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-50) },
                        new ProductReview { ReviewerName = "TravelerTom", Comment = "Lightweight and great battery life. Wish it had more ports.", Rating = 4, CreatedAt = DateTime.UtcNow.AddDays(-22) }
                    },
                    Questions = new List<ProductQuestion>
                    {
                        new ProductQuestion { Question = "Is the Alcantara durable?", Answer = "The Alcantara material offers a unique soft touch but requires gentle cleaning and may show wear over time more than the metal finish.", CreatedAt = DateTime.UtcNow.AddDays(-14) }
                    },
                    Visits = new List<ProductVisit>
                    {

                    }
                },


                new Product
                {
                    Name = "Acer Aspire 5 A515-57-53T2 i5-1235U 8GB 512GB 15.6\" FHD",
                    Slug = "acer-aspire-5-a515-57-53t2-i5-8gb-512gb",
                    Description = "A reliable and affordable laptop for everyday tasks, featuring a 12th Gen Intel Core processor and a Full HD display.",
                    Price = 90000,
                    DiscountPrice = null,
                    Brand = brands[2], // Acer
                    Category = categories[0],
                    SubCategory = subCategories[2], // Consumer
                    StockQuantity = 75,
                    IsFeatured = false,
                    IsDealOfTheDay = false,
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "/images/products/aspire-5-a515-57g-57le-01-500x500.png" },
                        new ProductImage { ImageUrl = "/images/products/aspire-5-a515-57g-57le-02-500x500.png" },
                        new ProductImage { ImageUrl = "/images/products/aspire-5-a515-57g-57le-04-500x500.png" }
                    },
                    AttributeValues = new List<ProductAttributeValue>
                    {
                        // Filter Attributes
                        new ProductAttributeValue { Name = "Brand", Slug="brand", Value = "Acer", SpecificationCategory = "Brand", FilterAttributeValueId = favBrandAcer.Id },
                        new ProductAttributeValue { Name = "Subcategory", Slug="subcategory", Value = "Consumer", SpecificationCategory = "subcategory", FilterAttributeValueId = favCatConsumer.Id },
                        new ProductAttributeValue { Name = "Processor Type", Slug="processor-type", Value = "Intel Core i5", SpecificationCategory = "Processor", FilterAttributeValueId = favProcI5.Id },
                        new ProductAttributeValue { Name = "Generation/Series", Slug="generation-series", Value = "Intel 12th Gen", SpecificationCategory = "Processor", FilterAttributeValueId = favGenIntel12.Id },
                        new ProductAttributeValue { Name = "Display Size", Slug="display-size", Value = "15.6\"", SpecificationCategory = "Display", FilterAttributeValueId = favDisplaySize15_6.Id },
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type", Value = "IPS", SpecificationCategory = "Display", FilterAttributeValueId = favDisplayIps.Id },
                        new ProductAttributeValue { Name = "RAM Size", Slug="ram-size", Value = "8GB", SpecificationCategory = "Memory", FilterAttributeValueId = favRam8.Id },
                        new ProductAttributeValue { Name = "RAM Type", Slug="ram-type", Value = "DDR4", SpecificationCategory = "Memory", FilterAttributeValueId = favRamDdr4.Id },
                        new ProductAttributeValue { Name = "SSD", Slug="ssd", Value = "512GB", SpecificationCategory = "Storage", FilterAttributeValueId = favSsd512.Id },
                        new ProductAttributeValue { Name = "Graphics Memory", Slug="graphics-memory", Value = "Shared/Integrated", SpecificationCategory = "Graphics", FilterAttributeValueId = favGfxShared.Id },
                        new ProductAttributeValue { Name = "Operating System", Slug="operating-system", Value = "Windows 11 Home", SpecificationCategory = "Software", FilterAttributeValueId = favOsWin11Home.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-backlit-keyboard", Value = "Backlit Keyboard", SpecificationCategory = "Keyboard", FilterAttributeValueId = favFeatBacklit.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-fingerprint", Value = "Fingerprint Reader", SpecificationCategory = "Security", FilterAttributeValueId = favFeatFingerprint.Id },

                        // Detailed Attributes (remain unchanged as they don't have filter matches)
                        new ProductAttributeValue { Name = "Processor Model", Slug="processor-model", Value = "Core i5-1235U", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Processor Cores/Threads", Slug="processor-cores-threads", Value = "10 Cores (2P+8E) / 12 Threads", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Processor Max Turbo Frequency", Slug="processor-max-turbo", Value = "Up to 4.4 GHz", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Graphics Card Model", Slug="graphics-model", Value = "Intel Iris Xe Graphics", SpecificationCategory = "Graphics" },
                        new ProductAttributeValue { Name = "Display Resolution", Slug="display-resolution", Value = "1920x1080 (Full HD)", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Technology", Slug="display-technology", Value = "Acer ComfyView (Matte)", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Memory Speed", Slug="memory-speed", Value = "3200MHz", SpecificationCategory = "Memory" },
                        new ProductAttributeValue { Name = "Memory Slots", Slug="memory-slots", Value = "2x SODIMM (Upgradeable)", SpecificationCategory = "Key Feature" },
                        new ProductAttributeValue { Name = "Storage Type", Slug="storage-type", Value = "NVMe SSD", SpecificationCategory = "Storage" },
                        new ProductAttributeValue { Name = "Storage Slots", Slug="storage-slots", Value = "1x M.2 Slot", SpecificationCategory = "Storage" },
                        new ProductAttributeValue { Name = "Wireless Connectivity", Slug="wireless-connectivity", Value = "Wi-Fi 6, Bluetooth 5.2", SpecificationCategory = "Key Feature" },
                        new ProductAttributeValue { Name = "Ports", Slug="ports", Value = "1x USB-C 3.2 Gen 2 (Thunderbolt 4 optional), 3x USB-A 3.2 Gen 1, HDMI 2.1, Ethernet (RJ-45), 3.5mm jack", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Webcam", Slug="webcam", Value = "720p HD Webcam", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Audio", Slug="audio", Value = "Acer TrueHarmony", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Battery", Slug="battery", Value = "50Wh", SpecificationCategory = "Power" },
                        new ProductAttributeValue { Name = "Battery Life", Slug="battery-life", Value = "Up to 8 hours", SpecificationCategory = "Power" },
                        new ProductAttributeValue { Name = "Weight", Slug="weight", Value = "Approx. 1.77 kg (3.9 lbs)", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "Build Material", Slug="build-material", Value = "Plastic Chassis, Aluminum Top Cover", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "Numeric Keypad", Slug="numeric-keypad", Value = "Yes", SpecificationCategory = "Key Feature" },
                        new ProductAttributeValue { Name = "Value Proposition", Slug="value-prop", Value = "Strong performance for the price", SpecificationCategory = "Key Feature" }
                    },
                    Reviews = new List<ProductReview>
                    {
                        new ProductReview { ReviewerName = "BudgetBuyer", Comment = "Great laptop for the price! Handles all my daily tasks easily.", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-40) },
                        new ProductReview { ReviewerName = "StudentSam", Comment = "Screen is decent, keyboard is comfortable. Build quality is okay.", Rating = 4, CreatedAt = DateTime.UtcNow.AddDays(-18) }
                    },
                    Questions = new List<ProductQuestion>
                    {
                        new ProductQuestion { Question = "Is the USB-C port Thunderbolt?", Answer = "On this specific model (A515-57-53T2), the USB-C port supports data transfer and DisplayPort, but Thunderbolt 4 support might vary by configuration.", CreatedAt = DateTime.UtcNow.AddDays(-9) }
                    },
                    Visits = new List<ProductVisit>
                    {

                    }
                },

                new Product
                {
                    Name = "MSI Katana 15 B13VFK i7-13620H RTX 4060 16GB 1TB 144Hz",
                    Slug = "msi-katana-15-b13vfk-i7-rtx4060",
                    Description = "Unleash your gaming potential with the MSI Katana 15, featuring a 13th Gen Intel Core processor, RTX 40 series graphics, and a high refresh rate display.",
                    Price = 170000,
                    DiscountPrice = 163500,
                    Brand = brands[6], // MSI
                    Category = categories[0],
                    SubCategory = subCategories[1], // Gaming
                    StockQuantity = 22,
                    IsFeatured = true,
                    IsDealOfTheDay = false,
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "/images/products/katana-15-b13vfk-01-500x500.png" },
                        new ProductImage { ImageUrl = "/images/products/katana-15-b13vfk-03-500x500.png" }
                    },
                    AttributeValues = new List<ProductAttributeValue>
                    {
                        // Filter Attributes
                        new ProductAttributeValue { Name = "Brand", Slug="brand", Value = "Msi", SpecificationCategory = "Brand", FilterAttributeValueId = favBrandMsi.Id },
                        new ProductAttributeValue { Name = "Subcategory", Slug="subcategory", Value = "Gamin", SpecificationCategory = "subcategory", FilterAttributeValueId = favCatGaming.Id },
                        new ProductAttributeValue { Name = "Processor Type", Slug="processor-type", Value = "Intel Core i7", SpecificationCategory = "Processor", FilterAttributeValueId = favProcI7.Id },
                        new ProductAttributeValue { Name = "Generation/Series", Slug="generation-series", Value = "Intel 13th Gen", SpecificationCategory = "Processor", FilterAttributeValueId = favGenIntel13.Id },
                        new ProductAttributeValue { Name = "Display Size", Slug="display-size", Value = "15.6\"", SpecificationCategory = "Display", FilterAttributeValueId = favDisplaySize15_6.Id },
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type", Value = "IPS", SpecificationCategory = "Display", FilterAttributeValueId = favDisplayIps.Id },
                        new ProductAttributeValue { Name = "RAM Size", Slug="ram-size", Value = "16GB", SpecificationCategory = "Memory", FilterAttributeValueId = favRam16.Id },
                        new ProductAttributeValue { Name = "RAM Type", Slug="ram-type", Value = "DDR5", SpecificationCategory = "Memory", FilterAttributeValueId = favRamDdr5.Id },
                        new ProductAttributeValue { Name = "SSD", Slug="ssd", Value = "1TB", SpecificationCategory = "Storage", FilterAttributeValueId = favSsd1tb.Id },
                        new ProductAttributeValue { Name = "Graphics Memory", Slug="graphics-memory", Value = "Dedicated 8GB", SpecificationCategory = "Graphics", FilterAttributeValueId = favGfx8gb.Id },
                        new ProductAttributeValue { Name = "Operating System", Slug="operating-system", Value = "Windows 11 Home", SpecificationCategory = "Software", FilterAttributeValueId = favOsWin11Home.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-high-refresh", Value = "High Refresh Rate Display", SpecificationCategory = "Key Feature", FilterAttributeValueId = favFeatHighRefresh.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-rgb-keyboard", Value = "RGB Keyboard", SpecificationCategory = "Keyboard", FilterAttributeValueId = favFeatRgb.Id },

                        // Detailed Attributes
                        new ProductAttributeValue { Name = "Processor Model", Slug="processor-model", Value = "Core i7-13620H", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Processor Cores/Threads", Slug="processor-cores-threads", Value = "10 Cores (6P+4E) / 16 Threads", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Processor Max Turbo Frequency", Slug="processor-max-turbo", Value = "Up to 4.9 GHz", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Graphics Card Model", Slug="graphics-model", Value = "NVIDIA GeForce RTX 4060 Laptop GPU", SpecificationCategory = "Graphics" },
                        new ProductAttributeValue { Name = "Graphics TGP", Slug="graphics-tgp", Value = "105W (with Dynamic Boost)", SpecificationCategory = "Graphics" },
                        new ProductAttributeValue { Name = "MUX Switch", Slug="mux-switch", Value = "Yes", SpecificationCategory = "Graphics" },
                        new ProductAttributeValue { Name = "Display Resolution", Slug="display-resolution", Value = "1920x1080 (Full HD)", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Refresh Rate", Slug="display-refresh-rate", Value = "144Hz", SpecificationCategory = "Display" }, // Explicit value
                        new ProductAttributeValue { Name = "Memory Speed", Slug="memory-speed", Value = "5200MHz", SpecificationCategory = "Memory" },
                        new ProductAttributeValue { Name = "Memory Slots", Slug="memory-slots", Value = "2x SODIMM, Up to 64GB", SpecificationCategory = "Memory" },
                        new ProductAttributeValue { Name = "Storage Type", Slug="storage-type", Value = "NVMe PCIe 4.0 SSD", SpecificationCategory = "Storage" },
                        new ProductAttributeValue { Name = "Storage Expansion", Slug="storage-expansion", Value = "1x M.2 SSD slot available", SpecificationCategory = "Storage" },
                        new ProductAttributeValue { Name = "Wireless Connectivity", Slug="wireless-connectivity", Value = "Intel Wi-Fi 6 AX201, Bluetooth 5.2", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Ports", Slug="ports", Value = "1x USB-C 3.2 Gen 1 (DP), 2x USB-A 3.2 Gen 1, 1x USB-A 2.0, HDMI 2.1, Ethernet (RJ-45), Audio Jack", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Webcam", Slug="webcam", Value = "720p HD Camera", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Audio", Slug="audio", Value = "Nahimic 3 Audio Enhancer", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Keyboard Type", Slug="keyboard-type", Value = "4-Zone RGB Gaming Keyboard", SpecificationCategory = "Key Feature" }, // Key Feature 2 - Gaming Keyboard
                        new ProductAttributeValue { Name = "Cooling System", Slug="cooling-system", Value = "Cooler Boost 5 Technology", SpecificationCategory = "Key Feature" }, // Key Feature 3 - MSI Cooling
                        new ProductAttributeValue { Name = "Battery", Slug="battery", Value = "53.5Wh", SpecificationCategory = "Power" },
                        new ProductAttributeValue { Name = "Weight", Slug="weight", Value = "Approx. 2.25 kg (4.96 lbs)", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "MSI Center Software", Slug="msi-center", Value = "Performance tuning and system monitoring", SpecificationCategory = "Key Feature" } // Key Feature 4 - Software Suite
                    },
                    Reviews = new List<ProductReview>
                    {
                        new ProductReview { ReviewerName = "MidRangeGamer", Comment = "Good performance for the price point. Runs modern games well at 1080p.", Rating = 4, CreatedAt = DateTime.UtcNow.AddDays(-55) },
                        new ProductReview { ReviewerName = "CasualPlayer", Comment = "Fans can get loud, but it keeps things cool. RGB is nice.", Rating = 4, CreatedAt = DateTime.UtcNow.AddDays(-30) }
                    },
                    Questions = new List<ProductQuestion>
                    {
                        new ProductQuestion { Question = "Does the USB-C port support charging?", Answer = "No, the USB-C port on this Katana model typically supports DisplayPort output but not Power Delivery (charging).", CreatedAt = DateTime.UtcNow.AddDays(-20) }
                    },
                    Visits = new List<ProductVisit>
                    {

                    }
                },


                new Product
                {
                    Name = "Huawei MateBook X Pro 2024 i7-1360P 16GB 1TB 3.1K Touch",
                    Slug = "huawei-matebook-x-pro-2024-i7-16gb-1tb",
                    Description = "Premium ultrabook with a stunning 3.1K Real Colour FullView touch display, ultra-slim design, and innovative features.",
                    Price = 230000,
                    DiscountPrice = null,
                    Brand = brands[3], // Huawei
                    Category = categories[0],
                    SubCategory = subCategories[0], // Ultrabook
                    StockQuantity = 15,
                    IsFeatured = true,
                    IsDealOfTheDay = false,
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "/images/products/61dx5YV5FOL._AC_SX425_.png" },
                        new ProductImage { ImageUrl = "/images/products/61a4zbqIq0L._AC_SX425_.png" }
                    },
                    AttributeValues = new List<ProductAttributeValue>
                    {
                        // Filter Attributes
                        new ProductAttributeValue { Name = "Brand", Slug="brand", Value = "Huawei", SpecificationCategory = "Brand", FilterAttributeValueId = favBrandHuawei.Id },
                        new ProductAttributeValue { Name = "Subcategory", Slug="subcategory", Value = "Ultrabook", SpecificationCategory = "subcategory", FilterAttributeValueId = favCatUltrabook.Id },
                         new ProductAttributeValue { Name = "Processor Type", Slug="processor-type", Value = "Intel Core i7", SpecificationCategory = "Processor", FilterAttributeValueId = favProcI7.Id },
                        new ProductAttributeValue { Name = "Generation/Series", Slug="generation-series", Value = "Intel 13th Gen", SpecificationCategory = "Processor", FilterAttributeValueId = favGenIntel13.Id },
                        new ProductAttributeValue { Name = "Display Size", Slug="display-size", Value = "14.2\"", SpecificationCategory = "Display", FilterAttributeValueId = favDisplaySize14.Id },
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type-touch", Value = "Touch Display", SpecificationCategory = "Display", FilterAttributeValueId = favDisplayTouch.Id },
                        new ProductAttributeValue { Name = "RAM Size", Slug="ram-size", Value = "16GB", SpecificationCategory = "Memory", FilterAttributeValueId = favRam16.Id },
                        new ProductAttributeValue { Name = "RAM Type", Slug="ram-type", Value = "LPDDR5 (Onboard)", SpecificationCategory = "Memory", FilterAttributeValueId = favRamDdr5.Id },
                        new ProductAttributeValue { Name = "SSD", Slug="ssd", Value = "1TB", SpecificationCategory = "Storage", FilterAttributeValueId = favSsd1tb.Id },
                        new ProductAttributeValue { Name = "Graphics Memory", Slug="graphics-memory", Value = "Shared/Integrated", SpecificationCategory = "Graphics", FilterAttributeValueId = favGfxShared.Id },
                        new ProductAttributeValue { Name = "Operating System", Slug="operating-system", Value = "Windows 11 Home", SpecificationCategory = "Software", FilterAttributeValueId = favOsWin11Home.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-fingerprint", Value = "Fingerprint Reader", SpecificationCategory = "Security", FilterAttributeValueId = favFeatFingerprint.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-thin-light", Value = "Thin and Light", SpecificationCategory = "Key Feature", FilterAttributeValueId = favFeatThinLight.Id },

                        // Detailed Attributes
                        new ProductAttributeValue { Name = "Processor Model", Slug="processor-model", Value = "Core i7-1360P", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Processor Cores/Threads", Slug="processor-cores-threads", Value = "12 Cores (4P+8E) / 16 Threads", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Graphics Card Model", Slug="graphics-model", Value = "Intel Iris Xe Graphics", SpecificationCategory = "Graphics" },
                        new ProductAttributeValue { Name = "Display Resolution", Slug="display-resolution", Value = "3120 x 2080 (3.1K)", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Name", Slug="display-name", Value = "Real Colour FullView Display", SpecificationCategory = "Key Feature" }, // Key Feature 2 - High-Res Display
                        new ProductAttributeValue { Name = "Display Refresh Rate", Slug="display-refresh-rate", Value = "90Hz", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Aspect Ratio", Slug="display-aspect-ratio", Value = "3:2", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Color Accuracy", Slug="display-color-accuracy", Value = "ΔE < 1, P3 & sRGB dual color gamut", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Storage Type", Slug="storage-type", Value = "NVMe PCIe SSD", SpecificationCategory = "Storage" },
                        new ProductAttributeValue { Name = "Wireless Connectivity", Slug="wireless-connectivity", Value = "Wi-Fi 6E, Bluetooth 5.2", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Ports", Slug="ports", Value = "2x Thunderbolt 4 (USB-C), 2x USB-C 3.2 Gen 1, 3.5mm jack", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Webcam", Slug="webcam", Value = "720p HD (Pop-up camera in keyboard - older models, verify for 2024)", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Audio", Slug="audio", Value = "Huawei Sound® 6 Speakers", SpecificationCategory = "Key Feature" }, // Key Feature 3 - Enhanced Audio
                        new ProductAttributeValue { Name = "Keyboard", Slug="keyboard", Value = "Full-size Backlit Keyboard", SpecificationCategory = "Keyboard" },
                        new ProductAttributeValue { Name = "Touchpad", Slug="touchpad", Value = "Huawei Free Touch (Pressure-sensitive)", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Battery", Slug="battery", Value = "60Wh", SpecificationCategory = "Power" },
                        new ProductAttributeValue { Name = "Weight", Slug="weight", Value = "Approx. 1.26 kg (2.78 lbs)", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "Build Material", Slug="build-material", Value = "Magnesium Alloy Body (Skin-soothing Metallic)", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "Huawei Share", Slug="huawei-share", Value = "Multi-screen collaboration with Huawei devices", SpecificationCategory = "Key Feature" } // Key Feature 4 - Ecosystem Feature
                    },
                    Reviews = new List<ProductReview>
                    {
                        new ProductReview { ReviewerName = "TechEnthusiast", Comment = "Absolutely stunning screen and build quality. Very light.", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-33) },
                        new ProductReview { ReviewerName = "BusinessUser", Comment = "Excellent for productivity, great keyboard. Webcam placement (if pop-up) is awkward.", Rating = 4, CreatedAt = DateTime.UtcNow.AddDays(-19) }
                    },
                    Questions = new List<ProductQuestion>
                    {
                        new ProductQuestion { Question = "Can I install Google services easily?", Answer = "Huawei laptops run standard Windows, so Google services (Chrome, Drive, etc.) can be installed and used normally like on any other Windows PC.", CreatedAt = DateTime.UtcNow.AddDays(-11) }
                    },
                    Visits = new List<ProductVisit>
                    {

                    }
                },


                new Product
                {
                    Name = "Xiaomi Mi Notebook Pro 14 Ryzen 7 7840HS 16GB 512GB 2.5K 120Hz",
                    Slug = "xiaomi-mi-notebook-pro-14-r7-16gb-512gb",
                    Description = "A sleek and powerful notebook offering a high-resolution 120Hz display, AMD Ryzen 7000 series processor, and premium build quality at a competitive price.",
                    Price = 80000,
                    DiscountPrice =null ,
                    Brand = brands[5], // Xiaomi
                    Category = categories[0],
                    SubCategory = subCategories[2], // Consumer (or Ultrabook)
                    StockQuantity = 35,
                    IsFeatured = false,
                    IsDealOfTheDay = false,
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "/images/products/notebook-pro-01-500x500.png" }

                    },
                    AttributeValues = new List<ProductAttributeValue>
                    {
                        // Filter Attributes
                        new ProductAttributeValue { Name = "Brand", Slug="brand", Value = "Xiaomi", SpecificationCategory = "Brand", FilterAttributeValueId = favBrandXiaomi.Id },
                        new ProductAttributeValue { Name = "Subcategory", Slug="subcategory", Value = "Consumer", SpecificationCategory = "subcategory", FilterAttributeValueId = favCatConsumer.Id },
                        new ProductAttributeValue { Name = "Processor Type", Slug="processor-type", Value = "AMD Ryzen 7", SpecificationCategory = "Processor", FilterAttributeValueId = favProcRyzen7.Id },
                        new ProductAttributeValue { Name = "Generation/Series", Slug="generation-series", Value = "Ryzen 7000 Series", SpecificationCategory = "Processor", FilterAttributeValueId = favGenRyzen7000.Id },
                        new ProductAttributeValue { Name = "Display Size", Slug="display-size", Value = "14\"", SpecificationCategory = "Display", FilterAttributeValueId = favDisplaySize14.Id },
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type", Value = "IPS", SpecificationCategory = "Display", FilterAttributeValueId = favDisplayIps.Id },
                        new ProductAttributeValue { Name = "RAM Size", Slug="ram-size", Value = "16GB", SpecificationCategory = "Memory", FilterAttributeValueId = favRam16.Id },
                        new ProductAttributeValue { Name = "RAM Type", Slug="ram-type", Value = "LPDDR5 (Onboard)", SpecificationCategory = "Memory", FilterAttributeValueId = favRamDdr5.Id },
                        new ProductAttributeValue { Name = "SSD", Slug="ssd", Value = "512GB", SpecificationCategory = "Storage", FilterAttributeValueId = favSsd512.Id },
                        new ProductAttributeValue { Name = "Graphics Memory", Slug="graphics-memory", Value = "Shared/Integrated", SpecificationCategory = "Graphics", FilterAttributeValueId = favGfxShared.Id },
                        new ProductAttributeValue { Name = "Operating System", Slug="operating-system", Value = "Windows 11 Home", SpecificationCategory = "Software", FilterAttributeValueId = favOsWin11Home.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-high-refresh", Value = "High Refresh Rate Display", SpecificationCategory = "Key Feature", FilterAttributeValueId = favFeatHighRefresh.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-backlit-keyboard", Value = "Backlit Keyboard", SpecificationCategory = "Keyboard", FilterAttributeValueId = favFeatBacklit.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-fingerprint", Value = "Fingerprint Reader", SpecificationCategory = "Security", FilterAttributeValueId = favFeatFingerprint.Id },

                        // Detailed Attributes
                        new ProductAttributeValue { Name = "Processor Model", Slug="processor-model", Value = "AMD Ryzen 7 7840HS", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Processor Cores/Threads", Slug="processor-cores-threads", Value = "8 Cores / 16 Threads", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Graphics Card Model", Slug="graphics-model", Value = "AMD Radeon 780M Graphics", SpecificationCategory = "Graphics" }, // Powerful iGPU
                        new ProductAttributeValue { Name = "Display Resolution", Slug="display-resolution", Value = "2560x1600 (2.5K)", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Refresh Rate", Slug="display-refresh-rate", Value = "120Hz", SpecificationCategory = "Display" }, // Explicit value
                        new ProductAttributeValue { Name = "Display Aspect Ratio", Slug="display-aspect-ratio", Value = "16:10", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Color Gamut", Slug="display-color-gamut", Value = "100% sRGB", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Memory Speed", Slug="memory-speed", Value = "6400MHz", SpecificationCategory = "Memory" },
                        new ProductAttributeValue { Name = "Storage Type", Slug="storage-type", Value = "NVMe PCIe 4.0 SSD", SpecificationCategory = "Storage" },
                        new ProductAttributeValue { Name = "Wireless Connectivity", Slug="wireless-connectivity", Value = "Wi-Fi 6E, Bluetooth 5.3", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Ports", Slug="ports", Value = "1x USB-C (Full function, likely Thunderbolt/USB4), 1x USB-C (Charging/Data), 1x USB-A 3.2 Gen 1, HDMI 2.1, 3.5mm jack", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Webcam", Slug="webcam", Value = "1080p FHD Webcam", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Audio", Slug="audio", Value = "Stereo Speakers with DTS Audio Processing", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Battery", Slug="battery", Value = "56Wh", SpecificationCategory = "Power" },
                        new ProductAttributeValue { Name = "Charging", Slug="charging", Value = "100W GaN USB-C Charger included", SpecificationCategory = "Key Feature" }, // Key Feature 2 - Fast Charging/GaN
                        new ProductAttributeValue { Name = "Weight", Slug="weight", Value = "Approx. 1.5 kg (3.3 lbs)", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "Build Material", Slug="build-material", Value = "CNC Aluminum Alloy Body", SpecificationCategory = "Key Feature" }, // Key Feature 3 - Build Quality for Price
                        new ProductAttributeValue { Name = "MIUI+ Integration", Slug="miui-plus", Value = "Cross-device collaboration with Xiaomi phones", SpecificationCategory = "Key Feature" } // Key Feature 4 - Ecosystem Link
                    },
                    Reviews = new List<ProductReview>
                    {
                        new ProductReview { ReviewerName = "ValueSeeker", Comment = "Amazing specs for the price! The screen is fantastic.", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-48) },
                        new ProductReview { ReviewerName = "PowerUser", Comment = "Handles multitasking and light gaming very well. Good build quality.", Rating = 4, CreatedAt = DateTime.UtcNow.AddDays(-21) }
                    },
                    Questions = new List<ProductQuestion>
                    {
                        new ProductQuestion { Question = "Is the warranty international?", Answer = "Xiaomi laptop warranty is typically regional and may not be valid outside the country/region of purchase. Check specific seller/regional terms.", CreatedAt = DateTime.UtcNow.AddDays(-13) }
                    },
                    Visits = new List<ProductVisit>
                    {

                    }
                },

                new Product
                {
                    Name = "Lenovo IdeaPad Slim 3 15IRU8 i3-1315U 8GB 256GB 15.6\" FHD Touch",
                    Slug = "lenovo-ideapad-slim-3-15iru8-i3-8gb-256gb-touch",
                    Description = "An affordable and slim laptop for everyday productivity and entertainment, featuring a 13th Gen Intel Core i3 processor and a touchscreen display.",
                    Price = 47500,
                    DiscountPrice = null,
                    Brand = brands[1], // Lenovo
                    Category = categories[0],
                    SubCategory = subCategories[3], // Office (or Consumer)
                    StockQuantity = 60,
                    IsFeatured = false,
                    IsDealOfTheDay = false,
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "/images/products/Lenovo-IdeaPad-Slim-3-4797.png" },
                        new ProductImage { ImageUrl = "/images/products/Lenovo-IdeaPad-Slim-3-a-7560.png"},
                        new ProductImage { ImageUrl = "/images/products/Lenovo-IdeaPad-Slim-3-b-7535.png"}
                    },
                    AttributeValues = new List<ProductAttributeValue>
                    {
                        // Filter Attributes
                        new ProductAttributeValue { Name = "Brand", Slug="brand", Value = "Lenovo", SpecificationCategory = "Brand", FilterAttributeValueId = favBrandLenovo.Id },
                        new ProductAttributeValue { Name = "Subcategory", Slug="subcategory", Value = "Office", SpecificationCategory = "subcategory", FilterAttributeValueId = favCatOffice.Id  },
                        new ProductAttributeValue { Name = "Processor Type", Slug="processor-type", Value = "Intel Core i3", SpecificationCategory = "Processor", FilterAttributeValueId = favProcI3.Id },
                        new ProductAttributeValue { Name = "Generation/Series", Slug="generation-series", Value = "Intel 13th Gen", SpecificationCategory = "Processor", FilterAttributeValueId = favGenIntel13.Id },
                        new ProductAttributeValue { Name = "Display Size", Slug="display-size", Value = "15.6\"", SpecificationCategory = "Display", FilterAttributeValueId = favDisplaySize15_6.Id },
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type-touch", Value = "Touch Display", SpecificationCategory = "Key Feature", FilterAttributeValueId = favDisplayTouch.Id },
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type-tn", Value = "TN", SpecificationCategory = "Display", FilterAttributeValueId = favDisplayTn.Id },
                        new ProductAttributeValue { Name = "RAM Size", Slug="ram-size", Value = "8GB", SpecificationCategory = "Memory", FilterAttributeValueId = favRam8.Id },
                        new ProductAttributeValue { Name = "RAM Type", Slug="ram-type", Value = "LPDDR5 (Onboard)", SpecificationCategory = "Memory", FilterAttributeValueId = favRamDdr5.Id },
                        new ProductAttributeValue { Name = "SSD", Slug="ssd", Value = "256GB", SpecificationCategory = "Storage", FilterAttributeValueId = favSsd256.Id },
                        new ProductAttributeValue { Name = "Graphics Memory", Slug="graphics-memory", Value = "Shared/Integrated", SpecificationCategory = "Graphics", FilterAttributeValueId = favGfxShared.Id },
                        new ProductAttributeValue { Name = "Operating System", Slug="operating-system", Value = "Windows 11 Home", SpecificationCategory = "Software", FilterAttributeValueId = favOsWin11Home.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-fingerprint", Value = "Fingerprint Reader", SpecificationCategory = "Security", FilterAttributeValueId = favFeatFingerprint.Id },

                        // Detailed Attributes
                        new ProductAttributeValue { Name = "Processor Model", Slug="processor-model", Value = "Core i3-1315U", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Processor Cores/Threads", Slug="processor-cores-threads", Value = "6 Cores (2P+4E) / 8 Threads", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Graphics Card Model", Slug="graphics-model", Value = "Intel UHD Graphics", SpecificationCategory = "Graphics" },
                        new ProductAttributeValue { Name = "Display Resolution", Slug="display-resolution", Value = "1920x1080 (Full HD)", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Brightness", Slug="display-brightness", Value = "250 nits", SpecificationCategory = "Display" }, // Typical for budget models
                        new ProductAttributeValue { Name = "Storage Type", Slug="storage-type", Value = "NVMe SSD", SpecificationCategory = "Storage" },
                        new ProductAttributeValue { Name = "Wireless Connectivity", Slug="wireless-connectivity", Value = "Wi-Fi 6, Bluetooth 5.1", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Ports", Slug="ports", Value = "1x USB-C 3.2 Gen 1 (PD/DP), 2x USB-A 3.2 Gen 1, HDMI 1.4b, SD card reader, 3.5mm jack", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Webcam", Slug="webcam", Value = "720p HD with Privacy Shutter", SpecificationCategory = "Key Feature" }, // Key Feature 2 - Privacy Shutter
                        new ProductAttributeValue { Name = "Audio", Slug="audio", Value = "Stereo speakers, Dolby Audio", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Keyboard", Slug="keyboard", Value = "Full-size Keyboard with Numeric Keypad", SpecificationCategory = "Keyboard" },
                        new ProductAttributeValue { Name = "Battery", Slug="battery", Value = "47Wh", SpecificationCategory = "Power" },
                        new ProductAttributeValue { Name = "Battery Life", Slug="battery-life", Value = "Up to 9 hours (MobileMark 2018)", SpecificationCategory = "Power" },
                        new ProductAttributeValue { Name = "Weight", Slug="weight", Value = "Approx. 1.62 kg (3.57 lbs)", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "Build Material", Slug="build-material", Value = "PC + ABS Plastic", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "Color", Slug="color", Value = "Arctic Grey", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "Slim Design", Slug="slim-design", Value = "Under 18mm thick", SpecificationCategory = "Key Feature" }, // Key Feature 3 - Slim Profile
                        new ProductAttributeValue { Name = "Rapid Charge", Slug="rapid-charge", Value = "Fast charging support", SpecificationCategory = "Key Feature" }, // Key Feature 4 - Charging Tech
                        new ProductAttributeValue { Name = "Operating Mode", Slug="operating-mode", Value = "Windows 11 in S Mode (switchable)", SpecificationCategory = "Software" }, // Often ships in S Mode
                        new ProductAttributeValue { Name = "Security Chip", Slug="security-chip", Value = "Firmware TPM 2.0", SpecificationCategory = "Security" }, // Added to reach 30
                    },
                    Reviews = new List<ProductReview>
                    {
                        new ProductReview { ReviewerName = "EverydayUser", Comment = "Good for basic tasks like Browse and email. Touchscreen is a nice bonus.", Rating = 4, CreatedAt = DateTime.UtcNow.AddDays(-38) },
                        new ProductReview { ReviewerName = "ParentPurchase", Comment = "Bought for my kid for school. Seems sturdy enough and works well.", Rating = 4, CreatedAt = DateTime.UtcNow.AddDays(-16) }
                    },
                    Questions = new List<ProductQuestion>
                    {
                        new ProductQuestion { Question = "Can I upgrade the RAM?", Answer = "RAM is typically soldered on the IdeaPad Slim 3 series and not user-upgradeable. Check the specific model's specs to confirm.", CreatedAt = DateTime.UtcNow.AddDays(-10) }
                    },
                    Visits = new List<ProductVisit>
                    {

                    }
                },

                new Product
                {
                    Name = "HP Envy x360 15-fe0053dx Ryzen 7 7730U 16GB 512GB 15.6\" FHD Touch",
                    Slug = "hp-envy-x360-15-fe0053dx-r7-16gb-512gb",
                    Description = "A versatile and stylish 2-in-1 laptop with a large touchscreen, powerful AMD Ryzen processor, and premium features for creativity and entertainment.",
                    Price = 148000,
                    DiscountPrice = 120000,
                    Brand = brands[8], // HP
                    Category = categories[0],
                    SubCategory = subCategories[0], // Could be Ultrabook or Consumer due to convertible nature
                    StockQuantity = 33,
                    IsFeatured = false,
                    IsDealOfTheDay = false,
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "/images/products/HP-ENVY-x360-15-fh0023dx-a-8887.png" },
                        new ProductImage { ImageUrl = "/images/products/HP-ENVY-x360-15-fh0023dx-c-8141.png" },
                        new ProductImage { ImageUrl = "/images/products/HP-ENVY-x360-15-fh0023dx-d-7650.png" },
                    },
                    AttributeValues = new List<ProductAttributeValue>
                    {
                        // Filter Attributes
                        new ProductAttributeValue { Name = "Brand", Slug="brand", Value = "HP", SpecificationCategory = "Brand", FilterAttributeValueId = favBrandHp.Id },
                        new ProductAttributeValue { Name = "Subcategory", Slug="subcategory", Value = "Consumer", SpecificationCategory = "subcategory", FilterAttributeValueId = favCatConsumer.Id },
                        new ProductAttributeValue { Name = "Processor Type", Slug="processor-type", Value = "AMD Ryzen 7", SpecificationCategory = "Processor", FilterAttributeValueId = favProcRyzen7.Id },
                        new ProductAttributeValue { Name = "Generation/Series", Slug="generation-series", Value = "Ryzen 7000 Series", SpecificationCategory = "Processor", FilterAttributeValueId = favGenRyzen7000.Id },
                        new ProductAttributeValue { Name = "Display Size", Slug="display-size", Value = "15.6\"", SpecificationCategory = "Display", FilterAttributeValueId = favDisplaySize15_6.Id },
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type-touch", Value = "Touch Display", SpecificationCategory = "Key Feature", FilterAttributeValueId = favDisplayTouch.Id },
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type-ips", Value = "IPS", SpecificationCategory = "Display", FilterAttributeValueId = favDisplayIps.Id },
                        new ProductAttributeValue { Name = "RAM Size", Slug="ram-size", Value = "16GB", SpecificationCategory = "Memory", FilterAttributeValueId = favRam16.Id },
                        new ProductAttributeValue { Name = "RAM Type", Slug="ram-type", Value = "DDR4", SpecificationCategory = "Memory", FilterAttributeValueId = favRamDdr4.Id },
                        new ProductAttributeValue { Name = "SSD", Slug="ssd", Value = "512GB", SpecificationCategory = "Storage", FilterAttributeValueId = favSsd512.Id },
                        new ProductAttributeValue { Name = "Graphics Memory", Slug="graphics-memory", Value = "Shared/Integrated", SpecificationCategory = "Graphics", FilterAttributeValueId = favGfxShared.Id },
                        new ProductAttributeValue { Name = "Operating System", Slug="operating-system", Value = "Windows 11 Home", SpecificationCategory = "Software", FilterAttributeValueId = favOsWin11Home.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-backlit-keyboard", Value = "Backlit Keyboard", SpecificationCategory = "Keyboard", FilterAttributeValueId = favFeatBacklit.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-convertible", Value = "Convertible", SpecificationCategory = "Physical", FilterAttributeValueId = favFeatConvertible.Id },

                        // Detailed Attributes
                        new ProductAttributeValue { Name = "Processor Model", Slug="processor-model", Value = "AMD Ryzen 7 7730U", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Processor Cores/Threads", Slug="processor-cores-threads", Value = "8 Cores / 16 Threads", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Processor Max Boost Clock", Slug="processor-max-boost", Value = "Up to 4.5 GHz", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Graphics Card Model", Slug="graphics-model", Value = "AMD Radeon Graphics", SpecificationCategory = "Graphics" },
                        new ProductAttributeValue { Name = "Display Resolution", Slug="display-resolution", Value = "1920x1080 (Full HD)", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Features", Slug="display-features", Value = "Edge-to-edge glass, micro-edge", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Memory Speed", Slug="memory-speed", Value = "3200MHz", SpecificationCategory = "Memory" }, // If DDR4
                        new ProductAttributeValue { Name = "Storage Type", Slug="storage-type", Value = "NVMe PCIe SSD", SpecificationCategory = "Storage" },
                        new ProductAttributeValue { Name = "Wireless Connectivity", Slug="wireless-connectivity", Value = "Wi-Fi 6E, Bluetooth 5.3", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Ports", Slug="ports", Value = "2x USB-C 3.2 Gen 2 (DP/PD), 2x USB-A 3.2 Gen 1, HDMI 2.1, SD card reader, 3.5mm jack", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Webcam", Slug="webcam", Value = "HP Wide Vision 5MP IR camera with privacy shutter", SpecificationCategory = "Key Feature" }, // Key Feature 2 - 5MP Webcam
                        new ProductAttributeValue { Name = "Audio", Slug="audio", Value = "Audio by Bang & Olufsen; Dual speakers", SpecificationCategory = "Key Feature" }, // Key Feature 3 - B&O Audio
                        new ProductAttributeValue { Name = "Keyboard", Slug="keyboard", Value = "Full-size backlit keyboard", SpecificationCategory = "Keyboard" },
                        new ProductAttributeValue { Name = "Security", Slug="security", Value = "Fingerprint reader, Camera privacy shutter", SpecificationCategory = "Security" },
                        new ProductAttributeValue { Name = "Battery", Slug="battery", Value = "51Wh", SpecificationCategory = "Power" },
                        new ProductAttributeValue { Name = "Weight", Slug="weight", Value = "Approx. 1.8 kg (3.97 lbs)", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "Build Material", Slug="build-material", Value = "Aluminum chassis", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "Stylus Support", Slug="stylus-support", Value = "Yes, MPP2.0 (pen often sold separately)", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Sustainable Materials", Slug="sustainable-materials", Value = "Contains ocean-bound plastic and recycled aluminum", SpecificationCategory = "Key Feature" } // Key Feature 4 - Eco Focus
                    },
                    Reviews = new List<ProductReview>
                    {
                        new ProductReview { ReviewerName = "CreativeCloudUser", Comment = "Great for photo editing on the go. The screen is nice and bright.", Rating = 4, CreatedAt = DateTime.UtcNow.AddDays(-65) },
                        new ProductReview { ReviewerName = "MultiTasker", Comment = "Handles multiple apps well. Love the flexibility of the 2-in-1 design.", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-32) }
                    },
                    Questions = new List<ProductQuestion>
                    {
                        new ProductQuestion { Question = "Does it come with the HP Pen?", Answer = "Typically, the HP Pen is sold separately for the Envy x360 series, but check the specific bundle or retailer offer.", CreatedAt = DateTime.UtcNow.AddDays(-21) }
                    },
                    Visits = new List<ProductVisit>
                    {

                    }
                },

                new Product
                {
                    Name = "Dell G15 5530 Gaming Laptop i5-13450HX RTX 3050 8GB 512GB 120Hz",
                    Slug = "dell-g15-5530-gaming-i5-rtx3050-8gb-512gb",
                    Description = "Affordable gaming laptop with a 13th Gen Intel Core processor, NVIDIA RTX graphics, and a fast refresh rate display for smooth gameplay.",
                    Price = 120000,
                    DiscountPrice = 112000,
                    Brand = brands[7], // DELL
                    Category = categories[0],
                    SubCategory = subCategories[1], // Gaming
                    StockQuantity = 45,
                    IsFeatured = false,
                    IsDealOfTheDay = false,
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "/images/products/g15-5530-01-500x500.png" },
                        new ProductImage { ImageUrl = "/images/products/g15-5530-02-500x500.png" },
                        new ProductImage { ImageUrl = "/images/products/g15-5530-04-500x500.png" }
                    },
                    AttributeValues = new List<ProductAttributeValue>
                    {
                        // Filter Attributes
                        new ProductAttributeValue { Name = "Brand", Slug="brand", Value = "Dell", SpecificationCategory = "Brand", FilterAttributeValueId = favBrandDell.Id },
                        new ProductAttributeValue { Name = "Subcategory", Slug="subcategory", Value = "Gaming", SpecificationCategory = "subcategory", FilterAttributeValueId = favCatGaming.Id  },
                        new ProductAttributeValue { Name = "Processor Type", Slug="processor-type", Value = "Intel Core i5", SpecificationCategory = "Processor", FilterAttributeValueId = favProcI5.Id },
                        new ProductAttributeValue { Name = "Generation/Series", Slug="generation-series", Value = "Intel 13th Gen", SpecificationCategory = "Processor", FilterAttributeValueId = favGenIntel13.Id },
                        new ProductAttributeValue { Name = "Display Size", Slug="display-size", Value = "15.6\"", SpecificationCategory = "Display", FilterAttributeValueId = favDisplaySize15_6.Id },
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type", Value = "WVA (Wide Viewing Angle)", SpecificationCategory = "Display", FilterAttributeValueId = favDisplayIps.Id }, // Assuming WVA is similar to IPS
                        new ProductAttributeValue { Name = "RAM Size", Slug="ram-size", Value = "8GB", SpecificationCategory = "Memory", FilterAttributeValueId = favRam8.Id },
                        new ProductAttributeValue { Name = "RAM Type", Slug="ram-type", Value = "DDR5", SpecificationCategory = "Memory", FilterAttributeValueId = favRamDdr5.Id },
                        new ProductAttributeValue { Name = "SSD", Slug="ssd", Value = "512GB", SpecificationCategory = "Storage", FilterAttributeValueId = favSsd512.Id },
                        new ProductAttributeValue { Name = "Graphics Memory", Slug="graphics-memory", Value = "Dedicated 6GB", SpecificationCategory = "Graphics", FilterAttributeValueId = favGfx6gb.Id },
                        new ProductAttributeValue { Name = "Operating System", Slug="operating-system", Value = "Windows 11 Home", SpecificationCategory = "Software", FilterAttributeValueId = favOsWin11Home.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-high-refresh", Value = "High Refresh Rate Display", SpecificationCategory = "Key Feature", FilterAttributeValueId = favFeatHighRefresh.Id },

                        // Detailed Attributes
                        new ProductAttributeValue { Name = "Processor Model", Slug="processor-model", Value = "Core i5-13450HX", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Processor Cores/Threads", Slug="processor-cores-threads", Value = "10 Cores (6P+4E) / 16 Threads", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Graphics Card Model", Slug="graphics-model", Value = "NVIDIA GeForce RTX 3050 Laptop GPU", SpecificationCategory = "Graphics" },
                        new ProductAttributeValue { Name = "Graphics VRAM", Slug="graphics-vram", Value = "6GB GDDR6", SpecificationCategory = "Graphics" },
                        new ProductAttributeValue { Name = "Display Resolution", Slug="display-resolution", Value = "1920x1080 (Full HD)", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Refresh Rate", Slug="display-refresh-rate", Value = "120Hz", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Brightness", Slug="display-brightness", Value = "250 nits", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Memory Speed", Slug="memory-speed", Value = "4800MHz", SpecificationCategory = "Memory" },
                        new ProductAttributeValue { Name = "Memory Slots", Slug="memory-slots", Value = "2x SODIMM (Upgradeable)", SpecificationCategory = "Memory" }, // Upgradeable RAM
                        new ProductAttributeValue { Name = "Storage Type", Slug="storage-type", Value = "NVMe PCIe SSD", SpecificationCategory = "Storage" },
                        new ProductAttributeValue { Name = "Wireless Connectivity", Slug="wireless-connectivity", Value = "Intel Wi-Fi 6 AX201, Bluetooth 5.2", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Ports", Slug="ports", Value = "1x USB-C 3.2 Gen 2 (DP), 3x USB-A 3.2 Gen 1, HDMI 2.1, Ethernet (RJ-45), 3.5mm jack", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Webcam", Slug="webcam", Value = "720p HD RGB camera", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Audio", Slug="audio", Value = "Stereo speakers with Dolby Audio", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Keyboard", Slug="keyboard", Value = "Backlit Keyboard (Orange or RGB option)", SpecificationCategory = "Keyboard" },
                        new ProductAttributeValue { Name = "Cooling System", Slug="cooling-system", Value = "Alienware-inspired thermal design", SpecificationCategory = "Key Feature" }, // Key Feature 2 - Cooling Design
                        new ProductAttributeValue { Name = "Game Shift Technology", Slug="game-shift", Value = "Boosts fan speed for intense gaming", SpecificationCategory = "Key Feature" }, // Key Feature 3 - Performance Boost
                        new ProductAttributeValue { Name = "Battery", Slug="battery", Value = "56Wh (Optional 86Wh)", SpecificationCategory = "Power" },
                        new ProductAttributeValue { Name = "Weight", Slug="weight", Value = "Approx. 2.65 kg (5.84 lbs)", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "Build Material", Slug="build-material", Value = "Plastic chassis", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "Alienware Command Center", Slug="awcc", Value = "System tuning software", SpecificationCategory = "Key Feature" } // Key Feature 4 - Software Suite
                    },
                    Reviews = new List<ProductReview>
                    {
                        new ProductReview { ReviewerName = "EntryGamer", Comment = "Good entry-level gaming laptop. Runs older titles smoothly.", Rating = 4, CreatedAt = DateTime.UtcNow.AddDays(-70) },
                        new ProductReview { ReviewerName = "CollegeStudent", Comment = "A bit heavy, but powerful enough for schoolwork and some gaming.", Rating = 4, CreatedAt = DateTime.UtcNow.AddDays(-40) }
                    },
                    Questions = new List<ProductQuestion>
                    {
                        new ProductQuestion { Question = "Is the 8GB RAM enough for modern games?", Answer = "8GB is the minimum for many modern games. Upgrading to 16GB (using the available SODIMM slots) is highly recommended for a better gaming experience.", CreatedAt = DateTime.UtcNow.AddDays(-25) }
                    },
                    Visits = new List<ProductVisit>
                    {

                    }
                },

                new Product
                {
                    Name = "Asus Zenbook 14 OLED UX3402VA i5-1340P 16GB 512GB 2.8K 90Hz",
                    Slug = "asus-zenbook-14-oled-ux3402va-i5-16gb-512gb",
                    Description = "An ultra-portable and stylish laptop featuring a stunning 2.8K OLED display, 13th Gen Intel Core power, and a lightweight design.",
                    Price = 140000,
                    DiscountPrice = null,
                    Brand = brands[0], // Asus
                    Category = categories[0],
                    SubCategory = subCategories[0], // Ultrabook
                    StockQuantity = 28,
                    IsFeatured = true,
                    IsDealOfTheDay = false,
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "/images/products/Asus-ZenBook-14X-OLED-UX3402Z.png" },
                        new ProductImage { ImageUrl = "/images/products/Asus-ZenBook-14X-OLED-UX3402Z-BD.png" }

                    },
                    AttributeValues = new List<ProductAttributeValue>
                    {
                        // Filter Attributes
                        new ProductAttributeValue { Name = "Brand", Slug="brand", Value = "Asus", SpecificationCategory = "Brand", FilterAttributeValueId = favBrandAsus.Id },
                        new ProductAttributeValue { Name = "Subcategory", Slug="subcategory", Value = "Ultrabook", SpecificationCategory = "subcategory", FilterAttributeValueId =favCatUltrabook.Id  },
                        new ProductAttributeValue { Name = "Processor Type", Slug="processor-type", Value = "Intel Core i5", SpecificationCategory = "Processor", FilterAttributeValueId = favProcI5.Id },
                        new ProductAttributeValue { Name = "Generation/Series", Slug="generation-series", Value = "Intel 13th Gen", SpecificationCategory = "Processor", FilterAttributeValueId = favGenIntel13.Id },
                        new ProductAttributeValue { Name = "Display Size", Slug="display-size", Value = "14\"", SpecificationCategory = "Display", FilterAttributeValueId = favDisplaySize14.Id },
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type-oled", Value = "OLED", SpecificationCategory = "Key Feature", FilterAttributeValueId = favDisplayOled.Id },
                        new ProductAttributeValue { Name = "RAM Size", Slug="ram-size", Value = "16GB", SpecificationCategory = "Memory", FilterAttributeValueId = favRam16.Id },
                        new ProductAttributeValue { Name = "RAM Type", Slug="ram-type", Value = "LPDDR5 (Onboard)", SpecificationCategory = "Memory", FilterAttributeValueId = favRamDdr5.Id },
                        new ProductAttributeValue { Name = "SSD", Slug="ssd", Value = "512GB", SpecificationCategory = "Storage", FilterAttributeValueId = favSsd512.Id },
                        new ProductAttributeValue { Name = "Graphics Memory", Slug="graphics-memory", Value = "Shared/Integrated", SpecificationCategory = "Graphics", FilterAttributeValueId = favGfxShared.Id },
                        new ProductAttributeValue { Name = "Operating System", Slug="operating-system", Value = "Windows 11 Home", SpecificationCategory = "Software", FilterAttributeValueId = favOsWin11Home.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-thin-light", Value = "Thin and Light", SpecificationCategory = "Key Feature", FilterAttributeValueId = favFeatThinLight.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-backlit-keyboard", Value = "Backlit Keyboard", SpecificationCategory = "Keyboard", FilterAttributeValueId = favFeatBacklit.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-fingerprint", Value = "Fingerprint Reader", SpecificationCategory = "Security", FilterAttributeValueId = favFeatFingerprint.Id },

                        // Detailed Attributes
                        new ProductAttributeValue { Name = "Processor Model", Slug="processor-model", Value = "Core i5-1340P", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Processor Cores/Threads", Slug="processor-cores-threads", Value = "12 Cores (4P+8E) / 16 Threads", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Graphics Card Model", Slug="graphics-model", Value = "Intel Iris Xe Graphics", SpecificationCategory = "Graphics" },
                        new ProductAttributeValue { Name = "Display Resolution", Slug="display-resolution", Value = "2880 x 1800 (2.8K)", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Refresh Rate", Slug="display-refresh-rate", Value = "90Hz", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Aspect Ratio", Slug="display-aspect-ratio", Value = "16:10", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Color Gamut", Slug="display-color-gamut", Value = "100% DCI-P3, Pantone Validated", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Storage Type", Slug="storage-type", Value = "NVMe PCIe 4.0 SSD", SpecificationCategory = "Storage" },
                        new ProductAttributeValue { Name = "Wireless Connectivity", Slug="wireless-connectivity", Value = "Wi-Fi 6E, Bluetooth 5.3", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Ports", Slug="ports", Value = "2x Thunderbolt 4 (DP/PD), 1x USB-A 3.2 Gen 2, HDMI 2.1, MicroSD reader, 3.5mm jack", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Webcam", Slug="webcam", Value = "1080p FHD Camera with IR for Windows Hello", SpecificationCategory = "Security" },
                        new ProductAttributeValue { Name = "Audio", Slug="audio", Value = "Harman Kardon certified speakers with Dolby Atmos", SpecificationCategory = "Key Feature" }, // Key Feature 3 - Premium Audio
                        new ProductAttributeValue { Name = "Keyboard", Slug="keyboard", Value = "ErgoSense keyboard", SpecificationCategory = "Keyboard" },
                        new ProductAttributeValue { Name = "NumberPad", Slug="numberpad", Value = "Integrated NumberPad 2.0 in touchpad", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Battery", Slug="battery", Value = "75Wh", SpecificationCategory = "Power" },
                        new ProductAttributeValue { Name = "Weight", Slug="weight", Value = "Approx. 1.39 kg (3.06 lbs)", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "Build Material", Slug="build-material", Value = "Aluminum Alloy chassis", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "Military Grade Durability", Slug="mil-std-810h", Value = "MIL-STD-810H certified", SpecificationCategory = "Key Feature" }, // Key Feature 4 - Durability
                        new ProductAttributeValue { Name = "MyASUS Software", Slug="myasus", Value = "System optimization and support tools", SpecificationCategory = "Software" }
                    },
                    Reviews = new List<ProductReview>
                    {
                        new ProductReview { ReviewerName = "VisualArtist", Comment = "The OLED display is just stunning for creative work! Super light too.", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-80) },
                        new ProductReview { ReviewerName = "OnTheGoPro", Comment = "Great battery life and performance for its size. Highly recommend.", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-45) }
                    },
                    Questions = new List<ProductQuestion>
                    {
                        new ProductQuestion { Question = "Is the RAM upgradeable?", Answer = "No, the LPDDR5 RAM in the Zenbook 14 OLED series is typically soldered to the motherboard and cannot be upgraded.", CreatedAt = DateTime.UtcNow.AddDays(-30) }
                    },
                    Visits = new List<ProductVisit>
                    {

                    }
                },
 
                new Product
                {
                    Name = "Acer Chromebook Spin 714 CP714-2WN i5-1335U 8GB 256GB 14\" WUXGA Touch",
                    Slug = "acer-chromebook-spin-714-cp714-2wn-i5-8gb-256gb",
                    Description = "A premium convertible Chromebook with a powerful Intel Core i5 processor, vibrant touchscreen, USI stylus support, and the speed and security of ChromeOS.",
                    Price = 200000,
                    DiscountPrice = null,
                    Brand = brands[2], // Acer
                    Category = categories[0],
                    SubCategory = subCategories[0], // Technically Ultrabook/Convertible, but OS differs
                    StockQuantity = 40,
                    IsFeatured = false,
                    IsDealOfTheDay = false,
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "/images/products/51XaD+FtODL._AC_SX355_.png" },
                    },
                    AttributeValues = new List<ProductAttributeValue>
                    {
                        // Filter Attributes (Adapting where needed for ChromeOS)
                        new ProductAttributeValue { Name = "Brand", Slug="brand", Value = "Acer", SpecificationCategory = "Brand", FilterAttributeValueId = favBrandAcer.Id },
                        new ProductAttributeValue { Name = "Subcategory", Slug="subcategory", Value = "Ultrabook", SpecificationCategory = "subcategory", FilterAttributeValueId = favCatUltrabook.Id },
                        new ProductAttributeValue { Name = "Processor Type", Slug="processor-type", Value = "Intel Core i5", SpecificationCategory = "Processor", FilterAttributeValueId = favProcI5.Id },
                        new ProductAttributeValue { Name = "Generation/Series", Slug="generation-series", Value = "Intel 13th Gen", SpecificationCategory = "Processor", FilterAttributeValueId = favGenIntel13.Id },
                        new ProductAttributeValue { Name = "Display Size", Slug="display-size", Value = "14\"", SpecificationCategory = "Display", FilterAttributeValueId = favDisplaySize14.Id },
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type-touch", Value = "Touch Display", SpecificationCategory = "Key Feature", FilterAttributeValueId = favDisplayTouch.Id },
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type-ips", Value = "IPS", SpecificationCategory = "Display", FilterAttributeValueId = favDisplayIps.Id },
                        new ProductAttributeValue { Name = "RAM Size", Slug="ram-size", Value = "8GB", SpecificationCategory = "Memory", FilterAttributeValueId = favRam8.Id },
                        new ProductAttributeValue { Name = "RAM Type", Slug="ram-type", Value = "LPDDR4x (Onboard)", SpecificationCategory = "Memory", FilterAttributeValueId = favRamDdr4.Id },
                        new ProductAttributeValue { Name = "SSD", Slug="ssd", Value = "256GB", SpecificationCategory = "Storage", FilterAttributeValueId = favSsd256.Id },
                        new ProductAttributeValue { Name = "Graphics Memory", Slug="graphics-memory", Value = "Shared/Integrated", SpecificationCategory = "Graphics", FilterAttributeValueId = favGfxShared.Id },
                        new ProductAttributeValue { Name = "Operating System", Slug="operating-system", Value = "ChromeOS", SpecificationCategory = "Key Feature", FilterAttributeValueId = favOsChrome.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-convertible", Value = "Convertible", SpecificationCategory = "Physical", FilterAttributeValueId = favFeatConvertible.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-stylus", Value = "Stylus Support", SpecificationCategory = "Features", FilterAttributeValueId = favFeatStylus.Id },

                        // Detailed Attributes
                        new ProductAttributeValue { Name = "Processor Model", Slug="processor-model", Value = "Core i5-1335U", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Processor Cores/Threads", Slug="processor-cores-threads", Value = "10 Cores (2P+8E) / 12 Threads", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Graphics Card Model", Slug="graphics-model", Value = "Intel Iris Xe Graphics", SpecificationCategory = "Graphics" },
                        new ProductAttributeValue { Name = "Display Resolution", Slug="display-resolution", Value = "1920 x 1200 (WUXGA)", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Aspect Ratio", Slug="display-aspect-ratio", Value = "16:10", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Protection", Slug="display-protection", Value = "Corning Gorilla Glass", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Storage Type", Slug="storage-type", Value = "NVMe SSD", SpecificationCategory = "Storage" },
                        new ProductAttributeValue { Name = "Wireless Connectivity", Slug="wireless-connectivity", Value = "Wi-Fi 6E, Bluetooth 5.2", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Ports", Slug="ports", Value = "2x USB-C (Thunderbolt 4), 1x USB-A 3.2 Gen 1, HDMI 2.1, 3.5mm jack", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Webcam", Slug="webcam", Value = "1080p FHD Webcam with Privacy Shade", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Audio", Slug="audio", Value = "DTS Audio; Upward-facing speakers", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Keyboard", Slug="keyboard", Value = "Backlit Keyboard", SpecificationCategory = "Keyboard" },
                        new ProductAttributeValue { Name = "Stylus", Slug="stylus", Value = "Garaged USI Stylus included (Rechargeable)", SpecificationCategory = "Key Feature" }, // Key Feature 3 - Included Stylus
                        new ProductAttributeValue { Name = "Battery Life", Slug="battery-life", Value = "Up to 10 hours", SpecificationCategory = "Power" },
                        new ProductAttributeValue { Name = "Weight", Slug="weight", Value = "Approx. 1.37 kg (3.02 lbs)", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "Build Material", Slug="build-material", Value = "Aluminum Chassis", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "Military Grade Durability", Slug="mil-std-810h", Value = "MIL-STD-810H certified", SpecificationCategory = "Key Feature" }, // Key Feature 4 - Durability
                        new ProductAttributeValue { Name = "Android App Support", Slug="android-apps", Value = "Access to Google Play Store", SpecificationCategory = "Software" } // Added to reach 30
                    },
                    Reviews = new List<ProductReview>
                    {
                        new ProductReview { ReviewerName = "ChromeOSFan", Comment = "Fast, secure, and the screen is great. Love the convertible form factor.", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-58) },
                        new ProductReview { ReviewerName = "Educator", Comment = "Excellent for classroom use and online learning. Stylus is very responsive.", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-33) }
                    },
                    Questions = new List<ProductQuestion>
                    {
                        new ProductQuestion { Question = "Can it run Windows software?", Answer = "Natively, no. ChromeOS runs web apps, Android apps, and Linux apps (in a container). Some Windows apps might have web or Android versions, or you could explore options like remote desktop or virtualization (CrossOver) for specific needs, but it's not the primary use case.", CreatedAt = DateTime.UtcNow.AddDays(-22) }
                    },
                    Visits = new List<ProductVisit>
                    {

                    }
                },

                new Product
                {
                    Name = "Lenovo Yoga 7i 16IRL8 i7-1355U 16GB 1TB 16\" 2.5K Touch",
                    Slug = "lenovo-yoga-7i-16irl8-i7-16gb-1tb",
                    Description = "A large-screen 2-in-1 laptop offering versatility, a high-resolution touch display, ample storage, and comfortable user experience.",
                    Price = 170000,
                    DiscountPrice = 165000,
                    Brand = brands[1], // Lenovo
                    Category = categories[0],
                    SubCategory = subCategories[0], // Convertible/Ultrabook leaning Consumer
                    StockQuantity = 26,
                    IsFeatured = false,
                    IsDealOfTheDay = false,
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "/images/products/lenovo-yoga-7i-gen7-16inch-01.png" },
                        new ProductImage { ImageUrl = "/images/products/cefav116qxqdhpokwl9wy9cmxeq0lv804754.png" },
                        new ProductImage { ImageUrl = "/images/products/lenovo-yoga-7i-gen7-16inch-02.png" }

                    },
                    AttributeValues = new List<ProductAttributeValue>
                    {
                        // Filter Attributes
                        new ProductAttributeValue { Name = "Brand", Slug="brand", Value = "Lenovo", SpecificationCategory = "Brand", FilterAttributeValueId = favBrandLenovo.Id },
                        new ProductAttributeValue { Name = "Subcategory", Slug="subcategory", Value = "Consumer", SpecificationCategory = "subcategory", FilterAttributeValueId = favCatConsumer.Id },
                        new ProductAttributeValue { Name = "Processor Type", Slug="processor-type", Value = "Intel Core i7", SpecificationCategory = "Processor", FilterAttributeValueId = favProcI7.Id },
                        new ProductAttributeValue { Name = "Generation/Series", Slug="generation-series", Value = "Intel 13th Gen", SpecificationCategory = "Processor", FilterAttributeValueId = favGenIntel13.Id },
                        new ProductAttributeValue { Name = "Display Size", Slug="display-size", Value = "16\"", SpecificationCategory = "Display", FilterAttributeValueId = favDisplaySize16.Id },
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type-touch", Value = "Touch Display", SpecificationCategory = "Key Feature", FilterAttributeValueId = favDisplayTouch.Id },
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type-ips", Value = "IPS", SpecificationCategory = "Display", FilterAttributeValueId = favDisplayIps.Id },
                        new ProductAttributeValue { Name = "RAM Size", Slug="ram-size", Value = "16GB", SpecificationCategory = "Memory", FilterAttributeValueId = favRam16.Id },
                        new ProductAttributeValue { Name = "RAM Type", Slug="ram-type", Value = "LPDDR5 (Onboard)", SpecificationCategory = "Memory", FilterAttributeValueId = favRamDdr5.Id },
                        new ProductAttributeValue { Name = "SSD", Slug="ssd", Value = "1TB", SpecificationCategory = "Storage", FilterAttributeValueId = favSsd1tb.Id },
                        new ProductAttributeValue { Name = "Graphics Memory", Slug="graphics-memory", Value = "Shared/Integrated", SpecificationCategory = "Graphics", FilterAttributeValueId = favGfxShared.Id },
                        new ProductAttributeValue { Name = "Operating System", Slug="operating-system", Value = "Windows 11 Home", SpecificationCategory = "Software", FilterAttributeValueId = favOsWin11Home.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-backlit-keyboard", Value = "Backlit Keyboard", SpecificationCategory = "Keyboard", FilterAttributeValueId = favFeatBacklit.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-fingerprint", Value = "Fingerprint Reader", SpecificationCategory = "Security", FilterAttributeValueId = favFeatFingerprint.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-convertible", Value = "Convertible", SpecificationCategory = "Physical", FilterAttributeValueId = favFeatConvertible.Id },

                        // Detailed Attributes
                        new ProductAttributeValue { Name = "Processor Model", Slug="processor-model", Value = "Core i7-1355U", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Processor Cores/Threads", Slug="processor-cores-threads", Value = "10 Cores (2P+8E) / 12 Threads", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Graphics Card Model", Slug="graphics-model", Value = "Intel Iris Xe Graphics", SpecificationCategory = "Graphics" },
                        new ProductAttributeValue { Name = "Display Resolution", Slug="display-resolution", Value = "2560 x 1600 (2.5K)", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Brightness", Slug="display-brightness", Value = "400 nits", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Color Gamut", Slug="display-color-gamut", Value = "100% sRGB", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Storage Type", Slug="storage-type", Value = "NVMe PCIe 4.0 SSD", SpecificationCategory = "Storage" },
                        new ProductAttributeValue { Name = "Wireless Connectivity", Slug="wireless-connectivity", Value = "Wi-Fi 6E, Bluetooth 5.1", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Ports", Slug="ports", Value = "2x Thunderbolt 4 (DP/PD), 1x USB-A 3.2 Gen 1 (Always On), 1x USB-A 3.2 Gen 1, HDMI 1.4b, MicroSD reader, 3.5mm jack", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Webcam", Slug="webcam", Value = "1080p FHD + IR with Privacy Shutter", SpecificationCategory = "Key Feature" }, // Key Feature 2 - FHD IR Cam w/ Shutter
                        new ProductAttributeValue { Name = "Audio", Slug="audio", Value = "User-facing stereo speakers, Dolby Atmos", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Keyboard", Slug="keyboard", Value = "Full-size backlit keyboard with numeric keypad", SpecificationCategory = "Key Feature" }, // Key Feature 3 - Numpad on Convertible
                        new ProductAttributeValue { Name = "Stylus Support", Slug="stylus-support", Value = "Yes, Lenovo Digital Pen support (often sold separately)", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Battery", Slug="battery", Value = "71Wh", SpecificationCategory = "Power" },
                        new ProductAttributeValue { Name = "Weight", Slug="weight", Value = "Approx. 2.04 kg (4.49 lbs)", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "Build Material", Slug="build-material", Value = "Aluminum chassis", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "Comfort Edge Design", Slug="comfort-edge", Value = "Rounded edges for comfortable handling", SpecificationCategory = "Key Feature" }, // Key Feature 4 - Ergonomic Design
                        new ProductAttributeValue { Name = "Rapid Charge Boost", Slug="rapid-charge-boost", Value = "Fast charging technology", SpecificationCategory = "Power" } // Added to reach 30
                    },
                    Reviews = new List<ProductReview>
                    {
                        new ProductReview { ReviewerName = "HomeOfficeHero", Comment = "Great large screen for multitasking. Comfortable keyboard.", Rating = 4, CreatedAt = DateTime.UtcNow.AddDays(-75) },
                        new ProductReview { ReviewerName = "MediaWatcher", Comment = "Love using it in tent mode for movies. Screen is sharp and bright.", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-50) }
                    },
                    Questions = new List<ProductQuestion>
                    {
                        new ProductQuestion { Question = "Is it too heavy to use as a tablet?", Answer = "At over 2kg (4.4 lbs), it's quite heavy for extended use purely as a handheld tablet, but works well in tent or stand mode or for occasional tablet use.", CreatedAt = DateTime.UtcNow.AddDays(-35) }
                    },
                    Visits = new List<ProductVisit>
                    {
                    }
                },

                new Product
                {
                    Name = "MSI Prestige 14 Evo A13M i7-13700H 16GB 1TB 14\" FHD+",
                    Slug = "msi-prestige-14-evo-a13m-i7-16gb-1tb",
                    Description = "A sleek and powerful business ultrabook meeting Intel Evo standards, offering high performance, portability, and enhanced security features.",
                    Price = 100000,
                    DiscountPrice = null,
                    Brand = brands[6], // MSI
                    Category = categories[0],
                    SubCategory = subCategories[3], // Office (or Ultrabook)
                    StockQuantity = 20,
                    IsFeatured = true,
                    IsDealOfTheDay = false,
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "/images/products/81tFo4uCawL._AC_SY355_.png" },
                        new ProductImage { ImageUrl = "/images/products/81yL4BushZL._AC_SY355_.png" },
                        new ProductImage { ImageUrl = "/images/products/71AK+TtV+ML._AC_SY355_.png" },
                        new ProductImage { ImageUrl = "/images/products/71fjpvLW7mL._AC_SY355_.png" }
                    },
                    AttributeValues = new List<ProductAttributeValue>
                    {
                        // Filter Attributes
                        new ProductAttributeValue { Name = "Brand", Slug="brand", Value = "Msi", SpecificationCategory = "Brand", FilterAttributeValueId = favBrandMsi.Id },
                        new ProductAttributeValue { Name = "Subcategory", Slug="subcategory", Value = "Office", SpecificationCategory = "subcategory", FilterAttributeValueId = favCatOffice.Id },
                         new ProductAttributeValue { Name = "Processor Type", Slug="processor-type", Value = "Intel Core i7", SpecificationCategory = "Processor", FilterAttributeValueId = favProcI7.Id },
                        new ProductAttributeValue { Name = "Generation/Series", Slug="generation-series", Value = "Intel 13th Gen", SpecificationCategory = "Processor", FilterAttributeValueId = favGenIntel13.Id },
                        new ProductAttributeValue { Name = "Display Size", Slug="display-size", Value = "14\"", SpecificationCategory = "Display", FilterAttributeValueId = favDisplaySize14.Id },
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type", Value = "IPS", SpecificationCategory = "Display", FilterAttributeValueId = favDisplayIps.Id },
                        new ProductAttributeValue { Name = "RAM Size", Slug="ram-size", Value = "16GB", SpecificationCategory = "Memory", FilterAttributeValueId = favRam16.Id },
                        new ProductAttributeValue { Name = "RAM Type", Slug="ram-type", Value = "LPDDR5 (Onboard)", SpecificationCategory = "Memory", FilterAttributeValueId = favRamDdr5.Id },
                        new ProductAttributeValue { Name = "SSD", Slug="ssd", Value = "1TB", SpecificationCategory = "Storage", FilterAttributeValueId = favSsd1tb.Id },
                        new ProductAttributeValue { Name = "Graphics Memory", Slug="graphics-memory", Value = "Shared/Integrated", SpecificationCategory = "Graphics", FilterAttributeValueId = favGfxShared.Id },
                        new ProductAttributeValue { Name = "Operating System", Slug="operating-system", Value = "Windows 11 Pro", SpecificationCategory = "Key Feature", FilterAttributeValueId = favOsWin11Pro.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-thin-light", Value = "Thin and Light", SpecificationCategory = "Physical", FilterAttributeValueId = favFeatThinLight.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-backlit-keyboard", Value = "Backlit Keyboard", SpecificationCategory = "Keyboard", FilterAttributeValueId = favFeatBacklit.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-fingerprint", Value = "Fingerprint Reader", SpecificationCategory = "Security", FilterAttributeValueId = favFeatFingerprint.Id },


                        // Detailed Attributes
                        new ProductAttributeValue { Name = "Processor Model", Slug="processor-model", Value = "Core i7-13700H", SpecificationCategory = "Processor" }, // H-series in an Evo is potent
                        new ProductAttributeValue { Name = "Processor Cores/Threads", Slug="processor-cores-threads", Value = "14 Cores (6P+8E) / 20 Threads", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Graphics Card Model", Slug="graphics-model", Value = "Intel Iris Xe Graphics", SpecificationCategory = "Graphics" },
                        new ProductAttributeValue { Name = "Display Resolution", Slug="display-resolution", Value = "1920 x 1200 (FHD+)", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Aspect Ratio", Slug="display-aspect-ratio", Value = "16:10", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Color Gamut", Slug="display-color-gamut", Value = "100% sRGB (approx.)", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Storage Type", Slug="storage-type", Value = "NVMe PCIe 4.0 SSD", SpecificationCategory = "Storage" },
                        new ProductAttributeValue { Name = "Wireless Connectivity", Slug="wireless-connectivity", Value = "Intel Killer Wi-Fi 6E AX1675, Bluetooth 5.3", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Ports", Slug="ports", Value = "2x Thunderbolt 4 (DP/PD), 1x USB-A 3.2 Gen 2, HDMI 2.1, MicroSD reader, 3.5mm jack", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Webcam", Slug="webcam", Value = "FHD IR Webcam with Noise Reduction", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Security", Slug="security", Value = "Fingerprint Reader, IR Camera (Windows Hello), TPM 2.0", SpecificationCategory = "Security" },
                        new ProductAttributeValue { Name = "Audio", Slug="audio", Value = "DTS Audio Processing", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Keyboard Travel", Slug="keyboard-travel", Value = "1.5mm key travel", SpecificationCategory = "Keyboard" },
                        new ProductAttributeValue { Name = "Intel Evo Certified", Slug="intel-evo", Value = "Meets Evo platform standards", SpecificationCategory = "Key Feature" }, // Key Feature 2 - Evo Certification
                        new ProductAttributeValue { Name = "Battery", Slug="battery", Value = "72Wh", SpecificationCategory = "Power" },
                        new ProductAttributeValue { Name = "Weight", Slug="weight", Value = "Approx. 1.49 kg (3.28 lbs)", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "Build Material", Slug="build-material", Value = "Aluminum Chassis", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "MSI Center Pro", Slug="msi-center-pro", Value = "Optimization and system management software", SpecificationCategory = "Key Feature" }, // Key Feature 3 - Business Software
                        new ProductAttributeValue { Name = "Hi-Res Audio", Slug="hi-res-audio", Value = "Certified for high-resolution audio playback", SpecificationCategory = "Key Feature" } // Key Feature 4 - Audio Quality
                    },
                    Reviews = new List<ProductReview>
                    {
                        new ProductReview { ReviewerName = "BusinessTraveler", Comment = "Very fast and responsive. Love the professional look and lightweight design.", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-68) },
                        new ProductReview { ReviewerName = "PowerUser", Comment = "The H-series processor provides excellent performance for demanding tasks.", Rating = 4, CreatedAt = DateTime.UtcNow.AddDays(-42) }
                    },
                    Questions = new List<ProductQuestion>
                    {
                        new ProductQuestion { Question = "Does it have a dedicated GPU option?", Answer = "While some MSI Prestige models might offer discrete GPU options, the Evo certified versions typically rely on the integrated Intel Iris Xe graphics to meet Evo's battery life and responsiveness targets.", CreatedAt = DateTime.UtcNow.AddDays(-28) }
                    },
                    Visits = new List<ProductVisit>
                    {

                    }
                },

                new Product
                {
                    Name = "Apple MacBook Pro 14-inch M3 Pro Chip (11-core CPU, 14-core GPU) 18GB RAM 512GB SSD",
                    Slug = "apple-macbook-pro-14-m3-pro-11c-14g-18gb-512gb",
                    Description = "The ultimate pro laptop with the groundbreaking M3 Pro chip, delivering phenomenal performance for demanding workflows, plus a stunning Liquid Retina XDR display.",
                    Price = 210000,
                    DiscountPrice = null,
                    Brand = brands[4], // Apple
                    Category = categories[0],
                    SubCategory = subCategories[0], // Could be Ultrabook or specialized Pro category
                    StockQuantity = 30,
                    IsFeatured = true,
                    IsDealOfTheDay = false,
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "/images/products/MacBook-Pro-M3-14inch-Silver-1368.png" },
                        new ProductImage { ImageUrl = "/images/products/MacBook-Pro-M3-14inch-Space-Black-3747.png" }
                    },
                    AttributeValues = new List<ProductAttributeValue>
                    {
                        // Filter Attributes (Adapting)
                        new ProductAttributeValue { Name = "Brand", Slug="brand", Value = "Apple", SpecificationCategory = "Brand", FilterAttributeValueId = favBrandApple.Id },
                        new ProductAttributeValue { Name = "Subcategory", Slug="subcategory", Value = "Ultrabook", SpecificationCategory = "subcategory", FilterAttributeValueId = favCatUltrabook.Id },
                        new ProductAttributeValue { Name = "Processor Type", Slug="processor-type", Value = "Apple M3 Pro", SpecificationCategory = "Processor", FilterAttributeValueId = favProcAppleM1Pro.Id }, // Using M1 Pro as closest match
                        new ProductAttributeValue { Name = "Generation/Series", Slug="generation-series", Value = "Apple Silicon M3", SpecificationCategory = "Processor" }, // No direct filter match
                        new ProductAttributeValue { Name = "Display Size", Slug="display-size", Value = "14.2\"", SpecificationCategory = "Display", FilterAttributeValueId = favDisplaySize14.Id },
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type", Value = "Liquid Retina XDR", SpecificationCategory = "Key Feature" }, // No direct filter match
                        new ProductAttributeValue { Name = "RAM Size", Slug="ram-size", Value = "18GB", SpecificationCategory = "Memory" }, // No direct filter match
                        new ProductAttributeValue { Name = "RAM Type", Slug="ram-type", Value = "Unified Memory", SpecificationCategory = "Memory" }, // No direct filter match
                        new ProductAttributeValue { Name = "SSD", Slug="ssd", Value = "512GB", SpecificationCategory = "Storage", FilterAttributeValueId = favSsd512.Id },
                        new ProductAttributeValue { Name = "Graphics Memory", Slug="graphics-memory", Value = "Shared/Integrated", SpecificationCategory = "Graphics", FilterAttributeValueId = favGfxShared.Id },
                        new ProductAttributeValue { Name = "Operating System", Slug="operating-system", Value = "macOS", SpecificationCategory = "Software", FilterAttributeValueId = favOsMac.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-fingerprint", Value = "Fingerprint Reader", SpecificationCategory = "Security", FilterAttributeValueId = favFeatFingerprint.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-backlit-keyboard", Value = "Backlit Keyboard", SpecificationCategory = "Keyboard", FilterAttributeValueId = favFeatBacklit.Id },

                        // Detailed Attributes
                        new ProductAttributeValue { Name = "Processor Model", Slug="processor-model", Value = "Apple M3 Pro chip", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Processor Cores", Slug="processor-cores", Value = "11-core CPU (5 performance, 6 efficiency)", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "GPU Cores", Slug="gpu-cores", Value = "14-core GPU", SpecificationCategory = "Graphics" },
                        new ProductAttributeValue { Name = "Neural Engine", Slug="neural-engine", Value = "16-core", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Memory Bandwidth", Slug="memory-bandwidth", Value = "150GB/s", SpecificationCategory = "Memory" },
                        new ProductAttributeValue { Name = "Display Resolution", Slug="display-resolution", Value = "3024x1964", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Brightness", Slug="display-brightness", Value = "1000 nits sustained (XDR), 1600 nits peak", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Technology", Slug="display-technology", Value = "ProMotion technology (up to 120Hz)", SpecificationCategory = "Key Feature" }, // Key Feature 2 - ProMotion
                        new ProductAttributeValue { Name = "Storage Type", Slug="storage-type", Value = "Superfast SSD storage", SpecificationCategory = "Storage" },
                        new ProductAttributeValue { Name = "Wireless Connectivity", Slug="wireless-connectivity", Value = "Wi-Fi 6E (802.11ax), Bluetooth 5.3", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Ports", Slug="ports", Value = "3x Thunderbolt 4 (USB-C), HDMI port, SDXC card slot, MagSafe 3 port, 3.5mm headphone jack", SpecificationCategory = "Key Feature" }, // Key Feature 3 - Port Selection
                        new ProductAttributeValue { Name = "Webcam", Slug="webcam", Value = "1080p FaceTime HD camera", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Audio", Slug="audio", Value = "High-fidelity six-speaker sound system with force-cancelling woofers", SpecificationCategory = "Key Feature" }, // Key Feature 4 - Pro Audio System
                        new ProductAttributeValue { Name = "Keyboard", Slug="keyboard", Value = "Magic Keyboard with Touch ID", SpecificationCategory = "Keyboard" },
                        new ProductAttributeValue { Name = "Trackpad", Slug="trackpad", Value = "Force Touch trackpad", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Battery Life", Slug="battery-life", Value = "Up to 18 hours Apple TV app movie playback", SpecificationCategory = "Power" },
                        new ProductAttributeValue { Name = "Weight", Slug="weight", Value = "1.61 kg (3.5 pounds)", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "Build Material", Slug="build-material", Value = "Recycled Aluminum Enclosure", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "Color", Slug="color", Value = "Space Black / Silver", SpecificationCategory = "Physical" } // Added to reach 30
                    },
                    Reviews = new List<ProductReview>
                    {
                        new ProductReview { ReviewerName = "VideoEditorPro", Comment = "Incredible speed for rendering and editing. The display is top-notch.", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-44) },
                        new ProductReview { ReviewerName = "DeveloperDan", Comment = "Compiles code much faster than my old laptop. Battery life is amazing.", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-29) }
                    },
                    Questions = new List<ProductQuestion>
                    {
                        new ProductQuestion { Question = "Can I upgrade the RAM or SSD later?", Answer = "No, the RAM (Unified Memory) and SSD storage on Apple Silicon MacBooks are integrated components and cannot be upgraded after purchase. Choose your configuration carefully.", CreatedAt = DateTime.UtcNow.AddDays(-19) }
                    },
                    Visits = new List<ProductVisit>
                    {

                    }
                },

                new Product
                {
                    Name = "Dell Latitude 5440 i5-1335U 16GB 512GB 14\" FHD",
                    Slug = "dell-latitude-5440-i5-16gb-512gb",
                    Description = "A reliable and secure business laptop designed for productivity, featuring 13th Gen Intel Core power, extensive connectivity, and enterprise-level manageability.",
                    Price = 127000, // Business laptops often have higher list prices
                    DiscountPrice = 120000,
                    Brand = brands[7], // DELL
                    Category = categories[0],
                    SubCategory = subCategories[3], // Office
                    StockQuantity = 50,
                    IsFeatured = false,
                    IsDealOfTheDay = false,
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "/images/products/latitude-5440-001-500x500.png" },
                        new ProductImage { ImageUrl = "/images/products/latitude-5440-006-500x500.png" },
                        new ProductImage { ImageUrl = "/images/products/latitude-5440-005-500x500.png" },
                    },
                    AttributeValues = new List<ProductAttributeValue>
                    {
                        // Filter Attributes
                        new ProductAttributeValue { Name = "Brand", Slug="brand", Value = "Dell", SpecificationCategory = "Brand", FilterAttributeValueId = favBrandDell.Id },
                        new ProductAttributeValue { Name = "Subcategory", Slug="subcategory", Value = "office", SpecificationCategory = "subcategory", FilterAttributeValueId = favCatOffice.Id },
                        new ProductAttributeValue { Name = "Processor Type", Slug="processor-type", Value = "Intel Core i5", SpecificationCategory = "Processor", FilterAttributeValueId = favProcI5.Id },
                        new ProductAttributeValue { Name = "Generation/Series", Slug="generation-series", Value = "Intel 13th Gen", SpecificationCategory = "Processor", FilterAttributeValueId = favGenIntel13.Id },
                        new ProductAttributeValue { Name = "Display Size", Slug="display-size", Value = "14\"", SpecificationCategory = "Display", FilterAttributeValueId = favDisplaySize14.Id },
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type", Value = "IPS", SpecificationCategory = "Display", FilterAttributeValueId = favDisplayIps.Id },
                        new ProductAttributeValue { Name = "RAM Size", Slug="ram-size", Value = "16GB", SpecificationCategory = "Memory", FilterAttributeValueId = favRam16.Id },
                        new ProductAttributeValue { Name = "RAM Type", Slug="ram-type", Value = "DDR4", SpecificationCategory = "Memory", FilterAttributeValueId = favRamDdr4.Id },
                        new ProductAttributeValue { Name = "SSD", Slug="ssd", Value = "512GB", SpecificationCategory = "Storage", FilterAttributeValueId = favSsd512.Id },
                        new ProductAttributeValue { Name = "Graphics Memory", Slug="graphics-memory", Value = "Shared/Integrated", SpecificationCategory = "Graphics", FilterAttributeValueId = favGfxShared.Id },
                        new ProductAttributeValue { Name = "Operating System", Slug="operating-system", Value = "Windows 11 Pro", SpecificationCategory = "Key Feature", FilterAttributeValueId = favOsWin11Pro.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-backlit-keyboard", Value = "Backlit Keyboard", SpecificationCategory = "Keyboard", FilterAttributeValueId = favFeatBacklit.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-fingerprint", Value = "Fingerprint Reader", SpecificationCategory = "Security", FilterAttributeValueId = favFeatFingerprint.Id },

                        // Detailed Attributes
                        new ProductAttributeValue { Name = "Processor Model", Slug="processor-model", Value = "Core i5-1335U", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Processor Cores/Threads", Slug="processor-cores-threads", Value = "10 Cores (2P+8E) / 12 Threads", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Graphics Card Model", Slug="graphics-model", Value = "Intel Iris Xe Graphics", SpecificationCategory = "Graphics" },
                        new ProductAttributeValue { Name = "Display Resolution", Slug="display-resolution", Value = "1920x1080 (Full HD)", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Features", Slug="display-features", Value = "Anti-Glare, 250 nits", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Memory Speed", Slug="memory-speed", Value = "3200MHz", SpecificationCategory = "Memory" },
                        new ProductAttributeValue { Name = "Memory Slots", Slug="memory-slots", Value = "2x SODIMM (Upgradeable)", SpecificationCategory = "Key Feature" }, // Key Feature 2 - RAM Upgradeability
                        new ProductAttributeValue { Name = "Storage Type", Slug="storage-type", Value = "NVMe SSD", SpecificationCategory = "Storage" },
                        new ProductAttributeValue { Name = "Wireless Connectivity", Slug="wireless-connectivity", Value = "Intel Wi-Fi 6E AX211, Bluetooth 5.3", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Ports", Slug="ports", Value = "2x Thunderbolt 4 (DP/PD), 2x USB-A 3.2 Gen 1, HDMI 2.0, Ethernet (RJ-45), MicroSD reader, Optional SmartCard Reader, 3.5mm jack", SpecificationCategory = "Key Feature" }, // Key Feature 3 - Port Selection & Ethernet
                        new ProductAttributeValue { Name = "Webcam", Slug="webcam", Value = "FHD IR Camera with Privacy Shutter", SpecificationCategory = "Security" },
                        new ProductAttributeValue { Name = "Audio", Slug="audio", Value = "Stereo speakers with MaxxAudio Pro", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Security Features", Slug="security-features", Value = "TPM 2.0, Fingerprint Reader, IR Camera, Wedge Lock Slot", SpecificationCategory = "Security" },
                        new ProductAttributeValue { Name = "Management Features", Slug="management-features", Value = "Intel vPro Essentials (on specific CPU SKUs)", SpecificationCategory = "Key Feature" }, // Key Feature 4 - Manageability
                        new ProductAttributeValue { Name = "Battery", Slug="battery", Value = "54Wh", SpecificationCategory = "Power" },
                        new ProductAttributeValue { Name = "Weight", Slug="weight", Value = "Approx. 1.36 kg (3.0 lbs)", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "Build Material", Slug="build-material", Value = "Durable chassis, often with recycled materials", SpecificationCategory = "Physical" },
                        new ProductAttributeValue { Name = "Dell Optimizer", Slug="dell-optimizer", Value = "AI-based optimization software", SpecificationCategory = "Software" } // Added to reach 30
                    },
                    Reviews = new List<ProductReview>
                    {
                        new ProductReview { ReviewerName = "CorpIT", Comment = "Solid, reliable machine for deployment. Good security features.", Rating = 4, CreatedAt = DateTime.UtcNow.AddDays(-85) },
                        new ProductReview { ReviewerName = "RemoteWorker", Comment = "Comfortable keyboard, good port selection for docking.", Rating = 4, CreatedAt = DateTime.UtcNow.AddDays(-55) }
                    },
                    Questions = new List<ProductQuestion>
                    {
                        new ProductQuestion { Question = "Does this model have vPro?", Answer = "Intel vPro support depends on the specific CPU configuration (e.g., i5-1345U or i7-1365U typically offer it). The i5-1335U usually has vPro Essentials or may not have full vPro capabilities. Check the exact SKU.", CreatedAt = DateTime.UtcNow.AddDays(-40) }
                    },
                    Visits = new List<ProductVisit>
                    {

                    }
                },

                new Product
                {
                    Name = "Asus TUF Gaming A15 FA507NV Ryzen 7 7735HS RTX 4060 16GB 512GB 144Hz",
                    Slug = "asus-tuf-gaming-a15-fa507nv-r7-rtx4060",
                    Description = "Durable and powerful gaming laptop built for action, featuring an AMD Ryzen 7000 series CPU, NVIDIA RTX 4060 graphics, and military-grade toughness.",
                    Price = 165000,
                    DiscountPrice = null,
                    Brand = brands[0], // Asus
                    Category = categories[0],
                    SubCategory = subCategories[1], // Gaming
                    StockQuantity = 38,
                    IsFeatured = true,
                    IsDealOfTheDay = false,
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage { ImageUrl = "/images/products/a15-fa507nur-00001-500x500.png" },
                        new ProductImage { ImageUrl = "/images/products/fa507nur-02-500x500.png" },
                        new ProductImage { ImageUrl = "/images/products/fa507nur-03-500x500.png" },
                        new ProductImage { ImageUrl = "/images/products/fa507nur-04-500x500.png" }
                    },
                    AttributeValues = new List<ProductAttributeValue>
                    {
                        // Filter Attributes
                        new ProductAttributeValue { Name = "Brand", Slug="brand", Value = "Asus", SpecificationCategory = "Brand", FilterAttributeValueId = favBrandAsus.Id },
                        new ProductAttributeValue { Name = "Subcategory", Slug="subcategory", Value = "gaming", SpecificationCategory = "subcategory", FilterAttributeValueId = favCatGaming.Id },
                        new ProductAttributeValue { Name = "Processor Type", Slug="processor-type", Value = "AMD Ryzen 7", SpecificationCategory = "Processor", FilterAttributeValueId = favProcRyzen7.Id },
                        new ProductAttributeValue { Name = "Generation/Series", Slug="generation-series", Value = "Ryzen 7000 Series", SpecificationCategory = "Processor", FilterAttributeValueId = favGenRyzen7000.Id },
                        new ProductAttributeValue { Name = "Display Size", Slug="display-size", Value = "15.6\"", SpecificationCategory = "Display", FilterAttributeValueId = favDisplaySize15_6.Id },
                        new ProductAttributeValue { Name = "Display Type", Slug="display-type", Value = "IPS", SpecificationCategory = "Display", FilterAttributeValueId = favDisplayIps.Id },
                        new ProductAttributeValue { Name = "RAM Size", Slug="ram-size", Value = "16GB", SpecificationCategory = "Memory", FilterAttributeValueId = favRam16.Id },
                        new ProductAttributeValue { Name = "RAM Type", Slug="ram-type", Value = "DDR5", SpecificationCategory = "Memory", FilterAttributeValueId = favRamDdr5.Id },
                        new ProductAttributeValue { Name = "SSD", Slug="ssd", Value = "512GB", SpecificationCategory = "Storage", FilterAttributeValueId = favSsd512.Id },
                        new ProductAttributeValue { Name = "Graphics Memory", Slug="graphics-memory", Value = "Dedicated 8GB", SpecificationCategory = "Graphics", FilterAttributeValueId = favGfx8gb.Id },
                        new ProductAttributeValue { Name = "Operating System", Slug="operating-system", Value = "Windows 11 Home", SpecificationCategory = "Software", FilterAttributeValueId = favOsWin11Home.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-high-refresh", Value = "High Refresh Rate Display", SpecificationCategory = "Key Feature", FilterAttributeValueId = favFeatHighRefresh.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-adaptive-sync", Value = "Adaptive Sync", SpecificationCategory = "Display", FilterAttributeValueId = favFeatAdaptiveSync.Id },
                        new ProductAttributeValue { Name = "Special Feature", Slug="feature-backlit-keyboard", Value = "Backlit Keyboard", SpecificationCategory = "Keyboard", FilterAttributeValueId = favFeatBacklit.Id },

                        // Detailed Attributes
                        new ProductAttributeValue { Name = "Processor Model", Slug="processor-model", Value = "AMD Ryzen 7 7735HS", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Processor Cores/Threads", Slug="processor-cores-threads", Value = "8 Cores / 16 Threads", SpecificationCategory = "Processor" },
                        new ProductAttributeValue { Name = "Graphics Card Model", Slug="graphics-model", Value = "NVIDIA GeForce RTX 4060 Laptop GPU", SpecificationCategory = "Graphics" },
                        new ProductAttributeValue { Name = "Graphics TGP", Slug="graphics-tgp", Value = "140W (with Dynamic Boost)", SpecificationCategory = "Graphics" },
                        new ProductAttributeValue { Name = "MUX Switch", Slug="mux-switch", Value = "Yes, with NVIDIA Advanced Optimus", SpecificationCategory = "Graphics" },
                        new ProductAttributeValue { Name = "Display Resolution", Slug="display-resolution", Value = "1920x1080 (Full HD)", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Display Refresh Rate", Slug="display-refresh-rate", Value = "144Hz", SpecificationCategory = "Display" },
                        new ProductAttributeValue { Name = "Memory Speed", Slug="memory-speed", Value = "4800MHz", SpecificationCategory = "Memory" },
                        new ProductAttributeValue { Name = "Memory Slots", Slug="memory-slots", Value = "2x SODIMM (Upgradeable)", SpecificationCategory = "Memory" },
                        new ProductAttributeValue { Name = "Storage Type", Slug="storage-type", Value = "NVMe PCIe 4.0 SSD", SpecificationCategory = "Storage" },
                        new ProductAttributeValue { Name = "Storage Expansion", Slug="storage-expansion", Value = "1x Empty M.2 Slot", SpecificationCategory = "Key Feature" }, // Key Feature 2 - Storage Upgrade Slot
                        new ProductAttributeValue { Name = "Wireless Connectivity", Slug="wireless-connectivity", Value = "Wi-Fi 6, Bluetooth 5.2", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Ports", Slug="ports", Value = "1x USB4 (Type-C, DP), 1x USB-C 3.2 Gen 2 (DP/PD/G-SYNC), 2x USB-A 3.2 Gen 1, HDMI 2.1, Ethernet (RJ-45), 3.5mm jack", SpecificationCategory = "Connectivity" },
                        new ProductAttributeValue { Name = "Webcam", Slug="webcam", Value = "720p HD Camera", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Audio", Slug="audio", Value = "Dolby Atmos, Hi-Res Audio certified", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Keyboard Type", Slug="keyboard-type", Value = "Backlit Chiclet Keyboard (RGB option)", SpecificationCategory = "Keyboard" },
                        new ProductAttributeValue { Name = "Cooling System", Slug="cooling-system", Value = "Arc Flow Fans, Multiple Heatpipes", SpecificationCategory = "Features" },
                        new ProductAttributeValue { Name = "Battery", Slug="battery", Value = "90Wh", SpecificationCategory = "Key Feature" }, // Key Feature 3 - Large Battery
                        new ProductAttributeValue { Name = "Military Grade Durability", Slug="mil-std-810h", Value = "MIL-STD-810H standard", SpecificationCategory = "Key Feature" } // Key Feature 4 - Durability Standard
                    },
                    Reviews = new List<ProductReview>
                    {
                        new ProductReview { ReviewerName = "TUF_Gamer", Comment = "Runs everything smoothly, feels really solid and durable.", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-90) },
                        new ProductReview { ReviewerName = "ValueHunter", Comment = "Great performance for the money. Screen could be brighter, but it's fine for gaming.", Rating = 4, CreatedAt = DateTime.UtcNow.AddDays(-60) }
                    },
                    Questions = new List<ProductQuestion>{},
                    Visits = new List<ProductVisit>
                    {

                    }
                },

            };
                context.Products.AddRange(products);
                context.SaveChanges();

            }
        }
    }
}
