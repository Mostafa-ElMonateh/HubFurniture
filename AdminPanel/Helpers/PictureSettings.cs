using System.Net;

namespace AdminPanel.Helpers
{
    public class PictureSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            // 1. Get Folder Path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", folderName);

            // 2. Set FileName UNIQUE
            var fileName = Guid.NewGuid() + file.FileName;
            
            // 3. Get FilePath
            var filePath = Path.Combine(folderPath, fileName);

            // 4. Save File as Streams
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                // 5. Copy File into Streams
                file.CopyTo(fileStream);
            }

            // 6. Return FileName
            return Path.Combine("images\\categoryProducts", fileName);
        }

        public static void DeleteFile(string folderName, string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
