using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PresionArterial.Models;

namespace PresionArterial
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class controlPresion : ContentPage
    {

        private string[] declaracionPrecion = new string[7]
        {
            "presion Baja",
            "Presion Optima",
            "Presion Normal",
            "Presion Alta Normal",
            "Hipertensión Grado 1",
            "Hipertensión Grado 2",
            "Hipertensión Grado 3"
        };
        public controlPresion(Usuario user)
        {
            InitializeComponent();
            BindingContext = user;
            listPresion();
        }


        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            if (validation())
            {
                if (onlyNumber())
                {
                    int systole = int.Parse(txtSistolica.Text);
                    int diastole = int.Parse(txtDiastolica.Text);

                    int message1 = statusSystole(systole);
                    int message2 = statusDiastole(diastole);

                    string message = declaracionPrecion[message1 > message2 ? message1 : message2];

                    Models.PresionArterial presion = new Models.PresionArterial
                    {
                        User_id = int.Parse(txtIdUser.Text),
                        Diastole = diastole,
                        Systole = systole,
                        Date = DateTime.Now,
                        StatusPresion = message
                    };

                    await App.StartConection.saveUpdatePresionAsync(presion);
                    clean();
                    await DisplayAlert("Alert", "Registro Guardado Correctamente", "OK");
                    listPresion();
                }
                else
                {
                    await DisplayAlert("Advertencia", "El formato es incorrecto, solo se aceptan numeros.", "OK");
                }                
            }
            else
            {
                await DisplayAlert("Alerta", "Campos Vacios", "OK");
            }

        }

        private async void listPresion()
        {
            int idUser = int.Parse(txtIdUser.Text);
            var data = await App.StartConection.listPresionAsync(idUser);
            if (data != null)
            {
                listNewsPresion.ItemsSource = data;
            }
        }

        private void clean()
        {
            txtIdPresion.Text = "";
            txtDiastolica.Text = "";
            txtSistolica.Text = "";            
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {

            var obj = await App.StartConection.getPresionByIdAsync(int.Parse(txtIdPresion.Text));
            if(obj != null)
            {
                await App.StartConection.deletePresionAsync(obj);
                clean();
                await DisplayAlert("Alert", "Registro Eliminado Correctamente", "OK");
                hideUpdate();
                listPresion();
            }
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {

            if (validation())
            {

                if (onlyNumber())
                {
                    int systole = int.Parse(txtSistolica.Text);
                    int diastole = int.Parse(txtDiastolica.Text);

                    int message1 = statusSystole(systole);
                    int message2 = statusDiastole(diastole);

                    string message = declaracionPrecion[message1 > message2 ? message1 : message2];

                    Models.PresionArterial presion = new Models.PresionArterial
                    {
                        User_id = int.Parse(txtIdUser.Text),
                        bloodPressure_id = int.Parse(txtIdPresion.Text),
                        Diastole = int.Parse(txtDiastolica.Text),
                        Systole = int.Parse(txtSistolica.Text),
                        StatusPresion = message,
                        Date = txtDate.Date
                    };
                    await App.StartConection.saveUpdatePresionAsync(presion);
                    clean();
                    await DisplayAlert("Alert", "Registro Actualizado Correctamente", "OK");
                    listPresion();
                    hideUpdate();
                }
                else
                {
                    await DisplayAlert("Advertencia", "El formato es incorrecto, solo se aceptan numeros.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Alerta", "Campos Vacios", "OK");
            }

            
        }

        private bool onlyNumber()
        {
            if (!txtDiastolica.Text.ToCharArray().All(Char.IsDigit) || !txtSistolica.Text.ToCharArray().All(Char.IsDigit))
            {                
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnCancelar_Clicked(object sender, EventArgs e)
        {
            hideUpdate();
            clean();
        }

        private void hideUpdate()
        {
            btnUDC.IsVisible = false;
            btnGuardar.IsVisible = true;
        }

        private bool validation()
        {
            bool response = true;

            if (string.IsNullOrEmpty(txtSistolica.Text))
            {
                response = false;
            }
            else if (string.IsNullOrEmpty(txtDiastolica.Text))
            {
                response = false;
            }

            return response;

        }

        private int statusSystole(int systole)
        {
            int message;

            if (systole < 105)
            {
                message = 0;
            }
            else if (systole < 120)
            {
                message = 1;
            }
            else if (systole < 130)
            {
                message = 2;
            }
            else if (systole <= 139)
            {
                message = 3;
            }
            else if (systole <= 159)
            {
                message = 4;
            }
            else if (systole <= 179)
            {
                message = 5;
            }
            else
            {
                message = 6;
            }

            return message;
        }

        private int statusDiastole(int diastole)
        {
            int message;

            if (diastole < 60)
            {
                message = 0;
            }
            else if (diastole < 80)
            {
                message = 1;
            }
            else if (diastole < 85)
            {
                message = 2;
            }
            else if (diastole <= 89)
            {
                message = 3;
            }
            else if (diastole <= 99)
            {
                message = 4;
            }
            else if (diastole <= 109)
            {
                message = 5;
            }
            else
            {
                message = 6;
            }

            return message;
        }

        private void listNewsPresion_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Models.PresionArterial)e.SelectedItem;
            if (obj != null)
            {
                txtIdPresion.Text = obj.bloodPressure_id.ToString();
                txtDiastolica.Text = obj.Diastole.ToString();
                txtSistolica.Text = obj.Systole.ToString();
                txtDate.Date = obj.Date;
            }
            btnGuardar.IsVisible = false;
            btnUDC.IsVisible = true;
        }
    }
}