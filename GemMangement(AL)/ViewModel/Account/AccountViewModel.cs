using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement_AL_.ViewModel.Account
{
    public class AccountViewModel
    {
        [Required(ErrorMessage ="Email Is Reqired")]
        [EmailAddress]
        public string Email {  get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Password Is Required")]
        public string  Password { get; set; }

        public bool RememberMe {  get; set; }
    }
}
