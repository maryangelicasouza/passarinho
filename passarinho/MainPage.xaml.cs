using Windows.Security.EnterpriseData;

namespace passarinho;

public partial class MainPage : ContentPage
{

	const int gravidade = 5;
	const int tempoEntreFrames = 25;
	bool estaMorto = false;
	double larguraJanela = 0;
	double alturaJanela = 0;
	int velocidade = 10;
	const int forcaPulo = 30;
	const int maxTempoPulando = 3;//frames
	bool estaPulando = false;
	int tempoPulando = 0;
	const int aberturaMinima = 300;
	int score = 0;



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
			if (estaPulando)
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
		score = 0;
		imgcanocima.TranslationX = -larguraJanela;
		imgcanobaixo.TranslationX = -larguraJanela;
		Passaro.TranslationX=0;
		Passaro.TranslationY = 0;
		Passaro.TranslationX=0;
		Passaro.TranslationX=0;
		GerenciaCanos();

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
			imgcanobaixo.TranslationX = 0;
			imgcanocima.TranslationX = 0;
			var alturaMax = -100;
			var alturaMin = -imgcanobaixo.HeightRequest;
			imgcanocima.TranslationY = Random.Shared.Next((int)alturaMin, (int)alturaMax);
			imgcanobaixo.TranslationY = imgcanocima.TranslationY + aberturaMinima + imgcanobaixo.HeightRequest;

			score++;
			labelScore.Text = "Canos :" + score.ToString("D3");
			inicio.Text = "Você passou por " + score.ToString("D3") + " canos!";


		}

	}

	bool VerificaColisao()
	{
		if (!estaMorto)
		{
			if (VerificaColisaoTeto() ||
			VerificaColisaoChao() ||
			VerificaColisaoCanoCima()||
			VerificaColisaoCanobaixo()
			)
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

	bool VerificaColisaoCanoCima()
	{
		var posHPassaro= (larguraJanela/2 )- (Passaro.WidthRequest/2 );
		var posVPassaro= (alturaJanela/2)- (Passaro.HeightRequest/2 ) + Passaro.TranslationY;
		if ( posHPassaro >=Math.Abs (imgcanocima.TranslationX) -imgcanocima.WidthRequest &&
		posHPassaro <=Math.Abs (imgcanocima.TranslationX) + imgcanocima. WidthRequest &&
		posVPassaro <= imgcanocima.HeightRequest + imgcanocima. TranslationY)
		{
			return true;
		}
		else 
		{
			return false;
		}
	}

	bool VerificaColisaoCanobaixo()
	{
		var posHPassaro= (larguraJanela/2 )- (Passaro.WidthRequest/2 );
		var posVPassaro= (alturaJanela/2)- (Passaro.HeightRequest/2 ) + Passaro.TranslationY;
		if ( posHPassaro <=Math.Abs (imgcanobaixo.TranslationX) -imgcanobaixo.WidthRequest &&
		posHPassaro >=Math.Abs (imgcanobaixo.TranslationX) + imgcanobaixo. WidthRequest &&
		posVPassaro >= imgcanobaixo.HeightRequest + imgcanobaixo. TranslationY)
		{
			return true;
		}
		else 
		{
			return false;
		}
	}




	void AplicaPulo()
	{
		Passaro.TranslationY -= forcaPulo;
		tempoPulando++;
		if (tempoPulando >= maxTempoPulando)
		{
			estaPulando = false;
			tempoPulando = 0;
		}
	}

	void OnGridClieked(Object s, TappedEventArgs a)
	{
		estaPulando = true;
	}

}

