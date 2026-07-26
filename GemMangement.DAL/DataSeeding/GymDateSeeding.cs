using GemMangement.Models;
using GemMangement.Pl.Dbcontext;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GemMangement.DAL.DataSeeding
{
    public class GymDateSeeding
    {
        public static async Task seedAsync(GemAppDpContext context,string seedFolderPath,ILogger logger,CancellationToken ct=default)
        {
            try
            {
                if (!context.planes.Any())
                {
                   var Filepath= Path.Combine(seedFolderPath, "plans.json");
                    if (File.Exists(Filepath))
                        throw new FileNotFoundException($"Seed Data File Not Found:{Filepath}");

                    var data=File.ReadAllText(Filepath);
                    var option = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };

                   var plan= JsonSerializer.Deserialize<List<plane>>(data, option) ?? [];

                    await context.planes.AddRangeAsync(plan);
                    await context.SaveChangesAsync(ct);
                }
               
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}
