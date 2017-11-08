using EnglishTutorial.Domain;
using Newtonsoft.Json;
using System.IO;

namespace EnglishTutorial.Infrastructure
{
  class UserDataFileRepository
  {
    public UserDataFileRepository(string nickname)
    {
      _filePath = string.Format("{0}.json", nickname);
    }

    public User LoadUserData()
    {
      try
      {
        var rawFiles = File.ReadAllText(_filePath);
        var user = JsonConvert.DeserializeObject<User>(rawFiles);
        return user;
      }
      catch (FileNotFoundException)
      {
        throw new FileNotFoundException("Пользователь не найден");
      }
    }
    public void SaveUserData(User user)
    {
      var serialized = JsonConvert.SerializeObject(user);
      File.WriteAllText(_filePath, serialized);
    }
    private readonly string _filePath;
  }
}
