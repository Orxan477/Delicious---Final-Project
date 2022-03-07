using System.IO;

namespace Restaurant.Business.Utilities
{
    public static class Helper
    {
        public static void RemoveFile(string root, string folder, string image)
        {
            string path = Path.Combine(root, folder, image);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
