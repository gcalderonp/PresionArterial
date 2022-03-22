using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PresionArterial.Data;
using PresionArterial.Models;
using System.IO;

namespace PresionArterial
{
    public partial class App : Application
    {

        static SqLitePresion db;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage
            {
                Title = "Control de Presion Arterial",

            });
        }

        public static SqLitePresion StartConection
        {
            get
            {
                if (db == null)
                {
                    db = new SqLitePresion(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "presion.db3"));
                }
                return db;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
