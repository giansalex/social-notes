using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Social_Notes.DataModel
{
    public class ItemRss : INotifyPropertyChanged
    {
        private string _fuente;
        private string _subtitle;
        private string _ruta;
        private string _rutaImagen;
        public String Fuente
        {
            get { return _fuente; }
            set { this.SetProperty(ref this._fuente, value); }
        }

        public String SubTitle
        {
            get { return _subtitle; }
            set { this.SetProperty(ref this._subtitle, value); }
        }
        public String Ruta
        {
            get { return _ruta; }
            set { this.SetProperty(ref this._ruta, value); }
        }

        public String Imagen
        {
            get { return _rutaImagen; }
            set { this.SetProperty(ref this._rutaImagen, value); }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value,[CallerMemberName]string propertyName = null)
        {
            if (object.Equals(storage, value)) return false; 
            storage = value; 
            this.OnPropertyChaned(propertyName);
            return true;
        }
        private void OnPropertyChaned(string propertyName) { 
            var eventHandler = this.PropertyChanged; 
            if (eventHandler != null) 
                eventHandler(this, new PropertyChangedEventArgs(propertyName)); 
        }
    }
}
