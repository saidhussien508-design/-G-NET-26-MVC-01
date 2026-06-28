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
        public static Result notfound(string massage="notfound") => new(false,massage,ResultKind.Notfound);
        public static Result validation(string massage) => new(false,massage,ResultKind.validationfiled);

    }
    public record Result<t>(bool susses,t? Value ,string? error = null, ResultKind kind = ResultKind.ok)
    {
        public static Result<t> ok(t value) => new(true,value);
        public static Result<t> fail(string massege, ResultKind kind = ResultKind.Conflict) => new(false,default ,massege, kind);
        public static Result<t> notfound(string massage = "notfound") => new(false,default ,massage, ResultKind.Notfound);
        public static Result<t> validation(string massage ) => new(false,default ,massage, ResultKind.validationfiled);

    }
}
