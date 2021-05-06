using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

namespace Conbot.SplatoonPlugin
{
    public class Schedules
    {
        [JsonProperty("regular")]
        private readonly IEnumerable<Rotation>? _regularRotations;

        public IEnumerable<Rotation> RegularRotations
            => _regularRotations is not null ? _regularRotations : Enumerable.Empty<Rotation>();

        [JsonProperty("gachi")]
        private readonly IEnumerable<Rotation>? _rankedRotations;

        public IEnumerable<Rotation> RankedRotations
            => _rankedRotations is not null ? _rankedRotations : Enumerable.Empty<Rotation>();

        [JsonProperty("league")]
        private readonly IEnumerable<Rotation>? _leagueRotations;

        public IEnumerable<Rotation> LeagueRotations
            => _leagueRotations is not null ? _leagueRotations : Enumerable.Empty<Rotation>();
    }
}
