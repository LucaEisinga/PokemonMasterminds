using PokemonMasterminds.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PokemonMasterminds.ViewModels
{
    class ScoreboardViewModel: INotifyPropertyChanged

    {
        public LobbyViewModel Lobby { get; }
        //GetScoreboardCommand : ICommand
        public ICommand GetScoreboardCommand { private set; get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public List<Player> PlayerList { get; set; }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public ScoreboardViewModel(Game game) 
        {
            PlayerList = game.Lobby.Players.ToList();
        }
        /*
             * 
        public class ScoreCalculator
        {
            private const int BasePoints = 10;
            private const int StreakBonus = 5;
            private const double MaxResponseTime = 10.0;

            public int CalculateScore(int streakCount, double responseTime)
            {
                int streakPoints = streakCount * StreakBonus;
                double timeFactor = (MaxResponseTime - responseTime) / MaxResponseTime;
                int bonusPoints = (int)(BasePoints * timeFactor);
                int totalScore = BasePoints + streakPoints + bonusPoints;
                return totalScore;
            }
            public class GameViewModel : INotifyPropertyChanged
{
    private ScoreCalculator scoreCalculator;
    private int streakCount;
    private double responseTime;
    private int score;

    public int StreakCount
    {
        get { return streakCount; }
        set
        {
            streakCount = value;
            UpdateScore();
            OnPropertyChanged();
        }
    }

    public double ResponseTime
    {
        get { return responseTime; }
        set
        {
            responseTime = value;
            UpdateScore();
            OnPropertyChanged();
        }
    }

    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            OnPropertyChanged();
        }
    }

    public GameViewModel()
    {
        scoreCalculator = new ScoreCalculator();
    }

    private void UpdateScore()
    {
        Score = scoreCalculator.CalculateScore(streakCount, responseTime);
    }

}
            */
    }
}

