using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace RestaurantApp.Model
{
    public class Dish : INotifyPropertyChanged

    {
        [Key]
        public long DishId { get; set; }

        public Dish(string name, int statusId, int restaurantId)
        {
            Name = name;
            StatusId = statusId;
            RestaurantId = restaurantId;
        }
        public string Name { get; set; }
        private int _statusId;
        public int StatusId { get
            {
                return _statusId;
            }
            set 
            {
                _statusId = value;
                NotifyPropertyChanged();
            }

        }
        public virtual Status? Status { get; set; }
        public int RestaurantId { get; set; }
        public virtual Restaurant? Restaurant { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
