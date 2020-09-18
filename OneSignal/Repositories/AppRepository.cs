using Newtonsoft.Json;
using OneSignal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace OneSignal.Repositories
{
    public class AppRepository : IAppRepository
    {
        private readonly string OneSignalAPIEndpoint = ConfigurationManager.AppSettings["OneSignalAPIEndpoint"];
        private readonly string OneSignalAPIKey = ConfigurationManager.AppSettings["OneSignalAPIKey"];
        public bool CreateApp(App app)
        {
            try
            {
                bool responseResult = false;
                using (var client = new HttpClient())
                {
                    #region Prepare Body
                    var bodyData = JsonConvert.SerializeObject(app);
                    HttpContent _Body = new StringContent(bodyData);
                    #endregion

                    #region Request
                    client.BaseAddress = new Uri(OneSignalAPIEndpoint);
                    client.DefaultRequestHeaders.Add("Authorization", OneSignalAPIKey);
                    _Body.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    #endregion

                    #region Handle response
                    var response = client.PostAsync(OneSignalAPIEndpoint, _Body).Result;
                    responseResult = response.IsSuccessStatusCode;
                    #endregion
                }

                return responseResult;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateApp(App app)
        {
            try
            {
                bool responseResult = false;
                using (var client = new HttpClient())
                {
                    #region Prepare Body
                    var bodyData = JsonConvert.SerializeObject(app);
                    HttpContent _Body = new StringContent(bodyData);
                    #endregion

                    #region Request
                    client.BaseAddress = new Uri(OneSignalAPIEndpoint + "/" + app.id);
                    client.DefaultRequestHeaders.Add("Authorization", OneSignalAPIKey);
                    _Body.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    #endregion

                    #region Handle response
                    var response = client.PutAsync(OneSignalAPIEndpoint + "/" + app.id, _Body).Result;
                    responseResult = response.IsSuccessStatusCode;
                    #endregion
                }
                return responseResult;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public AppViewModel ViewAllApps()
        {
            try
            {
                AppViewModel appViewModel = new AppViewModel();
                appViewModel = GetAllAppsAsync();
                return appViewModel;
            }
            catch (Exception)
            {
                AppViewModel appViewModel = new AppViewModel();
                return appViewModel;
            }
        }

        private AppViewModel GetAllAppsAsync()
        {
            try
            {
                AppViewModel appViewModel = new AppViewModel();
                List<App> listOfApps = new List<App>();
                using (var client = new HttpClient())
                {
                    #region Request
                    client.BaseAddress = new Uri(OneSignalAPIEndpoint);
                    client.DefaultRequestHeaders.Add("Authorization", OneSignalAPIKey);
                    #endregion

                    #region Handle response
                    var response = client.GetAsync(OneSignalAPIEndpoint).Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    listOfApps = JsonConvert.DeserializeObject<List<App>>(jsonString.Result);
                    #endregion
                }
                appViewModel.Apps = listOfApps;
                return appViewModel;
            }
            catch (Exception)
            {
                AppViewModel appViewModel = new AppViewModel();
                return appViewModel;
            }
        }
    }
}