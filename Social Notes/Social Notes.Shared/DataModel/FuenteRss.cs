using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Social_Notes.DataModel
{
    public class FuenteRss
    {
        public string Title { private set; get; }
        public string Description { private set; get; }

        public string Url { private set; get; }

        public string Imagen { set; get; }
        public ObservableCollection<DataGroup> Items { get; set; }
        public FuenteRss(string title, string subtitle, string uri)
        {
            this.Title = title;
            this.Description = subtitle;
            this.Url = uri;
        }
    }

    public sealed class SourceRSS
    {
        private static SourceRSS _source = new SourceRSS();
        public ObservableCollection<FuenteRss> Group
        {
            private set;
            get;
        }

        public static async Task<SourceRSS> GetGroupsAsync(ItemRss[] path)
        {
            await _source.GetRss(path);
            return _source; 
        }
        private async Task GetRss(ItemRss[] paths)
        {
            if (this.Group != null)
                return;
            this.Group = new ObservableCollection<FuenteRss>();
            foreach(ItemRss ruta1 in paths){
                var f = await DataSource.GetGroupsAsync(ruta1.Ruta);
                f.Imagen = ruta1.Imagen;
                this.Group.Add(f);
            }     
        }
    }
}
