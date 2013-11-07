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

using Mythe.Resources;
using Mythe.Players;
using Mythe.Level;
using Mythe.Animatie;

namespace Mythe.Level.Assets1
{
    public class Steen : AnimateSprite
    {
        public int health;
        public bool Crushed;

        private float timeElapsed;
        private float timeToUpdate = 0.05f;

        public int FramesPerSecond
        {
            set { timeToUpdate = (1f / value); }
        }
        
        public Steen(Texture2D texture,int frames,int animations)
            :base(texture,frames,animations)
        {
            health = 100;
            Crushed = false;
        }

        public void Update(GameTime gameTime)
        {
            if (health <= 0)
            {
                UpdateAnimation(gameTime);
                Animations[Animation].IsLooping = false;
                if (FrameIndex == 12)
                {
                    Crushed = true;
                }
            }

            rectangle = new Rectangle((int)position.X, (int)position.Y,60,80);
            Center = new Vector2(position.X + rectangle.Width / 2, position.Y + rectangle.Height /2 );
        }

        public void UpdateAnimation(GameTime gametime)
        {
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
