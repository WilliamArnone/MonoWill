using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
    class Time
    {
        public static float TimeScale { get; set; }

        /// <summary>
        /// Unscaled time between each frame.
        /// </summary>
        public static TimeSpan RealTimeDelta { get; private set; }
        /// <summary>
        /// Scaled time between each frame. If you want unscaled version use <see cref="realTimeDelta"/>
        /// </summary>
        public static TimeSpan TimeDelta { get; private set; }

        /// <summary>
        /// Unscaled total duration of the game. If you want unscaled version use <see cref="ScaledTime"/>
        /// </summary>
        public static TimeSpan RealTime { get; private set; }
        /// <summary>
        /// Scaled total duration of game.
        /// </summary>
        public static TimeSpan ScaledTime { get; private set; }

        internal static void Initialize()
        {
            RealTime = TimeSpan.Zero;
            RealTimeDelta = TimeSpan.Zero;
            ScaledTime = TimeSpan.Zero;
        }

        internal static void Update(GameTime gameTime)
        {
            RealTime = gameTime.TotalGameTime;
            RealTimeDelta = gameTime.ElapsedGameTime;
            TimeDelta = RealTimeDelta * TimeScale;
			ScaledTime += TimeDelta;
        }
    }
}
