namespace Traccaradora.Web.Clients
{
    using System = global::System;

    /// <summary>This is a permission map that contain two object indexes. It is used to link/unlink objects. Order is important. Example: { deviceId:8, geofenceId: 16 }</summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.3.0.0 (Newtonsoft.Json v12.0.0.0)")]
    public partial class Permission
    {
        /// <summary>User Id, can be only first parameter</summary>
        [Newtonsoft.Json.JsonProperty("userId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? UserId { get; set; }

        /// <summary>Device Id, can be first parameter or second only in combination with userId</summary>
        [Newtonsoft.Json.JsonProperty("deviceId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? DeviceId { get; set; }

        /// <summary>Group Id, can be first parameter or second only in combination with userId</summary>
        [Newtonsoft.Json.JsonProperty("groupId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? GroupId { get; set; }

        /// <summary>Geofence Id, can be second parameter only</summary>
        [Newtonsoft.Json.JsonProperty("geofenceId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? GeofenceId { get; set; }

        /// <summary>Calendar Id, can be second parameter only and only in combination with userId</summary>
        [Newtonsoft.Json.JsonProperty("calendarId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? CalendarId { get; set; }

        /// <summary>Computed Attribute Id, can be second parameter only</summary>
        [Newtonsoft.Json.JsonProperty("attributeId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? AttributeId { get; set; }

        /// <summary>Driver Id, can be second parameter only</summary>
        [Newtonsoft.Json.JsonProperty("driverId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? DriverId { get; set; }

        /// <summary>User Id, can be second parameter only and only in combination with userId</summary>
        [Newtonsoft.Json.JsonProperty("managedUserId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? ManagedUserId { get; set; }

        private System.Collections.Generic.IDictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

        [Newtonsoft.Json.JsonExtensionData]
        public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties; }
            set { _additionalProperties = value; }
        }


    }
}
#pragma warning restore 1591
#pragma warning restore 1573
#pragma warning restore 472
#pragma warning restore 114
#pragma warning restore 108