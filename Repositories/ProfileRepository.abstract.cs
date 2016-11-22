using Entities;

namespace Repositories
{
    public abstract partial class AbstractProfileRepository
    {
        public bool Save(object obj)
        {
            var profile = obj as Profile;
            var isValid = Validate(profile);

            SaveData(profile);

            PublishSaveResult(isValid);
            return isValid;
        }
    }
}