using Entities;
using Mediation;
using System;
using static Bizmonger.Patterns.MessageBus;

namespace Servers
{
    public class ProfileServer
    {
        public Profile GetProfile()
        {
            Profile profile = null;
            SubscribeFirstPublication(Messages.REQUEST_PROFILE_RESPONSE, obj => profile = obj as Profile);
            Publish(Messages.REQUEST_PROFILE);


            if (profile == null || profile?.Id == null)
            {
                profile = profile ?? new Profile();
                profile.Id = Guid.NewGuid().ToString();
                Publish(Messages.REQUEST_SAVE_PROFILE, profile);
            }

            return profile;
        }
    }
}