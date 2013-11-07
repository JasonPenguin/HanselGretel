using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Mythe.Resources
{
    public class Globals
    {
        //global variables
        public static ContentManager Content;
        public static GraphicsDeviceManager Graphics;
        public static SpriteBatch spritebatch;
        public static GameTime GameTime;
        public static Boolean WindowsFocused;
        public static Vector2 GameSize;
        public static RenderTarget2D BackBuffer;
        
    }
}
