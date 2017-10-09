using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JuegoOperaciones
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Juego : ContentPage
	{

        private int cantidadPreguntas;
        private int indicePreguntaActual = 0;
        private Operacion operacionActual;
        private int totalRespuestas = 0;

        public int Totalrespuestas
        {
            get
            {
                return totalRespuestas;
            }
        }

        public int CantidadPreguntas
        {
            get
            {
                return cantidadPreguntas;
            }
        }

        public event EventHandler FinalizarActivity;

		public Juego (int cantPreguntas)
		{
			InitializeComponent ();
            this.cantidadPreguntas = cantPreguntas;
            buttonContinuar.Clicked += OnContinuarButtonClick;
            generarOperacion();
		}

        private void generarOperacion()
        {
            indicePreguntaActual++;
            setTitulo();
            operacionActual = new Operacion();
            labelNumA.Text = operacionActual.NumA.ToString();
            labelSigno.Text = operacionActual.SignoToString(operacionActual.SignoOperacion);
            labelNumB.Text = operacionActual.NumB.ToString();
        }

        private void OnContinuarButtonClick(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(entryResultado.Text))
            {
                int resultado = operacionActual.SignoOperacion == Operacion.Signo.Mas ? operacionActual.NumA + operacionActual.NumB : operacionActual.NumA - operacionActual.NumB;
                if (resultado.ToString() == entryResultado.Text)
                {
                    totalRespuestas++;
                }

                entryResultado.Text = string.Empty;
                if (indicePreguntaActual < cantidadPreguntas)
                {
                    generarOperacion();
                }
                else
                {
                    //Salir
                    FinalizarActivity(this, EventArgs.Empty);
                    Navigation.PopAsync(true);
                    
                }
            }
            else
            {
                //Debe completar el resultado
            }
        }

        private void setTitulo()
        {
            labelTitulo.Text = string.Format("Pregunta {0} de {1}", indicePreguntaActual, cantidadPreguntas);
        }


        public class Operacion
        {
            public enum Signo
            {
                Mas,
                Menos
            }

            public int NumA { get; private set; }
            public Signo SignoOperacion { get; private set; }
            public int NumB { get; private set; }

            public Operacion()
            {
                Random r = new Random();
                this.NumA = r.Next(0,100);
                this.SignoOperacion = r.Next(0, 2) == 0 ? Signo.Mas : Signo.Menos;
                this.NumB = r.Next(0, 100);
            }

            public string SignoToString(Signo signo)
            {
                return signo == Signo.Mas? "+":"-";
            }
        }
	}
}