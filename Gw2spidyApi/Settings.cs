using System;
namespace Gw2spidyApi
{
    public class Settings
    {
        private static Properties.Settings DefaultSettings
        {
            get { return Properties.Settings.Default; }
        }

        public static Uri BaseUrl 
        {
            get
            {
                return BaseUrlBuilder.Uri;
            }
        }

        public static UriBuilder BaseUrlBuilder
        {
            get
            {
                return new UriBuilder(DefaultSettings.BaseUrl)
                {
                    Path = String.Format(DefaultSettings.BasePath,
                        DefaultSettings.ApiVersion, DefaultSettings.Format)
                };;
            }
        }
    }
}