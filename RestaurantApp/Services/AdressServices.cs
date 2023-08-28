using RestaurantApp.Model;
using System.Linq;

namespace RestaurantApp.Services
{
    public interface IAdressServices
    {
        int AddAdress(Adress adress);
        void EditAdress(Adress oldValue, Adress newValue);
        void DeleteAdress(int adressId);
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

        public void EditAdress(Adress oldValue, Adress newValue)
        {
            oldValue.Country = newValue.Country;
            oldValue.City = newValue.City;
            oldValue.Street = newValue.Street;
            oldValue.HouseNumber = newValue.HouseNumber;
            oldValue.PostalCode = newValue.PostalCode;

            _context.SaveChanges();
        }

        public void DeleteAdress(int adressId)
        {
            _context.Adresses.Remove(_context.Adresses.First(x => x.AdressId == adressId));
            _context.SaveChanges();
        }
    }
}
