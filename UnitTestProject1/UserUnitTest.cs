using System;
using EnglishTutorial.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
  [TestClass]
  public class UserUnitTest
  {
    [TestMethod]
    public void AddWordToVocabulary()
    {
      var nickname = "user1";
      User user = new User(nickname);
      var word = "hello";
      var wordInStudy = new WordInStudy(word).Word;
      user.AddWordToVocabulary(word);
      var wordInVocubulary = user.Vocabulary[0].Word;
      Assert.AreEqual(wordInStudy, wordInVocubulary);
    }

    [TestMethod]
    public void RiseCountTo1()
    {
      var nickname = "user1";
      User user = new User(nickname);
      var word = "hello";
      user.AddWordToVocabulary(word);
      user.RiseCount(word);
      var countInVocabulary = user.Vocabulary[0].Count;
      var count = 1;
      Assert.AreEqual(countInVocabulary, count);
    }

    [TestMethod]
    public void ReduceCountTo0()
    {
      var nickname = "user1";
      User user = new User(nickname);
      var word = "hello";
      user.AddWordToVocabulary(word);
      user.RiseCount(word);
      user.ReduceCount(word);
      var countInVocabulary = user.Vocabulary[0].Count;
      var count = 0;
      Assert.AreEqual(countInVocabulary, count);
    }
    [TestMethod]
    public void MethodExistOrNotExistMustReturnMinus1()
    {
      var nickname = "user1";
      User user = new User(nickname);
      var word = "hello";
      var word2 = "yes";
      user.AddWordToVocabulary(word);
      var count = user.ExistOrNotExistWord(word2);
      Assert.AreEqual(-1, count);
    }
    [TestMethod]
    public void MethodExistOrNotExistMustReturnIndexOfWordInVocabulary()
    {
      var nickname = "user1";
      User user = new User(nickname);
      var word = "hello";
      user.AddWordToVocabulary(word);
      var count = user.ExistOrNotExistWord(word);
      var wordIndex = 0;
      Assert.AreEqual(wordIndex, count);
    }
  }
}
