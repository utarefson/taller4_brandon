using BookmarkManager.Data;
using Newtonsoft.Json;
using System.IO;

namespace BookmarkManager.Components
{
    partial class CreateBookmarkComponent
    {
        private string NamePage { get; set; }
        private string Path { get; set; }

        DriveServices DriveService = new DriveServices();
        private async void CreateBookmark()
        {
            this.IsOpened = false;
            var bookmark = new Bookmark
            {
                IdBookmark = DriveService.GetListDrive().BookmarkList.Count,
                NameBookmark = this.NamePage,
                PathBookmark = this.Path,
                IdCollection = this.IdCollection
            };
            this.NamePage = "";
            this.Path = "";
            DriveService.DataBase.BookmarkList.AddLast(bookmark);
            string json = JsonConvert.SerializeObject(DriveService.DataBase);
            File.WriteAllText(DriveService.filePath, json);
            await DriveService.UpdateDataBase();
        }
    }
}
