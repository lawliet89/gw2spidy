using System;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Gw2spidyApi.Extensions;
using Gw2spidyApi.Network;
using Gw2spidyApi.Objects.Wrapper;

namespace Gw2spidyApi.Requests
{
    /// <summary>
    /// An abstract class that represents a type of API request
    /// </summary>
    /// <typeparam name="TObject">The object to serialise the results to</typeparam>
    public abstract class Request<TObject, TWrapper>
        where TWrapper : IWrapper<TObject>
    {
        public abstract Uri RequestUri { get; }
        public TObject Result { get; protected set; }
        
        protected IHttpRequest HttpRequest;
        protected JavaScriptSerializer JavaScriptSerializer;

        protected Uri ApiURl
        {
            get { return Settings.BaseUrl; }
        }

        protected UriBuilder ApiUrlBuilder
        {
            get { return Settings.BaseUrlBuilder; }
        }

        protected Request(IHttpRequest httpRequest)
        {
            HttpRequest = httpRequest;
            JavaScriptSerializer = new JavaScriptSerializer();
        }

        private Task<string> GetJson()
        {
            return HttpRequest.MakeJsonRequest(RequestUri);
        }

        public virtual Task Send()
        {
            return GetJson().Then(task =>
                {
                    if (task.IsCompleted)
                    {
                        var wrapped = JavaScriptSerializer.Deserialize<TWrapper> (task.Result);
                        Result = wrapped.Unwrap();
                    }
                    else if (task.IsCanceled)
                    {
                        throw new InvalidOperationException("API request was cancelled");
                    }
                    else if (task.IsFaulted)
                    {
                        throw task.Exception.InnerException;
                    }
                });
        }

        public TObject Get()
        {
            try
            {
                Send().Wait();
                return Result;;
            }
            catch (AggregateException e)
            {
                throw e.InnerException;
            }
        }
    }
}
