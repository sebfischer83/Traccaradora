#nullable enable

namespace Traccaradora.Web.Store.Data
{
    public record TraccarDevice
    {
        public int Id { get; init; }
        public string? Name { get; init; }
        public double? Latitude { get; init; }
        public double? Longitude { get; init; }
        public double? Altitude { get; init; }
    }

}
