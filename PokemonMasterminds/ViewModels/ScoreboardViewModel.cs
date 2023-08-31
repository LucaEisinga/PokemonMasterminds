using PokemonMasterminds.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PokemonMasterminds.ViewModels
{
    
    //viewmodel voor het scoreboard. hier wordt de score van speler weergegeven
    class ScoreboardViewModel: INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private string _player1Text;
        
        public string Player1Text
        {
            get { return _player1Text; }
            set { _player1Text = value; OnPropertyChanged(); }
        }

        private string _player1Score;
        
        public string Player1Score
        {
            get { return _player1Score; }
            set { _player1Score = value; OnPropertyChanged(); }
        }

        private string _scorePercentage { get; set; }
        
        public string ScorePercentage
        {
            get { return _scorePercentage; }
            set { _scorePercentage = value; OnPropertyChanged(); }
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
        
        public ScoreboardViewModel()
        {
            
            //hier de logica van het weergegeven van het scoreboard met bijhoorende tekst
            Player1Score = Game.Instance.Lobby.Player.Score.ToString() + " out of 15";
            
            if (Game.Instance.Lobby.Player.Score <= 5)
            {
                Player1Text = Game.Instance.Lobby.Player.Name + ", Better luck next time!";
            }
            else if (Game.Instance.Lobby.Player.Score <= 11)
            {
                Player1Text = Game.Instance.Lobby.Player.Name + ", Not bad!";
            }
            else if (Game.Instance.Lobby.Player.Score <= 14)
            {
                Player1Text = Game.Instance.Lobby.Player.Name + ", Almost a perfect score!";
            }
            else if (Game.Instance.Lobby.Player.Score == 15)
            {
                Player1Text = Game.Instance.Lobby.Player.Name + ", Wow! Got 'Em All Right!";
            }

            //berekenen van percentage goed geantwoorde vragen
            float percentage = Game.Instance.Lobby.Player.Score * (100f / 15f);
            
            ScorePercentage = Math.Round(percentage, 2) + "%";
        }
        
    }
}

