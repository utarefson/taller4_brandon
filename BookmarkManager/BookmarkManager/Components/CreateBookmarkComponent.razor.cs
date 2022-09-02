using BookmarkManager.Data;

namespace BookmarkManager.Components
{
    partial class CreateBookmarkComponent
    {
        private string PageName { get; set; }
        private string Path { get; set; }
        private GoogleDriveController DriveService = new GoogleDriveController();

        private async void CreateBookmark()
        {
            var updateData = this.DriveService.GetListDrive();
            this.IsOpened = false;
            var bookmark = new Bookmark
            {
                IdBookmark = updateData.BookmarkList.Count,
                NameBookmark = this.PageName,
                PathBookmark = this.Path,
                IdCollection = this.IdCollection
            };
            updateData.BookmarkList.Add(bookmark);
            this.PageName = string.Empty;
            this.Path = string.Empty;

            await this.DriveService.SaveChange(updateData);
        }
    }
}
