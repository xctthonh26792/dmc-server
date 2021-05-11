using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tenjin.Sys.Models.Cores
{
    public class Role
    {
        [JsonProperty("checked")]
        public bool Checked { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Permission
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("actions")]
        public Dictionary<string, Role> Actions { get; set; }

        [JsonProperty("childrens")]
        public IEnumerable<Permission> Childrens { get; set; }
    }
}
