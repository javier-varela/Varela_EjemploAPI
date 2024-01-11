using Newtonsoft.Json;
using Varela_EjemploAPI.Models;
namespace Varela_EjemploAPI.Views;

public partial class ClimaActual : ContentPage
{
	public ClimaActual()
	{
		InitializeComponent();
	}

    private async void ButtonConsultar_Clicked(object sender, EventArgs e)
    {
		string latitud = Lat.Text;
		string longitud = Long.Text;
		
		if(Connectivity.NetworkAccess == NetworkAccess.Internet)
		{
            using (var client = new HttpClient())
			{
                string url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitud}&lon={longitud}&appid=2f05306529af619b4ce7a6bc9748fe30";
                var response = await client.GetAsync(url);
				if(response.IsSuccessStatusCode)
				{
					var json = await response.Content.ReadAsStringAsync();
					var clima = JsonConvert.DeserializeObject<Root>(json);

					ValorClima.Text = clima.weather[0].description.ToUpper();
					Imagen.Source = $"https://openweathermap.org/img/wn/{clima.weather[0].icon}@2x.png";
					ValorUbicacion.Text = clima.name+" - "+clima.sys.country;
				}
			}
		}

    }
}