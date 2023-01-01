using Microsoft.AspNetCore.Components;
using RateMyManagement.Core.EntityFramework;
using RateMyManagement.Core.Paginiation;
using RateMyManagement.Core;
using RateMyManagement.Data;
using RateMyManagement.IServices;
using RateMyManagement.Shared.Layout;

namespace RateMyManagement.Pages
{
    public partial class Index
    {
        [Inject] private ICompanyService companyService { get; set; }
        [Inject] private ILocationService locationService { get; set; }
        [Inject] private ApplicationDbContext dbContext { get; set; }
        [Inject] private NavigationManager navManager { get; set; }
        [CascadingParameter] private MainLayout? _layout { get; set; }
        private List<Company> _currentPageCompanies = new List<Company>();

        private PagingMetaData? _pagingMetaData = new PagingMetaData()
        {
            CurrentPage = 0,
            PageSize = 0, // Doesn't matter
            TotalCount = 0, // Doesn't matter
            TotalPages = 2
        };
        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(1);
            _layout?.DisableSearch();
            _layout?.DisableLogo();
        }

        private async Task OnPageSelected(string str)
        {
            try
            {
                _currentPageCompanies = await companyService.QueryCompaniesByStartingLetter(str[0]);
            }
            catch (Exception exception)
            {
                var e = exception;
            }
        }
        private async Task PopulateSite()
        {
            try
            {
                var companies = BogusWrapper.GenerateFakeCompanies(100);
                foreach (var company in companies)
                {
                    var locations = BogusWrapper.GenerateFakeLocations(company, 20).ToList();
                    company.Locations = locations;
                    foreach (var location in company.Locations)
                    {
                        var reviews = BogusWrapper.GenerateFakeLocationReviews(location, 20).ToList();
                        location.LocatioReviews = reviews;
                    }
                }

                await dbContext.Companies.AddRangeAsync(companies);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                var exc = e;
            }
        }
    }
}
