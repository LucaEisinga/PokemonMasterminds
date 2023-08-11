using PokemonMasterminds.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PokemonMasterminds.ViewModels
{
    class ScoreboardViewModel: INotifyPropertyChanged

    {
        public LobbyViewModel Lobby { get; }
        //GetScoreboardCommand : ICommand
        public ICommand GetScoreboardCommand { private set; get; }

        public event PropertyChangedEventHandler PropertyChanged;
        //public List<Player> PlayerList { get; set; }
        
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
            Player1Score = Game.Instance.Lobby.Player.Score.ToString();

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

            int percentage = (Game.Instance.Lobby.Player.Score * (100 / 15));

            ScorePercentage = percentage + "%";
        }
        
    }
}

