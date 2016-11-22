using System;
using System.IO;
using IO;
using Xamarin.Forms;
using QuoteBuilder.Droid;

[assembly: Dependency(typeof(FileServer_Android))]
namespace QuoteBuilder.Droid
{
    public class FileServer_Android : IRead
    {
        public string CreateFile(string content, string filePath) => content;

        public string Read(string filename)
        {
            var stream = GetStream(filename);

            using (var reader = new System.IO.StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
        
        public string GetImagebase64(string filename)
        {
            var stream = GetStream(filename);

            using (var reader = new StreamReader(stream))
            {
                var imageData = new byte[stream.Length];
                stream.Read(imageData, 0, (int)stream.Length);
                return Convert.ToBase64String(imageData);
            }
        }

        Stream GetStream(string filename)
        {
            var assembly = typeof(QuoteBuilder.App).Assembly;
            var assemblyName = typeof(QuoteBuilder.App).Assembly.GetName().Name;
            var stream = assembly.GetManifestResourceStream($"{assemblyName}.Templates.{filename}");
            return stream;
        }
    }
}