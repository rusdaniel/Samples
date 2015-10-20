namespace MarkdownGenerator.Viewer
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Xml;
    using System.Xml.Serialization;

    public class Settings
    {
        public string CssFileName { get; set; }
    }

    public class CssSettings
    {
        private const string settingsFileName = @"Data\CssSettings.xml";

        private string path;

        private XmlSerializer serializer;

        public CssSettings()
        {
            var appLocation = Assembly.GetExecutingAssembly().Location;
            this.path = Path.Combine(Path.GetDirectoryName(appLocation), settingsFileName);
            this.serializer = new XmlSerializer(typeof(Settings));
        }

        public Settings GetCssSettings()
        {
            Settings cssSettings = null;
            using (var xmlReader = XmlReader.Create(this.path))
            {
                try
                {
                    if (this.serializer.CanDeserialize(xmlReader))
                    {
                        cssSettings = (Settings)this.serializer.Deserialize(xmlReader);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Cannot deserialize the CssSettings file", ex);
                }
            }

            return cssSettings;
        }
    }
}
