using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Mythe.Animatie
{
    public class FrameSprite : AnimateSprite
    {
        private float timeElapsed;
        private float timeToUpdate = 0.05f;

        public int FramesPerSecond
        {
            set { timeToUpdate = (1f / value); }
        }

        public FrameSprite(Texture2D texture, int frames, int animations)
            : base(texture, frames, animations)
        {

        }

        
       
    }
}
