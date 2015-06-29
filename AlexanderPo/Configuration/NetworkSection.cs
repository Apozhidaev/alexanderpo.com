using System.Configuration;

namespace AlexanderPo.Configuration
{
    public class NetworkSection : ConfigurationSection
    {
        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get
            {
                return (string)this["url"];
            }
        }
    }
}