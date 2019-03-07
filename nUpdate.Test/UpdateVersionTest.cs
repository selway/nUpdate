﻿// Author: Dominic Beger (Trade/ProgTrade)

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace nUpdate.Test
{
    [TestClass]
    public class UpdateVersionTest
    {
        // TODO: Rewrite UnitTests

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CanCheckVersionValidity()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new UpdateVersion("1.0.0."); // Invalid
        }

        [TestMethod]
        public void CanConstructOneDigitVersion()
        {
            var version = new UpdateVersion("1");
            Assert.AreEqual("1.0.0.0", version.ToString());
        }

        [TestMethod]
        public void CanConstructTwoDigitVersion()
        {
            var version = new UpdateVersion("1.0");
            Assert.AreEqual("1.0.0.0", version.ToString());
        }

        [TestMethod]
        public void CanConstructThreeDigitVersion()
        {
            var version = new UpdateVersion("1.0.0");
            Assert.AreEqual("1.0.0.0", version.ToString());
        }

        [TestMethod]
        public void CanConstructFourDigitVersion()
        {
            var version = new UpdateVersion("1.0.0.0");
            Assert.AreEqual("1.0.0.0", version.ToString());
        }

        [TestMethod]
        public void CanConstructOneDigitAlphaVersion()
        {
            var version = new UpdateVersion("1a1");
            Assert.AreEqual("1.0.0.0a1", version.ToString());
        }

        [TestMethod]
        public void CanConstructOneDigitAlphaVersionWithoutDevelopmentalStage()
        {
            var version = new UpdateVersion("1a");
            Assert.AreEqual("1.0.0.0a", version.ToString());
        }

        [TestMethod]
        public void CanGetCorrectReleaseCandidateVersionString()
        {
            var version = new UpdateVersion("1.2rc2");
            Assert.AreEqual("1.2.0.0rc2", version.ToString());
        }

        [TestMethod]
        public void CanConstructWithSemanticVersion() // TODO: Check, if necessary
        {
            var firstVersion = new UpdateVersion("1.0-a");
            Assert.AreEqual("1.0.0.0a", firstVersion.ToString());

            var secondVersion = new UpdateVersion("1.0-a.2");
            Assert.AreEqual("1.0.0.0a2", secondVersion.ToString());

            var thirdVersion = new UpdateVersion("1.0a.2");
            Assert.AreEqual("1.0.0.0a2", thirdVersion.ToString());
        }

        [TestMethod]
        public void CanCompareVersions()
        {
            /*var firstVersion = new UpdateVersion("1.2");
            var secondVersion = new UpdateVersion("1.3.0.0");
            Assert.AreEqual(true, firstVersion.IsOlderThan(secondVersion));

            var thirdVersion = new UpdateVersion("1.3a1");
            var fourthVersion = new UpdateVersion("1.3.0.0a");
            Assert.AreEqual(true, thirdVersion.IsNewerThan(fourthVersion));

            var fifthVersion = new UpdateVersion("1.4.0.0-b1");
            var sixthVersion = new UpdateVersion("1.4.0.0rc");
            var seventhVersion = new UpdateVersion("1.4.0.0-rc.1");
            var eighthVersion = new UpdateVersion("1.4.0.0");
            Assert.AreEqual(true, fifthVersion.IsOlderThan(sixthVersion) && sixthVersion.IsOlderThan(seventhVersion) && seventhVersion.IsOlderThan(eighthVersion));*/
        }
    }
}