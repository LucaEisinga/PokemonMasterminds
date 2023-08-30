using PokemonMasterminds.ViewModels;
using PokemonMasterminds.Model;

namespace PokemonMasterminds.Pages;

public partial class GamePage : ContentPage
{
    public IDispatcherTimer Timer;
    private DateTime _startTime;
    private int _duration;
    private double _progress;

    public GamePage()
   {
        BindingContext = new QuestionViewModel(Game.Instance);
        InitializeComponent();

        _startTime = DateTime.Now;

        UpdateArc();
    }

    //Adding the timer and setting the rules applied to the timer
    private async void UpdateArc()
    {
        if (Game.Instance.Count >= 0 && Game.Instance.Count < 5)
        {
            _duration = 16;
        }
        else if (Game.Instance.Count > 4 && Game.Instance.Count < 10)
        {
            _duration = 11;
        }
        else if (Game.Instance.Count > 9 && Game.Instance.Count < 15)
        {
            _duration = 6;
        }
        else if (Game.Instance.Count > 14)
        {
            
            _duration = 16;
        }

        TimeSpan elapsedTime = DateTime.Now - _startTime;
        
        while(elapsedTime.TotalSeconds <= _duration && Game.Instance.GameIsActive)
        {
            elapsedTime = DateTime.Now - _startTime;
            int secondsRemaining = (int)(_duration - elapsedTime.TotalSeconds);

            timerLabel.Text = $"{secondsRemaining}";

            _progress = Math.Ceiling(elapsedTime.TotalSeconds);
            _progress %= _duration;

            if (secondsRemaining == 0)
            {
                Game.Instance.Count++;
                
                //doorgaan zolang de hoeveelheid gestelde vragen tussen 0 en 15 is
                if (Game.Instance.Count >= 0 && Game.Instance.Count < 15)
                {
                    AddScorePointToPlayer();
                    await Task.Delay(1500);
                    await Navigation.PushAsync(new GamePage());
                }
                //stoppen zodra vraag 15 gesteld is, en naar het scoreboard navigeren
                else if (Game.Instance.Count > 14)
                {
                    AddScorePointToPlayer();
                    await Task.Delay(1500);
                    await Navigation.PushAsync(new Scoreboard());
                    Game.Instance.ResetGame();
                    return;
                }
                    
                await Task.Delay(500);

                return;
            }

            await Task.Delay(500);
        }
            
        ResetView();
    }

    //methode voor het toevoegen van een score punt aan de speler, indien juist geantwoord
    private void AddScorePointToPlayer()
    {
        //gegeven antwoord weergeven
        AddColorToAnswer();
        
        if (Game.Instance.SelectedAnswer == Game.Instance.CurrentQuestion.getCorrectAnswer())
        {
            Game.Instance.Lobby.Player.Score++;
        }
        Game.Instance.SetToQuestionNull();
    }

    private void AddColorToAnswer()
    {
        Answer selectedAnswer = Game.Instance.SelectedAnswer;
        Answer correctAnswer = Game.Instance.CurrentQuestion.getCorrectAnswer();
        
        //voor het tonen van of antwoord goed is in groen

        
         if (correctAnswer == Game.Instance.CurrentQuestion.Answers[0])
         {
             AnswerOne.BackgroundColor = Colors.Green;
         }
         else if (correctAnswer == Game.Instance.CurrentQuestion.Answers[1])
         {
             AnswerTwo.BackgroundColor = Colors.Green;
         }
         else if (correctAnswer == Game.Instance.CurrentQuestion.Answers[2])
         {
             AnswerThree.BackgroundColor = Colors.Green;
         }
         else if (correctAnswer == Game.Instance.CurrentQuestion.Answers[3])
         {
             AnswerFour.BackgroundColor = Colors.Green;
         }
        

        //voor het tonen van of antwoord fout is in rood
        if (selectedAnswer != correctAnswer)
        {
            if (selectedAnswer == Game.Instance.CurrentQuestion.Answers[0])
            {
                AnswerOne.BackgroundColor = Colors.Red;
            }
            else if (selectedAnswer == Game.Instance.CurrentQuestion.Answers[1])
            {
                AnswerTwo.BackgroundColor = Colors.Red;
            }
            else if (selectedAnswer == Game.Instance.CurrentQuestion.Answers[2])
            {
                AnswerThree.BackgroundColor = Colors.Red;
            }
            else if (selectedAnswer == Game.Instance.CurrentQuestion.Answers[3])
            {
                AnswerFour.BackgroundColor = Colors.Red;
            }
            
            timerLabel.Text = "Too bad! that's the wrong answer!";
        }
        else
        {
            timerLabel.Text = "Guessed 'em right!";
        }

        //voor wanneer er geen antwoord geselecteerd is
        if (selectedAnswer == null)
        {
            timerLabel.Text = "No answer selected! Times up!";
        }


    }

    private void ResetView()
    {
        _progress = 0;
        timerLabel.Text = _duration.ToString();
    }

    // Setting the awnsers for players
    async void OnAnswerOneClicked(object sender, EventArgs e)
    {
        SoundPlayer.Instance.PlayPlinkSound();
        
        AnswerOne.BackgroundColor = Colors.DarkGray;
        AnswerTwo.BackgroundColor = Colors.DarkCyan;
        AnswerThree.BackgroundColor = Colors.DarkCyan;
        AnswerFour.BackgroundColor = Colors.DarkCyan;
        
        AnswerOne.TextColor = Colors.Black;
        AnswerTwo.TextColor = Colors.White;
        AnswerThree.TextColor = Colors.White;
        AnswerFour.TextColor = Colors.White;

        Game.Instance.SelectedAnswer = Game.Instance.CurrentQuestion.Answers[0];
    }

    async void OnAnswerTwoClicked(object sender, EventArgs e)
    {
        SoundPlayer.Instance.PlayPlinkSound();
        
        AnswerOne.BackgroundColor = Colors.DarkCyan;
        AnswerTwo.BackgroundColor = Colors.DarkGray;
        AnswerThree.BackgroundColor = Colors.DarkCyan;
        AnswerFour.BackgroundColor = Colors.DarkCyan;
        
        AnswerOne.TextColor = Colors.White;
        AnswerTwo.TextColor = Colors.Black;
        AnswerThree.TextColor = Colors.White;
        AnswerFour.TextColor = Colors.White;
        
        Game.Instance.SelectedAnswer = Game.Instance.CurrentQuestion.Answers[1];
    }

    async void OnAnswerThreeClicked(object sender, EventArgs e)
    {
        SoundPlayer.Instance.PlayPlinkSound();
        
        AnswerOne.BackgroundColor = Colors.DarkCyan;
        AnswerTwo.BackgroundColor = Colors.DarkCyan;
        AnswerThree.BackgroundColor = Colors.DarkGray;
        AnswerFour.BackgroundColor = Colors.DarkCyan;
        
        AnswerOne.TextColor = Colors.White;
        AnswerTwo.TextColor = Colors.White;
        AnswerThree.TextColor = Colors.Black;
        AnswerFour.TextColor = Colors.White;
        
        Game.Instance.SelectedAnswer = Game.Instance.CurrentQuestion.Answers[2];
    }

    async void OnAnswerFourClicked(object sender, EventArgs e)
    {
        SoundPlayer.Instance.PlayPlinkSound();
        
        AnswerOne.BackgroundColor = Colors.DarkCyan;
        AnswerTwo.BackgroundColor = Colors.DarkCyan;
        AnswerThree.BackgroundColor = Colors.DarkCyan;
        AnswerFour.BackgroundColor = Colors.DarkGray;
        
        AnswerOne.TextColor = Colors.White;
        AnswerTwo.TextColor = Colors.White;
        AnswerThree.TextColor = Colors.White;
        AnswerFour.TextColor = Colors.Black;
        
        Game.Instance.SelectedAnswer = Game.Instance.CurrentQuestion.Answers[3];
    }
    
    async void OnScoreBoardClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Pages.Scoreboard());
    }
}
