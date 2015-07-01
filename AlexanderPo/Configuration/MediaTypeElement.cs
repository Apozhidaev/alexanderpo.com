using System.Configuration;

namespace AlexanderPo.Configuration
{
    public class MediaTypeElement : ConfigurationElement
    {
        [ConfigurationProperty("extension", IsRequired = true)]
        public string Extension
        {
            get
            {
                return (string)this["extension"];
            }
        }

        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get
            {
                return (string)this["value"];
            }
        }
    }
}