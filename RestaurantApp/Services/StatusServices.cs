using Microsoft.EntityFrameworkCore;
using RestaurantApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
