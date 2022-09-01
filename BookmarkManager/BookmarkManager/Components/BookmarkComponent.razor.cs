using BookmarkManager.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace BookmarkManager.Components
{
    partial class BookmarkComponent
    {
        DriveServices DriveService = new DriveServices();
        private async void DeleteBookmark()
        {
            LinkedList<Bookmark> BookmarkList = new LinkedList<Bookmark>();
            foreach(var bookmark in DriveService.GetListDrive().BookmarkList)
            {
                if(bookmark.IdBookmark != Bookmark.IdBookmark)
                {
                    BookmarkList.AddLast(bookmark);
                }
            }
            DriveService.DataBase.BookmarkList = BookmarkList;
            string json = JsonConvert.SerializeObject(DriveService.DataBase);

            File.WriteAllText(DriveService.filePath, json);
            await DriveService.UpdateDataBase();
        }
    }
}
