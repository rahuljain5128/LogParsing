using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogParser;
using LogParser.Entity;
using System;
using System.Collections.Generic;

namespace LogParserTest
{
    [TestClass]
    public class GetMinMaxAvgTime
    {
        ApiLogOutput apiLogOut = new ApiLogOutput
        {
            Url = "url",
            Method = "get",
            Count = 1,
            MinTime = 1,
            MaxTime = 1,
            TotalTime = 1
        };
        Dictionary<string, LogParser.Entity.ApiLogOutput> dict = new Dictionary<string, ApiLogOutput>();

        public GetMinMaxAvgTime()
        {
            dict.Add("get-url",apiLogOut);
        }
        [TestMethod]
        public void GetMinMaxAvgTime_EmptyDict_ReturnsEmpty()
        {
            var logicObj = new Logic();
            var result = logicObj.GetMinMaxAvgTime(new System.Collections.Generic.Dictionary<string, LogParser.Entity.ApiLogOutput>());
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void GetMinMaxAvgTime_NonEmptyDict_ReturnsResult()
        {
            var logicObj = new Logic();
            var result = logicObj.GetMinMaxAvgTime(dict);
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[0].MinTime == 1);
            Assert.IsTrue(result[0].MaxTime == 1);
      }
    }
}