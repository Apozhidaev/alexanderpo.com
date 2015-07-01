using System.Configuration;

namespace AlexanderPo.Configuration
{
    public class ContentSection : ConfigurationSection
    {
        private static readonly ConfigurationProperty MediaTypesProperty =
           new ConfigurationProperty(
               "mediaTypes",
               typeof(MediaTypeElementCollection),
               null,
               ConfigurationPropertyOptions.IsRequired);

        public ContentSection()
        {
            base.Properties.Add(MediaTypesProperty);
        }

        [ConfigurationProperty("mediaTypes", IsRequired = true)]
        public MediaTypeElementCollection MediaTypes
        {
            get { return (MediaTypeElementCollection)this[MediaTypesProperty]; }
        }

        [ConfigurationProperty("defaultMediaType", IsRequired = false)]
        public string DefaultMediaType
        {
            get
            {
                return (string)this["defaultMediaType"];
            }
        }
    }
}