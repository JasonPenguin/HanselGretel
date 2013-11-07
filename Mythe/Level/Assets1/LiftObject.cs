using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mythe.Level.Assets1
{
    public class LiftObject : Sprite
    {
        public bool lift = false;

        public LiftObject(Texture2D texture,Vector2 position)
            :base(texture,position)
        {

        }

        public override void Update(GameTime gametime)
        {
            
            base.Update(gametime);
            rectangle = new Rectangle((int)position.X, (int)position.Y, 180, 84);
            
        }

        public void LiftObj(Vector2 newposition,float speed)
        {
            
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);
        }
    }
}
