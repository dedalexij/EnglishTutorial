using System;
using EnglishTutorial.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
  [TestClass]
  public class WordInStudyUnitTest
  {
    [TestMethod]
    public void RiseCountTo1()
    {
      var word = "hello";
      WordInStudy wordInStudy = new WordInStudy(word);
      wordInStudy.RiseCount();
      var count = wordInStudy.Count;
      var countEqual1 = 1;
      Assert.AreEqual(countEqual1, count);
    }
    [TestMethod]
    public void ReduceCountTo0()
    {
      var word = "hello";
      WordInStudy wordInStudy = new WordInStudy(word);
      wordInStudy.RiseCount();
      wordInStudy.ReduceCount();
      var count = wordInStudy.Count;
      var countIsEqual0 = 0;
      Assert.AreEqual(countIsEqual0, count);
    }
  }
}
