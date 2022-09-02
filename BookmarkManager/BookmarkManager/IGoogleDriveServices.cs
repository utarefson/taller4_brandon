using BookmarkManager.Data;
using Google.Apis.Drive.v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookmarkManager
{
    public interface IGoogleDriveServices
    {
        DriveService GetServiceDrive();
        void GetDataBase();
        BaseContext GetListDrive();
        Task UpdateDataBase();
        Task SaveChange(BaseContext baseContext);
    }
}
