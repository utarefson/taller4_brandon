using BookmarkManager.Data;
using System.Linq;

namespace BookmarkManager.Components
{
    partial class CollectionContainerComponent
    {
        private int IdCollection { get; set; }
        private GoogleDriveController DriveService = new GoogleDriveController();

        private BaseContext GetListGoogleDrive()
        {
            this.BaseContext = this.DriveService.GetListDrive();
            return this.BaseContext;
        }

        private async void DeleteCollection(int id)
        {
            BaseContext updateData = this.GetListGoogleDrive();
            updateData.CollectionList.Remove(updateData.CollectionList.Single(e => e.IdCollection == id));
            updateData.BookmarkList.RemoveAll(e => e.IdCollection == id);

            await this.DriveService.SaveChange(updateData);
        }
    }
}
