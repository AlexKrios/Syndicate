using Newtonsoft.Json;

namespace Syndicate.Core.Services.Serialization
{
    public sealed class DefaultSerializerSettings : JsonSerializerSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSerializerSettings"/> class.
        /// </summary>
        public DefaultSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto;
            PreserveReferencesHandling = PreserveReferencesHandling.None;
            NullValueHandling = NullValueHandling.Ignore;
            DefaultValueHandling = DefaultValueHandling.Populate;
            ObjectCreationHandling = ObjectCreationHandling.Replace;
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;

            Converters.Add(new CompanyIdConverter());
        }
    }
}