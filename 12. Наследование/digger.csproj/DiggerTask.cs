using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digger
{
    public class Terrain : ICreature
    {
        public string GetImageFileName()
        {
            return "Terrain.png";
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }
    }

    public class Player : ICreature
    {
        public static int PositionX, PositionY = 0;

        public string GetImageFileName()
        {
            return "Digger.png";
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject.ToString() == "Digger.Gold")
            {
                Game.Scores = Game.Scores + 10;
            }
            if (conflictedObject.ToString() == "Digger.Sack" || conflictedObject.ToString() == "Digger.Monster")
            {
                return true;
            }
            return false;
        }

        public CreatureCommand Act(int x, int y)
        {
            PositionX = x;
            PositionY = y;
            int delX = 0;
            int delY = 0;

            if (Game.KeyPressed == System.Windows.Forms.Keys.Left)
            {
                delX = -1;
                delY = 0;
            }
            else if (Game.KeyPressed == System.Windows.Forms.Keys.Right)
            {
                delX = 1;
                delY = 0;
            }
            else if (Game.KeyPressed == System.Windows.Forms.Keys.Up)
            {
                delX = 0;
                delY = -1;
            }
            else if (Game.KeyPressed == System.Windows.Forms.Keys.Down)
            {
                delX = 0;
                delY = 1;
            }

            if (!((PositionX + delX >= 0 && PositionX + delX < Game.MapWidth) &&
              (PositionY + delY >= 0 && PositionY + delY < Game.MapHeight)))
            {
                return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
            }

            if (Game.Map[PositionX + delX, PositionY + delY] != null)
            {
                if (Game.Map[PositionX + delX, PositionY + delY].ToString() == "Digger.Sack")
                {
                    return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
                }
            }
            return new CreatureCommand() { DeltaX = delX, DeltaY = delY };
        }
    }

    public class Sack : ICreature
    {
        public int FallCount = 0;

        public string GetImageFileName()
        {
            return "Sack.png";
        }

        public int GetDrawingPriority()
        {
            return 3;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public CreatureCommand Fall()
        {
            return new CreatureCommand() { DeltaX = 0, DeltaY = 1 };
        }

        public CreatureCommand Lay()
        {
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }

        public CreatureCommand Act(int x, int y)
        {
            if (y < Game.MapHeight - 1)
            {
                if (Game.Map[x, y + 1] == null || (FallCount > 0 && (Game.Map[x, y + 1].ToString() == "Digger.Player" || Game.Map[x, y + 1].ToString() == "Digger.Monster")))
                {
                    FallCount++;
                    return Fall();
                }
            }

            if (FallCount > 1)
            {
                FallCount = 0;
                return new CreatureCommand() { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
            }

            FallCount = 0;

            return Lay();
        }
    }

    public class Gold : ICreature
    {
        public string GetImageFileName()
        {
            return "Gold.png";
        }

        public int GetDrawingPriority()
        {
            return 4;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject.ToString() == "Digger.Player" || conflictedObject.ToString() == "Digger.Monster";
        }

        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }
    }

    public class Monster : ICreature
    {
        public string GetImageFileName()
        {
            return "Monster.png";
        }

        public int GetDrawingPriority()
        {
            return 5;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject.ToString() == "Digger.Monster" || conflictedObject.ToString() == "Digger.Sack";
        }

        public CreatureCommand Act(int x, int y)
        {
            int positionX = x;
            int positionY = y;
            bool isDiggerAlive = false;
            int diggerX = 0;
            int diggerY = 0;
            int delX = 0;
            int delY = 0;

            for (int i = 0; i < Game.MapWidth; i++)
            {
                for (int j = 0; j < Game.MapHeight; j++)
                {
                    if (Game.Map[i, j] != null && Game.Map[i, j].ToString() == "Digger.Player")
                    {
                        isDiggerAlive = true;
                        diggerX = i;
                        diggerY = j;
                    }
                }
            }

            if (isDiggerAlive)
            {
                if (diggerX == positionX)
                {
                    if (diggerY < positionY) delY = -1;
                    else if (diggerY > positionY) delY = 1;
                }

                else if (diggerY == positionY)
                {
                    if (diggerX < positionX) delX = -1;
                    else if (diggerX > positionX) delX = 1;
                }
                else
                {
                    if (diggerX < positionX) delX = -1;
                    else if (diggerX > positionX) delX = 1;
                }
            }
            else return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };

            if (!((positionX + delX >= 0 && positionX + delX < Game.MapWidth) &&
              (positionY + delY >= 0 && positionY + delY < Game.MapHeight)))
            {
                return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
            }

            if (Game.Map[positionX + delX, positionY + delY] != null)
            {
                if (Game.Map[positionX + delX, positionY + delY].ToString() == "Digger.Sack" || Game.Map[positionX + delX, positionY + delY].ToString() == "Digger.Terrain" || Game.Map[positionX + delX, positionY + delY].ToString() == "Digger.Monster")
                {
                    return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
                }
            }
            return new CreatureCommand() { DeltaX = delX, DeltaY = delY };
        }
    }
}
