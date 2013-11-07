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

namespace Mythe.Players
{
    public enum HansStatus
    {
        climbing,
        walking,
        jumping,
        crouching,
        pushing,
    }

    public class Hans : AnimateSprite
    {

        private bool PulltheLever;

        private float speaktimer = 0;
        public int speed;
        public Vector2 velocity;
        bool hasJumped;
        float startY;
        bool gravity;
        int points;
        float gravitypower;
        float elapsedTime;

        AnimationClass IdleAnimate;
        public HansStatus status = HansStatus.walking;
        public TestLevel level;

        private float timeElapsed;
        private float timeToUpdate = 0.05f;

        public int FramesPerSecond
        {
            set { timeToUpdate = (1f / value); }
        }

        public Hans(Texture2D texture,int frames,int animations)
            : base(texture,frames,animations)
        {
            points = 0;
            speed = 50;

            gravitypower = 10;
            startY = position.Y;
            hasJumped = true;
            gravity = false;
        }

        public void LoadContent(ContentManager content)
        {    
            //animaties inladen
            IdleAnimate = new AnimationClass();
            FramesPerSecond = 17;

            addAnimation(Game1.Hanstextures[0], "idle", 1, 11, IdleAnimate.Copy());
            addAnimation(Game1.Hanstextures[1], "walk", 1, 14, IdleAnimate.Copy());
            addAnimation(Game1.Hanstextures[2], "jump", 1, 6, IdleAnimate.Copy());

            position = new Vector2(3700, 600);
            spriteEffects = SpriteEffects.FlipHorizontally;
        }

        public void UpdateAnimation(GameTime gametime)
        {
            timeElapsed += (float)gametime.ElapsedGameTime.TotalSeconds;

            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;

                if (FrameIndex < Animations[Animation].Frames - 1)
                    FrameIndex++;
                else if (Animations[Animation].IsLooping)
                    FrameIndex = 0;
            }
        }

        public void Update(GameTime gametime)
        {
            KeyboardState kb = Keyboard.GetState();
            elapsedTime += (float)gametime.ElapsedGameTime.TotalSeconds;
            speaktimer += (float)gametime.ElapsedGameTime.TotalSeconds;
            rectangle = new Rectangle((int)position.X, (int)position.Y, 40,48);
            Center = new Vector2(position.X + width / 2, position.Y + height / 2);


            Collision();

            // player movement en acties
            if (kb.IsKeyDown(Keys.Left))
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }

            if (kb.IsKeyDown(Keys.Right))
            {
                spriteEffects = SpriteEffects.None;
            }
            for (int i = 0; i < TestLevel.collectibles.Count; i++)
            {
                if (rectangle.Intersects(TestLevel.collectibles[i].rectangle))
                {

                    points += TestLevel.collectibles[i].value;
                    TestLevel.collectibles[i].pickedUp = true;
                }
            }

            if (speaktimer >= 20)
            {
                Game1.i_hope.Play(0.5f,0,0);
                
                speaktimer = 0;
            }

            foreach (Ladder ladder in TestLevel.ladders)
            {
                if (rectangle.Intersects(ladder.rectangle))
                {
                    if (kb.IsKeyDown(Keys.W))
                    {
                        status = HansStatus.climbing;
                    }
                }
            }

            for (int i = 0; i < TestLevel.buttons.Count; i++)
            {
                if (rectangle.Intersects(TestLevel.buttons[i].rectangle))
                {
                    if (PulltheLever)
                    {
                        speaktimer += (float)gametime.ElapsedGameTime.TotalSeconds;

                        for (int h = 0; h < TestLevel.doors.Count; h++)
                        {
                            if (TestLevel.buttons[i].indexNumber == TestLevel.doors[h].indexNumber)
                            {
                                if (speaktimer >= 1.5f)
                                {
                                    TestLevel.doors[i].OpenDoor();
                                    Game1.lever.Play();
                                    speaktimer = 0;
                                    PulltheLever = false;
                                }

                            }
                        }
                    }

                    if (kb.IsKeyDown(Keys.LeftControl))
                    {
                            if (PulltheLever == false)
                            {
                                PulltheLever = true;
                                Game1.pullLever.Play();
                            } 
                    }

                }
            }

            /*foreach (Door_button button in TestLevel.buttons)
            {
                
                if (rectangle.Intersects(button.rectangle))
                {

                    

                }
            }*/

            foreach (MoveDoorButton button in TestLevel.Mbuttons)
            {
                button.FrameIndex = 1;
                button.UpdateAnimation(gametime);
                if (rectangle.Intersects(button.rectangle))
                {
                    if (kb.IsKeyDown(Keys.LeftControl))
                    {
                        for (int h = 0; h < TestLevel.Mbuttons.Count; h++)
                        {
                            if (button.indexNumber == TestLevel.Mdoors[h].indexNumber)
                            {
                                TestLevel.Mdoors[h].move = true;
                            }
                        }
                    }
                }
            }

            if (status == HansStatus.walking)
            {
                if (kb.IsKeyDown(Keys.W) && hasJumped == false)
                {
                    position.Y -= 10f;
                    velocity.Y = -4f;
                    hasJumped = true;
                }

                else if (hasJumped)
                {
                    if (Animation != "jump")
                    {
                        Animation = "jump";
                        IdleAnimate.Frames = 6;
                    }

                    if (kb.IsKeyDown(Keys.A))
                    {

                        position.X += velocity.X;
                        velocity.X = -3;
                    }

                    if (position.X < 3875)
                    {
                        if (kb.IsKeyDown(Keys.D))
                        {

                            position.X += velocity.X;
                            velocity.X = 3;
                        }
                    }
                    
                    velocity.Y += 0.15f;
                    position.Y += velocity.Y;
                }
                
               else if (kb.IsKeyDown(Keys.A))
                {
                    
                    position.X += velocity.X;
                    velocity.X = -3;
                    if (Animation != "walk")
                    {
                        Animation = "walk";
                        IdleAnimate.Frames = 14;
                    }
                }

                else if (kb.IsKeyDown(Keys.D))
                {

                    position.X += velocity.X;
                    velocity.X = 3;
                    if (Animation != "walk")
                    {
                        Animation = "walk";
                        IdleAnimate.Frames = 14;
                    }
                    
                }

                else
                {
                    if (Animation != "idle")
                    {
                        Animation = "idle";
                        IdleAnimate.Frames = 11;
                    }
                }

                if (gravity)
                {
                    velocity.Y += gravitypower * (float)gametime.ElapsedGameTime.TotalSeconds;
                    position.Y += velocity.Y;
                }



                /*if (rectangle.Intersects(TestLevel.door.rectangle))
                {
                    position.X = TestLevel.door.position.X + TestLevel.door.texture.Width;
                }

                TestLevel.door.Update(gametime);
                TestLevel.button.ReactToDoor(TestLevel.door,position);
                */

                gravity = true;

            }

            if (status == HansStatus.climbing)
            {
                if (kb.IsKeyDown(Keys.W))
                {
                    position.Y -= 1;
                } 
                
                foreach (Ladder ladder in TestLevel.ladders)
                {
                    if (!rectangle.Intersects(ladder.rectangle))
                    {
                        status = HansStatus.walking;
                    }
                }
            }
        }

        public void Collision()
        {
            if (rectangle.Intersects(TestLevel.liftobject.rectangle))
            {
                if (rectangle.TouchTopOf(TestLevel.liftobject.rectangle))
                {
                    hasJumped = false;
                    gravity = false;
                    velocity.Y = 0;
                    position.Y = TestLevel.liftobject.rectangle.Y - rectangle.Height;
                }

                if (rectangle.TouchLeftOf(TestLevel.liftobject.rectangle))
                {
                    position.X = TestLevel.liftobject.rectangle.X - 180;
                }

                else if (rectangle.TouchRightOf(TestLevel.liftobject.rectangle))
                {
                    position.X = TestLevel.liftobject.rectangle.X + 180;
                }

                else if (rectangle.TouchBottomOf(TestLevel.liftobject.rectangle))
                {
                    position.Y = TestLevel.liftobject.rectangle.Y + rectangle.Height;
                }
            }

            foreach (PressurePlate plate in TestLevel.plates)
            {
                if (rectangle.Intersects(plate.rectangle))
                {
                    if (rectangle.TouchTopOf(plate.rectangle))
                    {
                        hasJumped = false;
                        gravity = false;
                        velocity.Y = 0;
                        position.Y = plate.rectangle.Y - rectangle.Height;
                        plate.pressed = true;
                    }
                    else
                    {
                        plate.pressed = false;
                    }

                    if (rectangle.TouchLeftOf(plate.rectangle))
                    {
                        position.X = plate.rectangle.X - 22;
                    }

                    else if (rectangle.TouchRightOf(plate.rectangle))
                    {
                        position.X = plate.rectangle.X + 22;
                    }

                    else if (rectangle.TouchBottomOf(plate.rectangle))
                    {
                        position.Y = plate.rectangle.Y + rectangle.Height;
                    }
                }

            }

            foreach (Door door in TestLevel.doors)
            {
                if (rectangle.Intersects(door.rectangle))
                {
                    if (rectangle.TouchTopOf(door.rectangle))
                    {
                        hasJumped = false;
                        gravity = false;
                        velocity.Y = 0;
                        position.Y = door.rectangle.Y - rectangle.Height;
                    }

                    else if (rectangle.TouchLeftOf(door.rectangle))
                    {
                        position.X = door.rectangle.X - rectangle.Width - 5;
                    }

                    else if (rectangle.TouchRightOf(door.rectangle))
                    {
                        position.X = door.rectangle.X + rectangle.Width + 5;
                    }

                    else if (rectangle.TouchBottomOf(door.rectangle))
                    {
                        position.Y = door.rectangle.Y + rectangle.Height;
                    }
                }
            }

            foreach (MoveDoor obj in TestLevel.Mdoors)
            {
                if (rectangle.Intersects(obj.rectangle))
                {
                    if (rectangle.TouchTopOf(obj.rectangle))
                    {
                        hasJumped = false;
                        gravity = false;
                        velocity.Y = 0;
                        position.Y = obj.rectangle.Y - rectangle.Height;
                    }

                    else if (rectangle.TouchLeftOf(obj.rectangle))
                    {
                        position.X = obj.rectangle.X - rectangle.Width - 30;
                    }

                    else if (rectangle.TouchRightOf(obj.rectangle))
                    {
                        position.X = obj.rectangle.X + rectangle.Width + 30;
                    }

                    if (rectangle.TouchBottomOf(obj.rectangle))
                    {
                    }
                }
            }

            foreach (MoveObject obj in TestLevel.moveObjects)
            {
                if (rectangle.Intersects(obj.rectangle))
                {
                    if (rectangle.TouchTopOf(obj.rectangle))
                    {
                        hasJumped = false;
                        gravity = false;
                        velocity.Y = 0;
                        position.Y = obj.rectangle.Y - rectangle.Height;
                    }

                    else if (rectangle.TouchLeftOf(obj.rectangle))
                    {
                        position.X = obj.rectangle.X - rectangle.Width;

                    }

                    else if (rectangle.TouchRightOf(obj.rectangle))
                    {
                        position.X = obj.rectangle.X + rectangle.Width;
                        
                    }

                    if (rectangle.TouchBottomOf(obj.rectangle))
                    {
                    }
                }
            }
            foreach (Steen steen in TestLevel.stones)
            {
                if (rectangle.Intersects(steen.rectangle))
                {
                    if (rectangle.TouchTopOf(steen.rectangle))
                    {
                        hasJumped = false;
                        gravity = false;
                        velocity.Y = 0;
                        position.Y = steen.rectangle.Y - rectangle.Height;
                    }

                    if (rectangle.TouchLeftOf(steen.rectangle))
                    {
                        position.X = steen.rectangle.X - rectangle.Width;
                    }

                    if (rectangle.TouchRightOf(steen.rectangle))
                    {
                        position.X = steen.rectangle.Right;
                    }

                    if (rectangle.TouchBottomOf(steen.rectangle))
                    {
                    }
                }
            }

            foreach (blok blok in TestLevel.Tiles)
            {
                if (rectangle.Intersects(blok.rectangle))
                {
                    if (rectangle.TouchTopOf(blok.rectangle))
                    {

                        velocity.Y = 0;

                        gravity = false;
                        hasJumped = false;
                        position.Y = blok.rectangle.Y - rectangle.Height;
                    }

                    else if (rectangle.TouchLeftOf(blok.rectangle))
                    {
                        position.X = blok.rectangle.X - rectangle.Width;
                    }

                    else if (rectangle.TouchRightOf(blok.rectangle))
                    {
                        position.X = blok.rectangle.X + rectangle.Width;
                    }

                    else if (rectangle.TouchBottomOf(blok.rectangle))
                    {
                        position.Y = blok.rectangle.Bottom;
                    }
                }

            }
        }

        public void SPulltheLever()
        {
            bool say = true;
            if(say)
            Game1.pullLever.Play();
            say = false;
        }

    }
}
