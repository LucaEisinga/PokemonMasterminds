using System.Diagnostics;
using PokemonMasterminds.ViewModels;
using PokemonMasterminds.Model;
using System;

namespace PokemonMasterminds.Pages;

public partial class GamePage : ContentPage
{
    public IDispatcherTimer timer;
    private DateTime startTime;
    private int duration;
    private double progress;

    public GamePage()
   {
        BindingContext = new QuestionViewModel(Game.Instance);
        InitializeComponent();

        startTime = DateTime.Now;

        UpdateArc();
    }

    //Adding the timer and setting the rules applied to the timer
    private async void UpdateArc()
    {
        if (Game.Instance.Count >= 0 && Game.Instance.Count < 5)
        {
            duration = 16;
        }
        else if (Game.Instance.Count > 4 && Game.Instance.Count < 10)
        {
            duration = 11;
        }
        else if (Game.Instance.Count > 9 && Game.Instance.Count < 15)
        {
            duration = 6;
        }
        else if (Game.Instance.Count > 14)
        {
            
            duration = 0;
        }

        TimeSpan elapsedTime = DateTime.Now - startTime;
        
        while(elapsedTime.TotalSeconds <= duration && Game.Instance.GameIsActive)
        {
            elapsedTime = DateTime.Now - startTime;
            int secondsRemaining = (int)(duration - elapsedTime.TotalSeconds);

            timerLabel.Text = $"{secondsRemaining}";

            progress = Math.Ceiling(elapsedTime.TotalSeconds);
            progress %= duration;

            if (secondsRemaining == 0)
                {
                    Game.Instance.Count++;
                    Debug.WriteLine(Game.Instance.Count.ToString());
                    if (Game.Instance.Count >= 0 && Game.Instance.Count < 15)
                    {
                        AddScorePointToPlayer();
                        await Navigation.PushAsync(new GamePage());
                    }
                    else if (Game.Instance.Count > 14)
                    {
                        AddScorePointToPlayer();
                        await Navigation.PushAsync(new Scoreboard());
                    }
                    
                    await Task.Delay(500);

                    return;
                }

            await Task.Delay(500);
        }
            
        ResetView();
    }

    private void AddScorePointToPlayer()
    {
        if (Game.Instance.SelectedAnswer == Game.Instance.CurrentQuestion.getCorrectAnswer())
        {
            Game.Instance.Lobby.Player.Score++;
            Game.Instance.SetToQuestionNull();
        }
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
        AnswerOne.TextColor = Colors.Black;
        AnswerTwo.BackgroundColor = Colors.DarkCyan;
        AnswerThree.BackgroundColor = Colors.DarkCyan;
        AnswerFour.BackgroundColor = Colors.DarkCyan;

        Game.Instance.SelectedAnswer = Game.Instance.CurrentQuestion.Answers[0];
    }

    async void OnAnswerTwoClicked(object sender, EventArgs e)
    {
        AnswerOne.BackgroundColor = Colors.DarkCyan;
        AnswerTwo.BackgroundColor = Colors.DarkGray;
        AnswerTwo.TextColor = Colors.Black;
        AnswerThree.BackgroundColor = Colors.DarkCyan;
        AnswerFour.BackgroundColor = Colors.DarkCyan;
        
        Game.Instance.SelectedAnswer = Game.Instance.CurrentQuestion.Answers[1];
    }

    async void OnAnswerThreeClicked(object sender, EventArgs e)
    {
        AnswerOne.BackgroundColor = Colors.DarkCyan;
        AnswerTwo.BackgroundColor = Colors.DarkCyan;
        AnswerThree.BackgroundColor = Colors.DarkGray;
        AnswerThree.TextColor = Colors.Black;
        AnswerFour.BackgroundColor = Colors.DarkCyan;
        
        Game.Instance.SelectedAnswer = Game.Instance.CurrentQuestion.Answers[2];
    }

    async void OnAnswerFourClicked(object sender, EventArgs e)
    {
        AnswerOne.BackgroundColor = Colors.DarkCyan;
        AnswerTwo.BackgroundColor = Colors.DarkCyan;
        AnswerThree.BackgroundColor = Colors.DarkCyan;
        AnswerFour.BackgroundColor = Colors.DarkGray;
        AnswerFour.TextColor = Colors.Black;
        
        Game.Instance.SelectedAnswer = Game.Instance.CurrentQuestion.Answers[3];
    }
    
    async void OnScoreBoardClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Pages.Scoreboard());
    }
}
