namespace BookStore.PL.Helper
{
    public static class DocumentSetting
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            //Get Located Folder Path
            //var folderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/Files", folderName);

            ////get file name and make unique
            //var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(file.FileName)}";

            ////get file path
            //var filePath = Path.Combine (folderPath, fileName);

            //using var fileStream = new FileStream(filePath, FileMode.Create);

            //file.CopyTo(fileStream);

            //return fileName;

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", folderName);

            // Ensure the folder exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Create a unique filename
            var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(file.FileName)}";

            // Get the full file path
            var filePath = Path.Combine(folderPath, fileName);

            // Save the file to disk
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            // Return the relative path to the file
            return fileName;
        }
    }
}
