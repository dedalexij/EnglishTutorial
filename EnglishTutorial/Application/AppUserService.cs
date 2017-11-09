using System;
using System.Collections.Generic;
using System.Linq;
using EnglishTutorial.Domain;
using EnglishTutorial.Infrastructure;

namespace EnglishTutorial.Application
{
  public class AppUserService
  {
    public AppUserService()
    {
    }


    public bool SingUp(string nickname)
    {
      if (!_userList.Any(name => name == nickname))
      {
        Sessions.Add(new User(nickname));
        _userList.Add(nickname);
        _userListRepository.SaveUserList(_userList);
        _userDataRepository.SaveUserData(Sessions[Sessions.Count-1]);
        return true;
      }
      else
      {
        return false;
      }
    }

    public bool LoginIn(string nickname)
    {
      if (_userList.Any(nick => nick == nickname))
      {
        Sessions.Add(_userDataRepository.LoadUserData(nickname));
        return true;
      }
      else
      {
        return false;
      }
    }

    public void SaveData(User currentSession)
    {
      foreach (var user in Sessions)
      {
        if (user.Nickname == currentSession.Nickname)
        {
          Sessions[Sessions.IndexOf(user)] = currentSession;
          _userDataRepository.SaveUserData(currentSession);
        }
        break;
      }
    }
    public void LoadVocabularyAndUserList()
    {
      _wordRepository = new EnglishWordsFileRepository();
      _userListRepository = new UsersListFileRepository();
      _userDataRepository = new UserDataFileRepository();
      _userList = _userListRepository.LoadUsersList();
      Sessions = new List<User>();
      EnglishWords = _wordRepository.LoadWords();
    }
    
    public List<EnglishWord> EnglishWords {private set; get; }
    public List<User> Sessions {private set; get; }
    private EnglishWordsFileRepository _wordRepository;
    private UsersListFileRepository _userListRepository;
    private UserDataFileRepository _userDataRepository;
    private List<string> _userList;
  }
}
