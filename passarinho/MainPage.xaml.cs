using Windows.Security.EnterpriseData;

namespace passarinho;

public partial class MainPage : ContentPage
{
    
const int gravidade =1 ;
const int tempoEntreFrames=25;
bool estaMorto = false;
double larguraJanela=0;
double alturaJanela=0;
int velocidade= 20;



	public MainPage()
	{
		InitializeComponent();
	}

void AplicaGravidade()
{
    Passaro.TranslationY += gravidade;
}
 public async void Desenha()
 {
	while (!estaMorto)
	{
		AplicaGravidade();
		await Task.Delay(tempoEntreFrames);
		GerenciaCanos ();
	}
 }

 void Oi (object s, TappedEventArgs e)
 {
	FrameGameOver.IsVisible = false;
	estaMorto = false;
	Inicializar();
	Desenha();
 }

	void Inicializar()
	{
		Passaro.TranslationY = 0;
	}

	
		protected override void OnSizeAllocated ( double w, double h)
		{
			base.OnSizeAllocated ( w,h);
			larguraJanela= w;
			alturaJanela= h ;
		}

		void GerenciaCanos ()
		{
			imgcanocima.TranslationX -= velocidade;
			imgcanobaixo.TranslationX -= velocidade;
			if ( imgcanobaixo.TranslationX < -larguraJanela)
			{
				imgcanobaixo.TranslationX = 0;
				imgcanocima.TranslationX =0;
			}
		}
	
}

