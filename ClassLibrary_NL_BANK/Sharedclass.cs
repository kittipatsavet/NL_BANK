using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack; // => หา Key จาก HTML Code
using PuppeteerSharp; // => จำลอง Chrome จกาเครื่องจริง เพื่อ Get HTML และ Send Click Event
using ClassLibrary_NL_BANK;

namespace ClassLibrary_NL_BANK
{
    public  class SharedClass
    {
        public  string GetIBANNumber()
        {
            #region หา Path Google Chrome เพื่อจำลองการใช้งาน Get HTML ผ่าน Browser (Real Simulate)
            const string suffix = @"Google\Chrome\Application\chrome.exe";
            var prefixes = new List<string> { Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) };
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            var programFilesx86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            if (programFilesx86 != programFiles)
            {
                prefixes.Add(programFiles);
            }
            else
            {
                var programFilesDirFromReg = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion", "ProgramW6432Dir", null) as string;
                if (programFilesDirFromReg != null) prefixes.Add(programFilesDirFromReg);
            }

            prefixes.Add(programFilesx86);
            var path = prefixes.Distinct().Select(prefix =>
            Path.Combine(prefix, suffix)).FirstOrDefault(File.Exists);
            #endregion 
            SharedClass p = new SharedClass();

            string result_IBANGen =
                    Task.Run(async () => await p.OpenWeb(path)).Result;
            return result_IBANGen;
        }
        private async Task<string> OpenWeb(string path)
        {
            // lauch browser and save in variable
            var _browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                ExecutablePath = path, // get path to chrome executable
            });

            string url = "http://randomiban.com/?country=Netherlands";

            var _page = await _browser.NewPageAsync();
            await _page.GoToAsync(url);

            await _page.ClickAsync("#gen_button");

            String result = await _page.GetContentAsync();

            //Console.WriteLine(result); = > HTML Call back on Click Gen_button

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(result);
            string randomkey = doc.GetElementbyId("demo").InnerHtml;

            Console.WriteLine(randomkey);
            return randomkey;
        }
    }
}
