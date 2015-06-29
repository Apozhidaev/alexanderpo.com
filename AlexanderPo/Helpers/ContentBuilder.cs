using System;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace AlexanderPo.Helpers
{
    public class ContentBuilder
    {
        private readonly StringBuilder _markupBuilder;

        public ContentBuilder()
        {
            _markupBuilder = new StringBuilder();
        }

        public ContentBuilder Add(string content, params object[] values)
        {
            _markupBuilder.AppendFormat("{0}{1}", 
                _markupBuilder.Length > 0 ? Environment.NewLine : String.Empty,
                String.Format(content, values));
            return this;
        }

        public ContentBuilder AddStyle(string styleFileName)
        {
            return Add("<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" />", styleFileName);
        }

        public ContentBuilder AddScript(string scriptFileName)
        {
            return Add("<script type=\"text/javascript\" src=\"{0}\"></script>", scriptFileName);
        }

        public ContentBuilder AddCache(string fileName)
        {
            return Add("/{0}", fileName);
        }

        public ContentBuilder AddScriptsFrom(string configPath)
        {
            var doc = XDocument.Load(configPath);
            foreach (var e in doc.XPathSelectElements("root/output/input"))
            {
                AddScript(e.Attribute("path").Value);
            }
            return this;
        }

        public ContentBuilder AddCacheFrom(string configPath)
        {
            var doc = XDocument.Load(configPath);
            foreach (var e in doc.XPathSelectElements("root/output/input"))
            {
                AddCache(e.Attribute("path").Value);
            }
            return this;
        }

        public string Build()
        {
            return _markupBuilder.ToString();
        }
    }
}