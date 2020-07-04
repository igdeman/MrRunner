using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrRunner
{
	public class Mover:MonoBehaviour
	{
		public Vector3 MoveTo;
		public Vector3 Step;
		public float Speed;
		public bool MoveChildren;

		[SerializeField]
		bool isMoving;
		public bool IsMoving
		{
			get{return isMoving;}
			set
			{
				isMoving = value;
			}
		}

		[SerializeField]
		bool isStepping;
		public bool IsStepping
		{
			get{return isStepping;}
			set
			{
				isStepping = value;
			}
		}

		void Update()
		{
			if(MoveChildren)
			{
				for(int i=0; i<transform.childCount; i++)
				{
					Transform child = transform.GetChild(i);
					if(child.gameObject.activeSelf)
						Move(transform.GetChild(i));
				}
			}
			else
			{
				Move(transform);
			}
		}

		void Move(Transform t)
		{
			if(isStepping)
			{
				t.localPosition = Vector3.Lerp(t.localPosition, t.localPosition + Step, Speed*Time.deltaTime);
			}
			if(isMoving)
			{
				Vector3 fromPosition = new Vector3(Mathf.Round(t.localPosition.x), Mathf.Round(t.localPosition.y), Mathf.Round(t.localPosition.z));
				Vector3 toPosition = new Vector3(Mathf.Round(MoveTo.x), Mathf.Round(MoveTo.y), Mathf.Round(MoveTo.z));
				if(fromPosition != toPosition)
					t.localPosition = Vector3.Lerp(t.localPosition, MoveTo, Speed*Time.deltaTime);
				else
				{
					t.localPosition = MoveTo;
					isMoving = false;
				}
			}
		}
	}
}
