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

namespace Mythe.Level
{
    public class LevelManager
    {
        public List<TestLevel> levels;
        public TestLevel level;
        public TestLevel level2;

        public LevelManager(ContentManager content)
        {
            levels = new List<TestLevel>();
            level = new TestLevel(Game1.pftexture, "map",new Vector2(0,720));
            level.LoadContent(content);
        }

        

        public void Update(GameTime gametime)
        {
            level.Update(gametime);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            level.Draw(spritebatch);
        }
    }
}
