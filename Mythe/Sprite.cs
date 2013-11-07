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

using Mythe.Level.Assets1;
using Mythe.Players;
using Mythe.Resources;

namespace Mythe
{
    public class Sprite
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 center;
        protected Vector2 origin;
        protected float rotation;
        public Vector2 velocity;
        public Rectangle rectangle;

        public Sprite(Texture2D texture,Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            
            velocity = Vector2.Zero;
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            center = new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2);
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public virtual void Update(GameTime gametime)
        {
            this.center = new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2);
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture,center, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0);
        }
    }
}
