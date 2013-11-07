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
    public class StartScreen
    {
        public Texture2D canvas;
        public Texture2D button_up;
        public Texture2D landschapSill;
        public Texture2D plank;
        public Texture2D touwen;

        public float speed = 0.3f;
        public float touwspeed;

        public StartScreen()
        {

        }

        public void Update(GameTime gametime)
        {
            speed += 0.1f;
            touwspeed += 0.3f;
        }

        public void LoadContent(ContentManager content)
        {
            canvas = content.Load<Texture2D>("GFX\\Startscreen\\startscherm");
            touwen = content.Load<Texture2D>("GFX\\Startscreen\\canvas_scherm");
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            spritebatch.Draw(touwen,new Rectangle(10,0,1000,700), Color.White);
            spritebatch.Draw(canvas, new Vector2(0, 0), Color.White);
            spritebatch.End();
        }
    }
}
