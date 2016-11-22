using System;

namespace Repositories.Core
{
    public interface IRepository
    {
        bool Save(object entity);
        Object Get(int id);
    }
}