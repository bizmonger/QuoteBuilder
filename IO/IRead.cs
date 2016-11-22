namespace IO
{
    public interface IRead
    {
        string Read(string file);
        string GetImagebase64(string filename);
        string CreateFile(string content, string filePath);
    }
}