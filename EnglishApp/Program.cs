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
      user.LoadVocabularyAndUserList();
      Console.WriteLine("Авторизация/Регистрация");
      Console.WriteLine("L/R");
      var log = Console.ReadLine();
      while (log != "R" && log != "L")
      {
        Console.WriteLine("Неправиьный формат введенных данных. Повторите попытку");
        Console.WriteLine("Авторизация/Регистрация");
        Console.WriteLine("L/R");
        log = Console.ReadLine();
      }
      Console.WriteLine("Введите логин");
      var nickname = Console.ReadLine();
      switch (log)
      {
        case "L":
        {
          var userexsist = user.LoginIn(nickname);
          while (!userexsist)
          { 
            Console.WriteLine("Неверный логин. Повторите попытку.");
            nickname = Console.ReadLine();
            user.LoginIn(nickname);
            userexsist = user.LoginIn(nickname);
          }
          Console.WriteLine("Вход выполнен");
          break;
        }
        case "R":
        {
          var userexsist = user.SingUp(nickname); ;
          while (!userexsist)
          {
            Console.WriteLine("Пользователь существует. Повторите попытку.");
            nickname = Console.ReadLine();
            userexsist = user.SingUp(nickname);
            }
          Console.WriteLine("Регистрация прошла успешно");
          Console.WriteLine("Вход выполнен");
          break;
        }
      }
      var exercises = new AppWordsService(user);
      bool exit=false;
      while (exit == false)
      {
        Console.WriteLine("\n" + "Выберите действие:" +"\n" + "1.Выйти из программы" + "\n" + "2.Посмотреть список изученных слов" + "\n" + "3.Начать упражнение" + "\n" + "Введите 1 / 2 / 3");
        var action = Console.ReadLine();
        switch (action)
        {
          case "1":
          {
              exit = true;
              exercises.SaveSession(user, nickname);
              break;
          }
          case "2":
          {
            var wordList = exercises.ShowProgress(nickname);
            foreach (var word in wordList)
            {
              Console.WriteLine(word);
            }
            break;
          }
          case "3":
          {
            Console.WriteLine("Количество слов в одном упражнении:");
            var count = Int32.Parse(Console.ReadLine());
            for (int i = 0; i < count; i++)
            {
              var word = exercises.GetNewWord(nickname, 3);
              Console.WriteLine(word);
              Console.WriteLine("Yes/No");
              var answer = Console.ReadLine();
              var result = exercises.Check(answer, nickname);
              Console.WriteLine(result);
            }
            exercises.SaveSession(user, nickname);
            break;
          }
        }
      }


    }
  }
}
