using GemMangement.DAL.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement.DAL.Models
{
    public abstract class GemUser:BaseEntity
    {
        public string Name {  get; set; }
        public string Email {  get; set; }
        public string phone {  get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }  
        public Address Address { get; set; }    

    }
    public class Address
    {
        public int BuildingNumber {  get; set; }
        public string Street { get; set; }
        public string City {  get; set; }
    }
}
