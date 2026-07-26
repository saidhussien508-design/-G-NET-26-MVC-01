using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement_AL_.Servicess.Attasment
{
    public  interface Iattasmentservicess
    {
        Task<string?> UploadAsync(Stream fileStream, string folderName, string filename, CancellationToken ct = default);
        bool Delete(string foldername,string filename);
        (Stream strem, string contenttype)? GetFile(string foldername, string filename);
    }
}
