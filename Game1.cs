using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace _1_5__Big_Animation_Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont introTitleText, mediumText;
        //c stands for Coyote and r stands for Roadrunner
        Texture2D cStill, cRunning, cReady, rStill, rRunning, rSprinting, inroBackround, animationBackround;
        Rectangle coyoteRect, roadrunnerRect;
        Vector2 coyoteVector, roadrunnerVector;
        MouseState mouseState;
        KeyboardState keyboardState;
        
        enum Screen
        {
            Intro,
            Animation,
            Outro
        }
        Screen screen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screen = Screen.Intro;
            _graphics.PreferredBackBufferWidth = 1000; // Sets the width of the window
            _graphics.PreferredBackBufferHeight = 700; // Sets the height of the window
            _graphics.ApplyChanges(); // Applies the new dimensions

            coyoteRect = new Rectangle(100,200, 190, 190);
            coyoteVector = new Vector2(0, 0);

            roadrunnerRect = new Rectangle(300,200, 130, 130);
            roadrunnerVector = new Vector2(0, 0);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            inroBackround = Content.Load<Texture2D>("introScreenRoadrunner");
            animationBackround = Content.Load<Texture2D>("animationBackround");

            cStill = Content.Load<Texture2D>("coyote-still");
            cRunning = Content.Load<Texture2D>("coyote-running");
            cReady = Content.Load<Texture2D>("coyote-ready");
            rStill = Content.Load<Texture2D>("roadrunner-still");
            rRunning = Content.Load<Texture2D>("roadrunner-running");
            rSprinting = Content.Load<Texture2D>("roadrunner-sprinting");

            introTitleText = Content.Load<SpriteFont>("Title");
            mediumText = Content.Load<SpriteFont>("mediumText");
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (screen == Screen.Intro)
            {

                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    screen = Screen.Animation;
                    
                    _graphics.PreferredBackBufferWidth = 1200; // Sets the width of the window
                    _graphics.PreferredBackBufferHeight = 1000; // Sets the height of the window
                    _graphics.ApplyChanges(); // Applies the new dimensions
                }


            }
            else if (screen == Screen.Animation)
            {

                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    
                    roadrunnerVector.Y = -2;
                    
                }
                else if (keyboardState.IsKeyDown(Keys.Down))
                {

                    
                    roadrunnerVector.Y = 2;
                }
                else if (keyboardState.IsKeyDown(Keys.Left))
                {

                    
                    roadrunnerVector.X = -2;
                }
                else if (keyboardState.IsKeyDown(Keys.Right))
                {

                    
                    roadrunnerVector.X = 2;
                }
                if (!keyboardState.IsKeyDown(Keys.Up) && !keyboardState.IsKeyDown(Keys.Right) && !keyboardState.IsKeyDown(Keys.Left) && !keyboardState.IsKeyDown(Keys.Down))
                {
                    

                    roadrunnerVector.Y = 0;
                    roadrunnerVector.X = 0;
                }
                if (roadrunnerRect.Y > coyoteRect.Bottom)
                    coyoteVector.Y = 1;
                if (roadrunnerRect.Bottom < coyoteRect.Y)
                    coyoteVector.Y = -1;

                if (roadrunnerRect.X > coyoteRect.Right)
                    coyoteVector.X = 1;
                if (roadrunnerRect.Right < coyoteRect.X)
                    coyoteVector.X = -1;


                coyoteRect.Y += (int)coyoteVector.Y;
                coyoteRect.X += (int)coyoteVector.X;

                

                roadrunnerRect.Y += (int)roadrunnerVector.Y;
                roadrunnerRect.X += (int)roadrunnerVector.X;

            }
            else if (screen == Screen.Outro)
            {
                



            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(inroBackround, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.DrawString(introTitleText, "Roadrunner Chase!", new Vector2(120, 350), Color.White);
                _spriteBatch.DrawString(mediumText, "Click to Continue", new Vector2(120, 420), Color.White);

            }
            else if (screen == Screen.Animation)
            {
                _spriteBatch.Draw(animationBackround, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.Draw(cReady, coyoteRect, Color.White);
                if (roadrunnerVector.X == 0 && roadrunnerVector.Y ==0)
                    _spriteBatch.Draw(rStill, roadrunnerRect, Color.White);
                else 
                    _spriteBatch.Draw(rRunning, roadrunnerRect, Color.White);
            }
            else if (screen == Screen.Outro)
            {

                
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}