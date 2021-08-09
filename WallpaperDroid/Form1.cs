using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WallpaperDroid
{
    public partial class tagForm : MetroFramework.Forms.MetroForm
    {
        //random wallhaven.cc image search API link. sorting=random guarantees unique image almost everytime
        //category=111 includes all categories ->  general, anime, people
        static string apiURL = "https://wallhaven.cc/api/v1/search?categories=111&sorting=random";
        private static readonly HttpClient Client = new HttpClient(); //MSDN says instantiate once per program

        /*static int listCount = 0;*/


        static string[] apiURLRandom = new string[3];
        /*static string[] searchTag = new string[3];//searchTag[0] to searchTag[4] for 5 search tags */

        string screenWidth = Screen.PrimaryScreen.Bounds.Width.ToString();//screen width
        string screenHeight = Screen.PrimaryScreen.Bounds.Height.ToString();//screen height

        string saveToPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\WPDroid";

        //to get current wallpaper location to revert back to it if user desires
        string originalWallpaper = Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", 0).ToString();//(keyname,valuename,default value if valuename not found)

        [DllImport("user32.dll", CharSet = CharSet.Auto)]//import user32.dll for setting wallpapers
        static extern bool SystemParametersInfo(uint uiAction, uint uiParam, string pvParam, uint fwinIni);//SystemParametersInfo API call. Refer msdn docs for SPI_SETDESKWALLPAPER

        private int _ticks;//track the no of ticks

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
            try
            {
                File.Copy(originalWallpaper, Path.Combine(saveToPath, Path.GetFileName(originalWallpaper)), true);//overwrite=true
            }
            catch { }


            screenResLabel.Text = "Your screen resolution: " + screenWidth + " x " + screenHeight;//shows resolution info
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();//minimize to tray once filters have been provided
            Array.Clear(apiURLRandom, 0, 3);//reset array everytime button is pressed before assigning again below

            //apply custom resolution and keywords

            /*for (int i = 0; i < tagListBox.Items.Count; i++)
            {
                //array of apiURLs that will be chosen at random to give room for all tags to take effect
                apiURLRandom[i] = "https://wallhaven.cc/api/v1/search?q=" + searchTag[i] + "&categories=111&atleast=" + screenWidth + "x" + screenHeight + "&sorting=random";
            }*/

            try
            {
                apiURLRandom[0] = "https://wallhaven.cc/api/v1/search?q=" + tagListBox.Items[0] + "&categories=111&atleast=" + screenWidth + "x" + screenHeight + "&sorting=random";
                apiURLRandom[1] = "https://wallhaven.cc/api/v1/search?q=" + tagListBox.Items[1] + "&categories=111&atleast=" + screenWidth + "x" + screenHeight + "&sorting=random";
                apiURLRandom[2] = "https://wallhaven.cc/api/v1/search?q=" + tagListBox.Items[2] + "&categories=111&atleast=" + screenWidth + "x" + screenHeight + "&sorting=random";
            }
            catch { }


        }

        private void restoreStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Show();

        }

        private async void nextStripMenuItem_Click(object sender, EventArgs e)
        {
            /*//get list of non-empty indices in searchTag
            List<int> indexesNotNull = new List<int>();
            for (int i = 0; i < searchTag.Length; i++)
            {
                if (searchTag[i] != null)
                {
                    indexesNotNull.Add(i);
                }
            }*/

            if (tagListBox.Items.Count == 0)
            {
                apiURL = "https://wallhaven.cc/api/v1/search?categories=111&atleast=" + screenWidth + "x" + screenHeight + "&sorting=random";
            }
            else
            {
                //select random apiURL from apiURLRandom[0] to apiURLRandom[maxindex]
                Random rand = new Random();
                apiURL = apiURLRandom[rand.Next(0, tagListBox.Items.Count)];
            }

            bool tryagain = true;

            while (tryagain)
            {
                //get API response from wallhaven with async and await so our UI doesn't lag while it receives wallpapers
                try
                {
                    var apiResponse = await getTopWallpapersAsync();
                    var wallpaperURL = apiResponse.data.First().path;//the first link inside data will be stored
                    nextStripMenuItem.Enabled = false;

                    //code to set this wallpaper//
                    var wallpaperPath = await downloadWallpaperAsync(wallpaperURL);//download fetched wallpaperURL
                    setWallpaper(wallpaperPath);//set desktop wallpaper
                    tryagain = false;//if no exception occurs, no need of running try block again
                    nextStripMenuItem.Enabled = true;
                }

                catch (IOException)
                {
                    tryagain = true;//if file already exists, try again
                    nextStripMenuItem.Enabled = true;
                    return;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please try again later! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


        }


        private static async Task<SearchResponse> getTopWallpapersAsync()
        {
            var response = await Client.GetAsync(apiURL);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SearchResponse>(json);

        }

        private void setWallpaper(string wallpaperPath)
        {
            SystemParametersInfo(0x0014, 0, wallpaperPath, 0x0001);
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
            using (var fs = new FileStream(wallpaperPath, FileMode.CreateNew))//using automatically disposes of our filestream fs once task completes
            {
                await response.Content.CopyToAsync(fs);//store wallpaper onto disk
            }
            return wallpaperPath;
        }

        private void searchTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //doesn't allow white spaces to be typed in
            e.Handled = e.KeyChar != (char)Keys.Back && !char.IsLetter(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        private void addTagButton_Click(object sender, EventArgs e)
        {

            if (searchTextBox.Text == "")
            {
                MessageBox.Show("Search tag can't be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (tagListBox.Items.Count >= 3)
            {
                MessageBox.Show("Sorry! You can't add more than 3 tags at a time.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                /*listCount = 3;*/
                return;
            }

            tagListBox.Items.Add(searchTextBox.Text);
            searchTextBox.Text = "";

            /*try
            {
                searchTag[listCount] = tagListBox.Items[listCount].ToString();
                listCount++;
            }
            catch { }*/



        }

        private void removeTagButton_Click(object sender, EventArgs e)
        {
            try
            {
                tagListBox.Items.RemoveAt(tagListBox.SelectedIndex);

            }
            catch { }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please specify the time interval between wallpaper changes!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (Int32.Parse(textBox1.Text)<10)
            {
                MessageBox.Show("Interval needs to be at least 10 seconds!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (button2.Text == "Schedule task")
            {
                button2.Text = "Stop task";
                button1.Enabled = false;
                addTagButton.Enabled = false;
                removeTagButton.Enabled = false;
                _ticks = 0;
                nextTaskCountTimer.Start();
                scheduleTimer.Interval = Int32.Parse(textBox1.Text) * 1000;
                scheduleTimer.Start();
                dueChangeLabel.Visible = true;

            }
            else if(button2.Text == "Stop task")
            {
                button2.Text = "Schedule task";
                button1.Enabled = true;
                addTagButton.Enabled = true;
                removeTagButton.Enabled = true;
                nextTaskCountTimer.Stop();
                scheduleTimer.Stop();
                dueChangeLabel.Visible = false;

            }
                        
        }


        private void nextTaskCountTimer_Tick(object sender, EventArgs e)
        {               
            _ticks++;//increases every second
            if (_ticks == Int32.Parse(textBox1.Text))
            {//reset _ticks once _ticks=entered interval
                _ticks = 0;

            }
            dueChangeLabel.Text = "Next change due in: " + (Int32.Parse(textBox1.Text) - _ticks).ToString();
        }

        private void scheduleTimer_Tick(object sender, EventArgs e)
        {
            
           nextStripMenuItem_Click(sender,e);//simply call next method
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //only allow numbers
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) /*&& (e.KeyChar != '.')*/)
            {
                e.Handled = true;
            }

            /*// only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }*/
        }

        private void tagForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.WindowState = FormWindowState.Normal;

                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(1000);
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }
    }
}
