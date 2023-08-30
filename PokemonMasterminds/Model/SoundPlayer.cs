using Plugin.Maui.Audio;

namespace PokemonMasterminds.Model;

//dit is de soundplayer class, met behulp van deze class kunnen geluiden worden afgespeeld
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

    //hiermee wordt een plink geluid afgespeeld
    public async void PlayPlinkSound()
    {
        var player = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("PlinkPokemonSound2.mp3"));

        player.Play();
    }
    
    // dit is de methode voor het afspelen van het achtergrond geluid
     public async void PlayBackgroundSound() 
     { 
         _backgroundMusic = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("EndingTheme.mp3"));

         _backgroundMusic.Volume = 0.08;
         _backgroundMusic.Loop = true;
         _backgroundMusic.Play();
     }
     
     public async void PauseBackgroundSound() 
     {
         _backgroundMusic.Pause();
     }
}