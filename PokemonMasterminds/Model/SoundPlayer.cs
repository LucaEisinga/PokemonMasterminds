using Plugin.Maui.Audio;

namespace PokemonMasterminds.Model;

public class SoundPlayer
{
    private readonly AudioManager _audioManager;
    private IAudioPlayer _backgroundMusic;
     
    private SoundPlayer()
    {
        this._audioManager = new AudioManager();
    }
    
    private static SoundPlayer _instance;
    
    public static SoundPlayer Instance => _instance ??= new SoundPlayer();

    public async void PlayPlinkSound()
    {
        var player = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("PlinkPokemonSound2.mp3"));

        player.Play();
    }
    
     public async void PlayBackgroundSound() 
     { 
         _backgroundMusic = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("EndingTheme.mp3"));

         _backgroundMusic.Volume = 0.05;
         _backgroundMusic.Loop = true;
         _backgroundMusic.Play();
     }
     
     public async void PauseBackgroundSound() 
     {
         _backgroundMusic.Pause();
     }
}