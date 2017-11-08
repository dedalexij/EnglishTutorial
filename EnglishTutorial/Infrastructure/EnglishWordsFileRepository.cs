using System.Collections.Generic;
using System.IO;
using System.Linq;
using EnglishTutorial.Domain;
using Newtonsoft.Json;

namespace EnglishTutorial.Infrastructure
{
  class EnglishWordsFileRepository
  {
    public EnglishWordsFileRepository()
    {
      _filePath = "EnglishWord.json";
    }

    public List<EnglishWord> LoadWords()
    {
      try
      {
        var rawFiles = File.ReadAllText(_filePath);
        var dictionary = JsonConvert.DeserializeObject<EnglishWord[]>(rawFiles).ToList();
        return dictionary;
      }
      catch (FileNotFoundException)
      {
        return new List<EnglishWord>(0);
      }
    }
    private readonly string _filePath;
  }
}
