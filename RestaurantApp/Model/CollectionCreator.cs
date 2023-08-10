using RestaurantApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RestaurantApp.Model
{
    public class CollectionCreator
    {
        public CollectionCreator()
        {
        }
        
        public string? SearchRestaurantValue { get; set; }

        public ICollectionView GetCollection(ObservableCollection<Restaurant>? collection)
        {
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(collection);
            collectionView.SortDescriptions.Add(new SortDescription(nameof(Restaurant.Name), ListSortDirection.Ascending));
            collectionView.Filter = FilterRestaurants;
            return collectionView;
        }

        private bool FilterRestaurants(object obj)
        {
            if (obj is not Restaurant restaurant)
            {
                return false;
            }
            return restaurant.Name.Contains(SearchRestaurantValue ?? string.Empty, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
