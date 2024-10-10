using Windows.Security.EnterpriseData;

namespace passarinho;

public partial class MainPage : ContentPage
{
    
const int gravidade =1 ;
const int tempoEntreFrames=25;
bool estaMorto = false;


	public MainPage()
	{
		InitializeComponent();
	}

void AplicaGravidade(){
    passaro.TraslationY += gravidade;
}
Protected override void OnAppearing()
{
    base.OnAppearing();
    Desenha();
}
}

