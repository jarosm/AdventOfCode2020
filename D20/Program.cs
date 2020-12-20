using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace D20
{
    class Program
    {
        const int TileSize = 10;
        const int monsterHeight = 3;
        const int monsterWidth = 20;

        static (int row, int col)[] monster = { (1, 0), (2, 1), (2, 4), (1, 5), (1, 6), (2, 7), (2, 10), (1, 11), (1, 12), (2, 13), (2, 16), (1, 17), (1, 18), (1, 19), (0, 18) };


        static List<Tile> tiles = new List<Tile>();


        public enum TileSide
        {
            None,
            Top,
            Bottom,
            Left,
            Right
        }


        public class Tile
        {
            public int Id;
            public int IdTop;
            public int IdBottom;
            public int IdLeft;
            public int IdRight;
            public char[,] ImageTile;

            public Tile()
            {
                Id = -1;
                IdTop = -1;
                IdBottom = -1;
                IdLeft = -1;
                IdRight = -1;
                ImageTile = new char[TileSize, TileSize];
            }
        }


        private static char[] GetSideVals(char[,] arr, TileSide side)
        {
            char[] sideVals = new char[TileSize];

            if (side == TileSide.Bottom)
            {
                for (int i = 0; i < TileSize; i++)
                    sideVals[i] = arr[TileSize - 1, i];
            }
            if (side == TileSide.Top)
            {
                for (int i = 0; i < TileSize; i++)
                    sideVals[i] = arr[0, i];
            }
            if (side == TileSide.Left)
            {
                for (int i = 0; i < TileSize; i++)
                    sideVals[i] = arr[i, 0];
            }
            if (side == TileSide.Right)
            {
                for (int i = 0; i < TileSize; i++)
                    sideVals[i] = arr[i, TileSize - 1];
            }

            return sideVals;
        }


        private static (int index, TileSide side) FindTile(int tileIndex, TileSide side)
        {
            (int index, TileSide side) result = (-1, TileSide.None);

            char[] sideVals = GetSideVals(tiles[tileIndex].ImageTile, side);

            for (int i = tileIndex + 1; i < tiles.Count; i++)
            {
                char[] sideValsChecked = GetSideVals(tiles[i].ImageTile, TileSide.Bottom);
                if (sideValsChecked.SequenceEqual(sideVals) || sideValsChecked.Reverse().SequenceEqual(sideVals))
                {
                    result = (i, TileSide.Bottom);
                    break;
                }

                sideValsChecked = GetSideVals(tiles[i].ImageTile, TileSide.Top);
                if (sideValsChecked.SequenceEqual(sideVals) || sideValsChecked.Reverse().SequenceEqual(sideVals))
                {
                    result = (i, TileSide.Top);
                    break;
                }

                sideValsChecked = GetSideVals(tiles[i].ImageTile, TileSide.Left);
                if (sideValsChecked.SequenceEqual(sideVals) || sideValsChecked.Reverse().SequenceEqual(sideVals))
                {
                    result = (i, TileSide.Left);
                    break;
                }

                sideValsChecked = GetSideVals(tiles[i].ImageTile, TileSide.Right);
                if (sideValsChecked.SequenceEqual(sideVals) || sideValsChecked.Reverse().SequenceEqual(sideVals))
                {
                    result = (i, TileSide.Right);
                    break;
                }
            }

            return result;
        }


        private static void SetId(int index, int value, TileSide side)
        {
            switch (side)
            {
                case TileSide.Bottom:
                    tiles[index].IdBottom = value;
                    break;
                case TileSide.Top:
                    tiles[index].IdTop = value;
                    break;
                case TileSide.Left:
                    tiles[index].IdLeft = value;
                    break;
                case TileSide.Right:
                    tiles[index].IdRight = value;
                    break;
            }
        }


        private static char[,] FlipHorizontaly(char[,] arr, int size)
        {
            char[,] newarr = new char[size, size];
            for (int r = 0; r < size; r++)
                for (int c = 0; c < size; c++)
                    newarr[size - r - 1, c] = arr[r, c];
            return newarr;
        }
        private static char[,] FlipVerticaly(char[,] arr, int size)
        {
            char[,] newarr = new char[size, size];
            for (int r = 0; r < size; r++)
                for (int c = 0; c < size; c++)
                    newarr[r, size - c - 1] = arr[r, c];
            return newarr;
        }
        private static char[,] RotateRight(char[,] arr, int size)
        {
            char[,] newarr = new char[size, size];
            for (int r = 0; r < size; r++)
                for (int c = 0; c < size; c++)
                    newarr[c, size - r - 1] = arr[r, c];
            return newarr;
        }
        private static char[,] RotateLeft(char[,] arr, int size)
        {
            char[,] newarr = new char[size, size];
            for (int r = 0; r < size; r++)
                for (int c = 0; c < size; c++)
                    newarr[size - c - 1, r] = arr[r, c];
            return newarr;
        }


        private static Tile OrientTile(Tile orientWhat, Tile orientTo, TileSide side)
        {
            Tile orientedTile = new Tile();

            if (side == TileSide.Left)
            {
                if (orientWhat.IdLeft == orientTo.Id)
                {
                    orientedTile = orientWhat;
                }
                else if (orientWhat.IdTop == orientTo.Id)
                {
                    orientedTile.Id = orientWhat.Id;
                    orientedTile.IdLeft = orientWhat.IdTop;
                    orientedTile.IdTop = orientWhat.IdRight;
                    orientedTile.IdRight = orientWhat.IdBottom;
                    orientedTile.IdBottom = orientWhat.IdLeft;
                    orientedTile.ImageTile = RotateLeft(orientWhat.ImageTile, TileSize);
                }
                else if (orientWhat.IdRight == orientTo.Id)
                {
                    orientedTile.Id = orientWhat.Id;
                    orientedTile.IdLeft = orientWhat.IdRight;
                    orientedTile.IdTop = orientWhat.IdTop;
                    orientedTile.IdRight = orientWhat.IdLeft;
                    orientedTile.IdBottom = orientWhat.IdBottom;
                    orientedTile.ImageTile = FlipVerticaly(orientWhat.ImageTile, TileSize);
                }
                else if (orientWhat.IdBottom == orientTo.Id)
                {
                    orientedTile.Id = orientWhat.Id;
                    orientedTile.IdLeft = orientWhat.IdBottom;
                    orientedTile.IdTop = orientWhat.IdLeft;
                    orientedTile.IdRight = orientWhat.IdTop;
                    orientedTile.IdBottom = orientWhat.IdRight;
                    orientedTile.ImageTile = RotateRight(orientWhat.ImageTile, TileSize);
                }

                char[] sideValsWhat = GetSideVals(orientedTile.ImageTile, TileSide.Left);
                char[] sideValsTo = GetSideVals(orientTo.ImageTile, TileSide.Right);
                if (sideValsWhat.Reverse().SequenceEqual(sideValsTo))
                {
                    orientedTile.ImageTile = FlipHorizontaly(orientedTile.ImageTile, TileSize);
                    int temp = orientedTile.IdTop;
                    orientedTile.IdTop = orientedTile.IdBottom;
                    orientedTile.IdBottom = temp;
                }
            }

            if (side == TileSide.Top)
            {
                if (orientWhat.IdTop == orientTo.Id)
                {
                    orientedTile = orientWhat;
                }
                else if (orientWhat.IdRight == orientTo.Id)
                {
                    orientedTile.Id = orientWhat.Id;
                    orientedTile.IdLeft = orientWhat.IdTop;
                    orientedTile.IdTop = orientWhat.IdRight;
                    orientedTile.IdRight = orientWhat.IdBottom;
                    orientedTile.IdBottom = orientWhat.IdLeft;
                    orientedTile.ImageTile = RotateLeft(orientWhat.ImageTile, TileSize);
                }
                else if (orientWhat.IdBottom == orientTo.Id)
                {
                    orientedTile.Id = orientWhat.Id;
                    orientedTile.IdLeft = orientWhat.IdLeft;
                    orientedTile.IdTop = orientWhat.IdBottom;
                    orientedTile.IdRight = orientWhat.IdRight;
                    orientedTile.IdBottom = orientWhat.IdTop;
                    orientedTile.ImageTile = FlipHorizontaly(orientWhat.ImageTile, TileSize);
                }
                else if (orientWhat.IdLeft == orientTo.Id)
                {
                    orientedTile.Id = orientWhat.Id;
                    orientedTile.IdLeft = orientWhat.IdBottom;
                    orientedTile.IdTop = orientWhat.IdLeft;
                    orientedTile.IdRight = orientWhat.IdTop;
                    orientedTile.IdBottom = orientWhat.IdRight;
                    orientedTile.ImageTile = RotateRight(orientWhat.ImageTile, TileSize);
                }

                char[] sideValsWhat = GetSideVals(orientedTile.ImageTile, TileSide.Top);
                char[] sideValsTo = GetSideVals(orientTo.ImageTile, TileSide.Bottom);
                if (sideValsWhat.Reverse().SequenceEqual(sideValsTo))
                {
                    orientedTile.ImageTile = FlipVerticaly(orientedTile.ImageTile, TileSize);
                    int temp = orientedTile.IdLeft;
                    orientedTile.IdLeft = orientedTile.IdRight;
                    orientedTile.IdRight = temp;
                }
            }

            return orientedTile;
        }


        private static List<int> GetCorners()
        {
            List<int> corners = new List<int>();

            for (int index = 0;  index < tiles.Count; index++)
            {
                int counter = 0;

                (int index, TileSide side) neighbour;
                if (tiles[index].IdTop == -1)
                {
                    neighbour = FindTile(index, TileSide.Top);
                    if (neighbour.index > -1)
                    {
                        SetId(neighbour.index, tiles[index].Id, neighbour.side);
                        tiles[index].IdTop = tiles[neighbour.index].Id;
                        counter++;
                    }
                }
                else
                    counter++;

                if (tiles[index].IdBottom == -1)
                {
                    neighbour = FindTile(index, TileSide.Bottom);
                    if (neighbour.index > -1)
                    {
                        SetId(neighbour.index, tiles[index].Id, neighbour.side);
                        tiles[index].IdBottom = tiles[neighbour.index].Id;
                        counter++;
                    }
                }
                else
                    counter++;

                if (tiles[index].IdLeft == -1)
                {
                    neighbour = FindTile(index, TileSide.Left);
                    if (neighbour.index > -1)
                    {
                        SetId(neighbour.index, tiles[index].Id, neighbour.side);
                        tiles[index].IdLeft = tiles[neighbour.index].Id;
                        counter++;
                    }
                }
                else
                    counter++;

                if (tiles[index].IdRight == -1)
                {
                    neighbour = FindTile(index, TileSide.Right);
                    if (neighbour.index > -1)
                    {
                        SetId(neighbour.index, tiles[index].Id, neighbour.side);
                        tiles[index].IdRight = tiles[neighbour.index].Id;
                        counter++;
                    }
                }
                else
                    counter++;


                if (counter == 2)
                    corners.Add(tiles[index].Id);
            }

            return corners;
        }


        private static void PrintTile(Tile tile)
        {
            Console.WriteLine("Id: " + tile.Id + " ; Top: " + tile.IdTop + " ; Left: " + tile.IdLeft + " ; Bottom: " + tile.IdBottom + " ; Right: " + tile.IdRight);
            for (int r = 0; r < TileSize; r++)
            {
                for (int c = 0; c < TileSize; c++)
                {
                    Console.Write(tile.ImageTile[r, c]);
                }
                Console.WriteLine();
            }
        }


        private static (int imageSize, char[,] arr) CreateImage()
        {
            int imageSize = (TileSize - 2) * (int)Math.Sqrt(tiles.Count);
            (int imageSize, char[,] image) result = (imageSize, new char[imageSize, imageSize]);

            Tile firstTile = tiles.Find(t => t.IdTop == -1 && t.IdLeft == -1);
            Tile currentTile = firstTile;

            int row = 0, col = 0;
            while (true)
            {
                while (true)
                {
                    for (int r = 1; r < TileSize - 1; r++)
                    {
                        for (int c = 1; c < TileSize - 1; c++)
                        {
                            result.image[(TileSize - 2) * row + r - 1, (TileSize - 2) * col + c - 1] = currentTile.ImageTile[r, c];
                        }
                    }

                    if (currentTile.IdRight != -1)
                    {
                        currentTile = OrientTile(tiles.Find(t => t.Id == currentTile.IdRight), currentTile, TileSide.Left);
                        col++;
                    }
                    else
                        break;
                }

                if (firstTile.IdBottom != -1)
                {
                    firstTile = OrientTile(tiles.Find(t => t.Id == firstTile.IdBottom), firstTile, TileSide.Top);
                    currentTile = firstTile;
                    col = 0;
                    row++;
                }
                else
                    break;
            }

            return result;
        }


        private static int FindMonsters(int imageSize, char[,] image)
        {
            int count = 0;
            for (int r = 0; r <= imageSize - monsterHeight; r++)
            {
                for (int c = 0; c <= imageSize - monsterWidth; c++)
                {
                    if (monster.All(a => image[r + a.row, c + a.col] == '#'))
                        count++;
                }
            }
            return count;
        }


        private static int GetWaterRoughness(int imageSize, char[,] image)
        {
            int sum = 0, monsterCount = 0, counter = 0;

            while (counter < 8)
            {
                monsterCount = FindMonsters(imageSize, image);
                if (monsterCount > 0)
                    break;

                image = RotateRight(image, imageSize);
                counter++;
                if (counter == 4)
                    image = FlipVerticaly(image, imageSize);
            }

            for (int r = 0; r < imageSize; r++)
            {
                for (int c = 0; c < imageSize; c++)
                {
                    if (image[r, c] == '#')
                        sum++;
                }
            }
            sum -= monsterCount * monster.Length;

            return sum;
        }


        static private void D20()
        {
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D20\\input.txt"))
            {
                string line = "";
                Tile tile = new Tile();
                int row = 0;
                while ((line = input.ReadLine()) != null)
                {
                    if (line.Length == 0)
                    {
                        tiles.Add(tile);
                        tile = new Tile();
                        row = 0;
                        continue;
                    }

                    if (line.StartsWith("Tile"))
                        tile.Id = Convert.ToInt32(line.Replace("Tile ", "").Replace(":", ""));
                    else
                    {
                        for (int i = 0; i < line.Length; i++)
                            tile.ImageTile[row, i] = line[i];
                        row++;
                    }
                }
                tiles.Add(tile);
            }


            List<int> corners = GetCorners();
            ulong mult = (ulong)corners[0] * (ulong)corners[1] * (ulong)corners[2] * (ulong)corners[3];
            Console.WriteLine("Part 1: " + mult);


            (int imageSize, char[,] image) image = CreateImage();
            Console.WriteLine("Part 2: " + GetWaterRoughness(image.imageSize, image.image));

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            D20();
        }
    }
}
