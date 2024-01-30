using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using VehicleTracking.Core.Web.API.Models;

namespace VehicleTracking.Service.Implementation
{
    public class ApiService : IDisposable
    {
        private readonly string _apiBaseUrl;
        private readonly RestClient _restClient;
        public ApiService(string apiBaseUrl)
        {
            _apiBaseUrl = apiBaseUrl;
            _restClient = new RestClient(_apiBaseUrl);
        }
        public string SendPostRequest(string resource, Vehicle vehicle)
        {
            try
            {
                // POST isteği oluştur
                var request = new RestRequest(resource, Method.Post);

                // JSON formatında gövdeyi ekle
                request.AddJsonBody(vehicle);

                // Content-Type başlığını ekleyin
                request.AddHeader("Content-Type", "application/json");

                // İsteği gönder ve yanıtı al
                RestResponse response = _restClient.Execute(request);

                // Yanıtı kontrol et
                if (response.IsSuccessful)
                {
                    return response.Content ?? "İşlem başarılı ama geri dönen veri yok. StatusCode: " + response.StatusCode;
                }
                else
                {
                    return "Hata Kodu: " + response.StatusCode + "Hata Mesajı: " + response.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                return "Hata: " + ex.Message;
            }
        }

            public string GetDataFromApi(string endPoint)
            {
                var request = new RestRequest(endPoint, Method.Get);

                try
                {
                    RestResponse response = _restClient.Execute(request);

                    if (response.IsSuccessful)
                    {
                        return response.Content ?? "İşlem başarılı ama geri dönen veri yok. StatusCode: " + response.StatusCode;
                    }
                    else
                    {
                        return "Hata Kodu: " + response.StatusCode + "Hata Mesajı: " + response.ErrorMessage;

                    }
                }
                catch (Exception ex)
                {
                    return "Hata: " + ex.Message;
                }
            }

        public string SendPutRequest(string resource, Vehicle vehicle)
        {
            try
            {
                var request = new RestRequest(resource, Method.Put);

                request.AddJsonBody(vehicle);

                var response = _restClient.Execute(request);

                if (response.IsSuccessful)
                {
                    // Başarılı işlem
                    return response.Content ?? "İşlem başarılı ama geri dönen veri yok. StatusCode: " + response.StatusCode;
                }
                else
                {
                    // Hata durumu
                    return "Hata Kodu: " + response.StatusCode + "Hata Mesajı: " + response.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                return "Hata: " + ex.Message;
            }
        }
            public void Dispose()
        {
            _restClient?.Dispose();
        }
    }
}
