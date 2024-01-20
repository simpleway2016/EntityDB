﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EJClient
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        class logininfo
        {
            public string url;
            public string username;
            public List<string> history;
        }
        public static Login instance;
        logininfo Logininfo;
        public Login()
        {
            instance = this;
            //System.IO.File.ReadAllText(@"D:\注释\2016\EasyJobCore\Way.EJServer\bin\Debug\netcoreapp1.0\web\a.txt").ToJsonObject<Net.RemotingClient.ResultInfo<Way.EntityDB.WayDataTable>>();
            InitializeComponent();
            try
            {
                Logininfo = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "url.txt").ToJsonObject<logininfo>();
                txtAddress.Text = Logininfo.url;
                txtUserName.Text = Logininfo.username;
                if(Logininfo.history != null)
                {
                    txtAddress.ItemsSource = Logininfo.history;
                }
                else
                {
                    Logininfo.history = new List<string>();
                }
            }
            catch
            {
                Logininfo = new logininfo();
                Logininfo.history = new List<string>();
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            instance = null;
            base.OnClosing(e);
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
               

                string url = txtAddress.Text;
                while (url.EndsWith("/"))
                    url = url.Substring(0, url.Length - 1);
                //if (url.StartsWith("https://") == false)
                //    throw new Exception("url must start with https://");
                Helper.Client = new Net.RemotingClient(url);

                this.Cursor = Cursors.Wait;

                Helper.Client.Invoke<int[]>("Login", (result, error) =>
               {
                   this.Cursor = null;
                   if (error != null)
                   {
                       MessageBox.Show(this, error);
                   }
                   else
                   {
                       if (Application.Current.MainWindow  != this)
                       {
                           this.DialogResult = true;
                           return;
                       }

                       Helper.WebSite = url;
                       Helper.CurrentUserRole = (EJ.User_RoleEnum)result[0];
                       Helper.CurrentUserID = result[1];

                       if(Logininfo.history.Contains(Helper.WebSite) == false)
                             Logininfo.history.Add(Helper.WebSite);

                       Logininfo.url = Helper.WebSite;
                       Logininfo.username = txtUserName.Text.Trim();

                       System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "url.txt", Logininfo.ToJsonString());
                       if (Helper.CurrentUserRole == EJ.User_RoleEnum.客户端测试人员)
                       {
                           Application.Current.MainWindow = new Forms.BugCenter.BugRecorder();
                       }
                       else
                       {
                           Application.Current.MainWindow = new MainWindow();
                       }
                       Application.Current.MainWindow.Show();
                       this.Close();
                   }

               }, txtUserName.Text.Trim(), txtPwd.Password);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }
    }
}
