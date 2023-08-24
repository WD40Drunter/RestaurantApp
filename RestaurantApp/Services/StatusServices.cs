using RestaurantApp.Model;
using System.Collections.Generic;

namespace RestaurantApp.Services
{
    public interface IStatusServices
    {
        IEnumerable<Status> GetAll();
    }
    public class StatusServices : IStatusServices
    {
        public StatusServices(Context context)
        {
            _context = context;
        }
        private readonly Context _context;

        public IEnumerable<Status> GetAll()
        {
            return _context.Statuses;
        }

    }
}
