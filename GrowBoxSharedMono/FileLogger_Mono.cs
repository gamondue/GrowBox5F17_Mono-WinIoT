using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using Windows.Storage;

namespace GrowBoxShared
{    // !!!! DA AGGIUSTARE, ORA NON SALVA !!!!
    class FileLogger
    {
        //StorageFolder folder;
        //StorageFile file;
        private int id = 0;

        // Constructor
        public FileLogger(string filename)
        {
            OpenFile(filename);
        }

        private async void OpenFile(string filename)
        {
            //folder = ApplicationData.Current.LocalCacheFolder;
            //file = await folder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
        }

        public async void WriteLogger(string s)
        {
            string orologio = DateTime.Now.ToString();
            try
            {
                // Ogni tanto senza un motivo apparente parte un'eccezione!!!!!!! E non scrive la riga
                // ed a volte la scrittura delle righe non è in ordine cronologico!!!!!!
                //await FileIO.AppendTextAsync(file, (id++) + ";" + orologio + ";" + s + '\r' + '\n');
            }
            catch { }
        }

        public void Close()
        {

        }
    }
}
