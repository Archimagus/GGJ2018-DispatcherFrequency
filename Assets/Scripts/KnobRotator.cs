using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class KnobRotator : MonoBehaviour
{
	public bool Limit;
	public float Min;
	public float Max;
	public float Value;

	public float Scale=1;

	public KnobRotatedEvent ValueChanged;
	
	private bool _dragging;
	private Vector3 _dragStartPoint;
	private void Start()
	{
		ValueChanged?.Invoke(Value);
	}
	void Update ()
	{
		if(_dragging)
		{
			var dif = (Input.mousePosition - _dragStartPoint) * Scale;
			Value += dif.x;
			if(Limit)
			{
				if (Value > Max)
					Value = Max;
				if (Value < Min)
					Value = Min;
			}
			transform.localEulerAngles =  (Vector3.forward*Value);
			ValueChanged?.Invoke(Value);

			_dragStartPoint = Input.mousePosition;
		}
	}
	private void OnMouseDown()
	{
		_dragging = true;
		_dragStartPoint = Input.mousePosition;
	}
	private void OnMouseUp()
	{
		_dragging = false;
	}
}
[System.Serializable]
public class KnobRotatedEvent : UnityEvent<float>
{
}