using System.Windows.Input;

namespace PokemonMasterminds.ViewModels;

public class AnswerViewModel
{
	public string Question { get; set; }
	public string CorrectAnswer { get; set; }
	public bool IsCorrectAnswer { get; set; }
	public ICommand NextPageCommand { get; set; }

	public AnswerViewModel(string Question, string CorrectAnswer, bool IsCorrectAnswer)
	{
		this.Question = Question;
		this.CorrectAnswer = CorrectAnswer;
		this.IsCorrectAnswer = IsCorrectAnswer;

		INavigation navigation = App.Current.MainPage.Navigation;

		NextPageCommand = new Command(async () =>
		{

		});
	}
}