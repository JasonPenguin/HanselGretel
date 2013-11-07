using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using Mythe.Animatie;

namespace Mythe.Level.Assets1
{
    public class CollectItem : AnimateSprite
    {
        public int value;
        public bool pickedUp;
        public AnimationClass idleAnimate;
        public float timeElapsed;
        public float timeToUpdate = 0.05f;

        public CollectItem(Texture2D texture,int frames,int animations)
            :base(texture,frames,animations)
        {
            pickedUp = false;
            value = 10;
        }

        public int FramesPerSecond
        {
            set { timeToUpdate = (1f / value); }
        }

        public void UpdateAnimation(GameTime gametime)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, 60, 30);
            Center = new Vector2(position.X + rectangle.Width / 2, position.Y + rectangle.Height / 2);
            timeElapsed += (float)gametime.ElapsedGameTime.TotalSeconds;

            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;

                if (FrameIndex < Animations[Animation].Frames - 1)
                    FrameIndex++;
                else if (Animations[Animation].IsLooping)
                    FrameIndex = 0;
            }
        }
    }
}
