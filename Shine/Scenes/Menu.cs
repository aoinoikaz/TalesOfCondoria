using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using Tales_of_Condoria.Shine;
using Tales_of_Condoria.Shine.UI;

namespace Tales_of_Condoria.Scenes
{
    public class Menu : Scene
    {
        public readonly Image Background;
        public readonly Image SettingsPanel;
        public readonly Button PlayButton;
        public readonly Button HelpButton;
        public readonly Button AboutButton;
        public readonly Button BackButton;
        public readonly Font Logo;
        public readonly Font AboutInfo;
        public readonly Font HelpInfo;

        public Song BackgroundMusic;

        public Menu(Game game, string name) : base(game, name)
        {
            GameLoop gameLoop = (GameLoop)game;

            // Play Button
            Texture2D playBtnImg = gameLoop.Content.Load<Texture2D>("UI/large_Red");

            int xSizeOffset = 175;
            int ySizeOffset = 100;
            int playBtnWidth = playBtnImg.Width - xSizeOffset;
            int playBtnHeight = playBtnImg.Height - ySizeOffset;

            Rectangle playBtnDestination = new Rectangle(
                (int)GameLoop.Stage.X / 2 - playBtnWidth / 2, 
                (int)GameLoop.Stage.Y / 2 - playBtnHeight / 2 - 32,
                playBtnWidth,
                playBtnHeight);

            PlayButton = new Button(gameLoop, gameLoop.SpriteBatch, playBtnImg, "Play", "Fonts/play", playBtnDestination);
            PlayButton.OnClick += OnPlayBtnClicked;

            // Help Button
            Texture2D helpBtnImg = gameLoop.Content.Load<Texture2D>("UI/medium_Green");

            int helpBtnWidth = helpBtnImg.Width - xSizeOffset;
            int helpBtnHeight = helpBtnImg.Height - ySizeOffset;

            Rectangle helpBtnDestination = new Rectangle(
             (int)GameLoop.Stage.X / 2 - helpBtnWidth / 2,
             (int)GameLoop.Stage.Y / 2 - helpBtnHeight + 90,
             helpBtnWidth,
             helpBtnHeight);

            HelpButton = new Button(gameLoop, gameLoop.SpriteBatch, helpBtnImg, "Help", "Fonts/helpabout", helpBtnDestination);
            HelpButton.OnClick += OnHelpBtnClicked;

            // About button
            Texture2D aboutBtnImg = gameLoop.Content.Load<Texture2D>("UI/medium_Green");
            int aboutBtnWidth = aboutBtnImg.Width - xSizeOffset;
            int aboutBtnHeight = aboutBtnImg.Height - ySizeOffset;

            Rectangle aboutBtnDestination = new Rectangle(
             (int)GameLoop.Stage.X / 2 - aboutBtnWidth / 2,
             (int)GameLoop.Stage.Y / 2 - aboutBtnHeight + 170,
             aboutBtnWidth,
             aboutBtnHeight);

            AboutButton = new Button(gameLoop, gameLoop.SpriteBatch, aboutBtnImg, "About", "Fonts/helpabout", aboutBtnDestination);
            AboutButton.OnClick += OnAboutBtnClicked;

            // About Information
            SpriteFont aboutInfoSpriteFont = gameLoop.Content.Load<SpriteFont>("Fonts/paragraph");
            string aboutInfoText = "~~~~~~~~~~ About ~~~~~~~~~~\n\n          PROG2370 - Final Project\n           Devon Tomlin - 7321599\n              Sunday Dec 12, 2021";
            AboutInfo = new Font(gameLoop, gameLoop.SpriteBatch, aboutInfoSpriteFont, aboutInfoText, new Vector2(
                GameLoop.Stage.X / 2 - aboutInfoSpriteFont.MeasureString(aboutInfoText).X / 2, 
                GameLoop.Stage.Y / 2 - aboutInfoSpriteFont.MeasureString(aboutInfoText).Y / 2 - 30), Color.White);

            // Help Information
            SpriteFont helpInfoSpriteFont = gameLoop.Content.Load<SpriteFont>("Fonts/paragraph");
            string helpInfoText = "~~~~~~~~~~ Help ~~~~~~~~~~\n\n            - Use WASD to move!\n    - Left click while moving to shoot!\n                  - Have fun!";
            HelpInfo = new Font(gameLoop, gameLoop.SpriteBatch, helpInfoSpriteFont, helpInfoText, new Vector2(
                GameLoop.Stage.X / 2 - helpInfoSpriteFont.MeasureString(helpInfoText).X / 2, 
                GameLoop.Stage.Y / 2 - helpInfoSpriteFont.MeasureString(helpInfoText).Y / 2 - 35), Color.White);

            // Back button
            Texture2D backBtnImg = gameLoop.Content.Load<Texture2D>("UI/medium_Green");
            int backBtnWidth = backBtnImg.Width - xSizeOffset;
            int backBtnHeight = backBtnImg.Height - ySizeOffset;

            Rectangle backBtnDestination = new Rectangle(
             (int)GameLoop.Stage.X / 2 - backBtnWidth / 2,
             (int)GameLoop.Stage.Y / 2 - backBtnHeight + 170,
             backBtnWidth,
             backBtnHeight);
                
            BackButton = new Button(gameLoop, gameLoop.SpriteBatch, aboutBtnImg, "Back", "Fonts/helpabout", aboutBtnDestination);
            BackButton.OnClick += OnBackButtonClicked;

            // Background
            Texture2D backgroundImage = gameLoop.Content.Load<Texture2D>("Backgrounds/menu");
            Background = new Image(gameLoop, gameLoop.SpriteBatch, backgroundImage, 
                new Rectangle(0,0,
                (int)GameLoop.Stage.X, 
                (int)GameLoop.Stage.Y));

            // Settings Panel
            Texture2D settingsPanelImage = gameLoop.Content.Load<Texture2D>("Panels/settingsPanel");
            int settingsPanelWidth = settingsPanelImage.Width / 2 - 140;
            int settingsPanelHeight = settingsPanelImage.Height / 2;

            Rectangle settingsPanelDestination = new Rectangle(
             (int)GameLoop.Stage.X / 2 - settingsPanelWidth / 2,
             (int)GameLoop.Stage.Y / 2 - settingsPanelHeight + 240,
             settingsPanelWidth,
             settingsPanelHeight);

            SettingsPanel = new Image(gameLoop, gameLoop.SpriteBatch, settingsPanelImage, settingsPanelDestination);

            // Logo
            SpriteFont spriteFont = gameLoop.Content.Load<SpriteFont>("Fonts/logo");
            string msg = "Tales of Condoria";
            Logo = new Font(gameLoop, gameLoop.SpriteBatch, spriteFont, msg, new Vector2(
                GameLoop.Stage.X / 2 - spriteFont.MeasureString(msg).X / 2,  GameLoop.Stage.Y / 2 - spriteFont.MeasureString(msg).Y / 2 - 250), Color.White);

            this.BackgroundMusic = gameLoop.Content.Load<Song>("Sounds & FX/forestwalk_bg");
            MediaPlayer.Play(BackgroundMusic);
            MediaPlayer.IsRepeating = true;

            // We must add the components manually in the order we want them drawn on the screen
            // Using spritemode and layering wont work because we're using multiple draw calls (this took me a bit to figure out)
            // Layering only works if all gameobjects are in the same batch. Therefore in order to render text on top of the buttons,
            // we must add them after the button is added. But i guess thats the point of this. Each component is abstracted out
            // and has all logic nested inside, completely unaware of any other object
            this.GameObjects.Add(Background);

            this.GameObjects.Add(SettingsPanel);
            this.GameObjects.Add(AboutInfo);
            this.GameObjects.Add(HelpInfo);

            this.GameObjects.Add(Logo);

            this.GameObjects.Add(PlayButton);
            this.GameObjects.Add(PlayButton.Font);
            this.GameObjects.Add(HelpButton);
            this.GameObjects.Add(HelpButton.Font);
            this.GameObjects.Add(AboutButton);
            this.GameObjects.Add(AboutButton.Font);

            this.GameObjects.Add(BackButton);
            this.GameObjects.Add(BackButton.Font);

            BackButton.Hide();
            AboutInfo.Hide();
            HelpInfo.Hide();
        }

        public void HideMenu()
        {
            PlayButton.Hide();
            AboutButton.Hide();
            HelpButton.Hide();
        }

        public void ShowMenu()
        {
            PlayButton.Show();
            AboutButton.Show();
            HelpButton.Show();
        }


        private void OnPlayBtnClicked(object sender, ButtonClickEventArgs e)
        {
            base.Hide();
            Scene tutorialIsland = SceneManager.Instance.GetSceneByName("Tutorial Island");
            tutorialIsland.Show();
        }

        private void OnBackButtonClicked(object sender, ButtonClickEventArgs e)
        {
            ShowMenu();
            BackButton.Hide();
            AboutInfo.Hide();
            HelpInfo.Hide();
        }

        private void OnAboutBtnClicked(object sender, ButtonClickEventArgs e)
        {
            HideMenu();
            AboutInfo.Show();
            BackButton.Show();
        }

        private void OnHelpBtnClicked(object sender, ButtonClickEventArgs e)
        {
            HideMenu();
            HelpInfo.Show();
            BackButton.Show();
        }
    }
}
