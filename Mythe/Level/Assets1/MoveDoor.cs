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
using Mythe.Level.Assets1;
using Mythe.Resources;
using Mythe.Animatie;

namespace Mythe.Level.Assets1
{
    public class MoveDoor : Sprite
    {
        public bool move;
        public int indexNumber;

        public MoveDoor(Texture2D texture, Vector2 position,int indexNumber)
            : base(texture, position)
        {
            this.indexNumber = indexNumber;
            move = false;
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            rectangle = new Rectangle((int)position.X, (int)position.Y,92,132);

            if (move)
            {
                position.Y += 1;

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

        public void fall()
        {
            move = true;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);
        }
    }
}
