using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerUpdater
{
    public class MinecraftVersions
    {
        public Latest? Latest { get; set; }
        public List<Version>? Versions { get; set; }
    }

    public class Latest
    {
        public string? Release { get; set; }
        public string? Snapshot { get; set; }
    }

    public class Version
    {
        public string? Id { get; set; }
        public string? Type { get; set; }
        public string? Url { get; set; }
        public DateTime? Time { get; set; }
        public DateTime? ReleaseTime { get; set; }
    }

}
