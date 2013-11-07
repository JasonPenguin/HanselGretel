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

using Mythe.Level;
using Mythe.Level.Assets1;
using Mythe.Resources;
using Mythe.Players;
using Mythe.Animatie;

namespace Mythe
{
    public class EndScreen
    {
        public Texture2D background;
        public EndScreen()
        {

        }

        public void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>("GFX\\achtergrond");
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            spritebatch.Draw(background, new Vector2(0, 0), Color.White);
            spritebatch.End();
        }
    }
}
