using PBCJobAlert.Lib;
using PBCJobAlert.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PBCJobAlert
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private JobListingService JobListingService { get; set; } = new JobListingService();
        private List<JobListing> FilteredJobListings { get; set; } = new List<JobListing>();

        public string CollapseButtonText { get; set; } = "Collapse";
        public Visibility FilterVisibility { get; set; } = Visibility.Visible;
        public Filter Filter { get; set; } = new Filter();

        public string[] FilterModes { get; set; } = { "Simple", "Pattern" };
        private int _selectedFilterModeIndex = 0;
        public int SelectedFilterModeIndex
        {
            get {
                return _selectedFilterModeIndex;
            }
            set
            {
                _selectedFilterModeIndex = value;
                Filter.FilterMode = FilterModes[_selectedFilterModeIndex];
                Bindings.Update();
            }
        }
        public string JobTitlePlaceholder
        {
            get
            {
                return Filter.FilterMode == "Simple" ?
                    "Example: programmer" :
                    "Example: .*programmer.*";
            }
        }
        public string DepartmentPlaceholder
        {
            get
            {
                return Filter.FilterMode == "Simple" ?
                    "Example: information systems services" :
                    "Example: .*information systems services.*";
            }
        }

        public MainPage()
        {
            InitializeComponent();
            DataContext = this;
            UpdateJobListings();
        }
        private async Task MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private async Task UpdateJobListings()
        {
            var listings = await JobListingService.GetJobListings();

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                FilteredJobListings = listings.ToList();
                Bindings.Update();
            });
        }

        private void FilterVisibilityButton_Click(object sender, RoutedEventArgs e)
        {
            Filter.ShowFilter = !Filter.ShowFilter;
            if(!Filter.ShowFilter)
            {
                FilterVisibility = Visibility.Collapsed;
                CollapseButtonText = "Show";
            }
            else
            {
                FilterVisibility = Visibility.Visible;
                CollapseButtonText = "Collapse";
            }
            Bindings.Update();
        }

        private void JobTitle_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Department_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
