using Microsoft.AspNetCore.Identity;

namespace VehicleAccounting.Data
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "grvvgnj@gmail.com";
            string password = "Dotan12345_";
            if (await roleManager.FindByNameAsync("Директор") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Директор"));
            }
            if (await roleManager.FindByNameAsync("Главный бухгалтер") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Главный бухгалтер"));
            }
            if (await roleManager.FindByNameAsync("Бухгалтер") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Бухгалтер"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                IdentityUser admin = new IdentityUser { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Директор");
                }
            }
        }
    }
}
