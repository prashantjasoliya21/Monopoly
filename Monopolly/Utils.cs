using Microsoft.Xna.Framework;
using Monopolly.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Monopolly
{
	class Utils
	{
		List<SoldProperties> soldProperties = new List<SoldProperties> ();

		Game1 game1;

		internal int BuyProperties(int index, List<PlayerPosition> playerPosition, int currentPlayerIndex, List<Propertiies> properties,List<String> players,int price)
		{
			
			Propertiies propertyAtDestination = properties.ElementAtOrDefault(index);
			if (price != 0 || price != 50||price!=75)
			{
				soldProperties.Add(new SoldProperties(players[currentPlayerIndex], propertyAtDestination.Name, 1));
			}
			
			PlayerPosition playerPosition1 = playerPosition.ElementAtOrDefault(currentPlayerIndex);
			playerPosition1.Balance=playerPosition1.Balance-price;
			return price;
		}

		internal bool CkeckCardsOnPosition(int destinationIndex, List<Propertiies> properties)
		{
			Propertiies propertyAtDestination = properties.ElementAtOrDefault(destinationIndex);

			if (propertyAtDestination != null)
			{
				var foundProperty = CheckIsSold(propertyAtDestination.Name);
				if (foundProperty != null)
				{
					return true;
				}
				else
				{
					if (destinationIndex == 4||destinationIndex==30|destinationIndex==38)
					{
						return true;
					}
					return false;
				}
			
			}
			else
			{
				return false;
				
			}
		}

		internal int PayRentToOwner(int destinationIndex, List<PlayerPosition> playerPosition, int tempIndex, List<Propertiies> properties, List<string> players,int price)
		{
			Propertiies propertyAtDestination = properties.ElementAtOrDefault(destinationIndex);
			var foundProperty = CheckIsSold(propertyAtDestination.Name);

			
			//soldProperties.Add(new SoldProperties(players[currentPlayerIndex], propertyAtDestination.Name, 1));
			if (destinationIndex == 4 || destinationIndex == 30 | destinationIndex == 38)
			{

				PlayerPosition playerPosition2 = playerPosition.ElementAtOrDefault(tempIndex);
				playerPosition2.Balance = playerPosition2.Balance - price;
				return price;
			}
			else
			{
				int index = players.IndexOf(foundProperty.PlayerName);
				PlayerPosition playerPosition1 = playerPosition.ElementAtOrDefault(index);
				playerPosition1.Balance = playerPosition1.Balance + price;

				PlayerPosition playerPosition2 = playerPosition.ElementAtOrDefault(tempIndex);
				playerPosition2.Balance = playerPosition2.Balance - price;
			}
			

			

			return 0;
		}

		private SoldProperties CheckIsSold(string name)
		{
			var property = soldProperties.FirstOrDefault(p => p.PropertyName.Equals(name, StringComparison.OrdinalIgnoreCase));
			return property != null ? property : null;
		}
	}
}
