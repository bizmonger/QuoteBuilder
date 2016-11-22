using static Bizmonger.Patterns.MessageBus;
using Entities;
using Mediation;
using Mediation.Validation;
using Servers;

namespace Repositories
{
    public partial class AbstractProfileRepository
    {
        protected void MakePromises()
        {
            Subscribe(Messages.REQUEST_SAVE_PROFILE, OnSaveProfileResponse);
            Subscribe(Messages.REQUEST_PROFILE, OnProfileRequested);
        }

        protected void breakExistingPromises()
        {
            Unsubscribe(Messages.REQUEST_SAVE_PROFILE, OnSaveProfileResponse);
            Unsubscribe(Messages.REQUEST_PROFILE, OnProfileRequested);
        }

        protected void SendRequests() => Read();

        protected void OnSaveProfileResponse(object obj) => Save(obj);

        protected void OnProfileRequested(object obj) => Read();

        protected abstract void SaveData(Profile profile);
        protected abstract void Read();

        protected bool Validate(Profile profile) => new ProfileValidator().Validate(profile);

        void PublishSaveResult(bool isValid)
        {
            if (isValid) Publish(Messages.REQUEST_SAVE_PROFILE_RESPONSE, new ProfileServer().GetProfile());
            else Publish(Messages.REQUEST_SAVE_PROFILE_RESPONSE, null);
        }
    }
}