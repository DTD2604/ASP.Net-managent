/*using System;
using System.IO;

namespace StudentManager.Config
{
    public static class FileUploader
    {
        
        public static string UploadFile(string file, string pathFile, string[] allowedTypes = null, int maxSize = 0)
        {
            if (file == null || string.IsNullOrEmpty(pathFile))
            {
                return null;
            }

            string fileName = file.FileName;
            string fileType = file.ContentType;
            int fileSize = file.ContentLength;
            Stream fileStream = file.InputStream;

            if (allowedTypes != null && !allowedTypes.Contains(fileType, StringComparer.OrdinalIgnoreCase))
            {
                return null;
            }

            if (maxSize > 0 && fileSize > maxSize)
            {
                return null;
            }

            try
            {
                string filePath = Path.Combine(pathFile, fileName);
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    fileStream.CopyTo(fs);
                }
                return fileName;
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine("Error uploading file: " + ex.Message);
                return null;
            }
        }
    }
}
*/