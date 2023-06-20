<<<<<<< Updated upstream
using System.Diagnostics;
=======
using PokemonMasterminds.ViewModels;

>>>>>>> Stashed changes
namespace PokemonMasterminds.Pages;

public partial class GamePage : ContentPage
{
    public IDispatcherTimer timer;
    private DateTime startTime;
    private readonly int duration = 15;
    private double progress;
    private CancellationTokenSource cancellationTokenSource = new();

	public GamePage()
	{
        BindingContext = new QuestionViewModel();
		InitializeComponent();

        startTime = DateTime.Now;
        cancellationTokenSource = new CancellationTokenSource();
        UpdateArc();
    }

    private async void UpdateArc()
    {
        while(!cancellationTokenSource.IsCancellationRequested)
        {
            TimeSpan elapsedTime = DateTime.Now - startTime;
            int secondsRemaining = (int)(duration - elapsedTime.TotalSeconds);

            timerLabel.Text = $"{secondsRemaining}";

            progress = Math.Ceiling(elapsedTime.TotalSeconds);
            progress %= duration;
            
            if (secondsRemaining == 0)
            {
                cancellationTokenSource.Cancel();
                return;
            }

            await Task.Delay(500);
        }

        ResetView();
    }

    private void ResetView()
    {
        progress = 0;
        timerLabel.Text = duration.ToString();
    }

    async void OnAnswerOneClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Pages.Scoreboard());
        cancellationTokenSource.Cancel();
    }

    async void OnAnswerTwoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Pages.Scoreboard());
        cancellationTokenSource.Cancel();
    }

    async void OnAnswerThreeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Pages.Scoreboard());
        cancellationTokenSource.Cancel();
    }

    async void OnAnswerFourClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Pages.Scoreboard());
        cancellationTokenSource.Cancel();
    }
}