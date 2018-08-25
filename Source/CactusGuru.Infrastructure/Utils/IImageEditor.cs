namespace CactusGuru.Infrastructure.Utils
{
    public interface IImageEditor
    {
        byte[] CreateThumbnail(string filePath);
    }
}
