using System.Text.Json;
using System.Text.Json.Serialization;
using MongoDB.Bson.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RateMyManagement.Data;
using RateMyManagement.IServices;
using ZstdSharp.Unsafe;

namespace RateMyManagement.Services
{
    public class ImgbbService : IImgbbService
    {
        private IWebDriver webDriver;
        private readonly string _clientKey;
        private static readonly HttpClient _httpClient = new HttpClient();

        public ImgbbService()
        {
            _clientKey = "9efe730b72a119a8eca807331cc4bc0b";
        }

        public async Task<ImgbbUploadResponse> UploadImageAsync(byte[] imageArray)
        {
            try
            {
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                var parameters = new Dictionary<string, string>()
                {
                    {"key", _clientKey},
                    {"image", base64ImageRepresentation},
                    {"name", "test"}
                };
                var req = new HttpRequestMessage(HttpMethod.Post, "https://api.imgbb.com/1/upload");
                req.Content = new FormUrlEncodedContent(parameters.ToArray());
                var response = await _httpClient.SendAsync(req);
                var obj = JsonSerializer.Deserialize<ImgbbUploadResponse>(await response.Content.ReadAsStringAsync());
                return obj;
            }
            catch (Exception e)
            {
                throw new NotImplementedException();
            }
        }

        public async Task DeleteImageAsync(string deleteUrl)
        {
            webDriver = new ChromeDriver();
            webDriver.Url = deleteUrl;
            try
            {
                IWebElement deleteButton = webDriver.FindElement(By.ClassName("link--delete"));
                deleteButton.Click();
                await Task.Delay(5);
                IWebElement confirmDelete = webDriver.FindElement(By.ClassName("btn-container"))
                    .FindElement(By.ClassName("btn-input"));
                confirmDelete.Click();
            }
            catch (Exception e)
            {
                throw new Exception();
            }
            finally
            {
                webDriver.Quit();
                webDriver.Dispose();
            }
        }
    }
}
