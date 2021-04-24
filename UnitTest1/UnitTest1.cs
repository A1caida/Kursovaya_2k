using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTest1
{
    [TestClass]
    public class UnitTest1
    {
        kyrsovaya_2k.db_work a = new kyrsovaya_2k.db_work("95.104.192.212", "A1caida", "REvisE9023800", "A1caida");
        [TestMethod]
        public void log_in_system()
        {
            List<kyrsovaya_2k.db_work.user> test;
            test = a.log_is_sys("nyaa", "qsc");

            Assert.AreEqual(2, test.Count);
        }

        [TestMethod]
        public void check_registr()
        {
            Assert.AreEqual("Ахмедханов", a.registr_letters("аХМЕДХАНОВ"));
        }

        [TestMethod]
        public void check_ban()
        {
            Assert.AreEqual(0, a.ban("15"));
        }
    }
}
