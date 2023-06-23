using System.Diagnostics;
using PokemonMasterminds.ViewModels;
using PokemonMasterminds.Model;

namespace PokemonMasterminds.Pages;

public partial class GamePage : ContentPage
{
    public IDispatcherTimer timer;
    private DateTime startTime;
    private readonly int duration = 16;
    private double progress;
    private CancellationTokenSource cancellationTokenSource = new();
    private int Count;
    private Game game;

   public GamePage(Game game)
   {
       this.game = game;
        BindingContext = new QuestionViewModel(game);
        InitializeComponent();

        startTime = DateTime.Now;
        cancellationTokenSource = new CancellationTokenSource();

        UpdateArc();
    }

    private async void UpdateArc()
    {
        while (!cancellationTokenSource.IsCancellationRequested)
        {
            TimeSpan elapsedTime = DateTime.Now - startTime;
            int secondsRemaining = (int)(duration - elapsedTime.TotalSeconds);

            timerLabel.Text = $"{secondsRemaining}";

            progress = Math.Ceiling(elapsedTime.TotalSeconds);
            progress %= duration;
                if (secondsRemaining == 0)
                {
                    Game game = new Game();
                    await Navigation.PushAsync(new Pages.GamePage(game));
                    await Task.Delay(800);
                    cancellationTokenSource.Cancel();
                    Count++;
                    Debug.WriteLine(Count.ToString());
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

    async void OnAnswerTwoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Pages.Scoreboard(game));
        cancellationTokenSource.Cancel();
    }

    async void OnAnswerThreeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Pages.Scoreboard(game));
        cancellationTokenSource.Cancel();
    }

    async void OnAnswerFourClicked(object sender, EventArgs e)
    {

    }

    async void OnScoreBoardClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Pages.Scoreboard(game));
        cancellationTokenSource.Cancel();
    }
}
