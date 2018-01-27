using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpMover : MonoBehaviour
{
	public Vector3 Position1;
	public float Position1Value;

	public Vector3 Position2;
	public float Position2Value;
	[SerializeField]
	private float _lerpValue;

	public float LerpValue
	{
		get
		{
			return _lerpValue;
		}
		set
		{
			_lerpValue = value;
		}
	}

	// Update is called once per frame
	void Update()
	{
		float t = Mathf.InverseLerp(Position1Value, Position2Value, LerpValue);

		transform.localPosition = Vector3.Lerp(Position1, Position2, t);
	}

	private void OnValidate()
	{
		Update();
	}

	[ContextMenu("Record Position 1")]
	private void RecordPosition1()
	{
		Position1 = transform.localPosition;
	}

	[ContextMenu("Record Position 2")]
	private void RecordPosition2()
	{
		Position2 = transform.localPosition;
	}
}
