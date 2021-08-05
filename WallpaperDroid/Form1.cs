using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace WallpaperDroid
{
    public partial class tagForm : Form
    {
        const string apiURL = "https://wallhaven.cc/api/v1/search?categories=111&purity=100&sorting=random&order=desc"; //wallhaven.cc search API link
        private static readonly HttpClient Client = new HttpClient(); //MSDN says instantiate once per program

        string screenWidth = Screen.PrimaryScreen.Bounds.Width.ToString();//screen width
        string screenHeight = Screen.PrimaryScreen.Bounds.Height.ToString();//screen height

        string saveToPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\WPDroid";

        //to get current wallpaper location to revert back to it if user desires
        string originalWallpaper = Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", 0).ToString();//(keyname,valuename,default value if valuename not found)

        [DllImport("user32.dll",CharSet = CharSet.Auto)]//import user32.dll for setting wallpapers
        static extern bool SystemParametersInfo(uint uiAction, uint uiParam, string pvParam, uint fwinIni);//SystemParametersInfo API call. Refer msdn docs for SPI_SETDESKWALLPAPER

        public tagForm()
        {
            InitializeComponent();
        }

        private void tagForm_Load(object sender, EventArgs e)
        {
            notifyIcon1.BalloonTipTitle = "WallpaperDroid";
            notifyIcon1.BalloonTipText = "Currently running. Access options from taskbar!";
            notifyIcon1.Text = "WallpaperDroid";

            //create WPDroid directory
            if (!Directory.Exists(saveToPath))
                Directory.CreateDirectory(saveToPath);

            //safeguard original wallpaper on form load
            File.Copy(originalWallpaper, Path.Combine(saveToPath,Path.GetFileName(originalWallpaper)), true);//overwrite=true

            screenResLabel.Text ="Your screen resolution: "+screenWidth+" x "+screenHeight;//shows resolution info
        }

        private void tagForm_Resize(object sender, EventArgs e)
        {
            if(WindowState==FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.ShowBalloonTip(1000);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void filterStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.BalloonTipTitle = "WallpaperDroid";
            notifyIcon1.BalloonTipText = "Filter has been applied!";
            notifyIcon1.Text = "WallpaperDroid";
        }

        private async void nextStripMenuItem_Click(object sender, EventArgs e)
        {
            //get API response from wallhaven with async and await so our UI doesn't lag while it receives wallpapers
            var apiResponse = await getTopWallpapersAsync();
            var wallpaperURL = apiResponse.data.First().path;//the first link inside data will be stored

            //code to set this wallpaper//
            var wallpaperPath = await downloadWallpaperAsync(wallpaperURL);//download fetched wallpaperURL
            setWallpaper(wallpaperPath);//set desktop wallpaper
            

            
        }

        private void setWallpaper(string wallpaperPath)
        {
            SystemParametersInfo(0x0014, 0, wallpaperPath, 0x0001);
        }

        private static async Task<SearchResponse> getTopWallpapersAsync()
        {
            var response = await Client.GetAsync(apiURL);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SearchResponse>(json);
            
        }

        private void saveStripMenuItem_Click(object sender, EventArgs e)
        {

            setWallpaper(originalWallpaper);//sets the original wallpaper
            notifyIcon1.BalloonTipText = "Original wallpaper has been restored!";
            
        }

        private async Task<string> downloadWallpaperAsync(string wallpaperURL)
        {
            var response = await Client.GetAsync(wallpaperURL);
            var fileName = Path.GetFileName(wallpaperURL);//gets complete filename with extension from our obtained URL
            var wallpaperPath = saveToPath;//gets the path to My Pictures directory independent of user

            if (File.Exists(wallpaperPath))//if same file already exists, just return it
                return wallpaperPath;

            wallpaperPath = Path.Combine(wallpaperPath, fileName);//path on disk where wallpaper is stored
            using (var fs = new FileStream(wallpaperPath,FileMode.CreateNew))//using automatically disposes of our filestream fs once task completes
            {
                await response.Content.CopyToAsync(fs);//store wallpaper onto disk
            }
            return wallpaperPath;
        }
        
       
    }
}
