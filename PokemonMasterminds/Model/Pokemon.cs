namespace PokemonMasterminds.Model;

//dit is de pokemon class, hierin wordt alle benodigde info over de pokemon bijgehouden
public class Pokemon
{
    public int id { get; set; }

    public string name { get; set; }
    
    public Sprite sprites { get; set; }

    public class Sprite
    {
        public string front_default { get; set; }
        
        public string pokemonSprite { get; set; }

      public async Task SetImage()
      {
          using (HttpClient client = new HttpClient())
          {
              try
              {
                  byte[] imageData = await client.GetByteArrayAsync(front_default);

                  // genereer een random file name
                  string randomFileName = Path.GetRandomFileName();
                  string fileExtension = ".svg"; // Change to the appropriate image format

                  // combineer de file name + extensie van file
                  string fileName = $"{randomFileName}{fileExtension}";

                  // file path halen voor het opslaan van de image
                  string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);

                  // image data opslaan in het lokale bestand
                  File.WriteAllBytes(filePath, imageData);

                  // path naar de image opslaan in variable
                  pokemonSprite = filePath;
              }
              catch (Exception ex)
              {
                  Console.WriteLine($"Error: {ex.Message}");
              }
          }
      }
    }
   
}

