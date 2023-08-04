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
    private int Count;

   public GamePage()
   {
        BindingContext = new QuestionViewModel(Game.Instance);
        InitializeComponent();

        startTime = DateTime.Now;

        UpdateArc();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Start the timer when the page appears on the screen
        startTime = DateTime.Now;
        cancellationTokenSource = new CancellationTokenSource();
        UpdateArc();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        // Stop the timer when the page is removed from the screen
        cancellationTokenSource.Cancel();
    }

    //Adding the timer and setting the rules applied to the timer
    private async void UpdateArc()
    {
        TimeSpan elapsedTime = DateTime.Now - startTime;
        
        while(elapsedTime.TotalSeconds <= 16 && Game.Instance.GameIsActive)
        {
            elapsedTime = DateTime.Now - startTime;
            int secondsRemaining = (int)(duration - elapsedTime.TotalSeconds);

            timerLabel.Text = $"{secondsRemaining}";

            progress = Math.Ceiling(elapsedTime.TotalSeconds);
            progress %= duration;
                if (secondsRemaining == 0)
                {
                    await Navigation.PushAsync(new Pages.GamePage());
                    await Task.Delay(800);
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

    // Setting the awnsers for players
    async void OnAnswerOneClicked(object sender, EventArgs e)
    {
        AnswerOne.BackgroundColor = Colors.DarkGray;
        AnswerTwo.BackgroundColor = Colors.DarkCyan;
        AnswerThree.BackgroundColor = Colors.DarkCyan;
        AnswerFour.BackgroundColor = Colors.DarkCyan;
    }

    async void OnAnswerTwoClicked(object sender, EventArgs e)
    {
        AnswerOne.BackgroundColor = Colors.DarkCyan;
        AnswerTwo.BackgroundColor = Colors.DarkGray;
        AnswerThree.BackgroundColor = Colors.DarkCyan;
        AnswerFour.BackgroundColor = Colors.DarkCyan;
    }

    async void OnAnswerThreeClicked(object sender, EventArgs e)
    {
        AnswerOne.BackgroundColor = Colors.DarkCyan;
        AnswerTwo.BackgroundColor = Colors.DarkCyan;
        AnswerThree.BackgroundColor = Colors.DarkGray;
        AnswerFour.BackgroundColor = Colors.DarkCyan;
    }

    async void OnAnswerFourClicked(object sender, EventArgs e)
    {
        AnswerOne.BackgroundColor = Colors.DarkCyan;
        AnswerTwo.BackgroundColor = Colors.DarkCyan;
        AnswerThree.BackgroundColor = Colors.DarkCyan;
        AnswerFour.BackgroundColor = Colors.DarkGray;
    }
    
    async void OnScoreBoardClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Pages.Scoreboard());
    }
}
