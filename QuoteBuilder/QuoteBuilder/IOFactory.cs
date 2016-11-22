using static Bizmonger.Patterns.MessageBus;
using IO;
using Mediation;

namespace QuoteBuilder
{
    public class IOFactory
    {
        public void PromiseFileReader() =>
            Subscribe(Messages.REQUEST_FILE_READER, obj =>
                Publish(Messages.REQUEST_FILE_READER_RESPONSE, new FileServer()));
    }
}