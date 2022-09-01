using Newtonsoft.Json;
using System.IO;

namespace BookmarkManager.Components
{
    partial class EditCollectionComponent
    {
        private string Title { get; set; }

        DriveServices DriveService = new DriveServices();
        private async void EditCollection()
        {
            this.IsOpened = false;
            foreach (var collection in DriveService.GetListDrive().CollectionList)
            {
                if(collection.IdCollection == IdCollection)
                {
                    collection.NameCollection = this.Title;
                }
            }
            this.Title = "";
            string json = JsonConvert.SerializeObject(DriveService.DataBase);

            File.WriteAllText(DriveService.filePath, json);
            await DriveService.UpdateDataBase();
        }
    }
}