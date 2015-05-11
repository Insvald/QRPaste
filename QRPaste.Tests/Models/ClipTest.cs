using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QRPaste.Models;

namespace QRPaste.Tests
{
    [TestClass]
    public class ClipTest
    {
        [TestMethod]
        [ExpectedException(typeof(QRPException))]
        public void GetVoidURL()
        {
            Clip clip = new Clip();
            string url = clip.URL;
        }

        [TestMethod]
        public void SetAndGetURL()
        {
            Clip clip = new Clip();
            string testURL = "http://www.nowhere.com";
            clip.URL = testURL;
            
            //check that properties are set
            Assert.AreEqual(ClipType.URL, clip.Type);
            Assert.AreEqual(testURL, clip.URL);
            Assert.IsNotNull(clip.QRBitmap);
        }

        [TestMethod]
        [ExpectedException(typeof(QRPException))]
        public void GetVoidText()
        {
            Clip clip = new Clip();
            string text = clip.Text;
        }

        [TestMethod]
        public void SetAndGetText()
        {
            Clip clip = new Clip();
            string testText = "some weird stuff";
            clip.Text = testText;

            //check that properties are set
            Assert.AreEqual(ClipType.Text, clip.Type);
            Assert.AreEqual(testText, clip.Text);
            Assert.IsNotNull(clip.QRBitmap);
        }

    }
}
