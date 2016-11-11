using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Remnants
{
	class MessageQueue : UIItem
	{
		List<Message> messages;
        Mission mission;
		float msgDuration;
		float width, height;
        float MissionScale = 0.4f;
		
		public MessageQueue (Vector2 tl, Vector2 incposition, Vector2 textureScale, Texture2D txt, float msgDuration) : base(tl, incposition, textureScale, txt, true)
		{
			this.msgDuration = msgDuration;
			messages = new List<Message>();
			scale = 0.3f;
            mission = new Mission("Current Mission: Create a Solar Panel or Wind Turbine");
		}

		public void addMessage(string msg)
		{
			messages.Add(new Message(msg, msgDuration));
		}

		public override void Update(GameTime gameTime)
		{
            mission.isCompleted();
            /*
            if (mission.isCompleted()){
                mission = null;
            }
            */
			for (int i = 0; i < messages.Count; i++)
			{
				messages[i].Update(gameTime);
				if (messages[i].isCompleted())
				{
					messages.RemoveAt(i);
					i--;
				}
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			width = 0f;
			height = 0f;
			foreach (Message m in messages)
			{
				Vector2 tempVect = MenuController.Instance.font.MeasureString(m.msg) * scale;
				height += tempVect.Y;
				if (tempVect.X > width)
				{
					width = tempVect.X;
				}
			}
			width = width / Camera.Instance.viewportScale.Scale.X;

            //var v = new Vector2(0, (Camera.Instance.cam.Origin.Y * 2) - 32);
            //var center = new Vector2(topLeft.X + (width / 2), topLeft.Y + (height / 2));

            //this is extremely innefficient way to draw mission
            Vector2 v = new Vector2(topLeft.X, topLeft.Y - (MenuController.Instance.font.MeasureString(mission.msg)).Y * MissionScale);
            v = Vector2.Transform(v, Camera.Instance.viewportScale);
            mission.Draw(spriteBatch, v, MissionScale);

			float avgHeight = height / messages.Count;
			for (int i = 0; i < messages.Count; i++)
			{
				Vector2 pos = new Vector2(topLeft.X, topLeft.Y + (i * avgHeight));
				pos = Vector2.Transform(pos, Camera.Instance.viewportScale);
				messages[i].Draw(spriteBatch, pos, scale);
			}
		}
	}

	class Message
	{
		public string msg;
		float elapsedTime = 0f;
		float duration;

		public Message(string msg, float duration)
		{
			this.msg = msg;
			this.duration = duration;
		}

		public void Update(GameTime gameTime)
		{
			float deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;
			elapsedTime += deltaT;
		}

		public bool isCompleted()
		{
			return elapsedTime >= duration;
		}

		public void Draw(SpriteBatch spriteBatch, Vector2 pos, float scale)
		{
			spriteBatch.DrawString(MenuController.Instance.font, msg, pos, Color.Black, 0f, Vector2.Zero, scale, SpriteEffects.None, 1f);
		}
	}

    //for mission objectives to guide the player
    class Mission
    {
        public string msg;

        public Mission(string msg)
        {
            this.msg = msg;
        }

        public void Update(GameTime gameTime)
        {
            float deltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public bool isCompleted()
        {
            foreach(LevelData.buildingData b in LevelData.Instance.buildingList)
            {
                if(b.type == "SolarPanel" || b.type == "WindTrubine")
                {
                    msg = "Current Mission:";
                    return true;
                }
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 pos, float scale)
        {
            spriteBatch.DrawString(MenuController.Instance.font, msg, pos, Color.Black, 0f, Vector2.Zero, scale, SpriteEffects.None, 1f);
        }
    }
}

