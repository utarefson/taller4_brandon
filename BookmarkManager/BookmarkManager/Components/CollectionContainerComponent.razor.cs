using BookmarkManager.Data;
using Newtonsoft.Json;
using System.IO;

namespace BookmarkManager.Components
{
    partial class CollectionContainerComponent
    {
        private int IdCollection { get; set; }
        DriveServices DriveService = new DriveServices();

        private BaseContext GetListDrive()
        {
            BaseContext= DriveService.GetListDrive();
            return DriveService.GetListDrive();
        }

        private async void DeleteCollection(int id)
        {
            BaseContext updateData = new BaseContext();
            foreach (var collection in DriveService.GetListDrive().CollectionList)
            {
                if(collection.IdCollection != id)
                {
                    updateData.CollectionList.AddLast(collection);
                }
            }
            foreach(var bookmark in DriveService.GetListDrive().BookmarkList)
            {
                if (bookmark.IdCollection != id)
                {
                    updateData.BookmarkList.AddLast(bookmark);
                }
            }
            string json = JsonConvert.SerializeObject(updateData);

            File.WriteAllText(DriveService.filePath, json);
            await DriveService.UpdateDataBase();
        }
    }
}
