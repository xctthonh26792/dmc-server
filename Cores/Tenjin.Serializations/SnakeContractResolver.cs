﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using Tenjin.Helpers;

namespace Tenjin.Serializations
{
    public class SnakeContractResolver : DefaultContractResolver
    {
        private SnakeContractResolver()
        {
            NamingStrategy = new SnakeCaseNamingStrategy();
        }

        public static SnakeContractResolver Create()
        {
            return new SnakeContractResolver();
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization serialization)
        {
            var response = base.CreateProperty(member, serialization);
            if (member.HasAttribute<JsonPropertyAttribute>(out var _))
            {
                response.PropertyName = response.PropertyName.ToRegular();
            }
            return response;
        }
    }
}
