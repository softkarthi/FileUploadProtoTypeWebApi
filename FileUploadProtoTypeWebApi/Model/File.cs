using System;

namespace FileUploadProtoTypeWebApi.Model
{
    public class File
    {
        public string Name { get; set; }
        public string StoredName { get; set; }
        public int Size { get; set; }   
        public DateTime CreatedDate { get; set; }
    }
}