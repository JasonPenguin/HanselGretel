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

using Mythe.Animatie;

using Mythe.Resources;
using Mythe.Players;
using Mythe.Level;

namespace Mythe.Level.Assets1
{
    public class MoveObject : AnimateSprite
    {

        private float timeElapsed;
        private float timeToUpdate = 0.05f;

        public int FramesPerSecond
        {
            set { timeToUpdate = (1f / value); }
        }

        public MoveObject(Texture2D texture,int frames,int animations)
            :base(texture,frames,animations)
        {
            
        }

        public void UpdateAnimation(GameTime gameTime)
        {
           timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
           if (timeElapsed > timeToUpdate)
           {
               timeElapsed -= timeToUpdate;

               if (FrameIndex < Animations[Animation].Frames - 1)
                   FrameIndex++;
               else if (Animations[Animation].IsLooping)
                   FrameIndex = 0;
           }

           
        }

        public void Update(GameTime gametime)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, 40, 80);
            Center = new Vector2(position.X + rectangle.Width / 2, position.Y + rectangle.Height / 2);

            position.Y += 1;

            if (FrameIndex == 15)
            {
                FrameIndex = 5;
            }

            foreach (blok blok in TestLevel.Tiles)
            {
                if (rectangle.Intersects(blok.rectangle))
                {
                    if (rectangle.TouchTopOf(blok.rectangle))
                    {
                        position.Y--;
                    }

                    else if (rectangle.TouchLeftOf(blok.rectangle))
                    {
                        position.X = blok.rectangle.X - rectangle.Width;
                    }

                    else if (rectangle.TouchRightOf(blok.rectangle))
                    {
                        position.X = blok.rectangle.X + rectangle.Width;
                    }
                }

            }
        }
    }
}
