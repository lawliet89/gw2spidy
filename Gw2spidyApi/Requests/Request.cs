using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Gw2spidyApi.Extensions;
using Gw2spidyApi.Network;
using Gw2spidyApi.Objects.Wrapper;
using Gw2spidyApi.Objects.Converter;
using TaskExtensions = Gw2spidyApi.Extensions.TaskExtensions;

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
        public TWrapper Wrapper { get; protected set; }

        public IEnumerable<TObject> Result
        {
            get { return Wrapper.Unwrap(); }
        }

        protected IHttpRequest HttpRequest;
        protected JavaScriptSerializer JavaScriptSerializer;

        public static List<CacheObject<TWrapper>> Cache = new List<CacheObject<TWrapper>>();

        protected Uri ApiURl
        {
            get { return Settings.BaseUrl; }
        }

        protected UriBuilder ApiUrlBuilder
        {
            get { return Settings.BaseUrlBuilder; }
        }

        protected Request() : this(new HttpRequest())
        {
            
        } 

        protected Request(IHttpRequest httpRequest)
        {
            HttpRequest = httpRequest;
            JavaScriptSerializer = new JavaScriptSerializer();
            JavaScriptSerializer.RegisterConverters(new ObjectConverter().Yield());
        }

        private Task<string> GetJson()
        {
            return HttpRequest.MakeJsonRequest(RequestUri);
        }

        /// <summary>
        /// Use to send request. Override to provide other ways to retrieve data
        /// </summary>
        /// <returns></returns>
        public virtual Task Send(bool refresh = false)
        {
            var cache = FindInCache();
            if (cache == null)
            {
                return GetJson().Then(task =>
                {
                    if (task.IsCompleted)
                    {
                        Wrapper = JavaScriptSerializer.Deserialize<TWrapper>(task.Result);
                        CacheWrapper(Wrapper);
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
            Wrapper = cache.Result;
            return TaskExtensions.EmptyTask();
        }

        /// <summary>
        /// Shortcut method to send request and wait synchronously 
        /// </summary>
        /// <returns>The object requested, in an IEnumerable</returns>
        public IEnumerable<TObject> Get(bool refresh = false)
        {
            try
            {
                Send(refresh).Wait();
                return Result;;
            }
            catch (AggregateException e)
            {
                throw e.InnerException;
            }
        }

        protected IEnumerable<PropertyInfo> CacheParameters
        {
            get
            {
                return GetType().GetProperties()
                    .Where(p => p.GetCustomAttributes(typeof(CachePrameterAttribute), true).Length > 0);
            }
        }

        public virtual CacheObject<TWrapper> FindInCache()
        {
            return !CacheParameters.Any() ? null : FindInCache(GetCacheProperties());
        }

        public virtual CacheObject<TWrapper> FindInCache(IDictionary<string, object> parameters)
        {
            return Cache.SingleOrDefault(c => c.Parameters.SequenceEqual(parameters));
        } 

        protected virtual void CacheWrapper(TWrapper wrapper)
        {
            if (!CacheParameters.Any()) return;
            Cache.Add(MakeCacheWrapper(wrapper));
        }

        protected CacheObject<TWrapper> MakeCacheWrapper(TWrapper wrapper)
        {
            return new CacheObject<TWrapper>()
            {
                Result = wrapper,
                Parameters = GetCacheProperties()
            };
        }

        protected IDictionary<string, object> GetCacheProperties()
        {
            return CacheParameters
                .ToDictionary(property => property.Name, property => property.GetValue(this));
        } 
    }

    [AttributeUsage(AttributeTargets.Property)]
    class CachePrameterAttribute : Attribute
    {
        
    }

    public class CacheObject<TWrapper>
    {
        public TWrapper Result;
        public IDictionary<string, object> Parameters;
        public DateTime Timestamp { private set; get; }

        public CacheObject()
        {
            Timestamp = DateTime.Now;
        } 
    }
}
