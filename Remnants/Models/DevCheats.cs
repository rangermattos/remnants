using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Remnants
{
	class DevCheats
	{
		private static DevCheats instance;

		private DevCheats ()
		{
			
		}

		public static DevCheats Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new DevCheats();
				}
				return instance;
			}
		}

		// give the player a specified amount of a particular resources
		public void giveResource(int resource, int amount)
		{
			LevelData.Instance.resourceList[resource] += amount;
		}

		// give the player a specified amount of all resources
		public void giveResources(int amount)
		{
			for (int i = 0; i < 8; i++)
			{
				LevelData.Instance.resourceList[i+1] += amount;
			}
		}

		public void Update()
		{
			// use numpad for resource giving
			for (int i = 0; i < 8; i++)
			{
				if (InputManager.Instance.NumPadPressRelease(i))
				{
					giveResource(i,100);
				}
			}
			if (InputManager.Instance.NumPadPressRelease(0))
			{
				giveResources(100);
			}
		}
	}
}

