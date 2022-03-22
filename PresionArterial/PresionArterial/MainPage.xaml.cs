using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using PresionArterial.Models;

namespace PresionArterial
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            txtDate.MaximumDate = DateTime.Now;
            clean();
            ListUsers();
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {


            if (validation())
            {

                    DateTime d = txtDate.Date.Date;
                    Usuario user = new Usuario
                    {
                        Name = txtName.Text,
                        Date_of_birth = d
                    };
                    await App.StartConection.saveUpdateUserAsync(user);
                    clean();
                    await DisplayAlert("Alerta", "Registro Guardado Correctamente", "OK");
                    ListUsers();

                
            }
            else
            {
                await DisplayAlert("Alerta", "Campos Vacios", "OK");
            }
        }

        /* private bool onlyLetter()
        {
            if (!txtName.Text.ToCharArray().All(Char.IsLetter))
            {
                return false;
            }
            else
            {
                return true;
            }
        } */

        private async void ListUsers()
        {
            var data = await App.StartConection.ListUsersAsync();
            if (data != null)
            {
                listNewUsers.ItemsSource = data;
            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var user = await App.StartConection.getUserByIdAsync(int.Parse(txtId.Text));
            if (user != null)
            {
                await App.StartConection.deleteUserAsync(user);
                clean();
                await DisplayAlert("Alerta", "Registro Eliminado Correctamente", "OK");
                ListUsers();
                btnGuardar.IsVisible = true;
                btnUDC.IsVisible = false;
            }
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            //devuelve true si esta vacio
            if (validation())
            {

                    Usuario user = new Usuario()
                    {
                        id = int.Parse(txtId.Text),
                        Name = txtName.Text,
                        Date_of_birth = txtDate.Date
                    };
                    await App.StartConection.saveUpdateUserAsync(user);
                    clean();
                    await DisplayAlert("Alerta", "Registro Actualizado Correctamente", "OK");
                    ListUsers();
                    btnGuardar.IsVisible = true;
                    btnUDC.IsVisible = false;

            }
            else
            {
                await DisplayAlert("Alerta", "Campos Vacios", "OK");
            }
        }

        private void btnCancelar_Clicked(object sender, EventArgs e)
        {
            btnGuardar.IsVisible = true;
            btnUDC.IsVisible = false;
            clean();
        }

        private async void listNewUsers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Usuario)e.SelectedItem;
            
            btnGuardar.IsVisible = false;
            btnUDC.IsVisible = true;

            if ( !string.IsNullOrEmpty(obj.id.ToString()) )
            {
                var user = await App.StartConection.getUserByIdAsync(obj.id);

                if (user != null)
                {
                    txtId.Text = user.id.ToString();
                    txtName.Text = user.Name;
                    txtDate.Date = user.Date_of_birth;
                }
            }
        }
        private bool validation()
        {
            bool response = true;
            if ( string.IsNullOrEmpty(txtName.Text) )
            {
                response = false;            
            }
            return response;
        }

        private void clean()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtDate.Date = Convert.ToDateTime("01/01/1998");
        }

        private void btnGoControl_Clicked(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            Usuario user = (Usuario)btn.BindingContext;
            Navigation.PushAsync(new controlPresion(user));
            
        }

        private void listNewUsers_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //listNewUsers.ItemTapped = null;
        }
    }
}
