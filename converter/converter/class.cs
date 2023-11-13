public class FileModel
{
    public string Content { get; set; }

    public FileModel() { }

    public FileModel(string content)
    {
        Content = content;
    }
}