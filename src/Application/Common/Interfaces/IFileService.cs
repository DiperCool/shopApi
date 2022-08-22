using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IFileService
{
    Photo SaveFile(FileModel model);
    void DeleteFile(string path);
    string GetWebRootPath();
}
