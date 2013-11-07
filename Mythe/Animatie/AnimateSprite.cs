using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mythe.Animatie
{
    public class AnimateSprite
    {
        // de class voor de animaties
        public Texture2D texture;
        public Vector2 position;
        protected Vector2 Origin;
        public Rectangle rectangle;
        public SpriteEffects spriteEffects;
        public Vector2 Center;
        protected Dictionary<string, AnimationClass> Animations = new Dictionary<string, AnimationClass>();
        public int FrameIndex = 0;

        public int height;
        public int width;
        public int frames;

        private string animation;

        public string Animation
        {
            get { return animation; }
            set
            {
                animation = value;
                FrameIndex = 0;
            }
        }

        public AnimateSprite(Texture2D texture,int frames,int animations)
        {
            this.texture = texture;
            width = texture.Width / frames;
            height = texture.Height / animations;
            Origin = new Vector2(width / 2, height / 2);
            rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
           
        }

        public void addAnimation(Texture2D texture,string name, int row, int frames, AnimationClass animation)
        {
            //animatie toevoegen aan Dictionary
            Rectangle[] recs = new Rectangle[frames];

            width = texture.Width / frames;
            height = texture.Height;
            for (int i = 0; i < frames; i++)
            {
                recs[i] = new Rectangle(i * width, (row - 1) * height, width, height);
            }
            animation.Frames = frames;
            animation.texture = texture;
            animation.Rectangles = recs;
            Animations.Add(name, animation);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Animations[Animation].texture,Center,
                Animations[Animation].Rectangles[FrameIndex],
                Animations[Animation].Color,
                Animations[Animation].Rotation, Origin,
                Animations[Animation].Scale,
                spriteEffects, 0f);
        }
    }
}
