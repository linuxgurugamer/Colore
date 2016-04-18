﻿// <copyright file="StaticTests.cs" company="Corale">
//     Copyright © 2015 by Adam Hellberg and Brandon Scott.
//
//     Permission is hereby granted, free of charge, to any person obtaining a copy of
//     this software and associated documentation files (the "Software"), to deal in
//     the Software without restriction, including without limitation the rights to
//     use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
//     of the Software, and to permit persons to whom the Software is furnished to do
//     so, subject to the following conditions:
//
//     The above copyright notice and this permission notice shall be included in all
//     copies or substantial portions of the Software.
//
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//     IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//     FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//     AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
//     WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
//     CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
//     "Razer" is a trademark of Razer USA Ltd.
// </copyright>

namespace Corale.Colore.Tests.Razer.Mouse.Effects
{
    using Colore.Core;
    using Colore.Razer.Mouse;
    using Colore.Razer.Mouse.Effects;

    using NUnit.Framework;
    using UnityEngine;

    [TestFixture]
    public class StaticTests
    {
        [Test]
        public void ShouldConstructWithCorrectColor()
        {
            Assert.AreEqual(Color.red, new Static(Led.All, Color.red).Color);
        }

        [Test]
        public void ShouldConstructWithCorrectLed()
        {
            Assert.AreEqual(Led.Logo, new Static(Led.Logo, Color.black).Led);
        }
    }
}
