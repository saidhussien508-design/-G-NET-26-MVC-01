using GemMangement.DAL;
using GemMangement.DAL.DataSeeding;
using GemMangement.DAL.Models;
using GemMangement.DAL.Reposatours.Classes;
using GemMangement.DAL.Reposatours.Interfasses;
using GemMangement.Pl.Dbcontext;
using GemMangement_AL_;
using GemMangement_AL_.Servicess.Attasment;
using GemMangement_AL_.Servicess.Classes;
using GemMangement_AL_.Servicess.Interfases;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GemMangement
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

           // builder.Services.AddScoped<IplanReposatory,PlanRepository>();
           builder.Services.AddScoped(typeof(IGenaricRepository<>),typeof(GenaricRepository<>));
            builder.Services.AddScoped<GemAppDpContext>();
            builder .Services.AddScoped<IuniteOfWork,Uniteofwork>();
            builder .Services.AddScoped<IsessionRepository,SessionRepository>();
            builder.Services.AddScoped<IsessionServesises, SessionServieses>();
            builder.Services.AddScoped<IAnaiyticsServices,AnaiylticsServisess>();
            builder.Services.AddScoped<Iattasmentservicess, AttasmentServisess>();
            builder.Services.AddAutoMapper(s => s.AddProfile(new MappingProfile()));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                //اقدر اعدل في شروط ال يوزر و ال روول 
            
            }).AddEntityFrameworkStores<GemAppDpContext>();
            builder.Services.AddDbContext<GemAppDpContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("Defultconnection"));
            });
            builder.Services.AddScoped<ImemberServises, MemeberServices>();


            //seeding    my write
            var app = builder.Build();


           using var scop= app.Services.CreateScope();
           var context= scop.ServiceProvider.GetRequiredService<GemAppDpContext>();
            var logger = scop.ServiceProvider.GetRequiredService<ILogger<Program>>();
            var user = scop.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var role = scop.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var folderpath = Path.Combine(app.Environment.ContentRootPath, "wwwroot", "files");
            var pindingMigration=await context.Database.GetPendingMigrationsAsync();
            if (pindingMigration.Any())
            {
                await context.Database.MigrateAsync();//updatedatabase
            }

            await GymDateSeeding.seedAsync(context,folderpath,logger);
            await IdentityDataSeeding.SeedIdentityDataAsync(user,role,logger);
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
