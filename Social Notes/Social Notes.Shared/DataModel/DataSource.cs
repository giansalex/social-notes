using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Web.Syndication; 

namespace Social_Notes.DataModel
{
    /// <summary> 
    /// group data model. 
    /// </summary> 
    public class DataGroup
    {
        public DataGroup(String uniqueId, String title, String link, String description, String icono)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Link = link;
            this.Description = description;
            this.Icono = icono;
        }
        public string UniqueId { get; private set; }
        public string Title { get; private set; }
        public string Link { get; private set; }
        public string Description { get; private set; }

        public string Icono { get; private set; }
        public override string ToString()
        {
            return this.Title;
        }
    }

    public sealed class DataSource
    {
        private static DataSource _dataSource = new DataSource();

        private FuenteRss fuente;
        public static async Task<FuenteRss> GetGroupsAsync(string feedUrl)
        {
            await _dataSource.GetSampleDataAsync(feedUrl);

            return _dataSource.fuente;
        }
        public static async Task<DataGroup> GetGroupAsync(string uniqueId, string feedURL)
        {
            await _dataSource.GetSampleDataAsync(feedURL);
            var matches = _dataSource.fuente.Items.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }
        private async Task GetSampleDataAsync(string url)
        {

            SyndicationClient client = new SyndicationClient();
            Uri feedUri = new Uri(url);
            var feed = await client.RetrieveFeedAsync(feedUri);
            fuente = new FuenteRss(feed.Title.Text, feed.Subtitle.Text, feed.Links[0].Uri.ToString());
            fuente.Items = new ObservableCollection<DataGroup>();

            foreach (SyndicationItem item in feed.Items)
            {
                string data = string.Empty;
                if (feed.SourceFormat == SyndicationFormat.Atom10)
                {
                    data = item.Content.Text;
                }
                else if (feed.SourceFormat == SyndicationFormat.Rss20)
                {
                    data = item.Summary.Text;
                }

                Regex regx = new Regex("<[^>]*>", RegexOptions.IgnoreCase);
                data = regx.Replace(data, " "); ;
                var titulo = item.Title.Text;
                var icon = this.GetIcon(ref titulo);
                DataGroup group = new DataGroup(item.Id,
                                                titulo,
                                                item.Links[0].Uri.ToString(),
                                                data,
                                                icon);
                fuente.Items.Add(group);

            }
        }
        private string GetIcon(ref String str)
        {
            try
            {
                if(str.Contains('[') && str.Contains(']')){
                    var index = str.LastIndexOf('[');
                    var elemt = str.Substring(index, str.LastIndexOf(']') - index);
                    str = str.Remove(index); // Eliminamos texto entre []
                    elemt = elemt.ToUpper();
                    if (elemt.Contains("FOTO"))
                        return "People";
                    else
                        return "Video";
                }
            }
            catch(Exception){}

            return "List";
        }
    } 
}
