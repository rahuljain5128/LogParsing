using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogParser;
using LogParser.Entity;
using System;
using System.Collections.Generic;

namespace LogParserTest
{
    [TestClass]
    public class TopNHighestThroughPutApis
    {
        ApiLogOutput apiLogOut = new ApiLogOutput
        {
            Url = "url",
            Method = "get",
            Count = 1,
            MinTime = 1,
            MaxTime = 2,
            TotalTime = 1
        };
        Dictionary<string, LogParser.Entity.ApiLogOutput> dict = new Dictionary<string, ApiLogOutput>();

        public TopNHighestThroughPutApis()
        {
            dict.Add("get-url",apiLogOut);
        }
        [TestMethod]
        public void TopNHighestThroughPutApis_EmptyDict_ReturnsEmpty()
        {
            var logicObj = new Logic();
            var result = logicObj.TopNHighestThroughPutApis(1,new System.Collections.Generic.Dictionary<string, LogParser.Entity.ApiLogOutput>());
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void TopNHighestThroughPutApis_NonEmptyDict_ReturnsResult()
        {
            var logicObj = new Logic();
            var result = logicObj.TopNHighestThroughPutApis(1,dict);
            Assert.IsTrue(result.Count == 1);
            // compare object also
        }

        [TestMethod]
        public void TopNHighestThroughPutApis_NonEmptyDictAndCountIs0_ReturnsEmptyResult()
        {
            var logicObj = new Logic();
            var result = logicObj.TopNHighestThroughPutApis(0,dict);
            Assert.IsTrue(result.Count == 0);
        }
    }
}