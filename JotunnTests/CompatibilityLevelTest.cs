﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jotunn.Utils
{
    [TestClass]
    public class CompatibilityLevelTest
    { 
        [TestMethod]
        public void BothOnlyJotunn()
        {
            var clientVersionData = new ModCompatibility.ModuleVersionData(new Version(1, 0, 0), new List<Tuple<string, Version, CompatibilityLevel, VersionStrictness>>());
            var serverVersionData = new ModCompatibility.ModuleVersionData(new Version(1, 0, 0), new List<Tuple<string, Version, CompatibilityLevel, VersionStrictness>>());

            Assert.IsTrue(ModCompatibility.CompareVersionData(serverVersionData, clientVersionData));
        }

        [TestMethod]
        public void ClientHasModButServerDoesNot()
        {
            var clientMods = new List<Tuple<string, Version, CompatibilityLevel, VersionStrictness>>
            {
                new Tuple<string, Version, CompatibilityLevel, VersionStrictness>("TestMod", new Version(1, 0, 0), CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)
            };
            var clientVersionData = new ModCompatibility.ModuleVersionData(new Version(1, 0, 0), clientMods);
            var serverMods = new List<Tuple<string, Version, CompatibilityLevel, VersionStrictness>>();
            var serverVersionData = new ModCompatibility.ModuleVersionData(new Version(1, 0, 0), serverMods);
             
            Assert.IsFalse(ModCompatibility.CompareVersionData(serverVersionData, clientVersionData));
        }
         
        [TestMethod]
        public void ServerHasModButClientDoesntNeedIt()
        {
            var clientMods = new List<Tuple<string, Version, CompatibilityLevel, VersionStrictness>>();
            var clientVersionData = new ModCompatibility.ModuleVersionData(new Version(1, 0, 0), clientMods);
            var serverMods = new List<Tuple<string, Version, CompatibilityLevel, VersionStrictness>>
            {
                new Tuple<string, Version, CompatibilityLevel, VersionStrictness>("TestMod", new Version(1, 0, 0), CompatibilityLevel.ServerMustHaveMod, VersionStrictness.Minor)
            };
            var serverVersionData = new ModCompatibility.ModuleVersionData(new Version(1, 0, 0), serverMods);

            Assert.IsTrue(ModCompatibility.CompareVersionData(serverVersionData, clientVersionData));
        }
    }
}
