using System;
using System.Drawing;
using System.Linq;

namespace Eden_World_Maniputor_2._0
{
    class Tree
    {
        public static int newbaseHeight, newaddress, newBaseX, newBaseY, newi, newj;

        public static void PlaceLeaf(int i, int j, int k, int z, World world, int baseX, int baseY, int address, int baseHeight, int type, int color, int[] ct)
        {
            if (i < 0 || i > 15 || j < 0 || j > 15)
            {
                if (i < 0)
                {
                    newi = 16 + i;
                    newBaseX = world.WorldArea.X + (baseX - 16) / 16;
                }
                else if (i > 15)
                {
                    newi = i - 16;
                    newBaseX = world.WorldArea.X + (baseX + 16) / 16;
                }
                else if (i <= 15 || i >= 0)
                {
                    newi = i;
                    newBaseX = world.Chunks[address].X;
                }
                if (j < 0)
                {
                    newj = 16 + j;
                    newBaseY = world.WorldArea.Y + (baseY - 16) / 16;
                }
                else if (j > 15)
                {
                    newj = j - 16;
                    newBaseY = world.WorldArea.Y + (baseY + 16) / 16;
                }
                else if (j <= 15 || j >= 0)
                {
                    newj = j;
                    newBaseY = world.Chunks[address].Y;
                }
                newaddress = world.Chunks.First(c => c.Value == (new Point(newBaseX, newBaseY))).Key;
            }
            else
            {
                newi = i; newj = j;
                newaddress = address;
            }
            newbaseHeight = (((baseHeight * 16) + z) + (k - z)) / 16;
            World.bytes[newaddress + newbaseHeight * 8192 + newi * 256 + newj * 16 + k] = (byte)type;
            World.bytes[newaddress + newbaseHeight * 8192 + newi * 256 + newj * 16 + k + 4096] = (byte)ct[color];
        }

        public static void PineTree(int x, int y, int z,Random rnd, int address, int baseHeight, World world, int baseX, int baseY)
        {
            for (int i = 0; i < 2; i++)
            {
                newbaseHeight = (((baseHeight * 16) + z) + i) / 16;
                World.bytes[address + newbaseHeight * 8192 + x * 256 + y * 16 + z + i] = 6;
                World.bytes[address + newbaseHeight * 8192 + x * 256 + y * 16 + z + i + 4096] = 47;
            }

            int[] ct2 = { 0, 4, 13, 31, 40, 49, 50 };
            int[] ct = { 0, 4, 13, 31, 40, 49, 50 };
            int color = rnd.Next(7);
            int type = 5;

            for (int k = z + 2; k < z + 10; k++)
            {
                for (int i = x - 2; i <= x + 2; i++)
                {
                    for (int j = y - 2; j <= y + 2; j++)
                    {
                        if (k == z + 2 || k == z + 4)
                        {
                            if (((i == x - 2 || i == x + 2) && j == y) || ((j == y - 2 || j == y + 2) && i == x))
                            {
                                PlaceLeaf(i, j, k, z, world, baseX, baseY, address, baseHeight, type, color, ct);
                            }
                            else if (i != x - 2 && i != x + 2 && j != y - 2 && j != y + 2)
                            {
                                PlaceLeaf(i, j, k, z, world, baseX, baseY, address, baseHeight, type, color, ct);
                            }
                        }
                        else if (k == z + 3 || k == z + 5 || k == z + 7)
                        {
                            if (((i == x - 1 || i == x + 1) && j == y) || ((j == y - 1 || j == y + 1) && i == x))
                            {
                                PlaceLeaf(i, j, k, z, world, baseX, baseY, address, baseHeight, type, color, ct);
                            }
                            else if (i != x - 2 && i != x + 2 && j != y - 2 && j != y + 2 && i != x - 1 && i != x + 1 && j != y - 1 && j != y + 1)
                            {
                                PlaceLeaf(i, j, k, z, world, baseX, baseY, address, baseHeight, type, color, ct);
                            }
                        }
                        else if (k == z + 6 || k == z + 8 || k == z + 9)
                        {
                            if (j == y && i == x)
                            {
                                newbaseHeight = (((baseHeight * 16) + z) + (k - z)) / 16;
                                World.bytes[address + newbaseHeight * 8192 + i * 256 + j * 16 + k] = (byte)type;
                                World.bytes[address + newbaseHeight * 8192 + i * 256 + j * 16 + k + 4096] = (byte)ct[color];
                            }
                        }
                    }
                }
            }
        }

        public static void TallPineTree(int x, int y, int z, Random rnd, int address, int baseHeight, World world, int baseX, int baseY)
        {
            for (int i = 0; i < 2; i++)
            {
                newbaseHeight = (((baseHeight * 16) + z) + i) / 16;
                World.bytes[address + newbaseHeight * 8192 + x * 256 + y * 16 + z + i] = 6;
                World.bytes[address + newbaseHeight * 8192 + x * 256 + y * 16 + z + i + 4096] = 47;
            }

            int[] ct2 = { 0, 4, 13, 31, 40, 49, 50 };
            int[] ct = { 0, 4, 13, 31, 40, 49, 50 };
            int color = rnd.Next(7);
            int type = 5;

            for (int k = z + 2; k < z + 13; k++)
            {
                for (int i = x - 3; i <= x + 3; i++)
                {
                    for (int j = y - 3; j <= y + 3; j++)
                    {
                        if (k == z + 2 || k == z + 4)
                        {
                            if (((i == x - 3 || i == x + 3) && j == y) || ((j == y - 3 || j == y + 3) && i == x))
                            {
                                PlaceLeaf(i, j, k, z, world, baseX, baseY, address, baseHeight, type, color, ct);
                            }
                            else if (i != x - 3 && i != x + 3 && j != y - 3 && j != y + 3)
                            {
                                PlaceLeaf(i, j, k, z, world, baseX, baseY, address, baseHeight, type, color, ct);
                            }
                            if ((i == x - 2 && j == y - 2) || (i == x - 2 && j == y + 2) || (i == x + 2 && j == y - 2) || (i == x + 2 && j == y + 2))
                            {
                                PlaceLeaf(i, j, k, z, world, baseX, baseY, address, baseHeight, 0, 0, ct);
                            }
                        }
                        else if (k == z + 3 || k == z + 5 || k == z + 7)
                        {
                            if (((i == x - 2 || i == x + 2) && j == y) || ((j == y - 2 || j == y + 2) && i == x))
                            {
                                PlaceLeaf(i, j, k, z, world, baseX, baseY, address, baseHeight, type, color, ct);
                            }
                            else if (i != x - 2 && i != x + 2 && j != y - 2 && j != y + 2 && i != x - 3 && i != x + 3 && j != y - 3 && j != y + 3)
                            {
                                PlaceLeaf(i, j, k, z, world, baseX, baseY, address, baseHeight, type, color, ct);
                            }
                        }
                        else if (k == z + 6 || k == z + 8 || k == z + 10)
                        {
                            if (((i == x - 1 || i == x + 1) && j == y) || ((j == y - 1 || j == y + 1) && i == x))
                            {
                                PlaceLeaf(i, j, k, z, world, baseX, baseY, address, baseHeight, type, color, ct);
                            }
                            else if (i != x - 3 && i != x + 3 && j != y - 3 && j != y + 3 && i != x - 2 && i != x + 2 && j != y - 2 && j != y + 2 && i != x - 1 && i != x + 1 && j != y - 1 && j != y + 1)
                            {
                                PlaceLeaf(i, j, k, z, world, baseX, baseY, address, baseHeight, type, color, ct);
                            }
                        }
                        else if (k == z + 9 || k == z + 11 || k == z + 12)
                        {
                            if (j == y && i == x)
                            {
                                newbaseHeight = (((baseHeight * 16) + z) + (k - z)) / 16;
                                World.bytes[address + newbaseHeight * 8192 + i * 256 + j * 16 + k] = (byte)type;
                                World.bytes[address + newbaseHeight * 8192 + i * 256 + j * 16 + k + 4096] = (byte)ct[color];
                            }
                        }
                    }
                }
            }
        }

        public static void NormalTerrainTree(int x, int y, int z, Random rnd, int address, int baseHeight, World world, int baseX, int baseY)
        {
            int tree_height = rnd.Next(6) + 6;

            for (int i = 0; i < 3 * tree_height / 4; i++)
            {
                newbaseHeight = (((baseHeight * 16) + z) + i) / 16;
                World.bytes[address + newbaseHeight * 8192 + x * 256 + y * 16 + z + i] = 6;
                World.bytes[address + newbaseHeight * 8192 + x * 256 + y * 16 + z + i + 4096] = 0;
            }

            int[] ct2 = { 0, 19, 20, 21, 31, 40, 40 };
            int[] ct = { 0, 19, 20, 21, 31, 40, 40 };
            int color = rnd.Next(7);
            int type = 5;

            for (int i = x - 2; i <= x + 2; i++)
            {
                for (int j = y - 2; j <= y + 2; j++)
                {
                    for (int k = z + 2 * tree_height / 3; k < tree_height + z; k++)
                    {
                        if (i == x - 2 || i == x + 2 || j == y - 2 || j == y + 2)
                        {
                            if ((i == x - 2 || i == x + 2) && (j == y - 2 || j == y + 2) && (k == z + 2 * tree_height / 3 || k == z + tree_height - 1))
                            {
                            }
                            else
                                if (rnd.Next(2) == 0)
                            {
                                PlaceLeaf(i, j, k, z, world, baseX, baseY, address, baseHeight, type, color, ct);
                            }
                        }
                        else
                        {
                            PlaceLeaf(i, j, k, z, world, baseX, baseY, address, baseHeight, type, color, ct);
                        }
                    }
                }
            }
        }

        public static void NormalTree(int x, int y, int z, Random rnd, int address, int baseHeight, World world, int baseX, int baseY)
        {
            int tree_height = rnd.Next(6) + 3;

            for (int i = 0; i < tree_height; i++)
            {
                newbaseHeight = (((baseHeight * 16) + z) + i) / 16;
                World.bytes[address + newbaseHeight * 8192 + x * 256 + y * 16 + z + i] = 6;
                World.bytes[address + newbaseHeight * 8192 + x * 256 + y * 16 + z + i + 4096] = 0;
            }

            int[] ct2 = { 0, 19, 20, 21, 31, 40, 40 };
            int[] ct = { 0, 19, 20, 21, 31, 40, 40 };
            int color = rnd.Next(7);
            int type = 5;

            for (int k = z + tree_height; k < z + tree_height + 4; k++)
            {
                for (int i = x - 2; i <= x + 2; i++)
                {
                    for (int j = y - 2; j <= y + 2; j++)
                    {
                        if (k == z + tree_height || k == z + tree_height + 2)
                        {
                            if (((i == x - 1 || i == x + 1) && j == y) || ((j == y - 1 || j == y + 1) && i == x))
                            {
                                PlaceLeaf(i, j, k, z, world, baseX, baseY, address, baseHeight, type, color, ct);
                            }
                            else if (i != x - 2 && i != x + 2 && j != y - 2 && j != y + 2 && i != x - 1 && i != x + 1 && j != y - 1 && j != y + 1)
                            {
                                PlaceLeaf(i, j, k, z, world, baseX, baseY, address, baseHeight, type, color, ct);
                            }
                        }
                        else if (k == z + tree_height + 1)
                        {
                            if (((i == x - 2 || i == x + 2) && j == y) || ((j == y - 2 || j == y + 2) && i == x))
                            {
                                PlaceLeaf(i, j, k, z, world, baseX, baseY, address, baseHeight, type, color, ct);
                            }
                            else if (i != x - 2 && i != x + 2 && j != y - 2 && j != y + 2)
                            {
                                PlaceLeaf(i, j, k, z, world, baseX, baseY, address, baseHeight, type, color, ct);
                            }
                        }
                        else if (k == z + tree_height + 3)
                        {
                            if (j == y && i == x)
                            {
                                PlaceLeaf(i, j, k, z, world, baseX, baseY, address, baseHeight, type, color, ct);
                            }
                        }
                    }
                }
            }
        }

        public static void RealTree(int x, int y, int z, byte?[,,,] newMap, Random rnd)
        {
            int tree_height = rnd.Next(6) + 3;

            for (int i = 0; i < tree_height; i++)
            {
                newMap[x, y, z + i, 0] = 6;
                newMap[x, y, z + i, 1] = 0;
            }

            int[] ct2 = { 0, 19, 20, 21, 31, 40, 40 };
            int[] ct = { 0, 19, 20, 21, 31, 40, 40 };
            int color = rnd.Next(7);
            int type = 5;

            for (int k = z + tree_height; k < z + tree_height + 4; k++)
            {
                for (int i = x - 2; i <= x + 2; i++)
                {
                    for (int j = y - 2; j <= y + 2; j++)
                    {
                        if (k == z + tree_height || k == z + tree_height + 2)
                        {
                            if (((i == x - 1 || i == x + 1) && j == y) || ((j == y - 1 || j == y + 1) && i == x))
                            {
                                newMap[i, j, k, 0] = (byte)type;
                                newMap[i, j, k, 1] = (byte)ct[color];
                            }
                            else if (i != x - 2 && i != x + 2 && j != y - 2 && j != y + 2 && i != x - 1 && i != x + 1 && j != y - 1 && j != y + 1)
                            {
                                newMap[i, j, k, 0] = (byte)type;
                                newMap[i, j, k, 1] = (byte)ct[color];
                            }
                        }
                        else if (k == z + tree_height + 1)
                        {
                            if (((i == x - 2 || i == x + 2) && j == y) || ((j == y - 2 || j == y + 2) && i == x))
                            {
                                newMap[i, j, k, 0] = (byte)type;
                                newMap[i, j, k, 1] = (byte)ct[color];
                            }
                            else if (i != x - 2 && i != x + 2 && j != y - 2 && j != y + 2)
                            {
                                newMap[i, j, k, 0] = (byte)type;
                                newMap[i, j, k, 1] = (byte)ct[color];
                            }
                        }
                        else if (k == z + tree_height + 3)
                        {
                            if (j == y && i == x)
                            {
                                newMap[i, j, k, 0] = (byte)type;
                                newMap[i, j, k, 1] = (byte)ct[color];
                            }
                        }
                    }
                }
            }
        }
    }
}
