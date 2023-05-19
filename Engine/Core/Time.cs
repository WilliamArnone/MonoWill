using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
    public static class Time
    {
        public static float TimeScale { get; set; }

        /// <summary>
        /// Unscaled time between each frame.
        /// </summary>
        public static float RealTimeDelta { get; private set; }
        /// <summary>
        /// Scaled time between each frame. If you want unscaled version use <see cref="realTimeDelta"/>
        /// </summary>
        public static float TimeDelta { get; private set; }

        /// <summary>
        /// Unscaled total duration of the game. If you want the scaled version use <see cref="ScaledTime"/>
        /// </summary>
        public static float RealTime { get; private set; }
        /// <summary>
        /// Scaled total duration of game.
        /// </summary>
        public static float ScaledTime { get; private set; }

        internal static void Initialize()
        {
            RealTime = 0f;
            RealTimeDelta = 0f;
            ScaledTime = 0f;
            TimeScale = 1f;
		}

        internal static void Update(GameTime gameTime)
        {
            RealTime = (float) gameTime.TotalGameTime.TotalSeconds;
            RealTimeDelta = (float) gameTime.ElapsedGameTime.TotalSeconds;
            TimeDelta = RealTimeDelta * TimeScale;
			ScaledTime += TimeDelta;
        }
    }
}
