using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monopolly.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading;
using Timer = System.Windows.Forms.Timer;
using MonoGame.Extended;
using MonoGame.Extended.Gui;
using MonoGame.Extended.Gui.Controls;
using MonoGame.Extended.ViewportAdapters;
using System.Reflection;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Monopolly
{
	public enum GameState
	{
		StartMenu,
		About,
		Playing,
		Help
	}
	public class Game1 : Game
	{

		//Messagebox
		Texture2D messageImage;
		Rectangle messageRectangle;
		Texture2D yesButtonTexture, noButtonTexture;
		Rectangle yesButtonRectangle, noButtonRectangle;
		bool isMessageBoxVisible = false;

		int bankBalance = 1000;
		SoundEffect mySoundEffect,clickSound;
		SoundEffect dialog;

		Texture2D banner;
		Rectangle bannerRectangle;

		Texture2D bankImage;
		Rectangle bankRectangle;

		Texture2D p1Image;
		Rectangle p1Rectangle;

		Texture2D p2Image;
		Rectangle p2Rectangle;

		Texture2D p3Image;
		Rectangle p3Rectangle;

		Texture2D p4Image;
		Rectangle p4Rectangle;

		Texture2D Exit;
		Rectangle ExitRectangle;

		Texture2D exitMenu;
		Rectangle exitMenuRec;

		private Vector2 currentPosition;
		private Vector2 destinationPosition;


		private GameState _currentGameState = GameState.StartMenu;
		private KeyboardState _oldState;

		private string[] _menuItems = { "Start", "About", "Help" };

		string[] propertyValues = { "Go - COLLECT 200$ SALARY AS YOU PASS", "Bakery - 60$", "MYSTERY GIFT - FOLLOW INSTRUCTION ON TOP CARD", "FLOWER SHOP - 60$", "ZONING PERMIT FEE - PAY 200$", "CHOO CHOO CHARLIE'S STATION - 200$", "COFFEE SHOP - 100$", "VISITIOR CENTER", "DINER CAFE-RESTURANT", "ICE CREAM STAND GLACIER", "JUST VISITING - IN JAIL", "VIDEO GAME STORE - 140$", "BRIDGE POINT - 150$", "TOY STORE - 10$", "BIKE STORE - 160$", "CHUGGIN DUSTIN'S STATION - 200$", "SUSHI SHOP - 180$", "MYSTRY BOX - FOLLOW INSTRUCTION", "POOL HALL - 180$", "CINEMA - 200$", "FREE PARKING", "JET SKI SHOP - 220$", "LIFEGUARD STATION - 220$", "TONGA TOWER - 240$", "LOCOMOTIVE STATION - 200$", "DIAMOND BOUTIQUE - 260$", "TUXEDO RENTAL - 260$", "DAM BARRAGE - 150$", "WEDDING HALL - 280$", "GO TO JAIL", "OBSERVATORY - 300$", "FIREHOUSE - 300$", "MYSTERY GIFT - FOLLOW INSTRUCTION", "CAPITOL BUILDING - 320$", "MOVIN MAXINE'S STATION - 200$", "VISITOR CENTER", "ECO SKYSCRAPPER - 350$", "MINT TRESOR PUBLIC- PAY 75$", "PLATINUM TOWER - 400$" };

		string help = "Setup:\r\n\r\nEach player chooses a token and starts on \"Go.\"\r\nPlayers receive a starting amount of money (usually $1500).\r\nGameplay:\r\n\r\nPlayers take turns rolling two dice and moving around the board.\r\nBuy unowned properties you land on or auction them if you choose not to buy.\r\nPay rent if you land on properties owned by other players.\r\nComplete color groups to build houses and hotels, increasing rent.\r\nDraw cards when landing on Chance or Community Chest spaces.\r\nAvoid bankruptcy by managing your money and properties wisely.\r\nSpecial Rules:\r\n\r\nLanding on \"Go to Jail\" sends you to Jail; get out by rolling doubles, using a \"Get Out of Jail Free\" card, or paying a fee.\r\nIncome Tax and Luxury Tax spaces require payments to the bank.\r\nYou can mortgage properties to the bank for cash.\r\nWinning:\r\nThe game ends when all players except one are bankrupt. The remaining player is the winner.\r\n\r\nStrategy Tips:\r\n\r\nFocus on acquiring monopolies and building houses/hotels.\r\nTrade properties strategically with other players.\r\nKeep enough cash on hand for rent and other expenses.\r\nMonopoly combines luck with strategy and negotiation, making it a classic board game for friends and family.";
		private Timer animationTimer;
		private const int animationDuration = 1000;
	
		private List<Propertiies> properties = new List<Propertiies>();

		private List<Position> position = new List<Position>();
		private List<Position> position1 = new List<Position>();
		private List<Position> position2 = new List<Position>();
		private List<Position> position3 = new List<Position>();
		private List<PlayerPosition> playerPosition = new List<PlayerPosition>();
		private List<string> players = new List<string> { "Player 1", "Player 2", "Player 3", "Player 4" };
		private DateTime animationStartTime;
		private int currentPlayerIndex = 0;
		private int tempIndex = 0;
		private int destinationIndex = 0;
		int d = 0;

		 private string title = "Monopoly";
		private int _selectedIndex = 0;

		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		Texture2D boardTexture;
		Texture2D p1Texture,p2Texture,p3Texture,p4Texture;
		Vector2 imagePosition1 = new Vector2(885, 885);
		Vector2 imagePosition2 = new Vector2(905, 885);
		Vector2 imagePosition3 = new Vector2(925, 885);
		Vector2 imagePosition4 = new Vector2(945, 885);
		Texture2D buttonTexture; // Texture for the button
		Rectangle buttonRectangle; // Position and size of the button

		MouseState currentMouseState;
		MouseState previousMouseState;
		Random random = new Random();

		SpriteFont gameFont;


		int diceRollResult = 0;
		bool showDiceResult = false;
		double diceResultDisplayTime = 0;
		Utils utils = new Utils();



		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			//_graphics.IsFullScreen = true;
			_graphics.PreferredBackBufferWidth = 1920;
			_graphics.PreferredBackBufferHeight = 1080;
			_graphics.IsFullScreen = true;

			// Apply changes to the graphics device
			_graphics.ApplyChanges();
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			Window.Title = "Monopoly";
			playerPosition.Add(new PlayerPosition(0, 0, 1000));
			playerPosition.Add(new PlayerPosition(1, 0, 1000));
			playerPosition.Add(new PlayerPosition(2, 0, 1000));
			playerPosition.Add(new PlayerPosition(3, 0, 1000));


			createGameBoard();
			createCards();

			

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			 boardTexture = Content.Load<Texture2D>("bgboard");
			 p1Texture = Content.Load<Texture2D>("bt");
			 p2Texture = Content.Load<Texture2D>("yt");
			 p3Texture = Content.Load<Texture2D>("wt");
			 p4Texture = Content.Load<Texture2D>("pt");
			gameFont = Content.Load<SpriteFont>("galleryFont");

			buttonTexture = Content.Load<Texture2D>("rollDce");
			Exit = Content.Load<Texture2D>("exit");
			exitMenu = Content.Load<Texture2D>("exit");

			//mySong = Content.Load<Song>("diceSound");
			mySoundEffect = Content.Load<SoundEffect>("diceSound");
			clickSound = Content.Load<SoundEffect>("clicksound");
			dialog = Content.Load<SoundEffect>("dialog");
			buttonRectangle = new Rectangle(1100, 500, 300, 250);
			ExitRectangle = new Rectangle(1100, buttonRectangle.Bottom-50, 300, 250);
			exitMenuRec = new Rectangle(50,50, 100, 80);

			//bank
			bankImage = Content.Load<Texture2D>("bank");
			bankRectangle = new Rectangle(1600, 5, 300, 250);
			banner = Content.Load<Texture2D>("banner");
			bannerRectangle = new Rectangle(1100, 5, 400, 250);
			//player
			p1Image = Content.Load<Texture2D>("playerB");
			p1Rectangle = new Rectangle(1600, bankRectangle.Bottom+5, 300, 200);
	

			p2Image = Content.Load<Texture2D>("playerY");
			p2Rectangle = new Rectangle(1600, p1Rectangle.Bottom+5, 300, 200);

			p3Image = Content.Load<Texture2D>("playerW");
			p3Rectangle = new Rectangle(1600, p2Rectangle.Bottom+5, 300, 200);

			p4Image = Content.Load<Texture2D>("playerP");
			p4Rectangle = new Rectangle(1600, p3Rectangle.Bottom+5, 300, 200);




			// Load the textures
			string c1 = "message";
			messageImage = Content.Load<Texture2D>(c1);
			yesButtonTexture = Content.Load<Texture2D>("yes");
			noButtonTexture = Content.Load<Texture2D>("no");

			// Set up rectangles (positions and sizes)	
			messageRectangle = new Rectangle(300, 300, 400, 250);
			noButtonRectangle = new Rectangle(messageRectangle.Left-5, messageRectangle.Bottom - 50, 100, 200);
			yesButtonRectangle = new Rectangle(noButtonRectangle.Right+200, messageRectangle.Bottom-50, 100, 200);
			


		}


		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			KeyboardState newState = Keyboard.GetState();

			if (_currentGameState == GameState.StartMenu)
			{
				if (newState.IsKeyDown(Keys.Down) && !_oldState.IsKeyDown(Keys.Down))
				{
					_selectedIndex++;
					clickSound.Play();
					if (_selectedIndex > _menuItems.Length - 1) _selectedIndex = 0;
				}
				if (newState.IsKeyDown(Keys.Up) && !_oldState.IsKeyDown(Keys.Up))
				{
					_selectedIndex--;
					clickSound.Play();
					if (_selectedIndex < 0) _selectedIndex = _menuItems.Length - 1;
				}
				if (newState.IsKeyDown(Keys.Enter) && !_oldState.IsKeyDown(Keys.Enter))
				{
					switch (_selectedIndex)
					{
						case 0:
							// Start game logic
							clickSound.Play();
							_currentGameState = GameState.Playing;
							break;
						case 1:
							// About page logic
							clickSound.Play();
							_currentGameState = GameState.About;
							break;
						case 2:
							clickSound.Play();
							_currentGameState = GameState.Help;
							break;
					}
				}
			}

			_oldState = newState;


			currentMouseState = Mouse.GetState();
			if (currentMouseState.LeftButton == ButtonState.Pressed &&
			previousMouseState.LeftButton == ButtonState.Released &&
			exitMenuRec.Contains(currentMouseState.X, currentMouseState.Y))
			{
				clickSound.Play();
				//	bankBalance = 200;
				//MediaPlayer.Play(mySong);
_currentGameState= GameState.StartMenu;

			}



			// Check if the button is clicked
			if (currentMouseState.LeftButton == ButtonState.Pressed &&
				previousMouseState.LeftButton == ButtonState.Released &&
				buttonRectangle.Contains(currentMouseState.X, currentMouseState.Y))
			{
				clickSound.Play();
				//	bankBalance = 200;
				//MediaPlayer.Play(mySong);
				
				RollDiceButtonClick();
				
			}

			if (currentMouseState.LeftButton == ButtonState.Pressed &&
				previousMouseState.LeftButton == ButtonState.Released &&
				ExitRectangle.Contains(currentMouseState.X, currentMouseState.Y))
			{
				clickSound.Play();
				Exit();

			}

			if (showDiceResult)
			{
				diceResultDisplayTime -= gameTime.ElapsedGameTime.TotalSeconds;
				if (diceResultDisplayTime <= 0)
				{
					showDiceResult = false;
				}
			}

			previousMouseState = currentMouseState;


			// Messagebox logic-------------------------------------------------------------------------------------------

			MessageBoxLogic();
			

			base.Update(gameTime);
		}

		private void MessageBoxLogic()
		{
			Point mousePosition = new Point(currentMouseState.X, currentMouseState.Y);

			if (isMessageBoxVisible)
			{
				if (yesButtonRectangle.Contains(mousePosition) && currentMouseState.LeftButton == ButtonState.Pressed)
				{
					// Yes button clicked, hide message box
					clickSound.Play();
					CardBuyandSell(destinationIndex);

					isMessageBoxVisible = false;
				}

				if (noButtonRectangle.Contains(mousePosition) && currentMouseState.LeftButton == ButtonState.Pressed)
				{
					clickSound.Play();
					// No button clicked, hide message box


					isMessageBoxVisible = false;
				}
			}
			else
			{

			}
		}

		private void CardBuyandSell(int index)
		{
			switch (index)
			{
				case 0:
					
					
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,0);
					Debug.WriteLine("...................b1................." + destinationIndex);
					break;

				case 1:
					
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,60);

					Debug.WriteLine("...................b2................." + destinationIndex);
					break;
					case 2:
					
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,0);
					Debug.WriteLine("....................b3................" + destinationIndex);
					break;
					case 3:
					
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,60);
					Debug.WriteLine("....................b4................" + destinationIndex);
					break;
					case 4:
				
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,50);
					Debug.WriteLine("....................b5................" + destinationIndex);
					break;
					case 5:
					
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,200);
					Debug.WriteLine("....................b6................" + destinationIndex);
					break;
					case 6:
				
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,100);
					Debug.WriteLine("....................b7................" + destinationIndex);
					break;
					case 7:
					
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,0);
					Debug.WriteLine("....................b8................" + destinationIndex);
					break;
					case 8:
				
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,100);
					Debug.WriteLine("....................b9................" + destinationIndex);
					break;
					case 9:
					
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,120);
					Debug.WriteLine("....................b10................" + destinationIndex);
					break;
					case 10:
				
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,50);
					Debug.WriteLine("....................l1................" + destinationIndex);
					break;
					case 11:
					
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,140);
					Debug.WriteLine("....................l2................" + destinationIndex);
					break;
					case 12:
					
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,150);
					Debug.WriteLine("....................l3................" + destinationIndex);
					break;
					case 13:
					
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,140);
					Debug.WriteLine("....................l4................" + destinationIndex);
					break;
					case 14:
					
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,160);
					Debug.WriteLine("....................l5................" + destinationIndex);
					break;
					case 15:
				
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,200);
					Debug.WriteLine("....................l6................" + destinationIndex);
					break;
					case 16:
				
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,180);
					Debug.WriteLine("....................l7................" + destinationIndex);
					break;
					case 17:
					
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,0);
					Debug.WriteLine("....................l8................" + destinationIndex);
					break;
					case 18:
					
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,180);
					Debug.WriteLine("....................l9................" + destinationIndex);
					break;
					case 19:
					
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,200);
					Debug.WriteLine("....................l10................" + destinationIndex);
					break;
					case 20:
				
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,0);
					Debug.WriteLine("....................t1................" + destinationIndex);
					break;
					case 21:
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,220);
					Debug.WriteLine("....................t2................" + destinationIndex);
					break;
					case 22:
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,0);
					Debug.WriteLine("....................t3................" + destinationIndex);
					break;
					case 23:
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,220);
					Debug.WriteLine("....................t4................" + destinationIndex);
					break;
					case 24:
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,240);
					Debug.WriteLine("....................t5................" + destinationIndex);
					break;
					case 25:
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,200);
					Debug.WriteLine("....................t6................" + destinationIndex);
					break;
					case 26:
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,260);
					Debug.WriteLine("....................t7................" + destinationIndex);
					break;
					case 27:
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,260);
					Debug.WriteLine("....................t8................" + destinationIndex);
					break;
					case 28:
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,150);
					Debug.WriteLine("....................t9................" + destinationIndex);
					break;
					case 29:
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,280);
					Debug.WriteLine("....................t10................" + destinationIndex);

					break;
					case 30:
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,50);
					Debug.WriteLine("....................r1................" + destinationIndex);
					break;
					case 31:
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,300);
					Debug.WriteLine("....................r2................" + destinationIndex);

					break;
					case 32:
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,300);
					Debug.WriteLine("....................r3................" + destinationIndex);
					break;
				case 33:
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,0);
					Debug.WriteLine("....................r4................" + destinationIndex);
					break;
				case 34:
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,320);
					Debug.WriteLine("....................r5................" + destinationIndex);
					break;
				case 35:
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,200);
					Debug.WriteLine("....................r6................" + destinationIndex);
					break;
				case 36:
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,0);
					Debug.WriteLine("....................r7................" + destinationIndex);
					break;
				case 37:
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,350);
					Debug.WriteLine("....................r8................" + destinationIndex);
					break;
				case 38:
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,75);
					Debug.WriteLine("....................r9................" + destinationIndex);
					break;
				case 39:
					bankBalance += utils.BuyProperties(index, playerPosition, tempIndex, properties, players,400);
					Debug.WriteLine("....................r10................" + destinationIndex);
					break;
				default:
					break;




			}
			//Debug.WriteLine("...................................." + destinationIndex);
		}

		private void RollDiceButtonClick()
		{
			int die1 = RollDice();
			//int die2 = RollDice();

			//dice = die1 + die2;
			
			destinationIndex = playerPosition[currentPlayerIndex].CurrentPosition + die1;
			if (destinationIndex >= 40)
			{
				destinationIndex = destinationIndex - 40;
			}
			//destinationIndex = d;
			PlayerPosition propertyToUpdate = playerPosition.Cast<PlayerPosition>().ElementAt(currentPlayerIndex);
			propertyToUpdate.CurrentPosition = destinationIndex;

			tempIndex = currentPlayerIndex;
			title = properties[destinationIndex].Name.ToString();
			//MessageBox.Show($"{players[currentPlayerIndex]} rolled: {die1} and {die2}", "Dice Roll");

			//label1.Text = $"{players[currentPlayerIndex]} rolled: {die1} and {die2} Dice Roll";         // Move to the next player or start over if all players have rolled
			//label1.Text = destinationIndex.ToString();

			if (utils.CkeckCardsOnPosition(destinationIndex, properties))
			{
				switch (destinationIndex)
				{
					case 0:
					 	bankBalance+= utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,0);
						title += "- Pay rent 0$";
						break;

					case 1:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,80);
						title += "\nPay rent 80$";
						break;
					case 2:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,0);
						title += "\nPay rent 0$";
						break;
					case 3:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,80);
						title += "\nPay rent 80$";
						break;
					case 4:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,50);
						title += "\nPay rent 50$";
						break;
					case 5:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,250);
						title += "\nPay rent 250$";
						break;
					case 6:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,150);
						title += "\nPay rent 150$";
						break;
					case 7:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,0);
						title += "\nPay rent 0$";
						break;
					case 8:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,150);
						title += "\nPay rent 150$";
						break;
					case 9:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,180);
						title += "\nPay rent 180$";
						break;
					case 10:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,0);
						title += "\nPay rent 0$";
						break;
					case 11:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,180);
						title += "\nPay rent 180$";
						break;
					case 12:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,250);
						title += "\nPay rent 250$";
						break;
					case 13:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,180);
						title += "\nPay rent 180$";
						break;
					case 14:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,210);
						title += "\nPay rent 210$";
						break;
					case 15:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,300);
						title += "\nPay rent 300$";
						break;
					case 16:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,210);
						title += "\nPay rent 210$";
						break;
					case 17:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,0);
						title += "\nPay rent 0$";
						break;
					case 18:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,240);
						title += "\nPay rent 240$";
						break;
					case 19:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,250);
						title += "\nPay rent 250$";
						break;
					case 20:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,0);
						title += "\nPay rent 0$";
						break;
					case 21:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,280); 
						title += "\nPay rent 280$";
						break;
					case 22:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,0);
						title += "\nPay rent 0$";
						break;
					case 23:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,280);
						title += "\nPay rent 280$";
						break;
					case 24:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,310);
						title += "\nPay rent 310$";
						break;
					case 25:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,300);
						title += "\nPay rent 300$";
						break;
					case 26:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,310);
						title += "\nPay rent 310$";
						break;
					case 27:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,310);
						title += "\nPay rent 310$";
						break;
					case 28:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,210);
						title += "\nPay rent 210$";
						break;
					case 29:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,350);
						title += "\nPay rent 350$";
						break;
					case 30:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,50);
						title += "\nPay rent 50$";
						break;
					case 31:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,400);
						title += "\nPay rent 400$";

						break;
					case 32:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,450);
						title += "\nPay rent 450$";
						break;
					case 33:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,0);
						title += "\nPay rent 0$";
						break;
					case 34:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,380);
						title += "\nPay rent 380$";
						break;
					case 35:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,400);
						title += "\nPay rent 400$";
						break;
					case 36:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,0);
						title += "\nPay rent 0$";
						break;
					case 37:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,450);
						title += "\nPay rent 450$";
						break;
					case 38:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,75);
						title += "\nPay rent 75$";
						break;
					case 39:
						bankBalance += utils.PayRentToOwner(destinationIndex, playerPosition, tempIndex, properties, players,500);
						title += "\nPay rent 500$";
						break;
					default:
						break;




				}
				
			}
			else
			{
				if (destinationIndex == 2||destinationIndex==7||destinationIndex==10|destinationIndex==17||destinationIndex==20||destinationIndex==22||destinationIndex==30||destinationIndex==33||destinationIndex==36||destinationIndex==38)
				{
					isMessageBoxVisible = false;
				}
				else
				{
					dialog.Play();
					isMessageBoxVisible = true;
				}
			
			}

			Animation();


			
			
			
			//RollDice();
			//d++;
		}

		private int RollDice()
		{
			mySoundEffect.Play();
			diceRollResult = random.Next(1, 7); // Rolling a 6-sided dice
			showDiceResult = true;
			diceResultDisplayTime = 5; // Display result for 5 seconds, for example
			System.Diagnostics.Debug.WriteLine("Dice Rolled: " + diceRollResult);
			return diceRollResult;
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.ForestGreen);


			_spriteBatch.Begin();

			if (_currentGameState == GameState.StartMenu)
			{
				for (int i = 0; i < _menuItems.Length; i++)
				{
					Color color = (i == _selectedIndex) ? Color.Red : Color.White;
					_spriteBatch.DrawString(gameFont, _menuItems[i], new Vector2(940, 472 + i * 50), color);
				}
				
			}
			else if (_currentGameState == GameState.Playing)
			{
				// GameBoard draw ----------------------------------------------------------------------------------
				//_spriteBatch.Begin();
				_spriteBatch.Draw(boardTexture, Vector2.Zero, Color.White);
				_spriteBatch.Draw(p1Texture, imagePosition1, Color.White);
				_spriteBatch.Draw(p2Texture, imagePosition2, Color.White);
				_spriteBatch.Draw(p3Texture, imagePosition3, Color.White);
				_spriteBatch.Draw(p4Texture, imagePosition4, Color.White);
				_spriteBatch.DrawString(gameFont, title, new Vector2(1100, 300), Color.White);
				//_spriteBatch.End();

				// Rolldice button draw ----------------------------------------------------------------------------------
				//_spriteBatch.Begin();
				_spriteBatch.Draw(buttonTexture, buttonRectangle, Color.White);
				_spriteBatch.Draw(Exit, ExitRectangle, Color.White);
				
				_spriteBatch.Draw(banner, bannerRectangle, Color.White);
				//_spriteBatch.End();


				// Rolldice label draw ----------------------------------------------------------------------------------
				//_spriteBatch.Begin();
				if (showDiceResult)
				{
					string resultText = $"Dice Rolled: {diceRollResult}";
					Vector2 position1 = new Vector2(1100, 450); // Adjust the position as needed
					_spriteBatch.DrawString(gameFont, resultText, position1, Color.Black);

				}
				//_spriteBatch.End();

				// Messagebox draw ----------------------------------------------------------------------------------
				//_spriteBatch.Begin();
				if (isMessageBoxVisible)
				{
					_spriteBatch.Draw(messageImage, messageRectangle, Color.White);
					_spriteBatch.Draw(yesButtonTexture, yesButtonRectangle, Color.White);
					_spriteBatch.Draw(noButtonTexture, noButtonRectangle, Color.White);
				}
				//_spriteBatch.End();

				// bank draw -------------------------------------------------------------------------
				//_spriteBatch.Begin();
				_spriteBatch.Draw(bankImage, bankRectangle, Color.White);
				Vector2 position = new Vector2(1700, 150);
				_spriteBatch.DrawString(gameFont, bankBalance.ToString() + "$", position, Color.Black);
				//_spriteBatch.End();

				// Player draw------------------------------------------------------------------------------------
				//_spriteBatch.Begin();
				_spriteBatch.Draw(p1Image, p1Rectangle, Color.White);

				Vector2 p1 = new Vector2(1700, bankRectangle.Bottom + 125);
				Vector2 p1Name = new Vector2(1725, bankRectangle.Bottom + 50);
				_spriteBatch.DrawString(gameFont, playerPosition[0].Balance.ToString() + "$", p1, Color.Black);
				_spriteBatch.DrawString(gameFont, players[0], p1Name, Color.Black);

				_spriteBatch.Draw(p2Image, p2Rectangle, Color.White);

				Vector2 p2 = new Vector2(1700, p1Rectangle.Bottom + 125);
				Vector2 p2Name = new Vector2(1725, p1Rectangle.Bottom + 50);
				_spriteBatch.DrawString(gameFont, playerPosition[1].Balance.ToString() + "$", p2, Color.Black);
				_spriteBatch.DrawString(gameFont, players[1], p2Name, Color.Black);

				_spriteBatch.Draw(p3Image, p3Rectangle, Color.White);

				Vector2 p3 = new Vector2(1700, p2Rectangle.Bottom + 125);
				Vector2 p3Name = new Vector2(1725, p2Rectangle.Bottom + 50);
				_spriteBatch.DrawString(gameFont, playerPosition[2].Balance.ToString() + "$", p3, Color.Black);
				_spriteBatch.DrawString(gameFont, players[2], p3Name, Color.Black);

				_spriteBatch.Draw(p4Image, p4Rectangle, Color.White);
				Vector2 p4 = new Vector2(1700, p3Rectangle.Bottom + 125);
				Vector2 p4Name = new Vector2(1725, p3Rectangle.Bottom + 50);
				_spriteBatch.DrawString(gameFont, playerPosition[3].Balance.ToString() + "$", p4, Color.Black);
				_spriteBatch.DrawString(gameFont, players[3], p4Name, Color.Black);
			}
			else if (_currentGameState==GameState.Help)
			{
				_spriteBatch.Draw(exitMenu, exitMenuRec, Color.White);
				_spriteBatch.DrawString(gameFont, help, new Vector2(100, 100), Color.White);

			}
			else if (_currentGameState==GameState.About)
			{
				_spriteBatch.Draw(exitMenu, exitMenuRec, Color.White);
				_spriteBatch.DrawString(gameFont, " Monopoly game\n Conestoga : Game Final Project \n made by \n1.Prashant\n2.Faizan\n3.Sahil\n4.Dhruv", new Vector2(100, 100), Color.White);
			}
			_spriteBatch.End();

			base.Draw(gameTime);
		}


		private void createGameBoard()
		{
			Point btPosition =new Point(885,885);
			Point ytPosition = new Point(905,885);
			Point wtPosition = new Point(925,885);
			Point ptPosition = new Point(945,885);
			char[] sectionLetters = { 'B', 'L', 'T', 'R' };
			int sectionSize = 10;
			int index = 0;

			for (int section = 0; section < sectionLetters.Length; section++)
			{
				for (int i = 1; i <= sectionSize; i++)
				{
					char sectionLetter = sectionLetters[section];

					switch (sectionLetter)
					{
						case 'B':
							position.Add(new Position($"{sectionLetter}{i}", btPosition.X, btPosition.Y));
							position1.Add(new Position($"{sectionLetter}{i}", ytPosition.X, ytPosition.Y));
							position2.Add(new Position($"{sectionLetter}{i}", wtPosition.X, wtPosition.Y));
							position3.Add(new Position($"{sectionLetter}{i}", ptPosition.X, ptPosition.Y));
							btPosition.X -= 82;
							ytPosition.X -= 82;
							wtPosition.X -= 82;
							ptPosition.X -= 82;
							break;

						case 'L':
							if (i == 1)
							{
								btPosition.X = 122; ytPosition.X = 122; wtPosition.X = 122; ptPosition.X = 122;
								btPosition.Y = 885; ytPosition.Y = 905; wtPosition.Y = 925; ptPosition.Y = 945;
							}
							position.Add(new Position($"{sectionLetter}{i}", btPosition.X, btPosition.Y));
							position1.Add(new Position($"{sectionLetter}{i}", ytPosition.X, ytPosition.Y));
							position2.Add(new Position($"{sectionLetter}{i}", wtPosition.X, wtPosition.Y));
							position3.Add(new Position($"{sectionLetter}{i}", ptPosition.X, ptPosition.Y));
							btPosition.Y -= 82;
							ytPosition.Y -= 82;
							wtPosition.Y -= 82;
							ptPosition.Y -= 82;
							break;

						case 'T':
							if (i == 1)
							{
								btPosition.X = 125; ytPosition.X = 105; wtPosition.X = 85; ptPosition.X = 65;
								btPosition.Y = 123; ytPosition.Y = 123; wtPosition.Y = 123; ptPosition.Y = 123;
							}
							position.Add(new Position($"{sectionLetter}{i}", btPosition.X, btPosition.Y));
							position1.Add(new Position($"{sectionLetter}{i}", ytPosition.X, ytPosition.Y));
							position2.Add(new Position($"{sectionLetter}{i}", wtPosition.X, wtPosition.Y));
							position3.Add(new Position($"{sectionLetter}{i}", ptPosition.X, ptPosition.Y));
							btPosition.X += 82;
							ytPosition.X += 82;
							wtPosition.X += 82;
							ptPosition.X += 82;
							break;

						case 'R':
							if (i == 1)
							{
								btPosition.X = 885; ytPosition.X = 885; wtPosition.X = 885; ptPosition.X = 885;
								btPosition.Y = 123; ytPosition.Y = 103; wtPosition.Y = 83; ptPosition.Y = 63;
							}
							position.Add(new Position($"{sectionLetter}{i}", btPosition.X, btPosition.Y));
							position1.Add(new Position($"{sectionLetter}{i}", ytPosition.X, ytPosition.Y));
							position2.Add(new Position($"{sectionLetter}{i}", wtPosition.X, wtPosition.Y));
							position3.Add(new Position($"{sectionLetter}{i}", ptPosition.X, ptPosition.Y));
							btPosition.Y += 82;
							ytPosition.Y += 82;
							wtPosition.Y += 82;
							ptPosition.Y += 82;
							break;
					}
				}

			}

			Debug.WriteLine("...................................." + position.Count);

			foreach (var position in position)
			{
				Debug.WriteLine("...................................." + position.Name + ".........." + position.X + "............." + position.Y);
			}

		}


		private void createCards()
		{


			//List<Propertiies> propertiesList = new List<Propertiies>();


			for (int i = 0; i < propertyValues.Length; i++)
			{
				string[] parts = propertyValues[i].Split(new[] { " - " }, StringSplitOptions.None);
				string name = parts[0];
				string tag = parts.Length > 1 ? parts[1] : "";

				properties.Add(new Propertiies(i + 1, name, tag));
			}

			/*foreach (Propertiies prop in propertiesList)
			{
				Console.WriteLine($"Id: {prop.Id}, Name: {prop.Name}, Tag: {prop.Tag}");
			}*/
		}

		private void Animation()
		{


			if (currentPlayerIndex == 0)
			{
				//label1.Text += currentPlayerIndex.ToString();
				currentPosition = imagePosition1;

				destinationPosition = new Vector2(position[destinationIndex].X, position[destinationIndex].Y);
				animationTimer = new Timer();
				animationTimer.Interval = 50;
				animationTimer.Tick += AnimationTimer_Tick;


				StartAnimation();
				currentPosition = imagePosition1;
			}
			else if (currentPlayerIndex == 1)
			{
				currentPosition = imagePosition2;
				destinationPosition = new Vector2(position1[destinationIndex].X, position1[destinationIndex].Y);
				animationTimer = new Timer();
				animationTimer.Interval = 50;
				animationTimer.Tick += AnimationTimer_Tick;


				StartAnimation();
				currentPosition = imagePosition2;
			}
			else if (currentPlayerIndex == 2)
			{
				currentPosition = imagePosition3;
				destinationPosition = new Vector2(position2[destinationIndex].X, position2[destinationIndex].Y);
				animationTimer = new Timer();
				animationTimer.Interval = 50;
				animationTimer.Tick += AnimationTimer_Tick;


				StartAnimation();
				currentPosition = imagePosition3;
			}
			else
			{
				currentPosition = imagePosition4;
				destinationPosition = new Vector2(position3[destinationIndex].X, position3[destinationIndex].Y);
				animationTimer = new Timer();
				animationTimer.Interval = 50;
				animationTimer.Tick += AnimationTimer_Tick;


				StartAnimation();
				currentPosition = imagePosition4;
			}
		}

		private void AnimationTimer_Tick(object sender, EventArgs e)
		{
			
			// Calculate the progress of the animation
			double progress = (DateTime.Now - animationStartTime).TotalMilliseconds / animationDuration;

			// If the animation is complete, stop the timer
			if (progress >= 1.0)
			{
				animationTimer.Stop();
				if (currentPlayerIndex == 0)
				{
					imagePosition1 = destinationPosition;
				}
				else if (currentPlayerIndex == 1) { imagePosition2 = destinationPosition; }
				else if (currentPlayerIndex == 2) {imagePosition3 = destinationPosition; }
				else {imagePosition4 = destinationPosition; }
				currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
				// Set the final position
			}
			else
			{
				// Interpolate the current position based on the progress
				int newX = (int)(currentPosition.X + progress * (destinationPosition.X - currentPosition.X));
				int newY = (int)(currentPosition.Y + progress * (destinationPosition.Y - currentPosition.Y));
				if (currentPlayerIndex == 0)
				{
					imagePosition1 = new Vector2(newX, newY);
				}
				else if (currentPlayerIndex == 1) { imagePosition2 = new Vector2(newX, newY); }
				else if (currentPlayerIndex == 2) { imagePosition3 = new Vector2(newX, newY); }
				else { imagePosition4 = new Vector2(newX, newY); }
			}
		}

		private void StartAnimation()
		{
			// Start the animation timer
			//animationTimer.Elapsed += OnAnimationTimerElapsed;
			animationStartTime = DateTime.Now;
			animationTimer.Start();
		}
	}
}
