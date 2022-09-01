using BookmarkManager.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BookmarkManager
{
    public class DriveServices
    {
        public string filePath = @"C:\Users\Brand\Desktop\Prueba\prueba.json";
        public BaseContext DataBase = new BaseContext();

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
                Console.WriteLine("Credential file saved to: " + credentialPath);
            }
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
            return service;
        }

        public void GetDataBase()
        {
            var request = GetServiceDrive().Files.Get("167N2AXm8xlZdXC-ucFxcahenakUbHo8i");
            var stream = new MemoryStream();
            request.Download(stream);

            using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                stream.WriteTo(file);
            }
        }

        public BaseContext GetListDrive()
        {
            using (StreamReader jsonStream = File.OpenText(filePath))
            {
                var jsonR = jsonStream.ReadToEnd();
                this.DataBase = JsonConvert.DeserializeObject<BaseContext>(jsonR);
            }
            return this.DataBase;
        }

        public async Task UpdateDataBase()
        {
            string updateFileId = "167N2AXm8xlZdXC-ucFxcahenakUbHo8i";
            var updateFile = await GetServiceDrive().Files.Get(updateFileId).ExecuteAsync();

            var updateFileBody = new Google.Apis.Drive.v3.Data.File()
            {
                Name = "DataBase.json"
            };
            await using (var uploadStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var updateRequest = GetServiceDrive().Files.Update(updateFileBody, updateFile.Id, uploadStream, "application/json");
                var result = await updateRequest.UploadAsync(CancellationToken.None);
            }
        }
    }
}
