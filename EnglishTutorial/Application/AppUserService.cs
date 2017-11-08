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
      _wordRepository = new EnglishWordsFileRepository();
      _userListRepository = new UsersListFileRepository();
    }
    ~AppUserService()
    {
      _userDataRepository.SaveUserData(CurrentSession);
      _userListRepository.SaveUserList(_userList);
    }


    public void SingUp(string nickname)
    {
      if (!_userList.Any(name => name == nickname))
      {
        _userDataRepository = new UserDataFileRepository(nickname);
        CurrentSession = new User();
        _userList.Add(nickname);
      }
      else
      {
        throw new InvalidOperationException("Пользователь с таким именем уже существует");
      }
    }

    public void LoginIn(string nickname)
    {
      if (_userList.Any(nick => nick == nickname))
      {
        _userDataRepository = new UserDataFileRepository(nickname);
        CurrentSession = _userDataRepository.LoadUserData();
      }
      else
      {
        throw new InvalidOperationException("Пользователь с таким именем не существует");
      }
    }

    public void LoadData()
    {
      _userList = _userListRepository.LoadUsersList();
      EnglishWords = _wordRepository.LoadWords();
    }

    public void SaveSession(User currentSession)
    {
      CurrentSession = currentSession;
    }

    
    public List<EnglishWord> EnglishWords { set; get; }
    public User CurrentSession {private set; get; }
    private EnglishWordsFileRepository _wordRepository;
    private UsersListFileRepository _userListRepository;
    private UserDataFileRepository _userDataRepository;
    private List<string> _userList;
  }
}
