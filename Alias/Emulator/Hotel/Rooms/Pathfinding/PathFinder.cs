using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Alias.Emulator.Hotel.Rooms.Models;
using Alias.Emulator.Hotel.Rooms.Users;

namespace Alias.Emulator.Hotel.Rooms.Pathfinding
{
	class PathFinder
	{
		public Point[] DiagonalMovement = new[]
		{
			new Point(0, -1),
			new Point(1, 0),
			new Point(0, 1),
			new Point(-1, 0),
			new Point(-1,-1),
			new Point(1, 1),
			new Point(-1, 1),
			new Point(1, -1)
		};

		public Point[] NoDiagonalMovement = new[]
		{
			new Point(0, -1),
			new Point(1, 0),
			new Point(0, 1),
			new Point(-1, 0)
		};

		public Room Room
		{
			get; set;
		}

		public PathFinder(Room r)
		{
			this.Room = r;
		}

		public LinkedList<Point> Path(UserPosition position, UserPosition target)
		{
			LinkedList<Node> openNodes = new LinkedList<Node>();
			for (int x = 0; x < this.Room.DynamicModel.SizeX; x++)
			{
				for (int y = 0; y < this.Room.DynamicModel.SizeY; y++)
				{
					if (this.Room.DynamicModel.Tiles[x, y].State == TileState.OPEN)
					{
						if (this.Room.DynamicModel.ValidTile(x, y, target.X == x && target.Y == y) || (position.X == x && position.Y == y))
						{
							openNodes.AddLast(new Node(x, y));
						}
					}
				}
			}
			LinkedList<Node> duplicated = new LinkedList<Node>(openNodes);
			Node startNode = openNodes.Where(n => n.X == position.X && n.Y == position.Y).First();
			startNode.CostsToStartPoint = 0;
			startNode.ParentNode = startNode;
			LinkedList<Node> queue = new LinkedList<Node>();
			queue.AddFirst(startNode);
			LinkedList<Point> path = new LinkedList<Point>();
			while (queue.Count > 0)
			{
				Node current = queue.OrderBy(n => n.FullCosts).First();
				if (current.X == target.X && current.Y == target.Y)
				{
					while (current != startNode)
					{
						path.AddFirst(new Point(current.X, current.Y));
						current = current.ParentNode;
					}
					break;
				}
				queue.Remove(current);
				AdjacentNodes(duplicated, current, target, queue, position);
				current.Closed = true;
			}
			duplicated.Clear();
			openNodes.Clear();
			return path;
		}

		private void AdjacentNodes(LinkedList<Node> duplicatedMap, Node parent, UserPosition target, LinkedList<Node> queue, UserPosition start)
		{
			foreach (Point p in this.Room.RoomData.WalkDiagonal ? DiagonalMovement : NoDiagonalMovement)
			{
				if (duplicatedMap.Where(node => node.X == parent.X + p.X && node.Y == parent.Y + p.Y).Count() > 0)
				{
					Node n = duplicatedMap.Where(node => node.X == parent.X + p.X && node.Y == parent.Y + p.Y).First();
					if (n.Closed)
					{
						continue;
					}
					int newCostsToStart = parent.CostsToStartPoint + 1;
					int newCostsToTarget = newCostsToStart + Heuristic(new Point(n.X, n.Y), new Point(target.X, target.Y));
					if (queue.Contains(n) && newCostsToTarget >= n.FullCosts)
					{
						continue;
					}
					n.ParentNode = parent;
					n.CostsToStartPoint = newCostsToStart;
					n.FullCosts = newCostsToTarget;
					if (!queue.Contains(n))
					{
						queue.AddLast(n);
					}
				}
			}
		}

		public int Heuristic(Point now, Point target)
		{
			return Convert.ToInt32(Math.Sqrt(Math.Pow(target.X - now.X, 2) + Math.Pow(target.Y - now.Y, 2)));
		}

		public int Rotation(int X1, int Y1, int X2, int Y2)
		{
			int Rotation = 0;
			if (X1 > X2 && Y1 > Y2)
				Rotation = 7;
			else if (X1 < X2 && Y1 < Y2)
				Rotation = 3;
			else if (X1 > X2 && Y1 < Y2)
				Rotation = 5;
			else if (X1 < X2 && Y1 > Y2)
				Rotation = 1;
			else if (X1 > X2)
				Rotation = 6;
			else if (X1 < X2)
				Rotation = 2;
			else if (Y1 < Y2)
				Rotation = 4;
			else if (Y1 > Y2)
				Rotation = 0;
			return Rotation;
		}
	}
}
