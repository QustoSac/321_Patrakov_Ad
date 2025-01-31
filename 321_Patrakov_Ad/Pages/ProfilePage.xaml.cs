using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace _321_Patrakov_Ad.Pages
{
    public partial class ProfilePage : Page
    {
        private Users currentUser;
        private bool showCompletedAds = false;

        public ProfilePage(Users user)
        {
            InitializeComponent();
            currentUser = user;
            LoadUserAds();
            CalculateTotalEarnings();
        }

        private void LoadUserAds()
        {
            using (var db = new Entities())
            {
                var userAds = db.Ads.Where(a => a.user_id == currentUser.user_id).ToList();
                if (showCompletedAds)
                {
                    userAds = userAds.Where(a => a.status_id == 2).ToList();
                }
                UserAdsListView.ItemsSource = userAds;
            }
        }

        private void CalculateTotalEarnings()
        {
            using (var db = new Entities())
            {
                var totalEarnings = db.Ads
                    .Where(a => a.user_id == currentUser.user_id && a.status_id == 2)
                    .Select(a => a.cost)
                    .DefaultIfEmpty(0)
                    .Sum();
                TotalEarningsTextBlock.Text = totalEarnings.ToString("C");
            }
        }

        private void AddAdButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddAdPage(currentUser));
        }

        private void EditAdButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && int.TryParse(button.Tag.ToString(), out int adId))
            {
                NavigationService?.Navigate(new EditAdPage(adId, currentUser));
            }
        }

        private void DeleteAdButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && int.TryParse(button.Tag.ToString(), out int adId))
            {
                using (var db = new Entities())
                {
                    var ad = db.Ads.Find(adId);
                    if (ad != null)
                    {
                        db.Ads.Remove(ad);
                        db.SaveChanges();
                        LoadUserAds();
                        CalculateTotalEarnings();
                    }
                }
            }
        }

        private void CompletedAdsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            showCompletedAds = true;
            LoadUserAds();
        }

        private void CompletedAdsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            showCompletedAds = false;
            LoadUserAds();
        }
    }
}
