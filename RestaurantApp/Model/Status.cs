using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Model
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }
        public Status(string name) 
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
