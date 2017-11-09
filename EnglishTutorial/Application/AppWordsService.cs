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
      Sessions = userService.Sessions;
    }

    public string GetNewWord(string nickname, int count)
    {
      var userNum = GetUserNumInSessions(nickname);
      Random random = new Random();
      if (UnexaminedWords(userNum) == false)
      {
        return "Вы изучили все слова";
      }
      do
      {
        _lastWordNum = random.Next(EnglishWords.Count);
        
      } while (Sessions[userNum].Vocabulary.Any(item => EnglishWords[_lastWordNum].Word == item.Word && item.Count == count));
      var word = EnglishWords[_lastWordNum].Word;
      _translation = GetTranslation(_lastWordNum);
      string toReturn = String.Format("Перевод слова {0} - {1}?", word, _translation);
      return toReturn;
    }

    private int GetUserNumInSessions(string nickname)
    {
      var id = Sessions.First(user => user.Nickname == nickname);
      return Sessions.IndexOf(id);
    }

    public List<WordInStudy> ShowProgress(string nickname)
    {
      var userNum = GetUserNumInSessions(nickname);
      return Sessions[userNum].Vocabulary;
    }
    public string Check(string ans, string nickname)
    {
      var userNum = GetUserNumInSessions(nickname);
      var answer = Answer(ans);
      if (EnglishWords[_lastWordNum].Translation == _translation)
      {
        if (answer == true)
        {
          Sessions[userNum].RiseCount(EnglishWords[_lastWordNum].Word);
          return "Верно";
        }
        else
        {
          Sessions[userNum].ReduceCount(EnglishWords[_lastWordNum].Word);
          return "Не верно";
        }
      }
      else
      {
        if (answer == false)
        {
          Sessions[userNum].RiseCount(EnglishWords[_lastWordNum].Word);
          return "Верно";
        }
        else
        {
          Sessions[userNum].ReduceCount(EnglishWords[_lastWordNum].Word);
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

    public void SaveSession(AppUserService session, string nickname)
    {
      var userNum = GetUserNumInSessions(nickname);
      session.SaveData(Sessions[userNum]);
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

    private bool UnexaminedWords(int userNum)
    {
      if (Sessions[userNum].Vocabulary.Any(item => item.Count != 3))
      {
        return true;
      }
      foreach (var word in EnglishWords)
      {
        if (!Sessions[userNum].Vocabulary.Any(item => item.Word == word.Word))
          return true;
      }
      return false;
    }
    public List<EnglishWord> EnglishWords { set; get; }
    public List<User> Sessions;
    private int _lastWordNum;
    private string _translation;
  }
}
