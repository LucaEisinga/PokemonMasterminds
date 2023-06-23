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
        
        private string _player1Text;
        private string _player2Text;
        private string _player3Text;
        private string _player4Text;
        
        public string Player1Text
        {
            get { return _player1Text; }
            set { _player1Text = value; OnPropertyChanged(); }
        }
        
        public string Player2Text
        {
            get { return _player2Text; }
            set { _player2Text = value; OnPropertyChanged(); }
        }
        
        public string Player3Text
        {
            get { return _player3Text; }
            set { _player3Text = value; OnPropertyChanged(); }
        }
        
        public string Player4Text
        {
            get { return _player4Text; }
            set { _player4Text = value; OnPropertyChanged(); }
        }
        
        private string _player1Score;
        private string _player2Score;
        private string _player3Score;
        private string _player4Score;
        
        public string Player1Score
        {
            get { return _player1Score; }
            set { _player1Score = value; OnPropertyChanged(); }
        }
        
        public string Player2Score
        {
            get { return _player2Score; }
            set { _player2Score = value; OnPropertyChanged(); }
        }
        
        public string Player3Score
        {
            get { return _player3Score; }
            set { _player3Score = value; OnPropertyChanged(); }
        }
        
        public string Player4Score
        {
            get { return _player4Score; }
            set { _player4Score = value; OnPropertyChanged(); }
        }

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

            Player1Score = "4";
            Player1Text = "henk"; //game.Lobby.Players[0].Name;
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

