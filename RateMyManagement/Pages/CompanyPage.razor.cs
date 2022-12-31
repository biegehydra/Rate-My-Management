using Microsoft.AspNetCore.Components;
using RateMyManagement.Data;
using RateMyManagement.IServices;
using RateMyManagement.Shared;

namespace RateMyManagement.Pages
{
    public partial class CompanyPage
    {
        [Inject] private ILocationService _locationService { get; set; }
        [Inject] private ICompanyService _companyService { get; set; }
        [Inject] private NavigationManager _navManager { get; set; }

        [Parameter] public string? CompanyId { get; set; }
        private Company _company;
        private List<Location> _queriedLocations = new List<Location>();
        private bool _creatingLocation;
        private bool _editing { get; set; }

        private AddImage? _addImage;

        private string _query = string.Empty;

        public string Query
        {
            get { return _query; }
            set
            {
                _query = value;
                _queriedLocations = _company.Locations.Where(x => x.Address.Contains(_query) || x.City.Contains(_query)).ToList();
                if (string.IsNullOrWhiteSpace(_query))
                {
                    _queriedLocations = _company.Locations;
                }
            }
        }
        private string _locationAddress = String.Empty;
        private string _locationCity = String.Empty;
        protected override async Task OnInitializedAsync()
        {
            await GetCompanyAsync();
            _queriedLocations = _company.Locations;
        }
        private async Task GetCompanyAsync()
        {
            if (CompanyId == null) return;
            _company = await _companyService.GetCompanyAsync(CompanyId);
        }
        private async Task OnSubmitLocation()
        {
            if (string.IsNullOrWhiteSpace(_locationAddress) || string.IsNullOrWhiteSpace(_locationCity)) return;
            var location = new Location()
            {
                Id = Guid.NewGuid().ToString(),
                Company = _company,
                Address = _locationAddress,
                City = _locationCity,
                LocatioReviews = new()
            };
            await _locationService.SaveLocationAsync(location);
            ResetLocation();
        }
        private void ResetLocation()
        {
            _locationAddress = string.Empty;
            _locationCity = string.Empty;
            _creatingLocation = false;
        }
        private void Navaway(string id)
        {
            var url = $"/company/location/{id}";
            _navManager.NavigateTo(url);
        }
        private async Task OnCompanyChangesSaved(Company company)
        {
            await _companyService.SaveCompanyAsync(company);
            _editing = !_editing;
            if (_addImage != null)
            {
                await _addImage.Reset(false);
            }
        }
        private async Task OnBeginEditing()
        {
            _editing = !_editing;
            await Task.Delay(2);
            if (_addImage != null)
            {
                _addImage.SetImage(_company.LogoUrl);
            }
        }
        private async Task OnStopEditing()
        {
            _editing = !_editing;
            if (_addImage != null)
            {
                await _addImage.Reset(true);
            }
        }
        private void OnImageUploaded(ImgbbUploadResponse response)
        {
            _company.LogoUrl = response.data.display_url;
            _company.LogoDeleteUrl = response.data.delete_url;
        }
    }
}
