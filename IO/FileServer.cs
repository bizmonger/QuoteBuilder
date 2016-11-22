using Xamarin.Forms;

namespace IO
{
    public class FileServer : IRead
    {
        readonly IRead _reader = DependencyService.Get<IRead>();

        public string CreateFile(string content, string destinationFile) => content;

        public string Read(string file) => _reader.Read(file);

        public string GetImagebase64(string file) => _reader.GetImagebase64(file);
    }
}