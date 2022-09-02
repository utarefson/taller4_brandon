namespace BookmarkManager.Components
{
    partial class BookmarkComponent
    {
        private GoogleDriveController GoogleDriveController = new GoogleDriveController();

        private async void DeleteBookmark()
        {
            var bookmarkList = this.GoogleDriveController.GetListDrive();
            bookmarkList.BookmarkList.RemoveAll(x => x.IdBookmark == Bookmark.IdBookmark);

            await this.GoogleDriveController.SaveChange(bookmarkList);
        }
    }
}
