using UnityEngine;
using System.Collections;
using System;

public class CellEngine : MonoBehaviour {

	public enum States {
		Idle, Running
	}

	public CellSpawner spawner;

	public float updateInterval = 0.1f; 

	[HideInInspector] public float[] intervals = new float[] {1f, 0.5f, 0.2f, 0.1f, 0.05f, 0.01f};
	[HideInInspector] public Cell[,] cells;
	[HideInInspector] public States state = States.Idle;
	[HideInInspector] public int generation = 0;
	[HideInInspector] public Action<int> updateFinish;

	[HideInInspector] public Action cellUpdates = null; 
	[HideInInspector] public Action cellApplyUpdates = null; 

	private IEnumerator coroutine = null;

	private System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
		
	public void UpdateCells () {
		stopwatch.Reset ();
		stopwatch.Start ();

		cellUpdates ();
		cellApplyUpdates ();

		stopwatch.Stop ();
		Debug.Log ("Update time: " + (stopwatch.ElapsedMilliseconds / 1000f).ToString ());
	}

	public void SetInterval (int i) {
		updateInterval = intervals [i];
	}
		
	public void Run () {
		state = States.Running;
		coroutine = RunCoroutine ();
		StartCoroutine (coroutine);
	}

	public void Stop () {
		state = States.Idle;
		if (coroutine != null)
			StopCoroutine (coroutine);
	}

	public void Reset () {
		generation = 0;
		Stop ();
		for (int i = 0; i < spawner.size; i++)
			for (int j = 0; j < spawner.size; j++)
				cells [i, j].SetRandomState ();
	}

	public void ToggleState () {
		if (state == States.Idle)
			Run ();
		else
			Stop ();
	}

	private IEnumerator RunCoroutine () {
		while (state == States.Running) { 
			UpdateCells (); 
			generation++;
			if (updateFinish != null)
				updateFinish (generation);
			yield return new WaitForSeconds (updateInterval); 
		}
	}
}
