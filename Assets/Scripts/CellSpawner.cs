using UnityEngine;
using System.Collections;
using System;

public class CellSpawner : MonoBehaviour {

	public Cell cellPrefab;
	public GameObject cellsHolder;

	public CellEngine engine;

	public int cellPerStep = 100;

	[HideInInspector] public Cell[,] cells;
	[HideInInspector] public int size;

	[HideInInspector] public Action<int> spawnProgress;
	[HideInInspector] public Action spawnFinish;

	[HideInInspector] public int[] sizes = new int[] { 10, 20, 30, 40, 50, 70, 100, 200 };

	private System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

	void Start () {
		spawnFinish += FinishSpawning;
	}

	public void StartSpawning (int i) {
		stopwatch.Reset ();
		stopwatch.Start ();
		size = sizes [i];
		StartCoroutine (SpawnCoroutine (size));
	}

	private void FinishSpawning () {
		stopwatch.Stop ();
		Debug.Log ("Spawning took: " + stopwatch.ElapsedMilliseconds / 1000f);
	}

	IEnumerator SpawnCoroutine (int x) {
		cells = new Cell[x, x];
		engine.cells = cells;
		int totalCells = x * x;
		int nextBreak = cellPerStep;
		int lastX = 0;
		int spawnedCells = 0;

		if (spawnProgress != null)
			spawnProgress (0);
		while (spawnedCells != totalCells) {
			for (int i = lastX; i < nextBreak; i++) {
				CreateCell (i % x, (i / x));
				spawnedCells++;
				if (spawnProgress != null)
					spawnProgress (Mathf.RoundToInt(spawnedCells / (float)totalCells * 100f));
			}
			Debug.Log ("Spawned: " + spawnedCells);
			lastX += cellPerStep;
			nextBreak += cellPerStep;
			if (nextBreak > totalCells)
				nextBreak = totalCells;
			yield return null;
		}

		SetNeighbours (x);

		if (spawnProgress != null)
			spawnProgress (100);
		if (spawnFinish != null)
			spawnFinish ();
	}

	private void CreateCell (int i, int j) {
		Cell c = Instantiate (cellPrefab, new Vector3 ((float)i, (float)j, 0f), Quaternion.identity) as Cell; 
		cells [i, j] = c;
		c.Init (cellsHolder.transform, i, j); 
		c.SetRandomState (); 
		engine.cellUpdates += c.CellUpdate;
		engine.cellApplyUpdates += c.CellApplyUpdate;
	}

	private void SetNeighbours (int x) {
		for (int i = 0; i < x; i++) 
			for (int j = 0; j < x; j++) 
				cells [i, j].neighbours = GetNeighbours (i, j);
	}

	private Cell[] GetNeighbours (int x, int y) {
		Cell[] result = new Cell[8];
		result[0] = cells[x, (y + 1) % size]; // top
		result[1] = cells[(x + 1) % size, (y + 1) % size]; // top right
		result[2] = cells[(x + 1) % size, y % size]; // right
		result[3] = cells[(x + 1) % size, (size + y - 1) % size]; // bottom right
		result[4] = cells[x % size, (size + y - 1) % size]; // bottom
		result[5] = cells[(size + x - 1) % size, (size + y - 1) % size]; // bottom left
		result[6] = cells[(size + x - 1) % size, y % size]; // left
		result[7] = cells[(size + x - 1) % size, (y + 1) % size]; // top left
		return result;
	}
}
