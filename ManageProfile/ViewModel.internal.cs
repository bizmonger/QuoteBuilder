using static Bizmonger.Patterns.MessageBus;
using Entities;
using Mediation;
using Mediation.Validation;
using Messaging.Databases;

namespace ManageProfile
{
    public partial class ViewModel
    {
        Profile _profile = new Profile();
        readonly ProfileValidator _validator = new ProfileValidator();

        protected override void MakePromises()
        {
            Subscribe(Messages.REQUEST_SAVE_PROFILE_RESPONSE, OnSaveProfileResponse);
            Subscribe(Messages.REQUEST_PROFILE_RESPONSE, OnRequestProfileResponse);
        }

        protected override void BreakPromises()
        {
            Unsubscribe(Messages.REQUEST_SAVE_PROFILE_RESPONSE, OnSaveProfileResponse);
            Unsubscribe(Messages.REQUEST_PROFILE_RESPONSE, OnRequestProfileResponse);
        }

        void SendRequests() =>
            Publish(Messages.REQUEST_PROFILE_DATABASE);

        void OnRequestProfileResponse(object obj)
        {
            var profile = obj as Profile;
            FirstName = profile?.FirstName;
            LastName = profile?.LastName;
            BusinessName = profile?.BusinessName;
            Phone = profile?.Phone;
            Email = profile?.Email;
            Address1 = profile?.Address1;
            Address2 = profile?.Address2;
            City = profile?.City;
            State = profile?.State;
            Postal = profile?.Postal;
        }

        void OnSaveProfileResponse(object obj)
        {
            _profile = obj as Profile;

            Saved |= _profile != null;
        }

        void OnSave(object obj)
        {
            if (OnCanSave(null))
            {
                Publish(Messages.REQUEST_SAVE_PROFILE, _profile);
                Publish(Messages.REQUEST_PREVIOUS_VIEW);
            }
        }

        bool OnCanSave(object obj)
        {
            _profile.BusinessName = BusinessName;
            _profile.FirstName = FirstName;
            _profile.LastName = LastName;
            _profile.Phone = Phone;
            _profile.Email = Email;
            _profile.Address1 = Address1;
            _profile.Address2 = Address2;
            _profile.State = State;
            _profile.City = City;
            _profile.Postal = Postal;

            return _validator.Validate(_profile);
        }
    }
}