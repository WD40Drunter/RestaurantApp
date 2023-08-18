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
        Adress EditAdress(Adress oldValue, Adress newValue);
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

            return adress.AdressId;
        }

        public Adress EditAdress(Adress oldValue, Adress newValue)
        {
            oldValue.Country = newValue.Country;
            oldValue.City = newValue.City;
            oldValue.Street = newValue.Street;
            oldValue.HouseNumber = newValue.HouseNumber;
            oldValue.PostalCode = newValue.PostalCode;

            _context.SaveChanges();
            return oldValue;
        }

    }
}
