using OneSignal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneSignal.Repositories
{
    public interface IAppRepository
    {
        bool CreateApp(App app);
        bool UpdateApp(App app);
        AppViewModel ViewAllApps();
    }
}