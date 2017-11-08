using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  EnglishTutorial.Application;
using EnglishTutorial.Domain;

namespace EnglishApp
{
  class Program
  {
    static void Main(string[] args)
    {
      AppUserService user = new AppUserService();
      user.LoadData();
      user.LoginIn("Vova");
      AppWordsService exercises = new AppWordsService(user);
      //Console.WriteLine(exercises.GetNewWord());
      //Console.WriteLine("Yes/No");
      //var ans = Console.ReadLine();
      //var result = exercises.Check(ans);
      //Console.WriteLine(result);
      //exercises.SaveSession(user);
      List<WordInStudy> words = exercises.ShowProgress();
      foreach (var item in words)
      {
        Console.WriteLine(item);
      }

    }
  }
}
