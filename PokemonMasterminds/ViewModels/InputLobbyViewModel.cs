
using Microsoft.Maui.Controls;
using PokemonMasterminds.Model;
using PokemonMasterminds.Pages;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PokemonMasterminds.ViewModels;

public partial class InputLobbyViewModel : INotifyPropertyChanged
{
    private string playerName;

    public event PropertyChangedEventHandler PropertyChanged;

    public ICommand SaveName { get; private set; }
    public ICommand CreateLobby { get; private set; }
    public ICommand JoinLobby { get; private set; }

    public string PlayerName
    {
        get => playerName;
        set
        {
            playerName = value;
            OnPropertyChanged(nameof(PlayerName));
        }

    }

    public InputLobbyViewModel()
    {
        INavigation navigation = App.Current.MainPage.Navigation;

        SaveName = new Command(() =>
        {
            if (PlayerName is null or "")
            {
                return;
            }
            Players.Add(new Player(PlayerName));
            PlayerName = "";
        });

        CreateLobby = new Command(async () =>
        {
            if (Players.Count == 0)
            {
                return;
            }
            Game game = new()
            {
                Players = Players
            };
            await navigation.PushAsync(new Lobby(game));
        });

        JoinLobby = new Command(async () =>
        {
            if (Players.Count == 0)
            {
                return;
            }
            Game game = new()
            {
                Players = Players
            };
            await navigation.PushAsync(new Lobby(game));
        });
    }

    public ObservableCollection<Player> Players { get; } = new();

    

    bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (Object.Equals(storage, value))
            return false;

        storage = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}