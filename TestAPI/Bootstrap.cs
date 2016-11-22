using Bizmonger.Patterns;
using static Bizmonger.Patterns.MessageBus;
using Repositories.Core;
using System.Diagnostics;

namespace TestAPI
{
    [DebuggerNonUserCode]
    public class Bootstrap
    {
        public void Run()
        {
            new Mock().PrepareDatabases();
            new Autonomy().Activate();
        }
    }
}