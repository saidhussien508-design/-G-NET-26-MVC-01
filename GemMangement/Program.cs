using GemMangement.DAL;
using GemMangement.DAL.Reposatours.Classes;
using GemMangement.DAL.Reposatours.Interfasses;
using GemMangement.Pl.Dbcontext;
using GemMangement_AL_;
using GemMangement_AL_.Servicess.Classes;
using GemMangement_AL_.Servicess.Interfases;
using Microsoft.EntityFrameworkCore;

namespace GemMangement
{
    public class Program
    {
        public static void Main(string[] args)
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
            builder.Services.AddAutoMapper(s => s.AddProfile(new MappingProfile()));
            builder.Services.AddDbContext<GemAppDpContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("Defultconnection"));
            });
            builder.Services.AddScoped<ImemberServises, MemeberServices>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

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
