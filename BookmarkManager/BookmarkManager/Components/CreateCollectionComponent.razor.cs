using BookmarkManager.Data;
using Newtonsoft.Json;
using System.IO;

namespace BookmarkManager.Components
{
    partial class CreateCollectionComponent
    {
        private string NameCollection { get; set; }

        DriveServices DriveService = new DriveServices();
        private async void CreateCollection()
        {
            await this.onResult.InvokeAsync(false);
            var collection = new Collection
            {
                IdCollection = DriveService.GetListDrive().CollectionList.Count,
                NameCollection = this.NameCollection
            };
            DriveService.DataBase.CollectionList.AddFirst(collection);
            string json = JsonConvert.SerializeObject(DriveService.DataBase);

            File.WriteAllText(DriveService.filePath, json);
            await DriveService.UpdateDataBase();
        }
    }
}
