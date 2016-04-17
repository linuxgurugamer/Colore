﻿// ---------------------------------------------------------------------------------------
// <copyright file="Mousepad.cs" company="Corale">
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
// ---------------------------------------------------------------------------------------

namespace Corale.Colore.Core
{
    using System;

    using Corale.Colore.Razer.Mousepad.Effects;

    /// <summary>
    /// Class for interacting with a Chroma mouse pad.
    /// </summary>
    public sealed class Mousepad : Device, IMousepad
    {
        /// <summary>
        /// Lock object for thread-safe init.
        /// </summary>
        private static readonly object InitLock = new object();

        /// <summary>
        /// Singleton instance.
        /// </summary>
        private static IMousepad _instance;

        /// <summary>
        /// Internal <see cref="Custom" /> struct used for effects.
        /// </summary>
        private Custom _custom;

        /// <summary>
        /// Prevents a default instance of the <see cref="Mousepad" /> class from being created.
        /// </summary>
        private Mousepad()
        {
            Chroma.InitInstance();
            _custom = Custom.Create();
        }

        /// <summary>
        /// Gets the application-wide instance of the <see cref="IMousepad" /> interface.
        /// </summary>
        public static IMousepad Instance
        {
            get
            {
                lock (InitLock)
                {
                    return _instance ?? (_instance = new Mousepad());
                }
            }
        }

        /// <summary>
        /// Gets or sets a specific LED on the mouse pad.
        /// </summary>
        /// <param name="index">The index to access.</param>
        /// <returns>The current <see cref="Color" /> at the <paramref name="index"/>.</returns>
        public Color this[int index]
        {
            get
            {
                return _custom[index];
            }

            set
            {
                _custom[index] = value;
                SetCustom(_custom);
            }
        }

        /// <summary>
        /// Sets the color of all components on this device.
        /// </summary>
        /// <param name="color">Color to set.</param>
        public override void SetAll(Color color)
        {
            _custom.Set(color);
            SetCustom(_custom);
        }

        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="Effect.None" /> effect.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public void SetEffect(Effect effect)
        {
            SetGuid(NativeWrapper.CreateMousepadEffect(effect, IntPtr.Zero));
        }

        /// <summary>
        /// Sets a breathing effect on the mouse pad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Breathing" /> struct.</param>
        public void SetBreathing(Breathing effect)
        {
            SetGuid(NativeWrapper.CreateMousepadEffect(Effect.Breathing, effect));
        }

        /// <summary>
        /// Sets a breathing effect on the mouse pad.
        /// </summary>
        /// <param name="first">First color to breathe into.</param>
        /// <param name="second">Second color to breathe into.</param>
        public void SetBreathing(Color first, Color second)
        {
            SetBreathing(new Breathing(first, second));
        }

        /// <summary>
        /// Sets an effect on the mouse pad that causes
        /// it to breathe between random colors.
        /// </summary>
        public void SetBreathing()
        {
            SetBreathing(new Breathing(BreathingType.Random, Color.Black, Color.Black));
        }

        /// <summary>
        /// Sets a static color effect on the mouse pad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Static" /> struct.</param>
        public void SetStatic(Static effect)
        {
            SetGuid(NativeWrapper.CreateMousepadEffect(Effect.Static, effect));
        }

        /// <summary>
        /// Sets a static color effect on the mouse pad.
        /// </summary>
        /// <param name="color">Color to set.</param>
        public void SetStatic(Color color)
        {
            SetStatic(new Static(color));
        }

        /// <summary>
        /// Sets a wave effect on the mouse pad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Wave" /> struct.</param>
        public void SetWave(Wave effect)
        {
            SetGuid(NativeWrapper.CreateMousepadEffect(Effect.Wave, effect));
        }

        /// <summary>
        /// Sets a wave effect on the mouse pad.
        /// </summary>
        /// <param name="direction">Direction of the wave.</param>
        public void SetWave(Direction direction)
        {
            SetWave(new Wave(direction));
        }

        /// <summary>
        /// Sets a custom effect on the mouse pad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Custom" /> struct.</param>
        public void SetCustom(Custom effect)
        {
            SetGuid(NativeWrapper.CreateMousepadEffect(Effect.Custom, effect));
        }
    }
}
