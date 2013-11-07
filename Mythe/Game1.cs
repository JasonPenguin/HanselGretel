using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Mythe.Level;
using Mythe.Players;
using Mythe.Resources;


namespace Mythe
{
    public enum GameStatus
    {
        Gameplay,
        GameOver,
        StartScreen,
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public GameStatus status = GameStatus.StartScreen;
        public bool UpdateCam = true;
        Video video;
        VideoPlayer player;

        private SpriteFont font;
        private Vector2 textposition;
        private float TimeLapse;

        StartScreen startscreen;
        /*sounds*/
        //hans voice
        public static SoundEffect[] sfx;
        public static SoundEffect pullLever;
        public static SoundEffect i_hope;
        public static SoundEffect wow_look;

        public static Song i_shouldpress;
        public static Song lets_pull;
        public static Song upupup;

        //assets sounds
        public static SoundEffect lever;
        public static SoundEffect levercreak;
        public static Song rockdrag;
        public static SoundEffect rocksmash1;

        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        Texture2D grietjeTex;
        Texture2D hansTex;
        Texture2D bgtexture;
        Texture2D game_over;

        public static Texture2D door_tex;
        public static Texture2D button_tex;

        public static Texture2D[] laddertextures;
        public static Texture2D ladder1Tex;

        public static Texture2D ladder2Tex;
        public static Texture2D ladder3Tex;
        public static Texture2D ladder4Tex;
        public static Texture2D ladder5Tex;
        public static Texture2D ladder6Tex;
        public static Texture2D ladder7Tex;
        public static Texture2D ladder8Tex;
        public static Texture2D ladder9Tex;
        public static Texture2D ladder10Tex;
        public static Texture2D ladder11Tex;
        

        public static Texture2D pftexture;
        public static Texture2D slaktex;
        public static Texture2D crushstone;
        public static Texture2D stonetex;
        public static Texture2D fallStone;
        public static Texture2D pressureplate;
        public static Texture2D liftobj;

        public static Texture2D[] Gretatextures;
        public static int GretatexIndex = 0;
        public static Texture2D gretaIdle;
        public static Texture2D gretaWalk;
        public static Texture2D gretaSmash;
        public static Texture2D gretaJump;
        public static int GretaFrames = 12;

        public static Texture2D[] Hanstextures;
        public static int HanstexIndex = 0;
        public static Texture2D hansIdle;
        public static Texture2D hansWalk;
        public static Texture2D hansJump;

        Song bgm;
        Song startbgm;
        public static Song credits;

        public static int HansFrames = 11;

        Texture2D laag1;
        Texture2D laag2;
        Texture2D laag3;
        Hans hans;
        Grietje grietje;
        TestLevel level;
        LevelManager manager;
        blok blok;
        Viewport defaultView;
        Viewport leftView;
        Viewport rightView;

       
        Camera cameraOne;
        Camera cameraTwo;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textur
            font = Content.Load<SpriteFont>("Arial");

            spriteBatch = new SpriteBatch(GraphicsDevice);
            grietjeTex = Content.Load<Texture2D>("Pixel");
            hansTex = Content.Load<Texture2D>("Ht");
            pftexture = Content.Load<Texture2D>("Pfblok");
            stonetex = Content.Load<Texture2D>("GFX\\Level\\Assets\\schuif_steen");
            crushstone = Content.Load<Texture2D>("GFX\\Sheets\\breek_steen_1_test");
            fallStone = Content.Load<Texture2D>("blok_p1");

            startscreen = new StartScreen();
            startscreen.LoadContent(Content);

            game_over = Content.Load<Texture2D>("GFX\\achtergrond");
            laddertextures = new Texture2D[]
            {
                ladder11Tex = Content.Load<Texture2D>("GFX\\Level\\Assets\\Ladders\\ladder11"),
                ladder10Tex = Content.Load<Texture2D>("GFX\\Level\\Assets\\Ladders\\ladder10"),
                ladder9Tex = Content.Load<Texture2D>("GFX\\Level\\Assets\\Ladders\\ladder9"),
                ladder8Tex = Content.Load<Texture2D>("GFX\\Level\\Assets\\Ladders\\ladder8"),
                ladder7Tex = Content.Load<Texture2D>("GFX\\Level\\Assets\\Ladders\\ladder7"),
                ladder6Tex = Content.Load<Texture2D>("GFX\\Level\\Assets\\Ladders\\ladder6"),
                ladder5Tex = Content.Load<Texture2D>("GFX\\Level\\Assets\\Ladders\\ladder5"),
                ladder4Tex = Content.Load<Texture2D>("GFX\\Level\\Assets\\Ladders\\ladder4"),
                ladder3Tex = Content.Load<Texture2D>("GFX\\Level\\Assets\\Ladders\\ladder2"),
                ladder2Tex = Content.Load<Texture2D>("GFX\\Level\\Assets\\Ladders\\ladder3"),
                ladder1Tex = Content.Load<Texture2D>("GFX\\Level\\Assets\\Ladders\\ladder1"),
            };

            pullLever = Content.Load<SoundEffect>("GFX\\Hans\\HansSounds\\i_am_gonna_pull_the_lever");
            
            bgm = Content.Load<Song>("Sound\\MytheV6");
            credits = Content.Load<Song>("Sound\\CreditsV1Loop");
            startbgm = Content.Load<Song>("Sound\\StartscreenV1loop2");
            
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(startbgm);
            

            //assets sounds
            lever = Content.Load<SoundEffect>("GFX\\Level\\Assets\\AssetsSound\\Lever01");
            levercreak = Content.Load<SoundEffect>("GFX\\Level\\Assets\\AssetsSound\\Lever02_Creak");
            rockdrag = Content.Load<Song>("GFX\\Level\\Assets\\AssetsSound\\Rockdrag_01_01");
            rocksmash1 = Content.Load<SoundEffect>("GFX\\Level\\Assets\\AssetsSound\\Rocksmash2");

            //hans sounds
            upupup = Content.Load<Song>("GFX\\Hans\\HansSounds\\up-up-up-the-ladder-we-go");
            i_shouldpress = Content.Load<Song>("GFX\\Hans\\HansSounds\\i-should-press-it");
            lets_pull = Content.Load<Song>("GFX\\Hans\\HansSounds\\lets-pull-this");

            
            
            sfx = new SoundEffect[]
            {
                i_hope = Content.Load<SoundEffect>("GFX\\Hans\\HansSounds\\i_hope_we_find_our_home_soon"),
                wow_look = Content.Load<SoundEffect>("GFX\\Hans\\HansSounds\\wow-look-at-that-bird"),
                //assets sounds
            };

            Gretatextures = new Texture2D[]
            {
                gretaWalk = Content.Load<Texture2D>("GFX\\Grietje\\Greta_Walk2.2"),
                gretaIdle = Content.Load<Texture2D>("GFX\\Grietje\\Greta_Idle2.2"),
                gretaSmash = Content.Load<Texture2D>("GFX\\Grietje\\Greta_Smash2.2"),
                gretaJump = Content.Load<Texture2D>("GFX\\Grietje\\Greta_Jump2.2"),
            };

            Hanstextures = new Texture2D[]
            {
                hansIdle = Content.Load<Texture2D>("GFX\\Hans\\hansje_idle"),
                hansWalk = Content.Load<Texture2D>("GFX\\Hans\\hansje_walk"),
                hansJump = Content.Load<Texture2D>("GFX\\Hans\\hansje_jump"),
            };

            
            bgtexture = Content.Load<Texture2D>("GFX\\Level\\BG");
            button_tex = Content.Load<Texture2D>("GFX\\Level\\Assets\\button2");
            slaktex = Content.Load<Texture2D>("GFX\\Sheets\\slak_animatie");
            door_tex = Content.Load<Texture2D>("GFX\\Level\\Assets\\deur_rood");
            pressureplate = Content.Load<Texture2D>("GFX\\Level\\Assets\\knop");
            liftobj = Content.Load<Texture2D>("GFX\\Level\\Assets\\blok_g_1");

            hans = new Hans(Hanstextures[1], HansFrames, 1);
            hans.LoadContent(Content);

            grietje = new Grietje(Gretatextures[1],GretaFrames,1);
            grietje.LoadContent(Content);
            cameraOne = new Camera();
            defaultView = GraphicsDevice.Viewport;
            defaultView.Width = 1280;
            defaultView.Height = 720;
            

            manager = new LevelManager(Content);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();


            //leftView = defaultView;
            //rightView = defaultView;

            //leftView.Width = leftView.Width / 2;
            //rightView.Width = rightView.Width / 2;
            //rightView.X = leftView.Width;

            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            
        }

        public void Restart()
        {
            LoadContent();
            cameraOne.Update(new Vector2(2500, 700));
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (grietje.rectangle.Intersects(TestLevel.finish))
            {
                status = GameStatus.GameOver;
                MediaPlayer.Play(credits);
            }
            // Allows the game to exit
            KeyboardState kb = Keyboard.GetState();
            startscreen.Update(gameTime);
            if (status == GameStatus.StartScreen)
            {
                if (kb.IsKeyDown(Keys.Enter))
                {
                    status = GameStatus.Gameplay;

                    MediaPlayer.Play(bgm);
                }
            }
            
            if (status == GameStatus.GameOver)
            {
                if (kb.IsKeyDown(Keys.R))
                {
                    Restart();
                    TimeLapse = 0;
                    UpdateCam = true;
                    status = GameStatus.StartScreen;
                }
            }

            if (status == GameStatus.Gameplay)
            {
                TimeLapse += (float)gameTime.ElapsedGameTime.TotalSeconds;
                hans.Update(gameTime);
                hans.UpdateAnimation(gameTime);

                grietje.Update(gameTime);
                grietje.UpdateAnimation(gameTime);


                if (UpdateCam)
                {
                    if (grietje.position.X < 700)
                    {
                        UpdateCam = false;
                    }

                    if (grietje.position.X < 3200 && grietje.position.X > 700)
                    {
                        cameraOne.Update(new Vector2(grietje.Center.X - 640, 700));
                    }

                    else
                    {
                        cameraOne.Update(new Vector2(2500, 700));
                    }
                }

                if (!UpdateCam) 
                {
                    cameraOne.Update(new Vector2(100, 700));
                    if (grietje.position.X > 700)
                    {
                        cameraOne.Update(new Vector2(grietje.Center.X - 640, 700));
                    }
                }

                if (kb.IsKeyDown(Keys.Escape))
                {
                    status = GameStatus.GameOver;
                }

                manager.Update(gameTime);
            }
            
            /*
            if (Vector2.Distance(hans.position, grietje.position) > 640)
            {
                grietje.onPosition = false;
                grietje.Relocate(hans.position);
            }*/

            

            //Console.WriteLine(level.SpawnNextlvl);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (status == GameStatus.StartScreen)
            {
                GraphicsDevice.Clear(Color.SandyBrown);
                startscreen.Draw(spriteBatch);
            }

            if (status == GameStatus.GameOver)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(game_over, new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(font, "Time : " + TimeLapse, new Vector2(480, 230), Color.White);
                spriteBatch.DrawString(font, "Seconds", new Vector2(800,230), Color.White);
                spriteBatch.DrawString(font, "Greta : " + grietje.points, new Vector2(400,500), Color.White);
                spriteBatch.DrawString(font, "Hansel : " + grietje.points, new Vector2(800, 500), Color.White);
                spriteBatch.DrawString(font, "Press 'R' to Restart", new Vector2(1000,630), Color.White);
                spriteBatch.End();
            }

            GraphicsDevice.Viewport = defaultView;
            DrawSprites(cameraOne);

            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }

        void DrawSprites(Camera camera)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                null, null, null, null,
                camera.transform);
            //spriteBatch.Draw(Content.Load<Texture2D>("testbg"), new Vector2(0, -20), Color.White);
            

            if (status == GameStatus.Gameplay)
            {
                manager.Draw(spriteBatch);
                hans.Draw(spriteBatch);
                grietje.Draw(spriteBatch);  
            }                   
            spriteBatch.End();
        }
    }
}

static class RectangleHelper
{
    const int penetrationMargin = 5;

    public static bool isOnTopOf(this Rectangle r1,Rectangle r2)
    {
        return (r1.Bottom >= r2.Top - penetrationMargin &&
            r1.Bottom <= r2.Top + 1 &&
            r1.Right >= r2.Left + 5 &&
            r1.Left <= r2.Right - 5);
    }
}
