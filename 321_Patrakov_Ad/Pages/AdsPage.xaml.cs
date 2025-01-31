using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace _321_Patrakov_Ad.Pages
{
    public partial class AdsPage : Page
    {
        private Users currentUser;

        public AdsPage()
        {
            InitializeComponent();
            LoadCategories();
            LoadCities();
            var currentAds = Entities.GetContext().Ads.ToList();
            ListAds.ItemsSource = currentAds;
            UpdateAds();
        }

        private void LoadCategories()
        {
            var categories = Entities.GetContext().Categories.ToList();
            foreach (var category in categories)
            {
                CategoryBox.Items.Add(new ComboBoxItem { Content = category.category_name });
            }
        }

        private void LoadCities()
        {
            var cities = Entities.GetContext().Cities.ToList();
            foreach (var city in cities)
            {
                CityBox.Items.Add(new ComboBoxItem { Content = city.city_name });
            }
        }

        private void UpdateAds()
        {
            var currentAds = Entities.GetContext().Ads.ToList();

            if (!string.IsNullOrEmpty(SearchBox.Text))
            {
                currentAds = currentAds.Where(x => x.title.ToLower().Contains(SearchBox.Text.ToLower()) ||
                                                    x.description.ToLower().Contains(SearchBox.Text.ToLower())).ToList();
            }

            if (CategoryBox.SelectedIndex > 0)
            {
                var selectedCategory = (CategoryBox.SelectedItem as ComboBoxItem).Content.ToString();
                currentAds = currentAds.Where(x => x.Categories.category_name == selectedCategory).ToList();
            }

            if (CityBox.SelectedIndex > 0)
            {
                var selectedCity = (CityBox.SelectedItem as ComboBoxItem).Content.ToString();
                currentAds = currentAds.Where(x => x.Cities.city_name == selectedCity).ToList();
            }

            if (TypeBox.SelectedIndex > 0)
            {
                var selectedType = (TypeBox.SelectedItem as ComboBoxItem).Content.ToString();
                currentAds = currentAds.Where(x => x.Types.type_name == selectedType).ToList();
            }

            if (StatusBox.SelectedIndex > 0)
            {
                var selectedStatus = (StatusBox.SelectedItem as ComboBoxItem).Content.ToString();
                currentAds = currentAds.Where(x => x.Statuses.status_name == selectedStatus).ToList();
            }

            ListAds.ItemsSource = currentAds;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateAds();
        }

        private void CategoryBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAds();
        }

        private void CityBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAds();
        }

        private void TypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAds();
        }

        private void StatusBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAds();
        }

        private void ClearFilterButton_Click(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = string.Empty;
            CategoryBox.SelectedIndex = 0;
            CityBox.SelectedIndex = 0;
            TypeBox.SelectedIndex = 0;
            StatusBox.SelectedIndex = 0;

            UpdateAds();
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AuthPage());
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new ProfilePage(currentUser));
        }

        public void SetAuthenticatedUser(Users user)
        {
            currentUser = user;
            AuthButton.Visibility = Visibility.Collapsed;
            ProfileButton.Visibility = Visibility.Visible;
        }
    }
}