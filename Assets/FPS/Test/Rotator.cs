using UnityEngine;

namespace FPS
{
	public class Rotator : MonoBehaviour 
	{
		private Vector3 rotationVector;
		private MeshRenderer meshRenderer;

		void Awake () 
		{
			meshRenderer = GetComponent <MeshRenderer> ();
		}

		void Update () 
		{
			transform.Rotate (rotationVector * Time.deltaTime);
		}

		public void SetColorAndSpeed (Color color, float speed) 
		{
			meshRenderer.material.color = color;
			rotationVector = speed * Random.insideUnitSphere;
		}
	}
}
