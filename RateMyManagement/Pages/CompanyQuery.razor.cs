using Microsoft.AspNetCore.Components;
using RateMyManagement.Data;
using RateMyManagement.IServices;

namespace RateMyManagement.Pages
{
    public partial class CompanyQuery
    {
        [Inject] private ICompanyService companyService { get; set; }
        [Inject] private ILocationService locationService { get; set; }
        [Inject] private NavigationManager navManager { get; set; }
        [Parameter]
        public string? Query { get; set; }

        private bool _loading;
        public List<Company> CompaniesQueried { get; set; } = new List<Company>();
        protected override async Task OnParametersSetAsync()
        {
            CompaniesQueried = new List<Company>();
            await GetQueriedCompanies();
        }

        private async Task GetQueriedCompanies()
        {
            if (Query == null) return;
            _loading = true;
            var temp = await companyService.GetAllCompaniesAsync();
            CompaniesQueried = temp.Where(x => x.Name.ToLower().Contains(Query.ToLower())).ToList();
            _loading = false;
        }

        private void NavigateToCompany(string companyId)
        {
            navManager.NavigateTo($"/company/{companyId}");
        }
    }
}
