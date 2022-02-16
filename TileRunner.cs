using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using System;

namespace Emperia
{
	public class TileRunner
	{
		public Vector2 pos;
		public Vector2 speed;
		public Point16 hRange;
		public Point16 vRange;
		public double strength;
		public double str;
		public int steps;
		public int stepsLeft;
		public ushort type;
		public bool addTile;
		public bool overRide;

		public TileRunner(Vector2 pos, Vector2 speed, Point16 hRange, Point16 vRange, double strength, int steps, ushort type, bool addTile, bool overRide)
		{
			this.pos = pos;
			if (speed.X == 0 && speed.Y == 0)
			{
				this.speed = new Vector2(WorldGen.genRand.Next(hRange.X, hRange.Y + 1) * 0.1f, WorldGen.genRand.Next(vRange.X, vRange.Y + 1) * 0.1f);
			}
			else
			{
				this.speed = speed;
			}
			this.hRange = hRange;
			this.vRange = vRange;
			this.strength = strength;
			str = strength;
			this.steps = steps;
			stepsLeft = steps;
			this.type = type;
			this.addTile = addTile;
			this.overRide = overRide;
		}

		public void Start()
		{
			while (str > 0 && stepsLeft > 0)
			{
				str = strength * (double)stepsLeft / steps;

				PreUpdate();

				int a = (int)Math.Max(pos.X - str * 0.5, 1);
				int b = (int)Math.Min(pos.X + str * 0.5, Main.maxTilesX - 1);
				int c = (int)Math.Max(pos.Y - str * 0.5, 1);
				int d = (int)Math.Min(pos.Y + str * 0.5, Main.maxTilesY - 1);

				for (int i = a; i < b; i++)
				{
					for (int j = c; j < d; j++)
					{
						if (Math.Abs(i - pos.X) + Math.Abs(j - pos.Y) >= strength * StrengthRange())
							continue;
						Tile tile = Main.tile[i, j];
						if (type == 0)
						{
							tile.HasTile = false;
							continue;
						}
						if (overRide || !tile.HasTile)
							tile.TileType = type;
						if (addTile)
						{
							tile.HasTile = true;
							tile.LiquidType = 0;
							//tile.lava(false);
						}
					}
				}

				str += 50;
				while (str > 50)
				{
					pos += speed;
					stepsLeft--;
					str -= 50;
					speed.X += WorldGen.genRand.Next(hRange.X, hRange.Y + 1) * 0.05f;
					speed.Y += WorldGen.genRand.Next(vRange.X, vRange.Y + 1) * 0.05f;
				}

				speed = Vector2.Clamp(speed, new Vector2(-1, -1), new Vector2(1, 1));

				PostUpdate();
			}
		}

		public virtual double StrengthRange()
		{
			return 0.5 + WorldGen.genRand.Next(-10, 11) * 0.0075;
		}

		public virtual void PreUpdate()
		{

		}

		public virtual void PostUpdate()
		{

		}
	}
	public class TileRunnerTree : TileRunner
	{
		private bool branch;
		private int splitAt;

		public TileRunnerTree(Vector2 pos, bool branch = false) : base(pos, Vector2.Zero, new Point16(-1, 1), new Point16(-10), 13, 25, TileID.LivingWood, true, true)
		{
			this.branch = branch;
			if (branch)
			{
				hRange = new Point16(-10 + WorldGen.genRand.Next(2) * 20);
				vRange = new Point16(-10, 10);
				strength = 1002;
				stepsLeft = 10;
			}
			splitAt = 20 - WorldGen.genRand.Next(11);
		}

		public override void PreUpdate()
		{
			if (!branch)
			{
				if (strength > 7)
				{
					strength--;
				}
				str = strength - 2;
				if (stepsLeft == splitAt)
				{
					new TileRunnerTree(pos, true).Start();
					splitAt -= 3 + WorldGen.genRand.Next(6);
					if (splitAt < 10)
					{
						splitAt = 0;
					}
				}
				else if (stepsLeft == 1)
				{
					for (int i = 0; i < 4; i++)
					{
						TileRunnerTree tr = new TileRunnerTree(pos, true);
						tr.speed = Utils.RotatedBy(speed, -1.57f + i * 1.046f);
						tr.hRange = new Point16(-3, 3);
						tr.vRange = new Point16(-3, 3);
						tr.steps = 20;
						tr.stepsLeft = 12 + WorldGen.genRand.Next(9);
						tr.splitAt = 4 + WorldGen.genRand.Next(5);
						tr.Start();
					}
				}
			}
			else
			{
				str = 2;
				if (stepsLeft <= 4)
				{
					str -= 0.35;
				}
				if (stepsLeft == splitAt)
				{
					TileRunnerTree tr = new TileRunnerTree(pos, true);
					tr.speed = Utils.RotatedBy(speed, -0.6f);
					tr.hRange = new Point16(-3, 3);
					tr.vRange = new Point16(-3, 3);
					tr.stepsLeft = stepsLeft;
					tr.Start();

					speed = Utils.RotatedBy(speed, 0.6f);
				}
			}
		}
	}

	public class TileRunnerCave : TileRunner
	{
		Vector2 destination;
		bool diminishingStrength;

		public TileRunnerCave(Vector2 pos, Vector2 destination, double strength, bool diminishingStrength = false) : base(pos, Vector2.Zero, new Point16(-15, 15), new Point16(-15, 15), strength, 100, 0, false, true)
		{
			this.destination = destination;
			if (destination.X == 0)
			{
				speed = new Vector2(0, WorldGen.genRand.NextFloat());
				vRange = new Point16(0, 10);
			}
			this.diminishingStrength = diminishingStrength;
		}

		public override double StrengthRange()
		{
			return 0.8;
		}

		public override void PreUpdate()
		{
			if (!diminishingStrength)
				str = strength;
			if (destination.X != 0)
				speed = Vector2.Normalize(destination - pos) + new Vector2(WorldGen.genRand.Next(hRange.X, hRange.Y + 1) * 0.05f, WorldGen.genRand.Next(vRange.X, vRange.Y + 1) * 0.05f);
			else if (pos.Y > destination.Y)
				vRange = new Point16(-10, 10);
			else
				vRange = new Point16(0, 10);
		}
	}
}
