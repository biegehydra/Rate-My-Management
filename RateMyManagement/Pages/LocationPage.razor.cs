using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Radzen;
using RateMyManagement.Core.EntityFramework;
using RateMyManagement.IServices;
using System.Globalization;
using RateMyManagement.Data;

namespace RateMyManagement.Pages
{
    public partial class LocationPage
    {
        [Inject] ILocationService _locationService { get; set; }
        [Inject] ICompanyService _companyService {get;set;}
        [Inject] UserManager<ApplicationUser> _userManager {get;set;}
        [Parameter] public string Id { get; set; }
        private Location _location { get; set; }
        private bool _editing;

        protected override async Task OnInitializedAsync()
        {
            _location = await _locationService.GetLocationAsync(Id);
            _location.LocatioReviews = _location.LocatioReviews.OrderByDescending(x => DateTime.Parse(x.SentDateAndTime, CultureInfo.InvariantCulture)).ToList();
        }
        private async Task OnSubmitReview()
        {
            await OnInitializedAsync();
        }
        private async Task OnSaveLocationChanges(Location location)
        {
            await _locationService.SaveLocationAsync(location);
            _editing = !_editing;
        }
        private void OnSortByClicked(MenuItemEventArgs args)
        {
            switch (args.Text)
            {
                case "Most Recent":
                    _location.LocatioReviews = _location.LocatioReviews.OrderByDescending(x => DateTime.Parse(x.SentDateAndTime, CultureInfo.InvariantCulture)).ToList();
                    break;
                case "Least Recent":
                    _location.LocatioReviews = _location.LocatioReviews.OrderBy(x => DateTime.Parse(x.SentDateAndTime, CultureInfo.InvariantCulture)).ToList();
                    break;
                case "Sender Username":
                    _location.LocatioReviews = _location.LocatioReviews.OrderBy(x => x.SenderUsername).ToList();
                    break;
                case "Employee Name":
                    _location.LocatioReviews = _location.LocatioReviews.OrderBy(x => x.ManagerName).ToList();
                    break;

            }

        }

    }
}
