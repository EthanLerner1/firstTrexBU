using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TrexMyVersion1.View;
using TrexMyVersion1.Physics;
using TrexMyVersion1.Objects;
using System.Collections.Generic;

namespace TrexMyVersion1
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static event DlgDraw Event_Draw; // regestering an event that draws everything in the game
        public static event DlgUpdate Event_Update; //regestering an event that updates everything in the game

        Camera cam;
        public Biker player1;
        Level level1;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = 700;
            graphics.PreferredBackBufferWidth = 1200;
            graphics.ApplyChanges();
        }


        protected override void Initialize()
        {

            base.Initialize();
        }


        protected override void LoadContent()
        {
            Tools.Init(GraphicsDevice, Content,this);
            Global.Init();
            PhysicsManager pm = new PhysicsManager();

            //Global.initalLevels("levels", "levels/masks", 1);
           

            Texture2D[] biker = Animation.loadTextures("playerGif", 10);

            //level1 = new Level(Tools.cm.Load<Texture2D>("levels/masks/0m"), Tools.cm.Load<Texture2D>("levels/masks/0m"),1, new Vector2(0, 0), null, Color.White, 0, new Vector2(0, 0),
            //         new Vector2(Global.playerScale), 0, 0);
            List<Obstacle> OL = Obstacle.randomObstacle(Obstacle.loadTextures("obstacle",1), new Vector2 (256,431),pm, new Vector2 (0.2f), "obstacle/obstacleMask");
            player1 = new Biker("masks/Bikermask", biker, new UserKeys(Keys.Left, Keys.Right, Keys.Up, Keys.Down, Keys.Space), 0, PhysicsManager.createAndGetNewEngine(50, 0.2f, 25, 1),
                Content.Load<Texture2D>("playerGif/0"), new Vector2(0, -100), null, Color.White, 0, new Vector2(97, 65),
                       new Vector2(Global.playerScale), 0, 0, pm, 100);
            cam = new Camera(player1, new Viewport(0, 0, Tools.W, Tools.H), Vector2.Zero, Global.zoom);
        }


        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Event_Update?.Invoke();//update happend

            Window.Title = player1.message;
           
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Tools.sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
                null, null, null, null, cam.Mat);
            Event_Draw?.Invoke();//draw hapend
            Tools.sb.End();
            base.Draw(gameTime);
        }
    }
}
