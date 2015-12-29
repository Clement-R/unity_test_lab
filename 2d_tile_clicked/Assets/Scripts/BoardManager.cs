using UnityEngine;
using System;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

    public int columns = 8;
    public int rows = 8;

    public GameObject start;
    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject wallTile;
    public GameObject borderWallTile;

    public GameObject[] enemiesPrefabs;

    public int[][] level;

    private Transform boardHolder;

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

            // String lel = "";
            // foreach (int item in row) {
            //     lel += item + ":";
            // }
            // Debug.Log(lel);

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

                // Debug.Log("X : " + x + " : Y :" + y);

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    public void SetupScene(int level) {
        BoardSetup();
        start = GameObject.FindGameObjectWithTag("Start");
        start.name = "Start";
        exit = GameObject.FindGameObjectWithTag("Exit");
        exit.name = "Exit";
        AddEnemy(start.transform.position);
    }

    public void AddEnemy(Vector3 pos) {
        GameObject toInstantiate = enemiesPrefabs[0] as GameObject;
        GameObject instance = Instantiate(toInstantiate, pos, Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
    }

    public int[][] GetLevel() {

        int[][] reversedLevel = new int[level.Length][];

        for (int i = 0; i < level.Length; i++) {
            int[] row = level[(level.Length - 1) - i];
            reversedLevel[i] = row;
        }

        /*
        for (int i = 0; i < reversedLevel.Length; i++) {
            int[] row = reversedLevel[i];
            String lel = "";
            foreach (int item in row) {
                lel += item + ":";
            }
            Debug.Log(lel);
        }
        */

        return reversedLevel;
    }

	public int GetColumns() {
		return columns;
	}

	public int GetRows() {
		return rows;
	}
}
