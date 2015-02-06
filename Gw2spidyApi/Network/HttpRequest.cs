using System;
using System.Net;
using System.Threading.Tasks;

namespace Gw2spidyApi.Network
{
    // TODO: perhaps cache requests
    class HttpRequest : IHttpRequest
    {
        private static WebClient GetWebClient()
        {
            var client = new WebClient();
            if (client.Proxy != null)
            {
                client.UseDefaultCredentials = true;
                client.Proxy.Credentials = CredentialCache.DefaultCredentials;
            }

            return client;
        }

        // TODO: Make this an async method
        public Task<string> MakeJsonRequest(Uri uri)
        {
            return Task.Factory.StartNew(() =>
            {
                var webClient = GetWebClient();
                var taskCompletionSource = new TaskCompletionSource<string>();
                webClient.DownloadStringCompleted += (sender, args) =>
                {
                    if (args.Error != null)
                    {
                        taskCompletionSource.SetException(args.Error);
                    }
                    else if (args.Cancelled)
                    {
                        taskCompletionSource.SetCanceled();
                    }
                    else
                    {
                        taskCompletionSource.SetResult(args.Result);
                    }
                };

                webClient.DownloadStringAsync(uri);
                return taskCompletionSource.Task;
            }).Unwrap();
        }
    }
}
