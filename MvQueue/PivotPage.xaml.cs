﻿#region

using System;
using Windows.ApplicationModel.Resources;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using MvQueue.Common;
using MvQueue.DataModel;
using MvQueue.Model;
using MvQueue.View;

#endregion

// The Pivot Application template is documented at http://go.microsoft.com/fwlink/?LinkID=391641

namespace MvQueue
{
    public sealed partial class PivotPage : Page
    {
        private const string FirstGroupName = "FirstGroup";

        private readonly ObservableDictionary _defaultViewModel = new ObservableDictionary();
        private readonly NavigationHelper _navigationHelper;
        private readonly ResourceLoader _resourceLoader = ResourceLoader.GetForCurrentView("Resources");

        public PivotPage()
        {
            InitializeComponent();

            var statusBar = StatusBar.GetForCurrentView();
            statusBar.BackgroundOpacity = 1;
            statusBar.BackgroundColor = Util.ConvertStringToColor("#333333");

            NavigationCacheMode = NavigationCacheMode.Required;

            _navigationHelper = new NavigationHelper(this);
            _navigationHelper.LoadState += NavigationHelper_LoadState;
            _navigationHelper.SaveState += NavigationHelper_SaveState;

            StatusBar.GetForCurrentView().BackgroundColor = Colors.Black;
        }

        public NavigationHelper NavigationHelper
        {
            get { return _navigationHelper; }
        }

        public ObservableDictionary DefaultViewModel
        {
            get { return _defaultViewModel; }
        }

        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            var mainDataGroup = await MainDataSource.GetGroupAsync("Group-1");
            DefaultViewModel[FirstGroupName] = mainDataGroup;
        }


        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            // TODO: Save the unique state of the page here.
        }

        private void AddAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            //string groupName = pivot.SelectedIndex == 0 ? FirstGroupName : SecondGroupName;
            //var group = DefaultViewModel[groupName] as SampleDataGroup;
            //var nextItemId = group.Items.Count + 1;
            //var newItem = new SampleDataItem(
            //    string.Format(CultureInfo.InvariantCulture, "Group-{0}-Item-{1}", pivot.SelectedIndex + 1, nextItemId),
            //    string.Format(CultureInfo.CurrentCulture, _resourceLoader.GetString("NewItemTitle"), nextItemId),
            //    string.Empty,
            //    string.Empty,
            //    _resourceLoader.GetString("NewItemDescription"),
            //    string.Empty);

            //group.Items.Add(newItem);

            //// Scroll the new item into view.
            //var container = pivot.ContainerFromIndex(pivot.SelectedIndex) as ContentControl;
            //var listView = container.ContentTemplateRoot as ListView;
            //listView.ScrollIntoView(newItem, ScrollIntoViewAlignment.Leading);
        }


        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var itemId = ((MainDataItem) e.ClickedItem).UniqueId;
            switch (itemId)
            {
                case "MIB":
                    Frame.Navigate(typeof (MibPage));
                    break;
                case "Medica":
                    Frame.Navigate(typeof (MedicaPage));
                    break;
                case "SBI":
                    Frame.Navigate(typeof (SbiPage));
                    break;
                case "ADK":
                    Frame.Navigate(typeof (AdkPage));
                    break;
                case "MIRA":
                    Frame.Navigate(typeof (MiraPage));
                    break;
                case "Immigration":
                    Frame.Navigate(typeof (ImmigrationPage));
                    break;
                default:
                    throw new Exception(_resourceLoader.GetString("NavigationFailedExceptionMessage"));
            }
        }

        #region NavigationHelper registration

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}