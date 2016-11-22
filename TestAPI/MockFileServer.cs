using IO;
using System.Diagnostics;

namespace TestAPI
{
    [DebuggerNonUserCode]
    public class MockFileServer : IRead
    {
        public string CreateFile(string content, string filePath) => "some_text_value";
        
        public string Read(string file) => "some_text_value";

        public string GetImagebase64(string filename) => "some_text_value";
    }
}