using BookmarkManager.Data;

namespace BookmarkManager.Components
{
    partial class CreateCollectionComponent
    {
        private string NameCollection { get; set; }
        private GoogleDriveController DriveService = new GoogleDriveController();

        private async void CreateCollection()
        {
            var updateData = this.DriveService.GetListDrive();
            await this.onResult.InvokeAsync(false);
            var collection = new Collection
            {
                IdCollection = updateData.CollectionList.Count,
                NameCollection = this.NameCollection
            };
            updateData.CollectionList.AddFirst(collection);

            await this.DriveService.SaveChange(updateData);
        }
    }
}
