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
        int speed = 2;
        //c stands for Coyote and r stands for Roadrunner
        Texture2D cStill, cSprintingRight, cSprintingLeft, cReady, rStill, rRunning, rRunningRight, rSprinting, rSprintingRight, inroBackround, animationBackround;
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
            cSprintingRight = Content.Load<Texture2D>("coyote-sprinting");
            cSprintingLeft = Content.Load<Texture2D>("coyote-sprinting-left");
            cReady = Content.Load<Texture2D>("coyote-ready");
            rStill = Content.Load<Texture2D>("roadrunner-still");
            rRunning = Content.Load<Texture2D>("roadrunner");
            rRunningRight = Content.Load<Texture2D>("roadrunner-right");
            rSprinting = Content.Load<Texture2D>("roadrunner-sprinting");
            rSprintingRight = Content.Load<Texture2D>("roadrunner-sprinting-right");

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

                if (keyboardState.IsKeyDown(Keys.W))
                {                    
                    roadrunnerVector.Y = -speed;
                    
                }
                else if (keyboardState.IsKeyDown(Keys.S))
                {

                    roadrunnerVector.Y = speed;
                }
                else if (keyboardState.IsKeyDown(Keys.A))
                {

                    
                    roadrunnerVector.X = -speed;
                }
                else if (keyboardState.IsKeyDown(Keys.D))
                {
      
                    roadrunnerVector.X = speed;
                }
                else
                {
                    roadrunnerVector.Y = 0;
                    roadrunnerVector.X = 0;
                }
                if (keyboardState.IsKeyDown(Keys.Space))
                    speed = 3;
                else
                    speed = 2;
                {

                    
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

                //Keep in playable Area
                if (roadrunnerRect.Right >= _graphics.PreferredBackBufferWidth)
                {
                    roadrunnerRect.X = _graphics.PreferredBackBufferWidth- roadrunnerRect.Width;
                    roadrunnerVector.X = 0;
                }
                else if (roadrunnerRect.X <= 0)
                {
                    roadrunnerRect.X = 0;
                    roadrunnerVector.X = 0;
                }
                if (roadrunnerRect.Bottom >= _graphics.PreferredBackBufferHeight)
                {
                    roadrunnerRect.Y = _graphics.PreferredBackBufferHeight - roadrunnerRect.Height;
                    roadrunnerVector.Y = 0;
                }
                else if (roadrunnerRect.Y <= _graphics.PreferredBackBufferHeight/2)
                {
                    roadrunnerRect.Y = _graphics.PreferredBackBufferHeight / 2;
                    roadrunnerVector.Y = 0;
                }

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

                if (coyoteVector.X == 1)
                    _spriteBatch.Draw(cSprintingRight, coyoteRect, Color.White);
                else
                    _spriteBatch.Draw(cSprintingLeft, coyoteRect, Color.White);

                if (roadrunnerVector.X == 0 && roadrunnerVector.Y ==0)
                    _spriteBatch.Draw(rStill, roadrunnerRect, Color.White);
                else if (speed == 3)
                {
                    if (roadrunnerVector.X == speed)
                        _spriteBatch.Draw(rSprintingRight, roadrunnerRect, Color.White);
                    else
                        _spriteBatch.Draw(rSprinting, roadrunnerRect, Color.White);
                }
                else
                {
                    if (roadrunnerVector.X == speed)
                        _spriteBatch.Draw(rRunningRight, roadrunnerRect, Color.White);
                    else 
                        _spriteBatch.Draw(rRunning, roadrunnerRect, Color.White);
                }
                   
            }
            else if (screen == Screen.Outro)
            {

                
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}