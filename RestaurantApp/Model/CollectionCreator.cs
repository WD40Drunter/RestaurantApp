using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace RestaurantApp.Model
{
    public class CollectionCreator
    {
        public CollectionCreator()
        {
        }

        public string? SearchRestaurantValue { get; set; }
        public string? SearchDishValue { get; set; }
        public string? SearchUserValue { get; set; }

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

        public ICollectionView GetCollection(ObservableCollection<Dish>? collection)
        {
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(collection);
            collectionView.SortDescriptions.Add(new SortDescription(nameof(Dish.Name), ListSortDirection.Ascending));
            collectionView.Filter = FilterDishes;
            return collectionView;
        }

        private bool FilterDishes(object obj)
        {
            if (obj is not Dish dish)
            {
                return false;
            }
            return dish.Name.Contains(SearchDishValue ?? string.Empty, StringComparison.InvariantCultureIgnoreCase);
        }

        public static ICollectionView GetCollection(ObservableCollection<Status>? collection)
        {
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(collection);
            collectionView.SortDescriptions.Add(new SortDescription(nameof(Status.StatusId), ListSortDirection.Ascending));
            return collectionView;
        }

        public ICollectionView GetCollection(ObservableCollection<User>? collection)
        {
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(collection);
            collectionView.SortDescriptions.Add(new SortDescription(nameof(User.Login), ListSortDirection.Ascending));
            collectionView.Filter = FilterUsers;
            return collectionView;
        }

        private bool FilterUsers(object obj)
        {
            if (obj is not User dish)
            {
                return false;
            }
            return dish.Login.Contains(SearchUserValue ?? string.Empty, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
