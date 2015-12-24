using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

    public int columns = 8;
    public int rows = 8;

    public GameObject start;
    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject wallTile;
    public GameObject borderWallTile;

    public int[][] level;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    void BoardSetup() {
        boardHolder = new GameObject("Board").transform;

        level = new int[][] {
            new int[] { 1, 1, 1, 1, 1, 1, 3, 1 },
            new int[] { 1, 4, 0, 0, 0, 4, 0, 1 },
            new int[] { 1, 4, 0, 4, 0, 4, 0, 1 },
            new int[] { 1, 4, 0, 4, 0, 4, 0, 1 },
            new int[] { 1, 4, 0, 4, 0, 4, 0, 1 },
            new int[] { 1, 4, 0, 4, 0, 4, 0, 1 },
            new int[] { 1, 4, 0, 4, 0, 0, 0, 1 },
            new int[] { 1, 1, 2, 1, 1, 1, 1, 1 }
        };

        for (int i = 0; i < level.Length; i++) {
            int[] row = level[i];

            String lel = "";
            foreach (int item in row) {
                lel += item + ":";
            }
            Debug.Log(lel);

            for (int j = 0; j < row.Length; j++) {
                int tile = row[j];

                GameObject toInstantiate;

                switch (tile) {
                    case 0:
                        toInstantiate = floorTiles[0] as GameObject;
                        break;
                    case 1:
                        toInstantiate = borderWallTile as GameObject;
                        break;
                    case 2:
                        toInstantiate = start as GameObject;
                        break;
                    case 3:
                        toInstantiate = exit as GameObject;
                        break;
                    case 4:
                        toInstantiate = wallTile as GameObject;
                        break;
                    default:
                        toInstantiate = null;
                        break;
                }

                int x = j;
                int y = (level.Length - 1) - i;

                Debug.Log("X : " + x + " : Y :" + y);

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    public void SetupScene(int level) {
        BoardSetup();
    }
}
