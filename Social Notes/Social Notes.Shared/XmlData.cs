using System;
using System.Collections.Generic;
using System.Text;

using Windows.Data.Xml.Dom;
using Windows.Storage;

namespace Social_Notes
{
    class XmlData
    {
        private string File;
        public string content { private set; get; }
        public XmlData(string file)
        {
            this.File = file;
            LoadXML();
        }
        private async void LoadXML()
        {
            var arch = await ApplicationData.Current.LocalFolder.GetFileAsync(this.File);
            XmlDocument xml = await XmlDocument.LoadFromFileAsync(arch);
            this.content = xml.GetXml();
        }
    }
}
