namespace BookmarkManager.Components
{
    partial class EditCollectionComponent
    {
        private string Title { get; set; }
        private GoogleDriveController DriveService = new GoogleDriveController();

        private async void EditCollection()
        {
            var updateData = this.DriveService.GetListDrive();
            this.IsOpened = false;
            foreach (var collection in updateData.CollectionList)
            {
                if(collection.IdCollection == this.IdCollection)
                {
                    collection.NameCollection = this.Title;
                }
            }
            this.Title = string.Empty;

            await this.DriveService.SaveChange(updateData);
        }
    }
}