using System;
using Entities;
using Entities.Utilities;
using Mediation;
using static Bizmonger.Patterns.MessageBus;

namespace Repositories.Core
{
    public abstract class AbstractProfileDatabase : IDatabase
    {
        public void Read(string id)
        {
            var existingProfile = ExecuteReadFromProfileId();
            Publish(Messages.REQUEST_PROFILE_RESPONSE, existingProfile);
        }

        public void Read()
        {
            var existingProfile = ExecuteReadFromProfileId();

            if (existingProfile != null)
            {
                Publish(Messages.REQUEST_PROFILE_RESPONSE, existingProfile);
            }
        }

        public abstract void Initialize();

        public void OnSave(object entity)
        {
            var profile = entity as Profile;
            var existingProfile = ExecuteReadFromProfileId();

            if (existingProfile != null)
            {
                profile.Update(existingProfile);
                Update(existingProfile);
            }
            else
            {
                profile.Id = profile.Id ?? Guid.NewGuid().ToString();
                Add(profile);
            }
        }

        protected abstract Profile ExecuteReadFromProfileId();

        protected abstract void Update(Profile profile);

        protected abstract void Add(Profile profile);
    }
}