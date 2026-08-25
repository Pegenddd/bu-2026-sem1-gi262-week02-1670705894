using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Workshop.Student
{
    public class MapGenerator : MonoBehaviour
    {
        public int columns = 10;
        public int rows = 10;

        public GameObject[] floorTiles;
        public GameObject[] wallTiles;
        public GameObject[] foodTiles;

        public string[,] saveItemMap = new string[3, 3] {
            { " ", "Soda", " "},
            { " ", " ", " "},
            { " ", " ", "Food"},
        };

        // 1. declare Players variable
        public GameObject[] player;

        // 7. declare Exit variable 
        public GameObject[] Exit;

        public void Start()
        {

            // 1. random player at the position <0, 0> map
            for (int i = 0; i < player.Length; i++)
            {
                int r = UnityEngine.Random.Range(0, player.Length);
                GameObject playerObj = Instantiate(player[r], new Vector2(0, 0), Quaternion.identity);
                playerObj.name = "Player" + r;
            }
            // 2. create obstacles
            for (int i = 0; i < wallTiles.Length; i++)
            {
                int r = UnityEngine.Random.Range(0, wallTiles.Length);
                GameObject tile = Instantiate(wallTiles[0], new Vector2(5, i), Quaternion.identity);
                tile.name = "wall" + 5 + "_" + i;
            }

            // 3. create floor
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    int r = UnityEngine.Random.Range(0, floorTiles.Length);
                    GameObject tile = Instantiate(floorTiles[r], new Vector2(x, y), Quaternion.identity);
                    tile.name = "floor" + x + "_" + y;
                }
            }

            // 4. create walls
            for (int y = -1; y < rows + 1; y++)
            {
                for (int x = -1; x < columns + 1; x++)
                {
                    if (x == -1 || x == columns || y == -1 || y == rows)
                    {
                        int r = UnityEngine.Random.Range(0, wallTiles.Length);
                        GameObject tile = Instantiate(wallTiles[r], new Vector2(x, y), Quaternion.identity);
                        tile.name = "wall" + x + "_" + y;
                    }

                }
            }
            // 5. random foods
            int numberOffoods = UnityEngine.Random.Range(1, 6);
            for (int i = 0; i < numberOffoods; i++)
            {
                int x2 = UnityEngine.Random.Range(0, columns);
                int y2 = UnityEngine.Random.Range(0, rows);
                int r = UnityEngine.Random.Range(0, foodTiles.Length);
                Instantiate(foodTiles[r], new Vector2(x2, y2), Quaternion.identity);
            }

            // 6. generate item along with the saveItemMap
            for (int y = 0; y < saveItemMap.GetLength(0); y++)
            {
                for (int x = 0; x < saveItemMap.GetLength(1); x++)
                {
                    string item = saveItemMap[x, y];
                    if (!string.IsNullOrEmpty(item))
                    {
                        foreach (var foodtile in foodTiles)
                        {
                            if (foodtile.name == item)
                            {
                                GameObject food = Instantiate(foodtile, new Vector2(x, y), Quaternion.identity);
                                food.name = "Food" + x + "_" + y;
                                break;
                            }
                        }
                    }
                }
            }
            // 7. place exit
            for (int i = 0; i < Exit.Length; i++)
            {
                GameObject exitObj = Instantiate(Exit[0], new Vector2(columns - 1, rows - 1), Quaternion.identity);
                exitObj.name = "Exit" + i;
            }
        }
    }

}