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

using Mythe.Players;
using Mythe.Level.Assets1;
using Mythe.Resources;
using Mythe.Animatie;

namespace Mythe.Level
{

    public class TestLevel
    {
        public static List<blok> Tiles;
        public static List<blok> CheckRtiles;
        public static List<blok> CheckLtiles;
        public static List<blok> CheckBtiles;
        public Song Bgm;
        public static List<Steen> stones;
        public static List<Ladder> ladders;
        public static List<Door_button> buttons;
        public static List<CollectItem> collectibles;
        public static List<Door> doors;
        public static List<MoveObject> moveObjects;
        public static List<MoveDoor> Mdoors;
        public static List<MoveDoorButton> Mbuttons;
        public static List<PressurePlate> plates;
        public static LiftObject liftobject;

        public static Rectangle finish;
        private Texture2D laag1;
        private Texture2D laag2;
        private Texture2D laag3;
        private Texture2D laag4;

        private Texture2D wolken;
        private Texture2D mist;
        private float mist_speed = 0.40f;
        private float wolken_speed = 0.10f;
        private int buttonIndex;
        private int ladderIndex;
        private int doorIndex;

        private int moveButtonIndex = 0;
        private int moveDoorIndex = 0;

        private Texture2D pftexture;
        private Texture2D stoneTexture;

        public Texture2D tiletexture;
        public int tilesize = 60;

        public  Door door;
        public  Door_button button;

        public Vector2 center;
        public Vector2 position;

        public bool SpawnNextlvl;

        public string mapname;
        int[,] map;


        public static Rectangle bigRec;

        public int Width
        {
            get { return map.GetLength(1); }
        }

        public int Heigth
        {
            get { return map.GetLength(0); }
        }

        public TestLevel(Texture2D texture,string mapname,Vector2 position)
        {
            this.mapname = mapname;
            this.position = position;

            switch (mapname)
            {
                case "map":
                    map = new int[,]
                    {
                                    /*items*/                          
                        // 1 = blok      // 3 = ladder  // 5 = item // 7 = schuif obj // 9 = falldoor button // 11
                        // 2 = steen     // 4 = button  // 6 = door // 8 = fall door // 10 = pressure plate
                        
                        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,0,1,0,1,0,1,0,3,0,0,0,0,0,0,0,1,0,0,0,0,7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                        {1,1,1,1,0,0,0,0,0,10,0,10,0,0,0,0,0,0,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                        {1,1,1,1,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,1,3,0,0,0,6,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,1,1,1,1,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                        {1,1,1,1,11,0,0,0,1,1,1,1,3,1,1,1,1,1,1,1,0,0,4,0,0,0,1,0,1,1,1,3,0,0,0,0,0,0,0,0,0,7,0,0,0,0,0,3,0,1,1,1,1,0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                        {1,1,1,1,0,0,0,0,1,1,1,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,6,0,0,0,1,0,1,1,1,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                        {1,1,1,1,1,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,4,1,0,1,1,1,0,0,1,1,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                        {1,1,1,1,1,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1,1,1,0,1,1,1,1,0,1,1,0,1,1,1,1,5,0,0,1,1,1,0,0,0,0,0,0,0,0,6,0,0,0,0,1,1,1,1,1,1,1,1,1,0,1,1,0,0,0,0,0,0,0,2,0,0,0,0,0},
                        {1,1,1,1,1,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1,1,1,0,0,5,0,9,0,0,0,0,1,1,1,1,1,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0},
                        {1,1,1,1,1,1,1,0,0,0,0,0,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,1,1,1,1,1,1,1,1,1,0,0,1,1,1,3,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                        {1,1,1,1,1,1,1,3,0,0,0,0,0,1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,5,0,4,0,0,0,0,0,0,1,1,1,1,0,0,0,0,5,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                        {1,1,1,1,1,1,1,0,1,1,0,5,0,1,1,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,5,0,1,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,3,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                        {1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,0,1,0,1,0,1,0,1,0,1,1,1,1,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                        {1,1,1,1,1,1,1,0,5,0,0,0,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,1,1,1,1,1,1,1,5,0,0,0,1,1,1,1,1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                        {1,1,1,1,1,1,1,1,1,1,1,0,0,0,4,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                    };
                    break;
            }

            buttons = new List<Door_button>();
            ladders = new List<Ladder>();
            stones = new List<Steen>();
            Tiles = new List<blok>();
            collectibles = new List<CollectItem>();
            doors = new List<Door>();
            moveObjects = new List<MoveObject>();
            Mdoors = new List<MoveDoor>();
            Mbuttons = new List<MoveDoorButton>();
            plates = new List<PressurePlate>();

            CheckRtiles = new List<blok>();
            CheckLtiles = new List<blok>();
            CheckBtiles = new List<blok>();

            
            this.tiletexture = texture;

            buttonIndex = 0;
            ladderIndex = 0;
            doorIndex = 0;


            center = new Vector2(this.Width / 2 * 44, this.Heigth / 2*44);
            SpawnNextlvl = false;


            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Heigth; y++)
                {
                    //Assets in het Grid genereren
                    int textureIndex = map[y, x];
                    if (textureIndex == 0)
                        continue;
                    switch (textureIndex)
                    {
                        case 1:
                            blok tile = new blok(tiletexture, new Vector2(position.X + x * 44,position.Y + y * 44));
                            Tiles.Add(tile);
                            break;

                        case 2:
                            Steen steen = new Steen(Game1.crushstone,13, 1);
                            AnimationClass animate = new AnimationClass();
                            steen.position = new Vector2(position.X + x * 44, position.Y + y * 44);
                            steen.addAnimation(Game1.crushstone,"idle",1,13, animate.Copy());
                            steen.Animation = "idle";

                            stones.Add(steen);
                            break;

                        case 3:
                            Ladder ladder = new Ladder(Game1.laddertextures[ladderIndex], new Vector2(position.X + x * 44, position.Y + y * 44));
                            ladderIndex++;
                            ladders.Add(ladder);
                            break;

                        case 4:
                           Door_button button = new Door_button(Game1.button_tex, 12, 1, buttonIndex);
                            AnimationClass animate2 = new AnimationClass();
                            button.position = new Vector2(position.X + x * 44, position.Y + y * 44);
                            button.addAnimation(Game1.button_tex, "idle", 1, 12, animate2.Copy());
                            button.Animation = "idle";
                            buttonIndex++;
                            buttons.Add(button);
                            break;

                        case 5:
                            CollectItem item = new CollectItem(Game1.slaktex, 50, 1);
                            AnimationClass animate3 = new AnimationClass();
                            item.position = new Vector2(position.X + x * 44, position.Y + y * 44);
                            item.addAnimation(Game1.slaktex, "idle", 1, 50, animate3.Copy());
                            item.Animation = "idle";
                            collectibles.Add(item);
                            break;

                        case 6:
                            Door door = new Door(Game1.door_tex, new Vector2(position.X + x * 44, position.Y + y * 44), doorIndex);
                            doorIndex++;
                            doors.Add(door);
                            break;

                        case 7:
                            MoveObject obj = new MoveObject(Game1.stonetex,15, 1);
                            AnimationClass animate5 = new AnimationClass();
                            obj.position = new Vector2(position.X + x * 44, position.Y + y * 44);
                            obj.addAnimation(Game1.stonetex,"idle2",1, 15, animate5.Copy());
                            obj.Animation = "idle2";
                            moveObjects.Add(obj);
                            break;

                        case 8:
                            MoveDoor objdoor = new MoveDoor(Game1.fallStone, new Vector2(position.X + x * 44, position.Y + y * 44),moveDoorIndex);
                            moveDoorIndex++;
                            Mdoors.Add(objdoor);
                            break;

                        case 9:
                            MoveDoorButton button2 = new MoveDoorButton(Game1.button_tex,12, 1,moveButtonIndex);
                            AnimationClass animate6 = new AnimationClass();
                            button2.position = new Vector2(position.X + x * 44, position.Y + y * 44);
                            button2.addAnimation(Game1.button_tex, "idle", 1, 12, animate6.Copy());
                            button2.Animation = "idle";
                            moveButtonIndex++;
                            Mbuttons.Add(button2);
                            break;

                        case 10:
                            PressurePlate plate = new PressurePlate(Game1.pressureplate, new Vector2(position.X + x * 44, position.Y + y * 55));
                            plates.Add(plate);
                            break;

                        case 11:
                            liftobject = new LiftObject(Game1.liftobj, new Vector2(position.X + x * 44, position.Y + y * 44));
                            break;
                    }


                    
                }
            }
        }

        public void UpdateAnimations(GameTime gametime)
        {
            foreach (MoveObject obj in moveObjects)
            {
                obj.Update(gametime);
            }

            
        }

        public void LoadContent(ContentManager Content)
        {
            switch (mapname)
            {
                case "map":
                    pftexture = Content.Load<Texture2D>("Pfblok");
                    laag1 = Content.Load<Texture2D>("GFX\\Level\\Assets\\laag1");
                    laag2 = Content.Load<Texture2D>("GFX\\Level\\Assets\\Donker_lagen\\laag_2_donker");
                    laag3 = Content.Load<Texture2D>("GFX\\Level\\Assets\\Donker_lagen\\laag_3_donker");
                    laag4 = Content.Load<Texture2D>("GFX\\Level\\Assets\\Donker_lagen\\laag_4_donker");

                    wolken = Content.Load<Texture2D>("GFX\\Level\\Assets\\wolken");
                    mist = Content.Load<Texture2D>("GFX\\Level\\Assets\\mist");
                    stoneTexture = Content.Load<Texture2D>("GFX\\Level\\Assets\\steen");
                    
                   
                    break;
                case "map2":
                    pftexture = Content.Load<Texture2D>("Pfblok");
                    laag1 = Content.Load<Texture2D>("GFX\\Level\\1");
                    break;
            }

        }

        public void Update(GameTime gametime)
        {
            //Assets Updaten
            finish = new Rectangle(14,700,100, 180);
            bigRec = new Rectangle(0,0, 1750, 300);
            liftobject.Update(gametime);
            
            mist_speed -= 0.25f;
            wolken_speed -= 0.20f;
            for (int i = 0; i < plates.Count; i++)
            {
                if (plates[0].pressed == true && plates[1].pressed == true)
                {
                    liftobject.position.Y = 800;
                }
            }

            for (int i = 0; i < stones.Count; i++)
            {

                Steen steen = stones[i];
                steen.Update(gametime);
                //steen.UpdateAnimation(gametime);
                if (steen.Crushed)
                {
                    
                    stones.Remove(steen);
                    i--;  
                }
            }

            for (int k = 0; k < doors.Count; k++)
            {
                Door door = doors[k];
                if (door.open)
                {
                    doors.Remove(door);
                    k--;
                }
            }

            for (int j = 0; j < collectibles.Count; j++)
            {
                CollectItem item = collectibles[j];
                item.UpdateAnimation(gametime);
                if (item.pickedUp)
                {
                    collectibles.Remove(item);
                    j--;
                }
            }

            foreach (Ladder ladder in ladders)
            {
                ladder.Update(gametime);
            }

            foreach (MoveDoor door in Mdoors)
            {
                door.Update(gametime);
            }
        }

        public void SpawnNextlevel(Hans hans)
        {
            if (hans.position.X < center.X)
            {
                SpawnNextlvl = true;
            }
        }
            
        public void Draw(SpriteBatch batch)
        {
            switch (mapname)
            {
                case"map":
                    batch.Draw(laag4, new Vector2(Camera.center.X * 0.16f, 700), Color.White);
                    batch.Draw(laag3, new Vector2(position.X, position.Y -20 ), Color.White);

                    batch.Draw(laag2, new Vector2(position.X, position.Y - 20), Color.White);
                    batch.Draw(laag1, new Vector2(position.X, position.Y - 20), Color.White);

                    //batch.Draw(mist, new Vector2(Camera.center.X += mist_speed, position.Y - 20), Color.White);
                    //batch.Draw(wolken, new Vector2(Camera.center.X* 0.30f, position.Y - 20), Color.White);
                    break;

                case"map2":
                    
                    batch.Draw(laag1,new Vector2(position.X - 2,position.Y + 24), Color.White);
                    break;
            }

            foreach (MoveObject obj in moveObjects)
            {
                obj.Draw(Game1.spriteBatch);
            }

            foreach (blok blok in Tiles)
            {
                blok.Draw(Game1.spriteBatch);
            }

            foreach (blok coll in CheckRtiles)
            {
                coll.Draw(Game1.spriteBatch);
            }

            foreach (blok coll in CheckLtiles)
            {
                coll.Draw(Game1.spriteBatch);
            }

            foreach (Steen steen in stones)
            {
                steen.Draw(batch);
            }

            foreach (Ladder ladder in ladders)
            {
                ladder.Draw(batch);
            }

            foreach (Door_button button in buttons)
            {
                button.Draw(batch);
            }

            foreach (CollectItem item in collectibles)
            {
                item.Draw(batch);
            }

            foreach (Door door in doors)
            {
                door.Draw(batch);
            }

            foreach (MoveDoorButton mbutton in Mbuttons)
            {
                mbutton.Draw(batch);
            }

            foreach (MoveDoor mdoor in Mdoors)
            {
                mdoor.Draw(batch);
            }

            foreach (PressurePlate plate in plates)
            {
                plate.Draw(batch);
            }
            liftobject.Draw(batch);
        }
    }
}
