using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;

namespace Abp.WebApi.Client
{
    /// <summary>
    /// Used to make requests to ABP based Web APIs.
    /// ���ڻ���Web API������ABP
    /// </summary>
    public interface IAbpWebApiClient
    {
        /// <summary>
        /// Base URL for all request. 
        /// ��������Ļ���ַ
        /// </summary>
        string BaseUrl { get; set; }

        /// <summary>
        /// Timeout value for all requests (used if not supplied in the request method).Default: 90 seconds.
        /// ����ĳ�ʱֵ(�����������û���ṩ)Ĭ��ֵ��90��
        /// </summary>
        TimeSpan Timeout { get; set; }

        /// <summary>
        /// Used to set cookies for requests.
        /// ����Ϊ��������Cookies
        /// </summary>
        Collection<Cookie> Cookies { get; }

        /// <summary>
        /// Request headers.
        /// ����ͷ
        /// </summary>
        ICollection<NameValue> RequestHeaders { get; }

        /// <summary>
        /// Response headers.
        /// ��Ӧͷ
        /// </summary>
        ICollection<NameValue> ResponseHeaders { get; }

        /// <summary>
        /// Makes post request that does not get or return value.
        /// ʹPOST������ֱ�ӱ�Get�򷵻�ֵ
        /// </summary>
        /// <param name="url">Url / Url</param>
        /// <param name="timeout">Timeout as milliseconds / ��ʱʱ��(����)</param>
        Task PostAsync(string url, int? timeout = null);

        /// <summary>
        /// Makes post request that gets input but does not return value.
        /// ʹPOST����õ����룬���ǲ�����ֵ
        /// </summary>
        /// <param name="url">Url / Url</param>
        /// <param name="input">Input / ����</param>
        /// <param name="timeout">Timeout as milliseconds / ��ʱʱ��(����)</param>
        Task PostAsync(string url, object input, int? timeout = null);

        /// <summary>
        /// Makes post request that does not get input but returns value.
        /// ʹPOST���󲻵õ����룬���Ƿ���ֵ
        /// </summary>
        /// <param name="url">Url / Url</param>
        /// <param name="timeout">Timeout as milliseconds / ��ʱʱ��(����)</param>
        Task<TResult> PostAsync<TResult>(string url, int? timeout = null) where TResult : class;

        /// <summary>
        /// Makes post request that gets input and returns value.
        /// ʹPOST�����ܻ�ȡ�������Լ�����ֵ
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="input">Input / ����</param>
        /// <param name="timeout">Timeout as milliseconds / ��ʱʱ��(����)</param>
        Task<TResult> PostAsync<TResult>(string url, object input, int? timeout = null) where TResult : class;
    }
}