using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Hosting;

namespace CleanArchitecture.Infrastructure.Services;

public class FileService : IFileService
{
    IWebHostEnvironment _host;

    public FileService(IWebHostEnvironment host)
    {
        _host = host;
    }

    public void DeleteFile(string path)
    {
        File.Delete(path);
    }

    public string GetWebRootPath() => _host.WebRootPath;

    public Photo SaveFile(FileModel model)
    {
        string guid = Guid.NewGuid().ToString();
        string extensionsFile = Path.GetExtension(model.NameFile);
        string shortPath = Path.Combine(Path.Combine("api", "files"), guid + extensionsFile);
        string fullPath = Path.Combine(GetWebRootPath(), shortPath);
        File.WriteAllBytes(fullPath, model.Bytes);
        return new Photo
        {
            ShortPath = shortPath,
            FullPath = fullPath,
            FileExtension = extensionsFile,
            Length = model.Length
        };
    }
}
