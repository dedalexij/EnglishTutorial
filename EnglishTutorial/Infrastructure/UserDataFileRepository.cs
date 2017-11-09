using System.Collections.Generic;
using EnglishTutorial.Domain;
using Newtonsoft.Json;
using System.IO;

namespace EnglishTutorial.Infrastructure
{
  class UserDataFileRepository
  {
    public UserDataFileRepository()
    {
    }

    public User LoadUserData(string nickname)
    {
      var filePath = nickname + ".json";
      try
      {
        var rawFiles = File.ReadAllText(filePath);
        var user = JsonConvert.DeserializeObject<User>(rawFiles);
        return user;
      }
      catch (FileNotFoundException)
      {
        throw new FileNotFoundException("Файл не найден");
      }
    }
    public void SaveUserData(User user)
    {
      var serialized = JsonConvert.SerializeObject(user);
      var filePath  = user.Nickname + ".json";
      File.WriteAllText(filePath, serialized);
    }
  }
}
