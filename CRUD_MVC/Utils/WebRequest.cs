// ============================================================================
//    Author: Kenneth Perkins
//    Date:   May 11, 2021
//    Taken From: http://programmingnotes.org/
//    File:  Utils.cs
//    Description: Handles general utility functions
// ============================================================================
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Utils {
    public static class WebRequest {
        /// <summary>
        /// Executes a GET request on the given url
        /// </summary>
        /// <param name="url">The url to navigate to</param>
        /// <param name="options">The options for the web request</param>
        /// <returns>The result of the given request</returns>
        public static Response Get(string url, Options options = null) {
            return GetAsync(url, options: options).Result;
        }

        /// <summary>
        /// Executes a GET request on the given url as an asynchronous operation
        /// </summary>
        /// <param name="url">The url to navigate to</param>
        /// <param name="options">The options for the web request</param>
        /// <returns>The result of the given request</returns>
        public static Task<Response> GetAsync(string url, Options options = null) {
            return ExecuteAsync(Method.Get, url, payload: (byte[])null, options: options);
        }

        /// <summary>
        /// Executes a POST request on the given url
        /// </summary>
        /// <param name="url">The url to navigate to</param>
        /// <param name="payload">The data to post to the specified resource</param>
        /// <param name="options">The options for the web request</param>
        /// <returns>The result of the given request</returns>
        public static Response Post(string url, string payload, Options options = null) {
            return Post(url, payload: payload.GetBytes(), options: options);
        }

        /// <summary>
        /// Executes a POST request on the given url
        /// </summary>
        /// <param name="url">The url to navigate to</param>
        /// <param name="payload">The data to post to the specified resource</param>
        /// <param name="options">The options for the web request</param>
        /// <returns>The result of the given request</returns>
        public static Response Post(string url, byte[] payload, Options options = null) {
            return PostAsync(url, payload: payload, options: options).Result;
        }

        /// <summary>
        /// Executes a POST request on the given url as an asynchronous operation
        /// </summary>
        /// <param name="url">The url to navigate to</param>
        /// <param name="payload">The data to post to the specified resource</param>
        /// <param name="options">The options for the web request</param>
        /// <returns>The result of the given request</returns>
        public static Task<Response> PostAsync(string url, string payload, Options options = null) {
            return PostAsync(url, payload: payload.GetBytes(), options: options);
        }

        /// <summary>
        /// Executes a POST request on the given url as an asynchronous operation
        /// </summary>
        /// <param name="url">The url to navigate to</param>
        /// <param name="payload">The data to post to the specified resource</param>
        /// <param name="options">The options for the web request</param>
        /// <returns>The result of the given request</returns>
        public static Task<Response> PostAsync(string url, byte[] payload, Options options = null) {
            return ExecuteAsync(Method.Post, url, payload: payload, options: options);
        }

        /// <summary>
        /// Executes a PUT request on the given url
        /// </summary>
        /// <param name="url">The url to navigate to</param>
        /// <param name="payload">The data to put to the specified resource</param>
        /// <param name="options">The options for the web request</param>
        /// <returns>The result of the given request</returns>
        public static Response Put(string url, string payload, Options options = null) {
            return Put(url, payload: payload.GetBytes(), options: options);
        }

        /// <summary>
        /// Executes a PUT request on the given url
        /// </summary>
        /// <param name="url">The url to navigate to</param>
        /// <param name="payload">The data to put to the specified resource</param>
        /// <param name="options">The options for the web request</param>
        /// <returns>The result of the given request</returns>
        public static Response Put(string url, byte[] payload, Options options = null) {
            return PutAsync(url, payload: payload, options: options).Result;
        }

        /// <summary>
        /// Executes a PUT request on the given url as an asynchronous operation
        /// </summary>
        /// <param name="url">The url to navigate to</param>
        /// <param name="payload">The data to put to the specified resource</param>
        /// <param name="options">The options for the web request</param>
        /// <returns>The result of the given request</returns>
        public static Task<Response> PutAsync(string url, string payload, Options options = null) {
            return PutAsync(url, payload: payload.GetBytes(), options: options);
        }

        /// <summary>
        /// Executes a PUT request on the given url as an asynchronous operation
        /// </summary>
        /// <param name="url">The url to navigate to</param>
        /// <param name="payload">The data to put to the specified resource</param>
        /// <param name="options">The options for the web request</param>
        /// <returns>The result of the given request</returns>
        public static Task<Response> PutAsync(string url, byte[] payload, Options options = null) {
            return ExecuteAsync(Method.Put, url, payload: payload, options: options);
        }

        /// <summary>
        /// Executes a PATCH request on the given url
        /// </summary>
        /// <param name="url">The url to navigate to</param>
        /// <param name="payload">The data to patch to the specified resource</param>
        /// <param name="options">The options for the web request</param>
        /// <returns>The result of the given request</returns>
        public static Response Patch(string url, string payload, Options options = null) {
            return Patch(url, payload: payload.GetBytes(), options: options);
        }

        /// <summary>
        /// Executes a PATCH request on the given url
        /// </summary>
        /// <param name="url">The url to navigate to</param>
        /// <param name="payload">The data to patch to the specified resource</param>
        /// <param name="options">The options for the web request</param>
        /// <returns>The result of the given request</returns>
        public static Response Patch(string url, byte[] payload, Options options = null) {
            return PatchAsync(url, payload: payload, options: options).Result;
        }

        /// <summary>
        /// Executes a PATCH request on the given url as an asynchronous operation
        /// </summary>
        /// <param name="url">The url to navigate to</param>
        /// <param name="payload">The data to patch to the specified resource</param>
        /// <param name="options">The options for the web request</param>
        /// <returns>The result of the given request</returns>
        public static Task<Response> PatchAsync(string url, string payload, Options options = null) {
            return PatchAsync(url, payload: payload.GetBytes(), options: options);
        }

        /// <summary>
        /// Executes a PATCH request on the given url as an asynchronous operation
        /// </summary>
        /// <param name="url">The url to navigate to</param>
        /// <param name="payload">The data to patch to the specified resource</param>
        /// <param name="options">The options for the web request</param>
        /// <returns>The result of the given request</returns>
        public static Task<Response> PatchAsync(string url, byte[] payload, Options options = null) {
            return ExecuteAsync(Method.Patch, url, payload: payload, options: options);
        }

        /// <summary>
        /// Executes a DELETE request on the given url
        /// </summary>
        /// <param name="url">The url to navigate to</param>
        /// <param name="options">The options for the web request</param>
        /// <returns>The result of the given request</returns>
        public static Response Delete(string url, Options options = null) {
            return DeleteAsync(url, options: options).Result;
        }

        /// <summary>
        /// Executes a DELETE request on the given url as an asynchronous operation
        /// </summary>
        /// <param name="url">The url to navigate to</param>
        /// <param name="options">The options for the web request</param>
        /// <returns>The result of the given request</returns>
        public static Task<Response> DeleteAsync(string url, Options options = null) {
            return ExecuteAsync(Method.Delete, url, payload: (byte[])null, options: options);
        }

        /// <summary>
        /// Executes a request method on the given url as an asynchronous operation
        /// </summary>
        /// <param name="type">The type of request method to execute</param>
        /// <param name="url">The url to navigate to</param>
        /// <param name="payload">The data to send to the specified resource</param>
        /// <param name="options">The options for the web request</param>
        /// <returns>The result of the given request</returns>
        public static Task<Response> ExecuteAsync(Method type , string url
                                                    , string payload = null, Options options = null) {
            return ExecuteAsync(type, url, payload: (byte[])payload?.GetBytes(), options: options);
        }

        /// <summary>
        /// Executes a request method on the given url as an asynchronous operation
        /// </summary>
        /// <param name="type">The type of request method to execute</param>
        /// <param name="url">The url to navigate to</param>
        /// <param name="payload">The data to send to the specified resource</param>
        /// <param name="options">The options for the web request</param>
        /// <returns>The result of the given request</returns>
        public async static Task<Response> ExecuteAsync(Method type, string url
                                                    , byte[] payload = null, Options options = null) {
            var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            if (options != null) {
                request.CopyProperties(options);
            }
            request.Method = type.ToString().ToUpper();

            if (payload != null) {
                request.ContentLength = payload.Length;
                using (var requestStream = request.GetRequestStream()) {
                    using (var payloadStream = new System.IO.MemoryStream(payload)) {
                        payloadStream.CopyTo(requestStream);
                    }
                }
            }

            System.Net.HttpWebResponse webResponse = null;
            try {
                webResponse = (System.Net.HttpWebResponse)await request.GetResponseAsync();
            } catch (System.Net.WebException ex) {
                if (ex.Response == null) {
                    throw;
                }
                webResponse = (System.Net.HttpWebResponse)ex.Response;
            } catch {
                throw;
            }

            var result = new Response() {
                Result = webResponse,
                Bytes = GetBytes(webResponse)
            };

            return result;
        }

        private static byte[] GetBytes(System.Net.HttpWebResponse response) {
            byte[] bytes;
            var responseStream = response.GetResponseStream();
            using (var memoryStream = new System.IO.MemoryStream()) {
                responseStream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }
            return bytes;
        }

        public static byte[] GetBytes(this string str, System.Text.Encoding encode = null) {
            if (encode == null) { 
                encode = GetDefaultEncoding();
            }
            return encode.GetBytes(str);
        }

        public static string GetString(this byte[] bytes, System.Text.Encoding encode = null) {
            if (encode == null) { 
                encode = GetDefaultEncoding();
            }
            return encode.GetString(bytes);
        }

        private static System.Text.Encoding GetDefaultEncoding() {
            var encode = new System.Text.UTF8Encoding();
            return encode;
        }

        private static void CopyProperties<T1, T2>(this T1 dest, T2 src) {
            var srcProps = src.GetType().GetProperties();
            var destProps = dest.GetType().GetProperties();
            foreach (var srcProp in srcProps) {
                if (srcProp.CanRead) {
                    var destProp = destProps.FirstOrDefault(x => x.Name == srcProp.Name);
                    if (destProp != null && destProp.CanWrite) {
                        var value = srcProp.GetValue(src, srcProp.GetIndexParameters().Count() == 1 ? new object[] { null } : null);
                        destProp.SetValue(dest, value);
                    }
                }
            }
        }

        /// <summary>
        /// The response result of a <see cref="System.Net.HttpWebRequest"/> 
        /// </summary>
        public class Response {
            public System.Net.HttpWebResponse Result { get; set; } = null;
            public byte[] Bytes { get; set; } = null;
            public string Body {
                get {
                    if (_body == null && Bytes != null) { 
                        _body = Bytes.GetString();
                    }
                    return _body;
                }
            }
            private string _body = null;
        }

        /// <summary>
        /// Options for the given <see cref="System.Net.HttpWebRequest"/> 
        /// </summary>
        public class Options {
            public System.Net.WebHeaderCollection Headers { get; set; } = new System.Net.WebHeaderCollection();
            public System.Net.ICredentials Credentials { get; set; } = null;
            public string Connection { get; set; } = null;
            public bool KeepAlive { get; set; } = true;
            public string Expect { get; set; } = null;
            public DateTime IfModifiedSince { get; set; }
            public string TransferEncoding { get; set; }
            public string Accept { get; set; } = null;
            public bool AllowAutoRedirect { get; set; } = true;
            public bool AllowReadStreamBuffering { get; set; } = false;
            public bool AllowWriteStreamBuffering { get; set; } = true;
            public int MaximumAutomaticRedirections { get; set; } = 50;
            public string MediaType { get; set; } = null;
            public bool Pipelined { get; set; } = true;
            public bool PreAuthenticate { get; set; } = false;
            public string Referer { get; set; } = null;
            public bool SendChunked { get; set; } = false;
            public bool UseDefaultCredentials { get; set; } = false;
            public string UserAgent { get; set; } = null;
            public string ContentType { get; set; } = null;
        }

        public abstract class ContentType {
            public const string ApplicationUrlEncoded = "application/x-www-form-urlencoded";
            public const string ApplicationJson = "application/json";
            public const string TextXml = "text/xml";
        }

        public enum Method {
            Get,
            Post,
            Put,
            Patch,
            Delete
        }
    }
}// http://programmingnotes.org/