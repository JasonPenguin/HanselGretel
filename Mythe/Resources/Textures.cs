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
    public class Textures
    {
        //textures
        public static Texture2D texture;
        public static Texture2D bgtexture;
        public static Texture2D pftexture;
        public static Texture2D stonetex;

        public static Texture2D laag1;
        public static Texture2D laag2;
        public static Texture2D laag3;

        public static void Load()
        {
            //texture = Globals.Content.Load<Texture2D>("Pixel");
            //pftexture = Globals.Content.Load<Texture2D>("Pfblok");
            
            bgtexture = Globals.Content.Load<Texture2D>("GFX\\Level\\BG");
            laag1 = Globals.Content.Load<Texture2D>("GFX\\Level\\laag__1");
            laag2 = Globals.Content.Load<Texture2D>("GFX\\Level\\laag__2");
            laag3 = Globals.Content.Load<Texture2D>("GFX\\Level\\laag__3");
        }
    }
}
