using System;
using System.IO;
using System.Net;
using System.Text;

namespace Bitdiff.Utils
{
    public static class UriUtils
    {
        public static string BzipJsonPost(this Uri url, object data)
        {
            byte[] json = BzipJson.ConvertObjectToBzipJson(data);
            return HttpSendBytes(url, "POST", json, "application/octet-stream", null).GetDataChecked();
        }

        public static string JsonDelete(this Uri url, object data)
        {
            return HttpSend(url, "DELETE", data.ToJson(), "application/json").GetDataChecked();
        }

        public static string JsonPost(this Uri url, object data)
        {
            return HttpSend(url, "POST", data.ToJson(), "application/json").GetDataChecked();
        }

        public static string JsonPut(this Uri url, object data)
        {
            return HttpSend(url, "PUT", data.ToJson(), "application/json").GetDataChecked();
        }

        public static T JsonGet<T>(this Uri url) where T : class
        {
            return HttpSend(url, "GET").GetDataChecked().FromJson<T>();
        }

        public static string Get(this Uri url)
        {
            return HttpSend(url, "GET").GetDataChecked();
        }

        public static string Post(this Uri url, string data)
        {
            return HttpSend(url, "POST", data).GetDataChecked();
        }

        public static string Put(this Uri url, string data)
        {
            return HttpSend(url, "PUT", data).GetDataChecked();
        }

        public static string Delete(this Uri url, string data)
        {
            return HttpSend(url, "DELETE", data).GetDataChecked();
        }

        public static UriHttpResponseSummary HttpSend(this Uri url, string method)
        {
            return HttpSend(url, method, null, null);
        }

        public static UriHttpResponseSummary HttpSend(this Uri url, string method, string data)
        {
            return HttpSend(url, method, data, null);
        }

        public static UriHttpResponseSummary HttpSend(this Uri url, string method, string data, string contentType)
        {
            return HttpSend(url, method, data, contentType, null);
        }

        public static UriHttpResponseSummary HttpSend(this Uri url, string method, string data, string contentType, int? timeout)
        {
            var bytes = data == null ? null : Encoding.UTF8.GetBytes(data);
            return HttpSendBytes(url, method, bytes, contentType, timeout);
        }

        public static UriHttpResponseSummary HttpSendBytes(this Uri url, string method, byte[] data, string contentType, int? timeout)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.Proxy = WebRequest.GetSystemWebProxy();

            if (!string.IsNullOrEmpty(contentType))
                request.ContentType = contentType;

            if (data != null)
            {
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();
            }

            WebException webException;
            var response = GetResponse(request, out webException);

            Encoding encoding = Encoding.UTF8;
            try
            {
                if (response != null && !string.IsNullOrEmpty(response.CharacterSet))
                    encoding = Encoding.GetEncoding(response.CharacterSet);
            }
            catch (ArgumentException e)
            {
                throw new Exception("Cannot determine encoding", e);
            }

            string dataReceived = null;
            if (response != null)
            {
                Stream responseStream = response.GetResponseStream();
                if (responseStream != null)
                    using (var reader = new StreamReader(responseStream, encoding))
                        dataReceived = reader.ReadToEnd();
            }

            return new UriHttpResponseSummary(dataReceived, response, webException);
        }

        private static HttpWebResponse GetResponse(WebRequest request, out WebException webException)
        {
            try
            {
                webException = null;
                return (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                webException = e;
                return (HttpWebResponse)e.Response;
            }
        }
    }
}