using UnityEngine;
using System.Collections;

namespace MrRunner
{
	public class TransformSpinner : MonoBehaviour
	{
		public float Speed = 20f;
		public Vector3 EulerStep;
	
		void Update () 
		{
			Rotate(transform.eulerAngles + EulerStep);
		}

		void Rotate(Vector3 eulerRotation)
		{
			Quaternion rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, eulerRotation.z);
			transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * Speed);
		}
	}
}
