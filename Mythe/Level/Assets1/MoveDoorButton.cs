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

using Mythe.Players;
using Mythe.Animatie;

namespace Mythe.Level.Assets1
{
    public class MoveDoorButton : AnimateSprite
    {
        public int indexNumber;
        public string buttonName;
        private float timeElapsed;
        private float timeToUpdate = 0.05f;

        public int FramesPerSecond
        {
            set { timeToUpdate = (1f / value); }
        }

        public MoveDoorButton(Texture2D texture,int frames,int animations,int indexNumber)
            :base(texture,frames,animations)
        {
            this.indexNumber = indexNumber;
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

        public void ReactToDoor(Vector2 playerPos, Door door)
        {
            if (Vector2.Distance(position, playerPos) < 50)
            {
                if (this.buttonName == door.name)
                {
                    door.OpenDoor();
                }
            }
        }
    }
}
