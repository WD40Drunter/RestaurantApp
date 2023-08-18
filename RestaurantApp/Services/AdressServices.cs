using RestaurantApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Services
{
    public interface IAdressServices
    {
        int AddAdress(Adress adress);
    }
    public class AdressServices : IAdressServices
    {
        public AdressServices(Context context)
        {
            _context = context;
        }
        private readonly Context _context;

        public int AddAdress(Adress adress)
        {
            _context.Adresses.Add(adress);
            _context.SaveChanges();

            return _context.Adresses.FirstOrDefault(adress).AdressId;
        }
    }
}
