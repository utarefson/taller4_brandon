using Newtonsoft.Json;
using System.IO;

namespace BookmarkManager.Components
{
    partial class EditBookmarkComponent
    {
        private string Name { get; set; }
        private string Path { get; set; }

        DriveServices DriveService = new DriveServices();
        private async void EditCollection()
        {
            this.IsOpened = false;
            foreach (var bookmark in DriveService.GetListDrive().BookmarkList)
            {
                if (bookmark.IdBookmark == IdCard)
                {
                    bookmark.NameBookmark = this.Name;
                    bookmark.PathBookmark = this.Path;
                }
            }
            string json = JsonConvert.SerializeObject(DriveService.DataBase);

            File.WriteAllText(DriveService.filePath, json);
            await DriveService.UpdateDataBase();
        }
    }
}
