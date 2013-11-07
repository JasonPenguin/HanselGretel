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

namespace Mythe.Level
{
    public class Door : Sprite
    {
        public string name;
        public int indexNumber;

        public bool open;

        public Door(Texture2D texture,Vector2 position,int indexNumber)
            :base(texture,position)
        {
            this.indexNumber = indexNumber;
            open = false;
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
        }

        public void OpenDoor()
        {
            open = true;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);
        }
    }
}
