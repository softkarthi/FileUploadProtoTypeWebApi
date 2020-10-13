using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FileUploadProtoTypeWebApi.Service
{
    public interface IFileService
    {
        List<FileUploadProtoTypeWebApi.Model.File> GetFiles();
        Task<bool> WriteFile(IFormFile formFile);
    }

    public class FileService : IFileService
    {
        private readonly ILogger<FileService> _logger;
        private const string UploadPath = "Upload\\files";
        private static List<FileUploadProtoTypeWebApi.Model.File> _uploadedFiles = new List<FileUploadProtoTypeWebApi.Model.File>();

        public FileService(ILogger<FileService> logger)
        {
            _logger = logger;
        }

        public async Task<bool> WriteFile(IFormFile formFile)
        {
            bool writeFileSuccess = true;
            bool writeFileFailed = false;

            if (formFile == null || formFile.Length == 0) return writeFileFailed;

            string acutalFileName = formFile.FileName;
            string fileFullPath = ConstructFileName(acutalFileName);
            string savedFileName = Path.GetFileName(fileFullPath);
            
            FileStream fileStream = null;

            try
            {
                CheckDirectoryExits(fileFullPath);

                fileStream = new FileStream(fileFullPath, FileMode.Create);
                await formFile.CopyToAsync(fileStream);
                AddFile(acutalFileName, savedFileName);
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to save", e);
                return writeFileFailed;
            }
            finally
            {
                FileCloseAndDispose(fileStream);
            }

            return writeFileSuccess;
        }

        private static void FileCloseAndDispose(FileStream fileStream)
        {
            if (fileStream != null)
            {
                fileStream.Close();
                fileStream.Dispose();
            }
        }

        private static void CheckDirectoryExits(string fileFullPath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(fileFullPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileFullPath));
            }
        }

        private static string ConstructFileName(string fileName)
        {
            string currentFileExtension = Path.GetExtension(fileName);
            string currentFileName = Path.GetFileNameWithoutExtension(fileName);
            string currentDirectory = Path.Combine(Directory.GetCurrentDirectory(), UploadPath);
            return $"{currentDirectory}{currentFileName}_{DateTime.Now:yyyy-MM-dd_hh-mm-ss-tt}{currentFileExtension}";
        }

        private void AddFile(string actualFileName, string newFileName)
        {
            var file = new FileUploadProtoTypeWebApi.Model.File()
            {
                CreatedDate = DateTime.Now,
                Name = actualFileName,
                StoredName = newFileName,
                Size = 0
            };
            _uploadedFiles.Add(file);
        }

        public List<FileUploadProtoTypeWebApi.Model.File> GetFiles()
        {
            return _uploadedFiles;
        }
    }
}