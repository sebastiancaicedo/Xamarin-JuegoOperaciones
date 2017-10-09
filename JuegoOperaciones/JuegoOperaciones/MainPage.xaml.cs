using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JuegoOperaciones
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            buttonJugar.Clicked += OnButtonJugarClick;
		}

        private async void OnButtonJugarClick(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(entryNombre.Text) && pickerCantidad.SelectedIndex != -1)
            {
                string nombre = entryNombre.Text;
                int cantPreguntas = Convert.ToInt32(pickerCantidad.SelectedItem);
                Juego juego = new Juego(cantPreguntas);
                juego.FinalizarActivity += Juego_FinalizarActivity;
                await Navigation.PushAsync(juego);

            }
            else
            {
                await DisplayAlert("Error", "Uno o mas campos están vacios", "Aceptar");
            }
        }

        private void Juego_FinalizarActivity(object sender, EventArgs e)
        {
            Juego juego = (Juego)sender;
            DisplayAlert("Juego Finalizado", string.Format("Tuvo {0} preguntas correctas de {1}", juego.Totalrespuestas, juego.CantidadPreguntas), "Aceptar");
        }
    }
}
