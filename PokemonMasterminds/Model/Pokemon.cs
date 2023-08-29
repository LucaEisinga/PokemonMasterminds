using Microsoft.Maui.Storage;

namespace PokemonMasterminds.Model;

public class Pokemon
{
    public int id { get; set; }

    public string name { get; set; }
    
    public Sprite sprites { get; set; }

    public class Sprite
    {
        public string front_default { get; set; }
        
        public string pokemonSprite { get; set; }

      //public void SetImage()
      //{
      //    Image image = new Image();
      //    image.Source = ImageSource.FromUri(new Uri(front_default));
      //    

      //    pokemonSprite = image;
      //}
        
        
      public async Task SetImage()
      {
          using (HttpClient client = new HttpClient())
          {
              try
              {
                  byte[] imageData = await client.GetByteArrayAsync(front_default);

                  // Generate a random filename
                  string randomFileName = Path.GetRandomFileName();
                  string fileExtension = ".svg"; // Change to the appropriate image format

                  // Combine the random filename and extension
                  string fileName = $"{randomFileName}{fileExtension}";

                  // Get the file path for saving the image
                  string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);

                  // Save the image data to the local file
                  File.WriteAllBytes(filePath, imageData);

                  // Set the path to the downloaded image
                  pokemonSprite = filePath;
              }
              catch (Exception ex)
              {
                  // Handle any exceptions that might occur during image fetching or saving
                  Console.WriteLine($"Error: {ex.Message}");
              }
          }
      }
    }
   
}

