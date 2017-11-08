using EnglishTutorial.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTutorial.Infrastructure
{
  class UsersListFileRepository
  {
    public UsersListFileRepository()
    {
      _filePath = "Users.json";
    }

    public List<string> LoadUsersList()
    {
      try
      {
        var rawFiles = File.ReadAllText(_filePath);
        var users = JsonConvert.DeserializeObject<string[]>(rawFiles).ToList();
        return users;
      }
      catch (FileNotFoundException)
      {
        return new List<string>();
      }
    }
    public void SaveUserList(List<string> userList)
    {
      var serialized = JsonConvert.SerializeObject(userList);
      File.WriteAllText(_filePath, serialized);
    }
    private readonly string _filePath;
  }
}
