using libGenerator.DMGDungeonBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CoreTest
{
    [TestClass]
    public class TestDMGDungeon
    {
        [TestMethod]
        public void TestStartingChamber()
        {
            var gen = new DMGDungeon(2, PurposeType.DeathTrap);
            var chamber = gen.Chambers;
            
        }
    }
}
