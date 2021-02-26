using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace FPS 
{
	public class TestScript : MonoBehaviour 
	{
		public GameObject cubesContainer;
		public Text countText;
		public int cubesPerClick = 20;

		private int cubesCounter = 0;

		void Awake () 
		{
			AddCubes ();
		}

		public void AddCubes () 
		{
			for (int i = 0; i < cubesPerClick; i++) 
			{
				GameObject go = GameObject.CreatePrimitive (PrimitiveType.Cube);
				go.transform.position = new Vector3 (Random.Range (-5f, 5f), Random.Range (-5f, 5f), 0f);
				Rotator rotator = go.AddComponent <Rotator> ();
				rotator.SetColorAndSpeed (GetRandomColor (), Random.Range (30f, 120f));
				go.transform.parent = cubesContainer.transform;
			}
			cubesCounter += cubesPerClick;
			UpdateText ();
		}

		private Color GetRandomColor () 
		{
			float r = Random.Range (0f, 1f);
			float g = Random.Range (0f, 1f);
			float b = Random.Range (0f, 1f);
			return new Color (r,g,b);
		}

		private void UpdateText () 
		{
			countText.text = string.Format ("Cubes count: {0}", cubesCounter);
		}
	}
}


