namespace AuthServiceIN6BM.Application.Interfaces;

public interface ICloudinatyService
{
    Task<string> UploadImageAsync(IFieldData imageFile, string filename);
    Task<bool> DeleteImageAsync(string publicId);
    string GetDefaultAvatarUrl();
    string GetFullImageUrl(string imagePath);


}