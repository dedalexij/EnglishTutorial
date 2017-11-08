using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("UnitTestProject1")]

namespace EnglishTutorial.Domain
{
  public class User
  {
    public User()
    {
      Vocabulary = new List<WordInStudy>();
    }
    [JsonConstructor]
    public User( List<WordInStudy> list)
    {
      Vocabulary = list;
    }

    public int ExistOrNotExistWord(string word)
    {
      for (int i = 0; i < Vocabulary.Count; i++)
      {
        if (Vocabulary[i].Word == word)
          return i;
      }
      return -1;
    }

    public void ReduceCount(string word)
    {
      int count = ExistOrNotExistWord(word);
      if (count == -1)
      {
        AddWordToVocabulary(word);
      }
      else
      {
        Vocabulary[count].ReduceCount();
      }

    }
    public void RiseCount(string word)
    {
      int count = ExistOrNotExistWord(word);
      if (count == -1)
      {
        AddWordToVocabulary(word);
        Vocabulary[Vocabulary.Count-1].RiseCount();
      }
      else
      {
        Vocabulary[count].RiseCount();
      }
    }
    public void AddWordToVocabulary(string newWord)
    {
      Vocabulary.Add(new WordInStudy(newWord));
    }
    public List<WordInStudy> Vocabulary { set; get; }
  }
}
