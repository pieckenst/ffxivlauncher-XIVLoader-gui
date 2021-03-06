using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Media;
using System.Windows.Forms;





namespace XIVLoader_GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool isSteam = false;
        bool dx11 = true;

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string gamePath = gamepathTextbox.Text;

            string username = usernameBox.Text;

            passwordBox.PasswordChar = '*';

            string password = passwordBox.Text;
            string otp = otpBox.Text;

            int language = int.Parse(lngBox.Text);

            int expansionLevel = int.Parse(explvlBox.Text);

            int region = int.Parse(regionBox.Text);
            try
            {
                var sid = networklogic.GetRealSid(gamePath, username, password, otp, isSteam);
                if (sid.Equals("BAD"))
                    return;

                var ffxivGame = networklogic.LaunchGame(gamePath, sid, 1, dx11, expansionLevel, isSteam, region);



            }
            catch (Exception exc)
            {
                if (language == 0)
                {
                    Console.WriteLine("ログインに失敗しました。ログイン情報を確認するか、再試行してください.\n" + exc.Message);
                }
                if (language == 1)
                {
                    Console.WriteLine("Logging in failed, check your login information or try again.\n" + exc.Message);
                }
                if (language == 2)
                {
                    Console.WriteLine("Anmeldung fehlgeschlagen, überprüfe deine Anmeldedaten oder versuche es noch einmal.\n" + exc.Message);
                }
                if (language == 3)
                {
                    Console.WriteLine("Échec de la connexion, vérifiez vos informations de connexion ou réessayez.\n" + exc.Message);
                }
                if (language == 4)
                {
                    Console.WriteLine("Не удалось войти в систему, проверьте данные для входа или попробуйте еще раз.\n" + exc.Message);
                }

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(Directory.GetCurrentDirectory() + @"\gamepath.txt"))
            {
                TextReader tr = new StreamReader("gamepath.txt");
                string gamePathread = tr.ReadLine();
                gamepathTextbox.Text = gamePathread;
                tr.Close();
            }
            if (File.Exists(Directory.GetCurrentDirectory() + @"\gamepath.txt") && File.Exists(Directory.GetCurrentDirectory() + @"\password.txt") && File.Exists(Directory.GetCurrentDirectory() + @"\username.txt") && File.Exists(Directory.GetCurrentDirectory() + @"\booleansandvars.txt"))
            {
                DialogResult dialogResult = MessageBox.Show("Do you wish to load existing input?", "XIVLoader Input", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (File.Exists(Directory.GetCurrentDirectory() + @"\password.txt") && File.Exists(Directory.GetCurrentDirectory() + @"\username.txt"))
                    {
                        TextReader trx = new StreamReader("username.txt");
                        string usernameread = trx.ReadLine();
                        usernameBox.Text = usernameread;
                        trx.Close();
                        TextReader tr = new StreamReader("password.txt");
                        string passwordread = tr.ReadLine();
                        passwordBox.Text = passwordread;
                        tr.Close();
                    }
                    if (File.Exists(Directory.GetCurrentDirectory() + @"\booleansandvars.txt"))
                    {
                        TextReader tr = new StreamReader("booleansandvars.txt");
                        string languageread = tr.ReadLine();
                        string exlevelreader = tr.ReadLine();
                        string regionreader = tr.ReadLine();

                        lngBox.Text = languageread;
                        explvlBox.Text = exlevelreader;
                        regionBox.Text = regionreader;
                        tr.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Loading with default values");
                }
            }
            else {
                MessageBox.Show("Loading with default values");
            }

            this.Height = 305;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Directory.GetCurrentDirectory() + @"\booleansandvars.txt"))
            {
                TextWriter twxx = new StreamWriter("booleansandvars.txt");
                TextWriter trx = new StreamWriter("username.txt");
                TextWriter tr = new StreamWriter("password.txt");
                TextWriter trrx = new StreamWriter("gamepath.txt");
                string gamePath = gamepathTextbox.Text;

                string username = usernameBox.Text;
                string password = passwordBox.Text;
                string language = lngBox.Text;

                string expansionLevel = explvlBox.Text;

                string region = regionBox.Text;
                twxx.WriteLine(language);
                twxx.WriteLine(expansionLevel);
                twxx.WriteLine(region);
                trx.WriteLine(username);
                tr.WriteLine(password);
                trrx.WriteLine(gamePath);
                twxx.Close();
                trx.Close();
                tr.Close();
                trrx.Close();
            }
            else {
                TextWriter twxx = new StreamWriter("booleansandvars.txt");
                TextWriter trx = new StreamWriter("username.txt");
                TextWriter tr = new StreamWriter("password.txt");
                TextWriter trrx = new StreamWriter("gamepath.txt");
                string gamePath = gamepathTextbox.Text;

                string username = usernameBox.Text;
                string password = passwordBox.Text;
                string language = lngBox.Text;

                string expansionLevel = explvlBox.Text;

                string region = regionBox.Text;
                twxx.WriteLine(language);
                twxx.WriteLine(expansionLevel);
                twxx.WriteLine(region);
                trx.WriteLine(username);
                tr.WriteLine(password);
                trrx.WriteLine(gamePath);
                twxx.Close();
                trx.Close();
                tr.Close();
                trrx.Close();
            }
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            SoundPlayer sp = new SoundPlayer(XIVLoader_GUI.Properties.Resources.vanadiel_march_ffrk_arrange);
            if (checkBox1.Checked)
            {
                sp.Play();
            }
            else {
                sp.Stop();
            }
            
        }

        private void gamepathTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            
            if (checkBox2.Checked)
            {
                MessageBox.Show("Enabled the settings that allow gamepath , language , expansion level and region changing as well as enabling the launcher music");
                this.Height = 424;
            }
            else {
                this.Height = 305;
            }
            
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Gamepath is the path where your final fantasy xiv client is located \n Valid ffxiv language codes are 0 - Japanese , 1 - English , 2 - German , 3 - French \n  Valid region codes are 1- Japan , 2 - America , 3 - International \n Valid expansion level codes are 0- ARR - 1 - Heavensward - 2 - Stormblood - 3 - Shadowbringers \n To save your login information as well as settings please click the save settings button");
        }

        

        

    

        
    }
}
