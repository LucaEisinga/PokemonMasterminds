using System.Diagnostics;
using PokemonMasterminds.ViewModels;
using PokemonMasterminds.Model;
using System;

namespace PokemonMasterminds.Pages
{
    public partial class GamePage : ContentPage
    {
        private DateTime startTime;
        private int duration;
        private double progress;
        private bool isImageVisible = false; // Flag to track image visibility

        public GamePage()
        {
            BindingContext = new QuestionViewModel(Game.Instance);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            StartTimerIfImageVisible();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            // Check if the image is visible on the screen based on layout changes
            if (width > 0 && height > 0 && !isImageVisible)
            {
                isImageVisible = true;
                StartTimerIfImageVisible();
            }
        }

        private async void StartTimerIfImageVisible()
        {
            // Start the timer only if the image is visible
            if (isImageVisible)
            {
                await Task.Delay(1000);
                startTime = DateTime.Now;
                UpdateArc();
            }
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

            while (elapsedTime.TotalSeconds <= duration && Game.Instance.GameIsActive)
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
                        await Navigation.PushAsync(new GamePage());
                    }
                    else if (Game.Instance.Count > 14)
                    {
                        await Navigation.PushAsync(new Scoreboard());
                    }

                    await Task.Delay(500);

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
}
