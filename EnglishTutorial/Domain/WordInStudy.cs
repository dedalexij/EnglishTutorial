using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("UnitTestProject1")]

namespace EnglishTutorial.Domain
{
  public class WordInStudy
  {
    public WordInStudy(string word)
    {
      Word = word;
      Count = 0;
    }
    [JsonConstructor]
    public WordInStudy(string word, int count)
    {
      Word = word;
      Count = count;
    }

    public void RiseCount()
    {
      Count++;
    }

    public override string ToString()
    {
      return String.Format("{0} - {1} верных ответа", Word, Count);
    }

    public void ReduceCount()
    {
      Count = 0;
    }
    public string Word { set; get; }
    public int Count {private set; get; }
  }
}
