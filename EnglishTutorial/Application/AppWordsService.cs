using EnglishTutorial.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishTutorial.Application
{
  public class AppWordsService
  {
    public AppWordsService(AppUserService userService)
    {
      EnglishWords = userService.EnglishWords;
      CurrentSession = userService.CurrentSession;
    }

    public string GetNewWord()
    {
      Random random = new Random();
      if (UnexaminedWords() == false)
      {
        return "Вы изучили все слова";
      }
      do
      {
        _lastWordNum = random.Next(EnglishWords.Count);
        
      } while (CurrentSession.Vocabulary.Any(item => EnglishWords[_lastWordNum].Word == item.Word && item.Count == 3));
      var word = EnglishWords[_lastWordNum].Word;
      _translation = GetTranslation(_lastWordNum);
      string toReturn = String.Format("Перевод слова {0} - {1}?", word, _translation);
      return toReturn;
    }

    public List<WordInStudy> ShowProgress()
    {
      return CurrentSession.Vocabulary;
    }
    public string Check(string ans)
    {
      var answer = Answer(ans);
      if (EnglishWords[_lastWordNum].Translation == _translation)
      {
        if (answer == true)
        {
          CurrentSession.RiseCount(EnglishWords[_lastWordNum].Word);
          return "Верно";
        }
        else
        {
          CurrentSession.ReduceCount(EnglishWords[_lastWordNum].Word);
          return "Не верно";
        }
      }
      else
      {
        if (answer == false)
        {
          CurrentSession.RiseCount(EnglishWords[_lastWordNum].Word);
          return "Верно";
        }
        else
        {
          CurrentSession.ReduceCount(EnglishWords[_lastWordNum].Word);
          return "Не верно";
        }
      }
    }


    bool Answer(string ans)
    {
      if (ans == "Yes" || ans == "yes")
        return true;
      else 
      if (ans == "No" || ans == "no")
        return false;    
      throw  new Exception("Неверный формат ответа");
    }

    public void SaveSession(AppUserService session)
    {
      session.SaveSession(CurrentSession);
    }
    private string GetTranslation(int wordNum)
    {
      Random random = new Random();
      var trueFalseTranstaion = random.Next(2);
      int falseTranslation;
      string translation;
      switch (trueFalseTranstaion)
      {
        case 0:
        {
          translation = EnglishWords[wordNum].Translation;
          return translation;
        }
        case 1:
        {
          do
          {
            falseTranslation = random.Next(EnglishWords.Count);
          }
            while (wordNum == falseTranslation);
          translation = EnglishWords[falseTranslation].Translation;
          return translation;
        }
      }
      throw new Exception("что-то пошло не так");
    }

    private bool UnexaminedWords()
    {
      if (CurrentSession.Vocabulary.Any(item => item.Count != 3))
      {
        return true;
      }
      foreach (var word in EnglishWords)
      {
        if (!CurrentSession.Vocabulary.Any(item => item.Word == word.Word))
          return true;
      }
      return false;
    }
    public List<EnglishWord> EnglishWords { set; get; }
    public User CurrentSession;
    private int _lastWordNum;
    private string _translation;
  }
}
