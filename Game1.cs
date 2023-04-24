using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
namespace _1_5__Big_Animation_Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont introTitleText, mediumText;
        int speed = 2;
        float timeInterval = 1;
        int hintLocation = -600;
        float seconds;
        float startTime;
        
        //c stands for Coyote and r stands for Roadrunner
        Texture2D cSprintingRight, cSprintingLeft, rStill, rRunning, rRunningRight, rSprinting, rSprintingRight, inroBackround, animationBackround, boostBarImgae, outroBackround;
        Rectangle coyoteRect, coyoteRectHit, roadrunnerRect, boostBarGreen, boostBarRed;
        Vector2 coyoteVector, roadrunnerVector;
        MouseState mouseState;
        KeyboardState keyboardState;
        SoundEffect roadrunnerSound1, roadrunnerSound2, roadrunnerSound3;
        SoundEffectInstance roadrunnerSound1Inst, roadrunnerSound2Inst, roadrunnerSound3Inst;
        Song themeSong, gamplayMusic;

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

            coyoteRect = new Rectangle(100,400, 160, 160);
            //Smaller hitbox for coyote
            coyoteRectHit = new Rectangle(130, 430, 100, 100);
            coyoteVector = new Vector2(0, 0);

            boostBarGreen = new Rectangle(160, 160, 160, 50);
            boostBarRed = new Rectangle(160, 160, 160, 50);

            roadrunnerRect = new Rectangle(700,400, 140, 140);
            roadrunnerVector = new Vector2(0, 0);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            inroBackround = Content.Load<Texture2D>("introScreenRoadrunner");
            animationBackround = Content.Load<Texture2D>("animationBackround");
            outroBackround = Content.Load<Texture2D>("roadrunnerOutroBackround");

            
            cSprintingRight = Content.Load<Texture2D>("coyote-sprinting");
            cSprintingLeft = Content.Load<Texture2D>("coyote-sprinting-left");
           
            rStill = Content.Load<Texture2D>("roadrunner-still");
            rRunning = Content.Load<Texture2D>("roadrunner");
            rRunningRight = Content.Load<Texture2D>("roadrunner-right");
            rSprinting = Content.Load<Texture2D>("roadrunner-sprinting");
            rSprintingRight = Content.Load<Texture2D>("roadrunner-sprinting-right");

            introTitleText = Content.Load<SpriteFont>("Title");
            mediumText = Content.Load<SpriteFont>("mediumText");
            boostBarImgae = Content.Load<Texture2D>("rectangle");

            roadrunnerSound1 = Content.Load<SoundEffect>("roadrunnerSound1");
            roadrunnerSound2 = Content.Load<SoundEffect>("roadrunnerSound2");
            roadrunnerSound3 = Content.Load<SoundEffect>("roadrunnerSound3");
            roadrunnerSound1Inst = roadrunnerSound1.CreateInstance();
            roadrunnerSound2Inst = roadrunnerSound2.CreateInstance();
            roadrunnerSound3Inst = roadrunnerSound3.CreateInstance();
            themeSong = Content.Load<Song>("Road Runner");
            gamplayMusic = Content.Load<Song>("roadrunnerGameplayMusic");
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
                //Play them song on loop
                if (MediaPlayer.State == MediaState.Stopped)
                    MediaPlayer.Play(themeSong);

                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    screen = Screen.Animation;
                    MediaPlayer.Stop();
                    _graphics.PreferredBackBufferWidth = 1200; // Sets the width of the window
                    _graphics.PreferredBackBufferHeight = 1000; // Sets the height of the window
                    _graphics.ApplyChanges(); // Applies the new dimensions
                }
            }
            else if (screen == Screen.Animation)
            {
                //Play music
                if (MediaPlayer.State == MediaState.Stopped)
                    MediaPlayer.Play(gamplayMusic);

                //User input
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
               
                //Boost, play boost sound and change boost bar width
                if (keyboardState.IsKeyDown(Keys.Space) && seconds >=0&& seconds <=20)
                {
                    speed = 4;
                    seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;

                    Random rand = new Random();
                    bool soundPlaying = false;
                    if (roadrunnerSound1Inst.State == SoundState.Playing || roadrunnerSound2Inst.State == SoundState.Playing || roadrunnerSound3Inst.State == SoundState.Playing)
                        soundPlaying = true;
                    int sound = rand.Next(1, 4);

                    if (soundPlaying == false && sound == 1)
                        roadrunnerSound1Inst.Play();

                    else if (soundPlaying == false && sound == 2)
                        roadrunnerSound2Inst.Play();

                    else if (soundPlaying == false && sound == 3)
                        roadrunnerSound3Inst.Play();

                    if (seconds >=timeInterval)
                    {
                        timeInterval += 1;
                        boostBarGreen.Width -= 8;

                    }
                                       
                }

               
                else
                {
                    speed = 2;                   
                    startTime = (float)gameTime.TotalGameTime.TotalSeconds - seconds;
                }

                //Makes coyote track user
                if (roadrunnerRect.Y > coyoteRect.Bottom)
                    coyoteVector.Y = 1;
                if (roadrunnerRect.Bottom < coyoteRect.Y)
                    coyoteVector.Y = -1;

                if (roadrunnerRect.X > coyoteRect.Right)
                    coyoteVector.X = 1;
                if (roadrunnerRect.Right < coyoteRect.X)
                    coyoteVector.X = -1;

                //Move coyote
                coyoteRect.Y += (int)coyoteVector.Y;
                coyoteRect.X += (int)coyoteVector.X;
                //Move coyote hitbox
                coyoteRectHit.Y += (int)coyoteVector.Y;
                coyoteRectHit.X += (int)coyoteVector.X;

                //Move roadrunner
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
                else if (roadrunnerRect.Y <= _graphics.PreferredBackBufferHeight/2- 100)
                {
                    roadrunnerRect.Y = _graphics.PreferredBackBufferHeight / 2 - 100;
                    roadrunnerVector.Y = 0;
                }
                
                //Hint Movement
                hintLocation += 1;
                if (hintLocation >= _graphics.PreferredBackBufferWidth)
                    hintLocation = -600;
                //Leave Animaion
                if (coyoteRectHit.Intersects(roadrunnerRect))
                {
                    MediaPlayer.Stop();
                    screen = Screen.Outro;
                }  

            }
            else if (screen == Screen.Outro)
            {
                
                if (MediaPlayer.State == MediaState.Stopped)
                    MediaPlayer.Play(themeSong);
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
                _spriteBatch.DrawString(mediumText, "Move with W, A, S, D keys", new Vector2(120, 420), Color.White);
                _spriteBatch.DrawString(mediumText, "Don't get caught!", new Vector2(120, 470), Color.White);
                _spriteBatch.DrawString(mediumText, "Click to Continue", new Vector2(120, 530), Color.White);
            }
            else if (screen == Screen.Animation)
            {
                
                _spriteBatch.Draw(animationBackround, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.DrawString(mediumText, "Hint: press space for a boost", new Vector2(hintLocation, 100), Color.White);
                _spriteBatch.DrawString(mediumText, "Boost:", new Vector2(20, 160), Color.White);

                //Draw boost bar
                _spriteBatch.Draw(boostBarImgae, boostBarRed, Color.Red);
                _spriteBatch.Draw(boostBarImgae, boostBarGreen, Color.Green);

                //Coyote faces right 
                if (coyoteVector.X == 1)

                    _spriteBatch.Draw(cSprintingRight, coyoteRect, Color.White);
                //Coyote faces left
                else
                    _spriteBatch.Draw(cSprintingLeft, coyoteRect, Color.White);
                //Roadrunner when still
                if (roadrunnerVector.X == 0 && roadrunnerVector.Y == 0)
                    _spriteBatch.Draw(rStill, roadrunnerRect, Color.White);
                //Roadrunner with boost
                else if (speed == 4)
                {
                    if (roadrunnerVector.X == speed)
                        _spriteBatch.Draw(rSprintingRight, roadrunnerRect, Color.White);
                    else
                        _spriteBatch.Draw(rSprinting, roadrunnerRect, Color.White);
                }
                //Roadrunner without boost
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
                
                _spriteBatch.Draw(outroBackround, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.DrawString(mediumText, "You got Caught!", new Vector2(350, 350), Color.White);
                _spriteBatch.DrawString(introTitleText, "Thank you for Playing", new Vector2(350, 450), Color.White);

            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}