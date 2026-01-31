using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;
using System.Text.Json;

namespace GymManagementDAL.Data.DataSeed
{
    public static class GymDbContextSeeding
    {
        public static bool SeedData(GymDbContext dbContext)
        {
            try
            {
                var HasPlans = dbContext.Plans.Any();
                var HasCategories = dbContext.Categories.Any();
                if (HasPlans && HasCategories) return false;

                if (!HasPlans)
                {
                    var plans = LoadDataFromJsonFile<Plan>("plans.json");
                    if (plans.Any())
                    {
                        dbContext.Plans.AddRange(plans);
                    }
                }
                if (!HasCategories)
                {
                    var categories = LoadDataFromJsonFile<Category>("categories.json");
                    if (categories.Any())
                    {
                        dbContext.Categories.AddRange(categories);
                    }
                }

                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seeding Failed: {ex}");
                return false;
            }
        }



        private static List<T> LoadDataFromJsonFile<T>(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", fileName);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }
            string Data = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<List<T>>(Data, options) ?? new List<T>();
        }

    }
}
