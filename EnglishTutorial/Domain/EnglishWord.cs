using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTestProject1")]

namespace EnglishTutorial.Domain
{
  public class EnglishWord
  {
    public EnglishWord(string word, string translation)
    {
      Word = word;
      Translation = translation;
    }

    public string Word { set; get; }
    public string Translation { set; get; }
  }
}
