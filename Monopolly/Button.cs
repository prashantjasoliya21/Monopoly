using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monopolly
{
	public class Button
	{
		private Texture2D texture;
		private Rectangle bounds;

		public event EventHandler Click;

		public Button(Texture2D texture, Rectangle bounds)
		{
			this.texture = texture;
			this.bounds = bounds;
		}

		public void Update(MouseState mouseState, MouseState prevMouseState)
		{
			if (bounds.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed)
			{
				// Button clicked
				Click?.Invoke(this, EventArgs.Empty);
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, bounds, Color.White);
		}
	}

}
