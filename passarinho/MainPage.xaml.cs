using Windows.Security.EnterpriseData;

namespace passarinho;

public partial class MainPage : ContentPage
{

	const int gravidade = 7;
	const int tempoEntreFrames = 25;
	bool estaMorto = false;
	double larguraJanela = 0;
	double alturaJanela = 0;
	int velocidade = 20;
	const int forcaPulo=30;
	const int maxTempoPulando=3;//frames
	bool estaPulando =false;
	int tempoPulando=0;
    const int aberturaMinima= 100;



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

		{if(estaPulando)
			  AplicaPulo();
			  else 
			  AplicaGravidade();
			GerenciaCanos();
			if (VerificaColisao())
			{
				estaMorto = true;
				FrameGameOver.IsVisible = true;
				break;
			}
			await Task.Delay(tempoEntreFrames);
			

		}
	}

	void Oi(object s, TappedEventArgs e)
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


	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		larguraJanela = w;
		alturaJanela = h;
	}

	void GerenciaCanos()
	{
		imgcanocima.TranslationX -= velocidade;
		imgcanobaixo.TranslationX -= velocidade;
		if (imgcanobaixo.TranslationX < -larguraJanela)
		{
			var alturaMax= -100;
			var  alturaMin=-imgcanobaixo.HeightRequest;
			imgcanocima.TranslationY= Random.Shared.Next ((int) alturaMin, (int) alturaMax);
			imgcanobaixo.TranslationY= imgcanocima.TranslationY +aberturaMinima; //+imgcanobaixo.HeightRequest
		}
	    
	}

	bool VerificaColisao()
	{
		if (!estaMorto)
		{
			if (VerificaColisaoTeto() ||
			VerificaColisaoChao())
			{
				return true;
			}
		}
		return false;
	}
	bool VerificaColisaoTeto()
	{
		var minY = -alturaJanela / 2;
		if (Passaro.TranslationY <= minY)
			return true;
		else
			return false;
	}
	bool VerificaColisaoChao()
	{
		var maxY = alturaJanela / 2 - chao.HeightRequest;
		if (Passaro.TranslationY >= maxY)
			return true;
		else return false;
	}

	void AplicaPulo()
	{
		Passaro.TranslationY-= forcaPulo;
		tempoPulando ++;
		if (tempoPulando>= maxTempoPulando)
		{
			estaPulando= false;
			tempoPulando=0;
		}
	}

	void OnGridClieked (Object s, TappedEventArgs a)
	{
		estaPulando= true;
	}

}

