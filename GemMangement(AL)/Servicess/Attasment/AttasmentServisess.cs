using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement_AL_.Servicess.Attasment
{
    public class AttasmentServisess : Iattasmentservicess
    {
        public AttasmentServisess(IWebHostEnvironment webHostEnvironment) 
        {
            _env = webHostEnvironment;
        }
        
        private readonly long _MaxFileSize=5*1024*1024;
        private readonly string[] _extentions = [".jpg", ".jpeg", ".png"];
        private readonly IWebHostEnvironment _env;

        public async Task<string?> UploadAsync(Stream fileStream, string folderName, string filename, CancellationToken ct = default)
        {
            if (fileStream is null || !fileStream.CanRead) return null;
            if (fileStream.Length == 0) return null;
            if (fileStream.Length >_MaxFileSize) return null;
            var extention= Path.GetExtension(filename);
            if (string.IsNullOrWhiteSpace(extention) || !_extentions.Contains(extention)) return null;
            var uploadfolder = Path.Combine(_env.ContentRootPath, folderName);
            Directory.CreateDirectory(uploadfolder);
            var stortedfilename = $"{Guid.NewGuid()}{filename}";
            var filepath = Path.Combine(uploadfolder, stortedfilename);
            try
            {
                using var sf = new FileStream(filepath, FileMode.Create, FileAccess.Write);
                await fileStream.CopyToAsync(sf);
                return stortedfilename;
            }
            catch (Exception ex) 
            {
                return null;
            }

        }

        public bool Delete(string foldername, string filename)
        {
            try
            {
                var filepath=Path.Combine(_env.ContentRootPath,foldername,filename);
                if (!File.Exists(filepath)) return false;
                File.Delete(filepath);
                return true;
                    



            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public (Stream strem, string contenttype)? GetFile(string foldername, string filename)
        {
            if (string.IsNullOrWhiteSpace(filename) || string.IsNullOrWhiteSpace(foldername)) return null;
            var filepath = Path.Combine(_env.ContentRootPath, foldername, filename);
            if (!File.Exists(filepath)) return null;
            var stream=new FileStream(filepath,FileMode.Open,FileAccess.Read);
            var extention = Path.GetExtension(filepath).ToLower();
            var contenttype = extention switch
            {
              ".png"=>"image/png",
              ".jpg"or".jepg"=> "image/jpeg",
             _ =>"application/octet-stream"

            };
            return(stream,contenttype);

        }

       
    }
}
