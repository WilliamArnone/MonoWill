using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
    class Globals
    {
        public static ContentManager Content { get; internal set; }
        public static SpriteBatch SpriteBatch { get; internal set; }
    }
}
