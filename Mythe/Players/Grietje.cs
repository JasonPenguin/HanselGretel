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
using Mythe.Players;
using Mythe.Level.Assets1;
using Mythe.Resources;
using Mythe.Animatie;

namespace Mythe.Players
{
    public enum Status
    {
        climbing,
        walking,
        jumping,
        crouching,
        pushing,
    }

    class Grietje : AnimateSprite
    {
       
        public int speed;
        public Vector2 velocity;
        bool hasJumped;
        public bool onPosition;
        public static bool isPushing;
        bool gravity;
        
        AnimationClass IdleAnimate;
        public Steen stonetarget;
        public Status status = Status.walking;

        float gravitypower;
        public int points;
        public int Damage;

        float startY;

        private float timeElapsed;
        private float timeToUpdate = 0.05f;

        public int FramesPerSecond
        {
            set { timeToUpdate = (1f / value); }
        }

        public Grietje(Texture2D texture,int frames,int animations)
            : base(texture,frames,animations)
        {
            speed = 50;
            Damage = 10;
            points = 0;
            onPosition = true;
            startY = position.Y;
            hasJumped = true;
            gravity = true;
            isPushing = false;
            gravitypower = 10;
        }

        public void LoadContent(ContentManager content)
        {
            IdleAnimate = new AnimationClass();
            FramesPerSecond = 17;

            //Animaties
            addAnimation(Game1.Gretatextures[0], "walk", 1, 28, IdleAnimate.Copy());
            addAnimation(Game1.Gretatextures[1],"idle",1, 12, IdleAnimate.Copy());
            addAnimation(Game1.Gretatextures[2], "smash", 1, 24, IdleAnimate.Copy());
            addAnimation(Game1.Gretatextures[3], "jump", 1, 10, IdleAnimate.Copy());
            
            position = new Vector2(3700, 600);

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
            rectangle = new Rectangle((int)position.X, (int)position.Y,40, 83);
            //UpdateAnimation(gametime);
            Center = new Vector2(position.X + width/2, position.Y + height/2);
            Console.WriteLine(position.Y);
            

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

            
            if (rectangle.Intersects(TestLevel.bigRec))
            {
                rectangle.Width = 95;
                
            }
            else
            {
                rectangle.Width = 40;
            }

            if (status == Status.walking)
            {
                
                Collision();
                if (kb.IsKeyDown(Keys.Up) && hasJumped == false)
                {

                    
                    position.Y -= 20f;
                    velocity.Y = -3f;
                    hasJumped = true;
                }


                else if (hasJumped == true)
                {
                    if (Animation != "jump")
                    {
                        Animation = "jump";
                        IdleAnimate.Frames = 10;
                    }

                    if (kb.IsKeyDown(Keys.Left))
                    {
                        position.X += velocity.X;
                        velocity.X = -2f;
                    }

                    if (kb.IsKeyDown(Keys.Right))
                    {
                        position.X += velocity.X;
                        velocity.X = 2f;
                    }
                    velocity.Y += 0.15f;
                    position.Y += velocity.Y;
                }

                else if (kb.IsKeyDown(Keys.Left))
                {
                    position.X += velocity.X;
                    velocity.X = -2f;
                    if (Animation != "walk")
                    {
                        Animation = "walk";
                        IdleAnimate.Frames = 28;
                    } 
                }

                

                else if (kb.IsKeyDown(Keys.Right))
                {
                    position.X += velocity.X;
                    velocity.X = 2f;
                    if (Animation != "walk")
                    {
                        Animation = "walk";
                        IdleAnimate.Frames = 28;
                    }
                }



                else if (status == Status.pushing)
                {
                    if (kb.IsKeyDown(Keys.Left))
                    {
                        position.X += velocity.X;
                        velocity.X = -1f;
                    }

                    if (kb.IsKeyDown(Keys.Right))
                    {
                        position.X += velocity.X;
                        velocity.X = 1f;
                    }
                }

                else if (kb.IsKeyDown(Keys.RightControl))
                {
                    if (Animation != "smash")
                    {
                        Animation = "smash";
                        IdleAnimate.Frames = 24;
                        width = 40;
                    }
                    Attack(TestLevel.stones);
                }

                else
                {
                    if (Animation != "idle")
                    {
                        Animation = "idle";
                        IdleAnimate.Frames = 12;
                        FramesPerSecond = 17;
                    }
                }

                
                if (gravity)
                {
                    velocity.Y += gravitypower * (float)gametime.ElapsedGameTime.TotalSeconds;
                    position.Y += velocity.Y;
                }

                gravity = true;
            }

            if (status == Status.climbing)
            {
                if (kb.IsKeyDown(Keys.Up))
                {
                    position.Y -= 1;
                }

                foreach (Ladder ladder in TestLevel.ladders)
                {
                    if (!rectangle.Intersects(ladder.rectangle))
                    {
                        status = Status.walking;
                    }
                }
            }

            
            

            for (int i = 0; i < TestLevel.stones.Count; i++)
            {

                if(rectangle.Intersects(TestLevel.stones[i].rectangle))
                {
                    stonetarget = TestLevel.stones[i];
                }

                else
                {
                    stonetarget = null;
                }
            }

            
            foreach (Door_button button in TestLevel.buttons)
            {
                button.FrameIndex = 1;
                button.UpdateAnimation(gametime);
                if (rectangle.Intersects(button.rectangle))
                {

                    if (kb.IsKeyDown(Keys.RightControl))
                    {
                        for (int h = 0; h < TestLevel.doors.Count; h++)
                        {
                            if (button.indexNumber == TestLevel.doors[h].indexNumber)
                            {
                                TestLevel.doors[h].OpenDoor();
                            }
                        }
                    }
                }
            }

            foreach (MoveObject obj in TestLevel.moveObjects)
            {
                if (isPushing)
                {
                    obj.UpdateAnimation(gametime);
                }
            }

            foreach (Ladder ladder in TestLevel.ladders)
            {
                if (rectangle.Intersects(ladder.rectangle))
                {
                    if (kb.IsKeyDown(Keys.Up))
                    {
                        status = Status.climbing;

                    }
                }
            }

            /*
            if (rectangle.Intersects(TestLevel.door.rectangle))
            {
                position.X = TestLevel.door.position.X + TestLevel.door.texture.Width;
            }*/

            //TestLevel.door.Update(gametime);
            //TestLevel.button.ReactToDoor(TestLevel.door, position);
        }

        public void Attack(List<Steen> stones)
        {
            foreach (Steen steen in stones)
            {
                if (Vector2.Distance(Center, steen.Center) < 70)
                {
                    if (!steen.Crushed)
                    {
                        if (FrameIndex >= 18)
                        {
                            steen.health -= Damage;
                        }

                        if (FrameIndex == 18)
                        {
                            Game1.rocksmash1.Play();
                        }
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
                        position.X = plate.rectangle.X - 22 ;
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
                obj.Update(Globals.GameTime);

                if (rectangle.Intersects(obj.rectangle))
                {
                    isPushing = true;
                    if (rectangle.TouchTopOf(obj.rectangle))
                    {
                        hasJumped = false;
                        gravity = false;
                        velocity.Y = 0;
                        position.Y = obj.rectangle.Y - rectangle.Height;
                    }

                    else if (rectangle.TouchLeftOf(obj.rectangle))
                    {
                        obj.position.X++;
                        position.X = obj.rectangle.X - rectangle.Width;
                        velocity.X = 1;

                    }

                    else if (rectangle.TouchRightOf(obj.rectangle))
                    {
                        obj.position.X--;
                        position.X = obj.rectangle.X + rectangle.Width;
                        velocity.X = -1;
                    }

                    if (rectangle.TouchBottomOf(obj.rectangle))
                    {
                    }
                }

                else
                {
                    isPushing = false;
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

        public void Relocate(Vector2 position)
        {
            if (onPosition == false)
            {
                this.position = position;
                onPosition = true;
            }
            
        }
    }
}
