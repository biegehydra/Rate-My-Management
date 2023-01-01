using System.Reflection.Metadata;
using Microsoft.AspNetCore.Components;
using RateMyManagement.Core;
using RateMyManagement.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using RateMyManagement.IServices;

namespace RateMyManagement.Shared
{
    public partial class LocationReviewForm
    {
        [Inject] private ILocationService locationService { get; set; }
        [Inject] private AuthenticationStateProvider authenticationStateProvider { get; set; }
        [Parameter] public LocationReview LocationReview { get; set; } = new LocationReview();
        [Parameter] public string LocationId { get; set; }
        [Parameter] public EventCallback SubmitCallback { get; set; }
        private IEnumerable<string> _multiplateAttributes = new List<string>();
        public IEnumerable<string> MultipleAttributes
        {
            get { return _multiplateAttributes; }
            set
            {
                _multiplateAttributes = value;
                string temp = string.Empty;
                foreach (var str in value)
                {
                    temp += str + ",";
                }
                if (temp.Length > 0)
                {
                    temp.TrimEnd(',');
                }
                LocationReview.ManagerAttributes = temp;
            }
        }
        private bool _creatingReview;
        private List<string> ManagerTypes = ExtensionMethods.GetEnumNamesCorrected<ManagerType>();
        private List<string> ManagerAttributes = ExtensionMethods.GetEnumNamesCorrected<ManagerAttribute>();
        private ClaimsPrincipal? _claimsPrincipal;

        private string? _managerType;

        private int _reviewType;
        private int Reviewtype
        {
            get { return _reviewType; }
            set
            {
                _reviewType = value;
                OnReviewTypeChange(_reviewType);
            }
        }
        protected override async Task OnInitializedAsync()
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            _claimsPrincipal = authState.User;
        }
        private void OnReviewTypeChange(int args)
        {
            LocationReview.Type = args == 1 ? ReviewType.Employee.ToString() : ReviewType.Customer.ToString();
        }
        private void OnManagerTypeSelectionChange(object args)
        {
            var asString = args.ToString();
            ManagerType managerType;
            if (asString != null && asString.MatchesEnumItem(out managerType))
            {
                LocationReview.ManagerType = managerType.ToString();
            }

        }

        private void OnCancelCreateReview()
        {
            ResetForm();
            _creatingReview = false;
        }
        private async Task OnSubmitReview()
        {
            if (_claimsPrincipal == null || _claimsPrincipal.Identity == null || _claimsPrincipal.Identity.Name == null) return;
            LocationReview.SenderUsername = _claimsPrincipal.Identity.Name;
            LocationReview.Id = Guid.NewGuid().ToString();
            await locationService.AddLocationReviewAsync(LocationId, LocationReview);
            ResetForm();
            _creatingReview = false;
            await SubmitCallback.InvokeAsync();
        }
        private void ResetForm()
        {
            LocationReview = new LocationReview();
            MultipleAttributes = new List<string>();
            _managerType = null;
            Reviewtype = 0;
        }
    }
}
