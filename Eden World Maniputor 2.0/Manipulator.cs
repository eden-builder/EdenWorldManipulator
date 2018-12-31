using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Eden_World_Maniputor_2._0
{
    public static partial class Manipulator
    {
        public static bool normal = false, normalterrain = false, smallpine = false, largepine = false;
        public static byte[] undo;
        public static int Density;

        public static void Manipulate(this World world, Func<Block, int, int, int, int, Block> manipulatorDelegate, String path)
        {
            Random rnd = new Random();

            Block block;
            int length = xYxY.Count;
            int x1, y1, newbaseX, newbaseY, newaddress, chunknewX, chunknewY, newZ, newHeight;
            undo = World.bytes.ToArray();

            if (manipulatorDelegate == Winterize)
            {
                foreach (int address in world.Chunks.Keys)
                {
                    int baseX = (world.Chunks[address].X - world.WorldArea.X) * 16, baseY = (world.Chunks[address].Y - world.WorldArea.Y) * 16;
                    for (int x = 0; x < 16; x++)
                    {
                        for (int y = 0; y < 16; y++)
                        {
                            for (int baseHeight = 3; baseHeight >= 0; baseHeight--)
                            {
                                for (int z = 15; z >= 0; z--)
                                {
                                    block = GetBlockAtPosition(x, y, z, address, baseX, baseY, baseHeight);
                                    block = manipulatorDelegate(block, address, baseX, baseY, baseHeight);
                                    World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z] = (byte)block.BlockType;
                                    World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z + 4096] = (byte)block.Painting;
                                    if (z == 0 && baseHeight == 0)
                                    {
                                        done = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (manipulatorDelegate == MoveWorld)
            {
                foreach (int address in world.Chunks.Keys)
                {
                    if (world.WorldArea.X + ((X1 + XMove) / 16) <= world.Chunks[address].X && world.WorldArea.X + ((X2 + XMove) / 16) >= world.Chunks[address].X)
                    {
                        int baseX = (world.Chunks[address].X - world.WorldArea.X) * 16, baseY = (world.Chunks[address].Y - world.WorldArea.Y) * 16;
                        for (int baseHeight = 0; baseHeight < 4; baseHeight++)
                        {
                            for (int x = 0; x < 16; x++)
                            {
                                for (int y = 0; y < 16; y++)
                                {
                                    if ((baseY + y) >= Y1 + YMove && (baseY + y) <= Y2 + YMove)
                                    {
                                        chunknewX = world.WorldArea.X + ((baseX + x) - XMove) / 16; chunknewY = world.WorldArea.Y + ((baseY + y) - YMove) / 16;
                                        newaddress = world.Chunks.First(c => c.Value == (new Point(chunknewX, chunknewY))).Key;
                                        newbaseX = (world.Chunks[newaddress].X - world.WorldArea.X) * 16; newbaseY = (world.Chunks[newaddress].Y - world.WorldArea.Y) * 16;
                                        x1 = ((baseX + x) - XMove) - newbaseX; y1 = ((baseY + y) - YMove) - newbaseY;
                                        for (int z = 15; z >= 0; z--)
                                        {
                                            World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z] = undo[newaddress + baseHeight * 8192 + x1 * 256 + y1 * 16 + z];
                                            World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z + 4096] = undo[newaddress + baseHeight * 8192 + x1 * 256 + y1 * 16 + z + 4096];
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
            else if (manipulatorDelegate == Rotate)
            {
                if (Form1.rotation == "Right 90" || Form1.rotation == "Left 90")
                {
                    foreach (int address in world.Chunks.Keys)
                    {
                        if (world.WorldArea.X + (Xr1 / 16) <= world.Chunks[address].X && world.WorldArea.X + (Xr2 / 16) >= world.Chunks[address].X)
                        {
                            int baseX = (world.Chunks[address].X - world.WorldArea.X) * 16, baseY = (world.Chunks[address].Y - world.WorldArea.Y) * 16;
                            for (int baseHeight = 0; baseHeight < 4; baseHeight++)
                            {
                                for (int x = 0; x < 16; x++)
                                {
                                    for (int y = 0; y < 16; y++)
                                    {
                                        if ((baseY + y) >= Yr1 && (baseY + y) <= Yr2 && Form1.rotation == "Right 90")
                                        {
                                            chunknewX = world.WorldArea.X + (X1 + (Yr2 - (y + baseY))) / 16; chunknewY = world.WorldArea.Y + (Y1 + (Xr2 - (x + baseX))) / 16;
                                            newaddress = world.Chunks.First(c => c.Value == (new Point(chunknewX, chunknewY))).Key;
                                            newbaseX = (world.Chunks[newaddress].X - world.WorldArea.X) * 16; newbaseY = (world.Chunks[newaddress].Y - world.WorldArea.Y) * 16;
                                            x1 = (X1 + (Yr2 - (y + baseY))) - newbaseX; y1 = (Y1 + (Xr2 - (x + baseX))) - newbaseY;
                                            for (int z = 15; z >= 0; z--)
                                            {
                                                World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z] = undo[newaddress + baseHeight * 8192 + x1 * 256 + y1 * 16 + z];
                                                World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z + 4096] = undo[newaddress + baseHeight * 8192 + x1 * 256 + y1 * 16 + z + 4096];
                                            }
                                        }
                                        else if ((baseY + y) >= Yr1 && (baseY + y) <= Yr2 && Form1.rotation == "Left 90")
                                        {
                                            chunknewX = world.WorldArea.X + (X1 + (Yr2 - (y + baseY))) / 16; chunknewY = world.WorldArea.Y + (Y1 + ((x + baseX) - Xr1)) / 16;
                                            newaddress = world.Chunks.First(c => c.Value == (new Point(chunknewX, chunknewY))).Key;
                                            newbaseX = (world.Chunks[newaddress].X - world.WorldArea.X) * 16; newbaseY = (world.Chunks[newaddress].Y - world.WorldArea.Y) * 16;
                                            x1 = (X1 + (Yr2 - (y + baseY))) - newbaseX; y1 = (Y1 + ((x + baseX) - Xr1)) - newbaseY;
                                            for (int z = 15; z >= 0; z--)
                                            {
                                                World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z] = undo[newaddress + baseHeight * 8192 + x1 * 256 + y1 * 16 + z];
                                                World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z + 4096] = undo[newaddress + baseHeight * 8192 + x1 * 256 + y1 * 16 + z + 4096];
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (int address in world.Chunks.Keys)
                    {
                        if (world.WorldArea.X + ((X1 + XMove) / 16) <= world.Chunks[address].X && world.WorldArea.X + ((X2 + XMove) / 16) >= world.Chunks[address].X)
                        {
                            int baseX = (world.Chunks[address].X - world.WorldArea.X) * 16, baseY = (world.Chunks[address].Y - world.WorldArea.Y) * 16;
                            for (int baseHeight = 0; baseHeight < 4; baseHeight++)
                            {
                                for (int x = 0; x < 16; x++)
                                {
                                    for (int y = 0; y < 16; y++)
                                    {
                                        if ((baseY + y) >= Y1 + YMove && (baseY + y) <= Y2 + YMove)
                                        {
                                            chunknewX = world.WorldArea.X + (X1 + (X2 - ((baseX + x) - XMove))) / 16; chunknewY = world.WorldArea.Y + (Y1 + (Y2 - ((baseY + y) - YMove))) / 16;
                                            newaddress = world.Chunks.First(c => c.Value == (new Point(chunknewX, chunknewY))).Key;
                                            newbaseX = (world.Chunks[newaddress].X - world.WorldArea.X) * 16; newbaseY = (world.Chunks[newaddress].Y - world.WorldArea.Y) * 16;
                                            x1 = (X1 + (X2 - ((baseX + x) - XMove))) - newbaseX; y1 = (Y1 + (Y2 - ((baseY + y) - YMove))) - newbaseY;
                                            for (int z = 15; z >= 0; z--)
                                            {
                                                World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z] = undo[newaddress + baseHeight * 8192 + x1 * 256 + y1 * 16 + z];
                                                World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z + 4096] = undo[newaddress + baseHeight * 8192 + x1 * 256 + y1 * 16 + z + 4096];
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
            else if (manipulatorDelegate == LowerWorld)
            {
                foreach (int address in world.Chunks.Keys)
                {
                    int baseX = (world.Chunks[address].X - world.WorldArea.X) * 16, baseY = (world.Chunks[address].Y - world.WorldArea.Y) * 16;
                    for (int x = 0; x < 16; x++)
                    {
                        for (int y = 0; y < 16; y++)
                        {
                            if ((baseX + x) >= Xmin && (baseX + x) <= Xmax && (baseY + y) >= Ymin && (baseY + y) <= Ymax)
                            {
                                if (Z1 != 0)
                                {
                                    for (int baseHeight = 0; baseHeight < 4; baseHeight++)
                                    {
                                        for (int z = 0; z < 16; z++)
                                        {
                                            if ((baseHeight * 16) + z < 64 - Z1)
                                            {
                                                newHeight = (((baseHeight * 16) + z) + Z1) / 16;
                                                newZ = (z + Z1) % 16;
                                                World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z] = World.bytes[address + newHeight * 8192 + x * 256 + y * 16 + newZ];
                                                World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z + 4096] = World.bytes[address + newHeight * 8192 + x * 256 + y * 16 + newZ + 4096];
                                            }
                                            else
                                            {
                                                World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z] = 0;
                                                World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z + 4096] = 0;
                                            }
                                        }
                                    }
                                }
                                else if (Z2 != 0)
                                {
                                    for (int baseHeight = 3; baseHeight >= 0; baseHeight--)
                                    {
                                        for (int z = 15; z >= 0; z--)
                                        {
                                            if ((baseHeight * 16) + z > Z2)
                                            {
                                                newHeight = (((baseHeight * 16) + z) - Z2) / 16;
                                                newZ = ((64 + z) - Z2) % 16;
                                                World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z] = World.bytes[address + newHeight * 8192 + x * 256 + y * 16 + newZ];
                                                World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z + 4096] = World.bytes[address + newHeight * 8192 + x * 256 + y * 16 + newZ + 4096];
                                            }
                                            else
                                            {
                                                if ((baseHeight * 16) + z == 0)
                                                {
                                                    World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z] = 1;
                                                    World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z + 4096] = 0;
                                                }
                                                else
                                                {
                                                    World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z] = 2;
                                                    World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z + 4096] = 0;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (manipulatorDelegate == RandomTrees)
            {
                List<Point> placed = new List<Point>();
                bool placing = true;
                foreach (int address in world.Chunks.Keys)
                {
                    int baseX = (world.Chunks[address].X - world.WorldArea.X) * 16, baseY = (world.Chunks[address].Y - world.WorldArea.Y) * 16;
                    for (int baseHeight = 0; baseHeight < 4; baseHeight++)
                    {
                        for (int x = 0; x < 16; x++)
                        {
                            for (int y = 0; y < 16; y++)
                            {
                                for (int z = 15; z >= 0; z--)
                                {
                                    for (int i = 0; i < length; i += 4)
                                    {
                                        if (World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z] == 8 && (baseX + x) > xYxY[i] && (baseX + x) < xYxY[i + 1] && (baseY + y) > xYxY[i + 2] && (baseY + y) < xYxY[i + 3])
                                        {
                                            newHeight = (((baseHeight * 16) + z) + 1) / 16;
                                            if (World.bytes[address + newHeight * 8192 + x * 256 + y * 16 + z + 1] == 0/* && x > 2 && x < 13 && y > 2 && y < 13*/)
                                            {
                                                for (int j = 0; j < placed.Count; j ++)
                                                {
                                                    int d = (int)Math.Sqrt(Math.Pow((baseX + x) - placed[j].X, 2) + Math.Pow((baseY + y) - placed[j].Y, 2));
                                                    if (d <= 5)
                                                    {
                                                        placing = false;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        placing = true;
                                                    }
                                                }
                                                if (smallpine && rnd.Next(25 * Density) == 0 && placing == true)
                                                {
                                                    Tree.PineTree(x, y, z, rnd, address, baseHeight, world, baseX, baseY);
                                                    placed.Add(new Point(baseX + x, baseY + y));
                                                }
                                                else if (largepine && rnd.Next(25 * Density) == 0 && placing == true)
                                                {
                                                    Tree.TallPineTree(x, y, z, rnd, address, baseHeight, world, baseX, baseY);
                                                    placed.Add(new Point(baseX + x, baseY + y));
                                                }
                                                else if(normalterrain && rnd.Next(25 * Density) == 0 && placing == true)
                                                {
                                                    Tree.NormalTerrainTree(x, y, z, rnd, address, baseHeight, world, baseX, baseY);
                                                    placed.Add(new Point(baseX + x, baseY + y));
                                                }
                                                else if(normal && rnd.Next(25 * Density) == 0 && placing == true)
                                                {
                                                    Tree.NormalTree(x, y, z, rnd, address, baseHeight, world, baseX, baseY);
                                                    placed.Add(new Point(baseX + x, baseY + y));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (int address in world.Chunks.Keys)
                {
                    int baseX = (world.Chunks[address].X - world.WorldArea.X) * 16, baseY = (world.Chunks[address].Y - world.WorldArea.Y) * 16;
                    for (int baseHeight = 0; baseHeight < 4; baseHeight++)
                    {
                        for (int x = 0; x < 16; x++)
                        {
                            for (int y = 0; y < 16; y++)
                            {
                                if ((baseX + x) >= Xmin && (baseX + x) <= Xmax && (baseY + y) >= Ymin && (baseY + y) <= Ymax)
                                {
                                    for (int z = 15; z >= 0; z--)
                                    {
                                        block = GetBlockAtPosition(x, y, z, address, baseX, baseY, baseHeight);
                                        block = manipulatorDelegate(block, address, baseX, baseY, baseHeight);
                                        World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z] = (byte)block.BlockType;
                                        World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z + 4096] = (byte)block.Painting;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        static Block GetBlockAtPosition(int x, int y, int z, int address, int baseX, int baseY, int baseHeight)
        {
            return new Block((BlockType)World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z], (Painting)World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z + 4096], baseX + x, baseY + y, baseHeight * 16 + z);
        }

        static Painting GetPaintingAtPosition(int x, int y, int z, int address, int baseHeight)
        {
            return (Painting)World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z + 4096];
        }

        static BlockType GetBlockTypeAtPosition(int x, int y, int z, int address, int baseHeight)
        {
            return (BlockType)World.bytes[address + baseHeight * 8192 + x * 256 + y * 16 + z];
        }
    }
}
