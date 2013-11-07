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
    public class AnimationClass
    {
        public Rectangle[] Rectangles;
        public Color Color = Color.White;
        public Vector2 Origin;
        public float Rotation = 0f;
        public float Scale = 1f;
        public SpriteEffects SpriteEffect;
        public bool IsLooping = true;
        public int Frames;
        public Texture2D texture;

        public AnimationClass Copy()
        {
            AnimationClass ac = new AnimationClass();
            ac.Rectangles = Rectangles;
            ac.Color = Color;
            ac.Origin = Origin;
            ac.Rotation = Rotation;
            ac.Scale = Scale;
            ac.IsLooping = IsLooping;
            ac.Frames = Frames;
            ac.texture = texture;
            return ac;
        }
    }
}
