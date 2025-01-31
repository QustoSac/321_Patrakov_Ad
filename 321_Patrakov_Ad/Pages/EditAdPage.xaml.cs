using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace _321_Patrakov_Ad.Pages
{
    public partial class EditAdPage : Page
    {
        private int adId;
        private Users currentUser;
        private Ads currentAd;

        public EditAdPage(int adId, Users user)
        {
            InitializeComponent();
            this.adId = adId;
            currentUser = user;
            LoadAdData();
            LoadCities();
            LoadCategories();
            LoadTypes();
            LoadStatuses();
        }

        private void LoadAdData()
        {
            using (var db = new Entities())
            {
                currentAd = db.Ads.Find(adId);
                if (currentAd != null)
                {
                    TitleTextBox.Text = currentAd.title;
                    DescriptionTextBox.Text = currentAd.description;
                    CostTextBox.Text = currentAd.cost.ToString();

                    CityComboBox.SelectedValue = currentAd.city_id;
                    CategoryComboBox.SelectedValue = currentAd.category_id;
                    TypeComboBox.SelectedValue = currentAd.type_id;
                    StatusComboBox.SelectedValue = currentAd.status_id;
                }
            }
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
                CityComboBox.DisplayMemberPath = "Content";
                CityComboBox.SelectedValuePath = "Tag";
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
                CategoryComboBox.DisplayMemberPath = "Content";
                CategoryComboBox.SelectedValuePath = "Tag";
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
                TypeComboBox.DisplayMemberPath = "Content";
                TypeComboBox.SelectedValuePath = "Tag";
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
                StatusComboBox.DisplayMemberPath = "Content";
                StatusComboBox.SelectedValuePath = "Tag";
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
                currentAd.title = TitleTextBox.Text;
                currentAd.description = DescriptionTextBox.Text;
                currentAd.cost = decimal.Parse(CostTextBox.Text);
                currentAd.city_id = (int)CityComboBox.SelectedValue;
                currentAd.category_id = (int)CategoryComboBox.SelectedValue;
                currentAd.type_id = (int)TypeComboBox.SelectedValue;
                currentAd.status_id = (int)StatusComboBox.SelectedValue;

                db.Ads.AddOrUpdate(currentAd);
                db.SaveChanges();
                MessageBox.Show("Объявление успешно обновлено!");
                NavigationService?.GoBack();
            }
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
    }
}