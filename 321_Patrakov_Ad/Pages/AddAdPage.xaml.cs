using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace _321_Patrakov_Ad.Pages
{
    public partial class AddAdPage : Page
    {
        private Users currentUser;

        public AddAdPage(Users user)
        {
            InitializeComponent();
            currentUser = user;
            LoadCities();
            LoadCategories();
            LoadTypes();
            LoadStatuses();
        }

        private void LoadCities()
        {
            using (var db = new Entities())
            {
                var cities = db.Cities.ToList();
                foreach (var city in cities)
                {
                    CityComboBox.Items.Add(new ComboBoxItem { Content = city.city_name, Tag = city.city_id });
                }
            }
        }

        private void LoadCategories()
        {
            using (var db = new Entities())
            {
                var categories = db.Categories.ToList();
                foreach (var category in categories)
                {
                    CategoryComboBox.Items.Add(new ComboBoxItem { Content = category.category_name, Tag = category.category_id });
                }
            }
        }

        private void LoadTypes()
        {
            using (var db = new Entities())
            {
                var types = db.Types.ToList();
                foreach (var type in types)
                {
                    TypeComboBox.Items.Add(new ComboBoxItem { Content = type.type_name, Tag = type.type_id });
                }
            }
        }

        private void LoadStatuses()
        {
            using (var db = new Entities())
            {
                var statuses = db.Statuses.ToList();
                foreach (var status in statuses)
                {
                    StatusComboBox.Items.Add(new ComboBoxItem { Content = status.status_name, Tag = status.status_id });
                }
            }
        }

        private void SaveAdButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TitleTextBox.Text) || string.IsNullOrEmpty(DescriptionTextBox.Text) ||
                string.IsNullOrEmpty(CostTextBox.Text) || CityComboBox.SelectedItem == null ||
                CategoryComboBox.SelectedItem == null || TypeComboBox.SelectedItem == null ||
                StatusComboBox.SelectedItem == null)
            {
                MessageBox.Show("Все поля должны быть заполнены!");
                return;
            }

            using (var db = new Entities())
            {
                var newAd = new Ads
                {
                    publication_date = DateTime.Now,
                    city_id = (int)((ComboBoxItem)CityComboBox.SelectedItem).Tag,
                    user_id = currentUser.user_id,
                    category_id = (int)((ComboBoxItem)CategoryComboBox.SelectedItem).Tag,
                    type_id = (int)((ComboBoxItem)TypeComboBox.SelectedItem).Tag,
                    status_id = (int)((ComboBoxItem)StatusComboBox.SelectedItem).Tag,
                    title = TitleTextBox.Text,
                    description = DescriptionTextBox.Text,
                    cost = decimal.Parse(CostTextBox.Text)
                };

                db.Ads.Add(newAd);
                db.SaveChanges();
                MessageBox.Show("Объявление успешно добавлено!");
                NavigationService?.GoBack();
            }
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
    }
}