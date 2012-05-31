using System;
using System.Net;

namespace Bitdiff.Utils.Utils
{
    public class UriHttpResponseSummary
    {
        public UriHttpResponseSummary(string dataReceived, HttpWebResponse response, WebException exception)
        {
            Data = dataReceived;
            HttpWebResponse = response;
            WebException = exception;
        }

        public string Data { get; private set; }

        public HttpWebResponse HttpWebResponse { get; private set; }

        public WebException WebException { get; private set; }

        public string GetDataChecked()
        {
            if (WebException != null)
            {
                throw new Exception(Data, WebException);
            }

            return Data;
        }
    }
}