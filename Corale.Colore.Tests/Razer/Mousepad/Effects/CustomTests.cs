// <copyright file="CustomTests.cs" company="Corale">
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

namespace Corale.Colore.Tests.Razer.Mousepad.Effects
{
    using System;
    using Corale.Colore.Core;
    using Corale.Colore.Razer.Mousepad;
    using Corale.Colore.Razer.Mousepad.Effects;
    using NUnit.Framework;
    using UnityEngine;

    [TestFixture]
    public class CustomTests
    {
        [Test]
        public void ShouldThrowWhenOutOfRangeGet()
        {
            var custom = Custom.Create();

            // ReSharper disable once NotAccessedVariable
            Color dummy;

            Assert.That(
                () => dummy = custom[-1],
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                    .With.Property("ParamName")
                    .EqualTo("led")
                    .And.Property("ActualValue")
                    .EqualTo(-1));

            Assert.That(
                () => dummy = custom[Constants.MaxLeds],
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                    .With.Property("ParamName")
                    .EqualTo("led")
                    .And.Property("ActualValue")
                    .EqualTo(Constants.MaxLeds));
        }

        [Test]
        public void ShouldThrowWhenOutOfRangeSet()
        {
            var custom = Custom.Create();

            Assert.That(
                () => custom[-1] = Color.red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                    .With.Property("ParamName")
                    .EqualTo("led")
                    .And.Property("ActualValue")
                    .EqualTo(-1));

            Assert.That(
                () => custom[Constants.MaxLeds] = Color.red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                    .With.Property("ParamName")
                    .EqualTo("led")
                    .And.Property("ActualValue")
                    .EqualTo(Constants.MaxLeds));
        }

        [Test]
        public void ShouldSetAllColorsWithColorConstructor()
        {
            var effect = new Custom(Color.red);

            for (var i = 0; i < Constants.MaxLeds; i++)
                Assert.That(effect[i], Is.EqualTo(Color.red));
        }

        [Test]
        public void ShouldSetBlackColorsWithCreate()
        {
            var effect = Custom.Create();

            for (var i = 0; i < Constants.MaxLeds; i++)
                Assert.That(effect[i], Is.EqualTo(Color.black));
        }

        [Test]
        public void ShouldThrowOnInvalidListCount()
        {
            var colors = new Color[1];

            // ReSharper disable once NotAccessedVariable
            Custom dummy;

            Assert.That(
                () => dummy = new Custom(colors),
                Throws.InstanceOf<ArgumentException>().With.Property("ParamName").EqualTo("colors"));
        }

        [Test]
        public void ShouldSetCorrectColorsFromList()
        {
            var colors = new Color[Constants.MaxLeds];
            colors[0] = Color.red;
            colors[1] = Color.blue;
            colors[2] = Color.green;

            var effect = new Custom(colors);

            for (var i = 0; i < Constants.MaxLeds; i++)
                Assert.That(effect[i], Is.EqualTo(colors[i]));
        }

        [Test]
        public void ShouldSetAllColorsWithSet()
        {
            var effect = Custom.Create();

            effect.Set(Color.red);

            for (var i = 0; i < Constants.MaxLeds; i++)
                Assert.That(effect[i], Is.EqualTo(Color.red));
        }

        [Test]
        public void ShouldResetToBlackWithClear()
        {
            var effect = Custom.Create();
            effect.Set(Color.red);
            effect.Clear();

            for (var i = 0; i < Constants.MaxLeds; i++)
                Assert.That(effect[i], Is.EqualTo(Color.black));
        }

        [Test]
        public void ShouldGetCorrectColor()
        {
            var colors = new Color[Constants.MaxLeds];
            colors[5] = Color.red;

            var effect = new Custom(colors);

            Assert.That(effect[5], Is.EqualTo(colors[5]));
        }

        [Test]
        public void ShouldSetCorrectColor()
        {
            var effect = Custom.Create();
            effect[5] = Color.blue;

            Assert.That(effect[5], Is.EqualTo(Color.blue));
        }

        [Test]
        public void ShouldEqualIdenticalEffect()
        {
            var a = new Custom(Color.red);
            var b = new Custom(Color.red);

            Assert.True(a == b);
            Assert.False(a != b);
            Assert.True(a.Equals(b));
            Assert.AreEqual(a, b);
        }

        [Test]
        public void ShouldNotEqualDifferentEffect()
        {
            var a = new Custom(Color.red);
            var b = new Custom(Color.blue);

            Assert.False(a == b);
            Assert.True(a != b);
            Assert.False(a.Equals(b));
            Assert.AreNotEqual(a, b);
        }

        [Test]
        public void ShouldEqualIdenticalArray()
        {
            var effect = new Custom(Color.red);
            var array = new Color[Constants.MaxLeds];

            for (var i = 0; i < Constants.MaxLeds; i++)
                array[i] = Color.red;

            Assert.True(effect == array);
            Assert.False(effect != array);
            Assert.True(effect.Equals(array));
            Assert.AreEqual(effect, array);
        }

        [Test]
        public void ShouldNotEqualDifferentArray()
        {
            var effect = new Custom(Color.red);
            var array = new Color[Constants.MaxLeds];

            for (var i = 0; i < Constants.MaxLeds; i++)
                array[i] = Color.blue;

            Assert.False(effect == array);
            Assert.True(effect != array);
            Assert.False(effect.Equals(array));
            Assert.AreNotEqual(effect, array);
        }

        [Test]
        public void ShouldNotEqualArrayWithInvalidLength()
        {
            var effect = new Custom(Color.red);
            var array = new[] { Color.red, Color.red, Color.red };

            Assert.False(effect == array);
            Assert.True(effect != array);
            Assert.False(effect.Equals(array));
            Assert.AreNotEqual(effect, array);
        }

        [Test]
        public void ShouldNotEqualArbitraryObject()
        {
            var effect = Custom.Create();
            var obj = new object();

            Assert.False(effect == obj);
            Assert.True(effect != obj);
            Assert.False(effect.Equals(obj));
            Assert.AreNotEqual(effect, obj);
        }

        [Test]
        public void ShouldNotEqualNull()
        {
            var effect = default(Custom);

            Assert.False(effect == null);
            Assert.True(effect != null);
            Assert.False(effect.Equals(null));
            Assert.AreNotEqual(effect, null);
        }
    }
}
