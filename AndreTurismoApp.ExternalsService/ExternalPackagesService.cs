using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;
using Newtonsoft.Json;

namespace AndreTurismoApp.ExternalsService
{
    public class ExternalPackagesService
    {
        static readonly HttpClient packages = new HttpClient();
        public async Task<List<Package>> GetPackage()
        {
            try
            {
                HttpResponseMessage response = await packages.GetAsync("https://localhost:8084/api/Packages");
                response.EnsureSuccessStatusCode();
                string packagesJson = await response.Content.ReadAsStringAsync();
                var packagesList = JsonConvert.DeserializeObject<List<Package>>(packagesJson);
                return packagesList;
            }
            catch (HttpRequestException e)
            {
                return new List<Package>();
            }
        }
        public async Task<Package> GetPackageById(int id)
        {
            try
            {
                HttpResponseMessage response = await packages.GetAsync("https://localhost:8084/api/Packages/" + id);
                response.EnsureSuccessStatusCode();
                string packagesJson = await response.Content.ReadAsStringAsync();
                var packagesList = JsonConvert.DeserializeObject<Package>(packagesJson);
                return packagesList;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }
        public async Task<HttpStatusCode> PostPackage(Package package)
        {
            HttpResponseMessage response = await packages.PostAsJsonAsync("https://localhost:8084/api/Packages", package);
            return response.StatusCode;
        }
        public async Task<HttpStatusCode> PutPackage(Package package)
        {
            HttpResponseMessage response = await packages.PutAsJsonAsync("https://localhost:8084/api/Packages", package);
            return response.StatusCode;
        }
        public async Task<HttpStatusCode> DeletePackage(int id)
        {
            HttpResponseMessage response = await packages.DeleteAsync("https://localhost:8084/api/Packages/" + id);
            return response.StatusCode;
        }
    }
}
