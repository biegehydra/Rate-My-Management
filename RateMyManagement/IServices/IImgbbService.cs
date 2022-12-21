using RateMyManagement.Data;

namespace RateMyManagement.IServices
{
    public interface IImgbbService
    {
        public Task<ImgbbUploadResponse> UploadImageAsync(byte[] image);
        public Task DeleteImageAsync(string deleteUrl);
    }
}
