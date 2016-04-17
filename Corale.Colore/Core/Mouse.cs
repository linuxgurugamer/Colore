﻿// ---------------------------------------------------------------------------------------
// <copyright file="Mouse.cs" company="Corale">
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

    using Corale.Colore.Annotations;
    using Corale.Colore.Razer.Mouse;
    using Corale.Colore.Razer.Mouse.Effects;

    /// <summary>
    /// Class for interacting with a Chroma mouse.
    /// </summary>
    [PublicAPI]
    public sealed class Mouse : Device, IMouse
    {
        /// <summary>
        /// Lock object for thread-safe init.
        /// </summary>
        private static readonly object InitLock = new object();

        /// <summary>
        /// Holds the application-wide instance of the <see cref="IMouse" /> interface.
        /// </summary>
        private static IMouse _instance;

        /// <summary>
        /// Internal instance of a <see cref="Custom" /> struct.
        /// </summary>
        private Custom _custom;

        /// <summary>
        /// Internal instance of a <see cref="CustomGrid" /> struct.
        /// </summary>
        private CustomGrid _customGrid;

        /// <summary>
        /// Prevents a default instance of the <see cref="Mouse" /> class from being created.
        /// </summary>
        private Mouse()
        {
            Chroma.InitInstance();
            _custom = Custom.Create();
            _customGrid = CustomGrid.Create();
        }

        /// <summary>
        /// Gets the application-wide instance of the <see cref="IMouse" /> interface.
        /// </summary>
        [PublicAPI]
        public static IMouse Instance
        {
            get
            {
                lock (InitLock)
                {
                    return _instance ?? (_instance = new Mouse());
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for a specific LED index on the mouse.
        /// </summary>
        /// <param name="index">The index to query, between <c>0</c> and <see cref="Constants.MaxLeds" /> (exclusive upper-bound).</param>
        /// <returns>The <see cref="Color" /> at the specified index.</returns>
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
        /// Gets or sets the <see cref="Color" /> for a specific <see cref="Led" /> on the mouse.
        /// </summary>
        /// <param name="led">The <see cref="Led" /> to query.</param>
        /// <returns>The <see cref="Color" /> currently set for the specified <see cref="Led" />.</returns>
        public Color this[Led led]
        {
            get
            {
                return _custom[led];
            }

            set
            {
                _custom[led] = value;
                SetCustom(_custom);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for a specific position
        /// on the mouse's virtual grid.
        /// </summary>
        /// <param name="row">The row to query, between <c>0</c> and <see cref="Constants.MaxRows" /> (exclusive upper-bound).</param>
        /// <param name="column">The column to query, between <c>0</c> and <see cref="Constants.MaxColumns" /> (exclusive upper-bound).</param>
        /// <returns>The <see cref="Color" /> at the specified position.</returns>
        public Color this[int row, int column]
        {
            get
            {
                return _customGrid[row, column];
            }

            set
            {
                _customGrid[row, column] = value;
                SetGrid(_customGrid);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for a specified <see cref="GridLed" />
        /// on the mouse's virtual grid.
        /// </summary>
        /// <param name="led">The <see cref="GridLed" /> to query.</param>
        /// <returns>The <see cref="Color" /> currently set for the specified <see cref="GridLed" />.</returns>
        public Color this[GridLed led]
        {
            get
            {
                return _customGrid[led];
            }

            set
            {
                _customGrid[led] = value;
                SetGrid(_customGrid);
            }
        }

        /// <summary>
        /// Sets the color of a specific LED on the mouse.
        /// </summary>
        /// <param name="led">Which LED to modify.</param>
        /// <param name="color">Color to set.</param>
        /// <param name="clear">If <c>true</c>, the mouse will first be cleared before setting the LED.</param>
        public void SetLed(Led led, Color color, bool clear = false)
        {
            if (clear)
            {
                _custom.Clear();

                // Clear the grid effect as well, this way the mouse
                // will behave slightly more predictable for the caller.
                _customGrid.Clear();
            }

            _custom[led] = color;
            SetCustom(_custom);
        }

        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="Effect.None" /> effect.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public void SetEffect(Effect effect)
        {
            SetGuid(NativeWrapper.CreateMouseEffect(effect, IntPtr.Zero));
        }

        /// <summary>
        /// Sets a breathing effect on the mouse.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Breathing" /> effect.</param>
        public void SetBreathing(Breathing effect)
        {
            SetGuid(NativeWrapper.CreateMouseEffect(Effect.Breathing, effect));
        }

        /// <summary>
        /// Sets an effect on the mouse that causes it to breathe
        /// between two different colors, fading to black in-between each one.
        /// </summary>
        /// <param name="first">First color to breathe into.</param>
        /// <param name="second">Second color to breathe into.</param>
        /// <param name="led">The LED(s) on which to apply the effect.</param>
        public void SetBreathing(Color first, Color second, Led led = Led.All)
        {
            SetBreathing(new Breathing(led, first, second));
        }

        /// <summary>
        /// Sets an effect on the mouse that causes it to breathe
        /// a single color. The specified color will fade in
        /// on the mouse, then fade to black, and repeat.
        /// </summary>
        /// <param name="color">The color to breathe.</param>
        /// <param name="led">The LED(s) on which to apply the effect.</param>
        public void SetBreathing(Color color, Led led = Led.All)
        {
            SetBreathing(new Breathing(led, color));
        }

        /// <summary>
        /// Instructs the mouse to breathe random colors.
        /// </summary>
        /// <param name="led">The LED(s) on which to apply the effect.</param>
        public void SetBreathing(Led led = Led.All)
        {
            SetBreathing(new Breathing(led));
        }

        /// <summary>
        /// Sets a static color on the mouse.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Static" /> effect.</param>
        public void SetStatic(Static effect)
        {
            SetGuid(NativeWrapper.CreateMouseEffect(Effect.Static, effect));
        }

        /// <summary>
        /// Sets a static effect on the mouse.
        /// </summary>
        /// <param name="color">The color to use.</param>
        /// <param name="led">Which LED(s) to affect.</param>
        public void SetStatic(Color color, Led led = Led.All)
        {
            SetStatic(new Static(led, color));
        }

        /// <summary>
        /// Starts a blinking effect on the specified LED.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Blinking" /> effect.</param>
        public void SetBlinking(Blinking effect)
        {
            SetGuid(NativeWrapper.CreateMouseEffect(Effect.Blinking, effect));
        }

        /// <summary>
        /// Starts a blinking effect on the mouse.
        /// </summary>
        /// <param name="color">The color to blink with.</param>
        /// <param name="led">The LED(s) to affect.</param>
        public void SetBlinking(Color color, Led led = Led.All)
        {
            SetBlinking(new Blinking(led, color));
        }

        /// <summary>
        /// Sets a reactive effect on the mouse.
        /// </summary>
        /// <param name="effect">Effect options struct.</param>
        public void SetReactive(Reactive effect)
        {
            SetGuid(NativeWrapper.CreateMouseEffect(Effect.Reactive, effect));
        }

        /// <summary>
        /// Sets a reactive effect on the mouse.
        /// </summary>
        /// <param name="duration">How long the effect should last.</param>
        /// <param name="color">The color to react with.</param>
        /// <param name="led">Which LED(s) to affect.</param>
        public void SetReactive(Duration duration, Color color, Led led = Led.All)
        {
            SetReactive(new Reactive(led, duration, color));
        }

        /// <summary>
        /// Sets a spectrum cycling effect on the mouse.
        /// </summary>
        /// <param name="effect">Effect options struct.</param>
        public void SetSpectrumCycling(SpectrumCycling effect)
        {
            SetGuid(NativeWrapper.CreateMouseEffect(Effect.SpectrumCycling, effect));
        }

        /// <summary>
        /// Sets a spectrum cycling effect on the mouse.
        /// </summary>
        /// <param name="led">The LED(s) to affect.</param>
        public void SetSpectrumCycling(Led led = Led.All)
        {
            SetSpectrumCycling(new SpectrumCycling(led));
        }

        /// <summary>
        /// Sets a wave effect on the mouse.
        /// </summary>
        /// <param name="effect">Effect options struct.</param>
        public void SetWave(Wave effect)
        {
            SetGuid(NativeWrapper.CreateMouseEffect(Effect.Wave, effect));
        }

        /// <summary>
        /// Sets a wave effect on the mouse.
        /// </summary>
        /// <param name="direction">Direction of the wave.</param>
        public void SetWave(Direction direction)
        {
            SetWave(new Wave(direction));
        }

        /// <summary>
        /// Sets the color of all LEDs on the mouse.
        /// </summary>
        /// <param name="color">Color to set.</param>
        public override void SetAll(Color color)
        {
            // We update both the Custom and CustomGrid effect to keep them both
            // as synced as possible.
            _custom.Set(color);
            _customGrid.Set(color);
            SetGrid(_customGrid);
        }

        /// <summary>
        /// Sets a custom effect on the mouse.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Custom" /> struct.</param>
        public void SetCustom(Custom effect)
        {
            SetGuid(NativeWrapper.CreateMouseEffect(Effect.Custom, effect));
        }

        /// <summary>
        /// Sets a custom grid effect on the mouse.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="CustomGrid" /> struct.</param>
        public void SetGrid(CustomGrid effect)
        {
            SetGuid(NativeWrapper.CreateMouseEffect(Effect.CustomGrid, effect));
        }
    }
}
