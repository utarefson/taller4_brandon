namespace BookmarkManager.Components
{
    partial class EditBookmarkComponent
    {
        private string Name { get; set; }
        private string Path { get; set; }
        private GoogleDriveController DriveService = new GoogleDriveController();
        
        private async void EditCollection()
        {
            var updateData = this.DriveService.GetListDrive();
            this.IsOpened = false;
            foreach (var bookmark in updateData.BookmarkList)
            {
                if (bookmark.IdBookmark == this.IdCard)
                {
                    bookmark.NameBookmark = this.Name;
                    bookmark.PathBookmark = this.Path;
                }
            }

            await this.DriveService.SaveChange(updateData);
        }
    }
}
