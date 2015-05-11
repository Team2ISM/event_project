using System.Globalization;
using System.Threading;

namespace team2project
{
    public class MyCultureConfig
    {
        public static void SetCulture (string lang)
        {
            CultureInfo cultureInfo = new CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
        }
    }
}