using System.Collections.Generic;
using System.Linq;
using Entities;
using Repositories.Core;
using Entities.Utilities;
using System.Diagnostics;
using static Bizmonger.Patterns.MessageBus;
using Mediation;

namespace TestAPI
{
    //[DebuggerNonUserCode]
    public class MockProfileDatabase : AbstractProfileDatabase
    {
        readonly List<Profile> _profiles = new List<Profile>();

        public override void Initialize()
        {
            Subscribe(Messages.REQUEST_PROFILE_RESPONSE, obj =>
            {
                if (!_profiles.Any())
                {
                    Add(obj as Profile);
                }
            });
        }

        protected override Profile ExecuteReadFromProfileId() => _profiles.FirstOrDefault();

        protected override void Add(Profile profile) => _profiles.Add(profile);

        protected override void Update(Profile profile)
        {
            var existingProfile = _profiles.FirstOrDefault(p => p.Id == profile.Id);
            profile.Update(existingProfile);
        }
    }
}