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

namespace Mythe.Level.Assets1
{
    public class Ladder : Sprite
    {
        public Ladder(Texture2D texture, Vector2 position)
            :base(texture,position)
        {
        }

        public override void Update(GameTime gametime)
        {
            rectangle = new Rectangle((int)position.X,(int)position.Y,texture.Width,texture.Height);
            base.Update(gametime);
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);
        }
    }
}
