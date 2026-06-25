using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement_AL_.common
{
    public  record Result(bool susses,string? error=null,ResultKind kind=ResultKind.ok)
    {
        public static Result ok() => new(true);
        public static Result fail(string massege,ResultKind kind=ResultKind.Conflict) => new(false,massege,kind);
        public static Result notfound(string massage="notfound") => new(true);
        public static Result ok() => new(true);

    }
}
