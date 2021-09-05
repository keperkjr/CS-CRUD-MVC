using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_MVC.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Geo
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }

    public class Address
    {
        public string street { get; set; }
        public string suite { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public Geo geo { get; set; }
    }

    public class Company
    {
        public string name { get; set; }
        public string catchPhrase { get; set; }
        public string bs { get; set; }
    }

    public class Employee
    {
        [Display(Name = "Id #")]
        public int id { get; set; }
        [Display(Name = "Name")]
        public string name { get; set; }
        [Display(Name = "UserName")]
        public string username { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Address")]
        public Address address { get; set; }
        [Display(Name = "Phone #")]
        public string phone { get; set; }
        [Display(Name = "Website")]
        public string website { get; set; }
        [Display(Name = "Company")]
        public Company company { get; set; }
    }
}
