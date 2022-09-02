using BookmarkManager.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Newtonsoft.Json;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BookmarkManager
{
    public class GoogleDriveController : IGoogleDriveServices
    {
        private const string IdFileOnGoogleDrive = "167N2AXm8xlZdXC-ucFxcahenakUbHo8i";
        public const string FilePath = @"C:\Users\Brand\Desktop\Prueba\prueba.json";
        public BaseContext DataBase = new();

        public DriveService GetServiceDrive()
        {
            string[] scopes = { DriveService.Scope.Drive };
            UserCredential credential;
            using (var stream =
                   new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credentialPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credentialPath, true)).Result;
            }
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
            return service;
        }

        public void GetDataBase()
        {
            var request = this.GetServiceDrive().Files.Get(IdFileOnGoogleDrive);
            var stream = new MemoryStream();
            request.Download(stream);

            using (FileStream file = new FileStream(FilePath, FileMode.Create, FileAccess.ReadWrite))
            {
                stream.WriteTo(file);
            }
        }

        public BaseContext GetListDrive()
        {
            using (StreamReader jsonStream = File.OpenText(FilePath))
            {
                var jsonR = jsonStream.ReadToEnd();
                this.DataBase = JsonConvert.DeserializeObject<BaseContext>(jsonR);
            }
            return this.DataBase;
        }

        public async Task UpdateDataBase()
        {
            string updateFileId = IdFileOnGoogleDrive;
            var updateFile = await this.GetServiceDrive().Files.Get(updateFileId).ExecuteAsync();

            var updateFileBody = new Google.Apis.Drive.v3.Data.File()
            {
                Name = "DataBase.json"
            };
            await using (var uploadStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            {
                var updateRequest = this.GetServiceDrive().Files.Update(updateFileBody, updateFile.Id, uploadStream, "application/json");
                var result = await updateRequest.UploadAsync(CancellationToken.None);
            }
        }

        public async Task SaveChange(BaseContext baseContext)
        {
            string json = JsonConvert.SerializeObject(baseContext);
            File.WriteAllText(GoogleDriveController.FilePath, json);
            await this.UpdateDataBase();
        }
    }
}
