namespace Traccaradora.Web.Clients
{
    using System = global::System;

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.9.0.0 (NJsonSchema v10.3.0.0 (Newtonsoft.Json v12.0.0.0))")]
    public partial class ReportsClient
    {
        private string _baseUrl = "https://demo.traccar.org/api";
        private System.Net.Http.HttpClient _httpClient;
        private System.Lazy<Newtonsoft.Json.JsonSerializerSettings> _settings;

        public ReportsClient(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient;
            _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(CreateSerializerSettings);
        }

        private Newtonsoft.Json.JsonSerializerSettings CreateSerializerSettings()
        {
            var settings = new Newtonsoft.Json.JsonSerializerSettings();
            UpdateJsonSerializerSettings(settings);
            return settings;
        }

        public string BaseUrl
        {
            get { return _baseUrl; }
            set { _baseUrl = value; }
        }

        protected Newtonsoft.Json.JsonSerializerSettings JsonSerializerSettings { get { return _settings.Value; } }

        partial void UpdateJsonSerializerSettings(Newtonsoft.Json.JsonSerializerSettings settings);
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url);
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder);
        partial void ProcessResponse(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response);

        /// <summary>Fetch a list of Positions within the time period for the Devices or Groups</summary>
        /// <param name="from">in IS0 8601 format. eg. `1963-11-22T18:30:00Z`</param>
        /// <param name="to">in IS0 8601 format. eg. `1963-11-22T18:30:00Z`</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<SwaggerResponse<System.Collections.Generic.ICollection<Position>>> RouteAsync(System.Collections.Generic.IEnumerable<int> deviceId, System.Collections.Generic.IEnumerable<int> groupId, System.DateTimeOffset from, System.DateTimeOffset to)
        {
            return RouteAsync(deviceId, groupId, from, to, System.Threading.CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Fetch a list of Positions within the time period for the Devices or Groups</summary>
        /// <param name="from">in IS0 8601 format. eg. `1963-11-22T18:30:00Z`</param>
        /// <param name="to">in IS0 8601 format. eg. `1963-11-22T18:30:00Z`</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<SwaggerResponse<System.Collections.Generic.ICollection<Position>>> RouteAsync(System.Collections.Generic.IEnumerable<int> deviceId, System.Collections.Generic.IEnumerable<int> groupId, System.DateTimeOffset from, System.DateTimeOffset to, System.Threading.CancellationToken cancellationToken)
        {
            if (from == null)
                throw new System.ArgumentNullException("from");

            if (to == null)
                throw new System.ArgumentNullException("to");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/reports/route?");
            if (deviceId != null)
            {
                foreach (var item_ in deviceId) { urlBuilder_.Append(System.Uri.EscapeDataString("deviceId") + "=").Append(System.Uri.EscapeDataString(ConvertToString(item_, System.Globalization.CultureInfo.InvariantCulture))).Append("&"); }
            }
            if (groupId != null)
            {
                foreach (var item_ in groupId) { urlBuilder_.Append(System.Uri.EscapeDataString("groupId") + "=").Append(System.Uri.EscapeDataString(ConvertToString(item_, System.Globalization.CultureInfo.InvariantCulture))).Append("&"); }
            }
            urlBuilder_.Append(System.Uri.EscapeDataString("from") + "=").Append(System.Uri.EscapeDataString(from.ToString("s", System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("to") + "=").Append(System.Uri.EscapeDataString(to.ToString("s", System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Length--;

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<System.Collections.Generic.ICollection<Position>>(response_, headers_).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return new SwaggerResponse<System.Collections.Generic.ICollection<Position>>(status_, headers_, objectResponse_.Object);
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        /// <summary>Fetch a list of Events within the time period for the Devices or Groups</summary>
        /// <param name="type">% can be used to return events of all types</param>
        /// <param name="from">in IS0 8601 format. eg. `1963-11-22T18:30:00Z`</param>
        /// <param name="to">in IS0 8601 format. eg. `1963-11-22T18:30:00Z`</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<SwaggerResponse<System.Collections.Generic.ICollection<Event>>> EventsAsync(System.Collections.Generic.IEnumerable<int> deviceId, System.Collections.Generic.IEnumerable<int> groupId, System.Collections.Generic.IEnumerable<string> type, System.DateTimeOffset from, System.DateTimeOffset to)
        {
            return EventsAsync(deviceId, groupId, type, from, to, System.Threading.CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Fetch a list of Events within the time period for the Devices or Groups</summary>
        /// <param name="type">% can be used to return events of all types</param>
        /// <param name="from">in IS0 8601 format. eg. `1963-11-22T18:30:00Z`</param>
        /// <param name="to">in IS0 8601 format. eg. `1963-11-22T18:30:00Z`</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<SwaggerResponse<System.Collections.Generic.ICollection<Event>>> EventsAsync(System.Collections.Generic.IEnumerable<int> deviceId, System.Collections.Generic.IEnumerable<int> groupId, System.Collections.Generic.IEnumerable<string> type, System.DateTimeOffset from, System.DateTimeOffset to, System.Threading.CancellationToken cancellationToken)
        {
            if (from == null)
                throw new System.ArgumentNullException("from");

            if (to == null)
                throw new System.ArgumentNullException("to");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/reports/events?");
            if (deviceId != null)
            {
                foreach (var item_ in deviceId) { urlBuilder_.Append(System.Uri.EscapeDataString("deviceId") + "=").Append(System.Uri.EscapeDataString(ConvertToString(item_, System.Globalization.CultureInfo.InvariantCulture))).Append("&"); }
            }
            if (groupId != null)
            {
                foreach (var item_ in groupId) { urlBuilder_.Append(System.Uri.EscapeDataString("groupId") + "=").Append(System.Uri.EscapeDataString(ConvertToString(item_, System.Globalization.CultureInfo.InvariantCulture))).Append("&"); }
            }
            if (type != null)
            {
                foreach (var item_ in type) { urlBuilder_.Append(System.Uri.EscapeDataString("type") + "=").Append(System.Uri.EscapeDataString(ConvertToString(item_, System.Globalization.CultureInfo.InvariantCulture))).Append("&"); }
            }
            urlBuilder_.Append(System.Uri.EscapeDataString("from") + "=").Append(System.Uri.EscapeDataString(from.ToString("s", System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("to") + "=").Append(System.Uri.EscapeDataString(to.ToString("s", System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Length--;

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<System.Collections.Generic.ICollection<Event>>(response_, headers_).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return new SwaggerResponse<System.Collections.Generic.ICollection<Event>>(status_, headers_, objectResponse_.Object);
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        /// <summary>Fetch a list of ReportSummary within the time period for the Devices or Groups</summary>
        /// <param name="from">in IS0 8601 format. eg. `1963-11-22T18:30:00Z`</param>
        /// <param name="to">in IS0 8601 format. eg. `1963-11-22T18:30:00Z`</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<SwaggerResponse<System.Collections.Generic.ICollection<ReportSummary>>> SummaryAsync(System.Collections.Generic.IEnumerable<int> deviceId, System.Collections.Generic.IEnumerable<int> groupId, System.DateTimeOffset from, System.DateTimeOffset to)
        {
            return SummaryAsync(deviceId, groupId, from, to, System.Threading.CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Fetch a list of ReportSummary within the time period for the Devices or Groups</summary>
        /// <param name="from">in IS0 8601 format. eg. `1963-11-22T18:30:00Z`</param>
        /// <param name="to">in IS0 8601 format. eg. `1963-11-22T18:30:00Z`</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<SwaggerResponse<System.Collections.Generic.ICollection<ReportSummary>>> SummaryAsync(System.Collections.Generic.IEnumerable<int> deviceId, System.Collections.Generic.IEnumerable<int> groupId, System.DateTimeOffset from, System.DateTimeOffset to, System.Threading.CancellationToken cancellationToken)
        {
            if (from == null)
                throw new System.ArgumentNullException("from");

            if (to == null)
                throw new System.ArgumentNullException("to");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/reports/summary?");
            if (deviceId != null)
            {
                foreach (var item_ in deviceId) { urlBuilder_.Append(System.Uri.EscapeDataString("deviceId") + "=").Append(System.Uri.EscapeDataString(ConvertToString(item_, System.Globalization.CultureInfo.InvariantCulture))).Append("&"); }
            }
            if (groupId != null)
            {
                foreach (var item_ in groupId) { urlBuilder_.Append(System.Uri.EscapeDataString("groupId") + "=").Append(System.Uri.EscapeDataString(ConvertToString(item_, System.Globalization.CultureInfo.InvariantCulture))).Append("&"); }
            }
            urlBuilder_.Append(System.Uri.EscapeDataString("from") + "=").Append(System.Uri.EscapeDataString(from.ToString("s", System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("to") + "=").Append(System.Uri.EscapeDataString(to.ToString("s", System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Length--;

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<System.Collections.Generic.ICollection<ReportSummary>>(response_, headers_).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return new SwaggerResponse<System.Collections.Generic.ICollection<ReportSummary>>(status_, headers_, objectResponse_.Object);
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        /// <summary>Fetch a list of ReportTrips within the time period for the Devices or Groups</summary>
        /// <param name="from">in IS0 8601 format. eg. `1963-11-22T18:30:00Z`</param>
        /// <param name="to">in IS0 8601 format. eg. `1963-11-22T18:30:00Z`</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<SwaggerResponse<System.Collections.Generic.ICollection<ReportTrips>>> TripsAsync(System.Collections.Generic.IEnumerable<int> deviceId, System.Collections.Generic.IEnumerable<int> groupId, System.DateTimeOffset from, System.DateTimeOffset to)
        {
            return TripsAsync(deviceId, groupId, from, to, System.Threading.CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Fetch a list of ReportTrips within the time period for the Devices or Groups</summary>
        /// <param name="from">in IS0 8601 format. eg. `1963-11-22T18:30:00Z`</param>
        /// <param name="to">in IS0 8601 format. eg. `1963-11-22T18:30:00Z`</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<SwaggerResponse<System.Collections.Generic.ICollection<ReportTrips>>> TripsAsync(System.Collections.Generic.IEnumerable<int> deviceId, System.Collections.Generic.IEnumerable<int> groupId, System.DateTimeOffset from, System.DateTimeOffset to, System.Threading.CancellationToken cancellationToken)
        {
            if (from == null)
                throw new System.ArgumentNullException("from");

            if (to == null)
                throw new System.ArgumentNullException("to");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/reports/trips?");
            if (deviceId != null)
            {
                foreach (var item_ in deviceId) { urlBuilder_.Append(System.Uri.EscapeDataString("deviceId") + "=").Append(System.Uri.EscapeDataString(ConvertToString(item_, System.Globalization.CultureInfo.InvariantCulture))).Append("&"); }
            }
            if (groupId != null)
            {
                foreach (var item_ in groupId) { urlBuilder_.Append(System.Uri.EscapeDataString("groupId") + "=").Append(System.Uri.EscapeDataString(ConvertToString(item_, System.Globalization.CultureInfo.InvariantCulture))).Append("&"); }
            }
            urlBuilder_.Append(System.Uri.EscapeDataString("from") + "=").Append(System.Uri.EscapeDataString(from.ToString("s", System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("to") + "=").Append(System.Uri.EscapeDataString(to.ToString("s", System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Length--;

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<System.Collections.Generic.ICollection<ReportTrips>>(response_, headers_).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return new SwaggerResponse<System.Collections.Generic.ICollection<ReportTrips>>(status_, headers_, objectResponse_.Object);
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        /// <summary>Fetch a list of ReportStops within the time period for the Devices or Groups</summary>
        /// <param name="from">in IS0 8601 format. eg. `1963-11-22T18:30:00Z`</param>
        /// <param name="to">in IS0 8601 format. eg. `1963-11-22T18:30:00Z`</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<SwaggerResponse<System.Collections.Generic.ICollection<ReportStops>>> StopsAsync(System.Collections.Generic.IEnumerable<int> deviceId, System.Collections.Generic.IEnumerable<int> groupId, System.DateTimeOffset from, System.DateTimeOffset to)
        {
            return StopsAsync(deviceId, groupId, from, to, System.Threading.CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Fetch a list of ReportStops within the time period for the Devices or Groups</summary>
        /// <param name="from">in IS0 8601 format. eg. `1963-11-22T18:30:00Z`</param>
        /// <param name="to">in IS0 8601 format. eg. `1963-11-22T18:30:00Z`</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<SwaggerResponse<System.Collections.Generic.ICollection<ReportStops>>> StopsAsync(System.Collections.Generic.IEnumerable<int> deviceId, System.Collections.Generic.IEnumerable<int> groupId, System.DateTimeOffset from, System.DateTimeOffset to, System.Threading.CancellationToken cancellationToken)
        {
            if (from == null)
                throw new System.ArgumentNullException("from");

            if (to == null)
                throw new System.ArgumentNullException("to");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/reports/stops?");
            if (deviceId != null)
            {
                foreach (var item_ in deviceId) { urlBuilder_.Append(System.Uri.EscapeDataString("deviceId") + "=").Append(System.Uri.EscapeDataString(ConvertToString(item_, System.Globalization.CultureInfo.InvariantCulture))).Append("&"); }
            }
            if (groupId != null)
            {
                foreach (var item_ in groupId) { urlBuilder_.Append(System.Uri.EscapeDataString("groupId") + "=").Append(System.Uri.EscapeDataString(ConvertToString(item_, System.Globalization.CultureInfo.InvariantCulture))).Append("&"); }
            }
            urlBuilder_.Append(System.Uri.EscapeDataString("from") + "=").Append(System.Uri.EscapeDataString(from.ToString("s", System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("to") + "=").Append(System.Uri.EscapeDataString(to.ToString("s", System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Length--;

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<System.Collections.Generic.ICollection<ReportStops>>(response_, headers_).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return new SwaggerResponse<System.Collections.Generic.ICollection<ReportStops>>(status_, headers_, objectResponse_.Object);
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        protected struct ObjectResponseResult<T>
        {
            public ObjectResponseResult(T responseObject, string responseText)
            {
                this.Object = responseObject;
                this.Text = responseText;
            }

            public T Object { get; }

            public string Text { get; }
        }

        public bool ReadResponseAsString { get; set; }

        protected virtual async System.Threading.Tasks.Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(System.Net.Http.HttpResponseMessage response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers)
        {
            if (response == null || response.Content == null)
            {
                return new ObjectResponseResult<T>(default(T), string.Empty);
            }

            if (ReadResponseAsString)
            {
                var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    var typedBody = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseText, JsonSerializerSettings);
                    return new ObjectResponseResult<T>(typedBody, responseText);
                }
                catch (Newtonsoft.Json.JsonException exception)
                {
                    var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
                    throw new ApiException(message, (int)response.StatusCode, responseText, headers, exception);
                }
            }
            else
            {
                try
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (var streamReader = new System.IO.StreamReader(responseStream))
                    using (var jsonTextReader = new Newtonsoft.Json.JsonTextReader(streamReader))
                    {
                        var serializer = Newtonsoft.Json.JsonSerializer.Create(JsonSerializerSettings);
                        var typedBody = serializer.Deserialize<T>(jsonTextReader);
                        return new ObjectResponseResult<T>(typedBody, string.Empty);
                    }
                }
                catch (Newtonsoft.Json.JsonException exception)
                {
                    var message = "Could not deserialize the response body stream as " + typeof(T).FullName + ".";
                    throw new ApiException(message, (int)response.StatusCode, string.Empty, headers, exception);
                }
            }
        }

        private string ConvertToString(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return "";
            }

            if (value is System.Enum)
            {
                var name = System.Enum.GetName(value.GetType(), value);
                if (name != null)
                {
                    var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                    if (field != null)
                    {
                        var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(System.Runtime.Serialization.EnumMemberAttribute))
                            as System.Runtime.Serialization.EnumMemberAttribute;
                        if (attribute != null)
                        {
                            return attribute.Value != null ? attribute.Value : name;
                        }
                    }

                    var converted = System.Convert.ToString(System.Convert.ChangeType(value, System.Enum.GetUnderlyingType(value.GetType()), cultureInfo));
                    return converted == null ? string.Empty : converted;
                }
            }
            else if (value is bool)
            {
                return System.Convert.ToString((bool)value, cultureInfo).ToLowerInvariant();
            }
            else if (value is byte[])
            {
                return System.Convert.ToBase64String((byte[])value);
            }
            else if (value.GetType().IsArray)
            {
                var array = System.Linq.Enumerable.OfType<object>((System.Array)value);
                return string.Join(",", System.Linq.Enumerable.Select(array, o => ConvertToString(o, cultureInfo)));
            }

            var result = System.Convert.ToString(value, cultureInfo);
            return (result is null) ? string.Empty : result;
        }
    }
}
#pragma warning restore 1591
#pragma warning restore 1573
#pragma warning restore 472
#pragma warning restore 114
#pragma warning restore 108