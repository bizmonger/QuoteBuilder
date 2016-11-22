namespace Repositories.Core
{
    public abstract class AbstractPromise
    {
        protected abstract void MakePromises();
        protected abstract void BreakPromises();
    }
}