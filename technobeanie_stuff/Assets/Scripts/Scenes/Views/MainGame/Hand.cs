using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hand : MonoBehaviour 
{
	#region Members and properties
	// constants
	private const float SPEED = 500.0f;
	
	// enums
	
	// public
	public ControllerInputManager.eControllerId m_ControllerId = ControllerInputManager.eControllerId.CONTROLLER_01;
	
	// protected
	
	// private
	
	// properties
	#endregion
	
	#region Unity API
	private void Update()
	{
		Vector2 movement = ControllerInputManager.Instance.GetLeftJoystick(m_ControllerId);
		transform.position += new Vector3(movement.x, movement.y, 0.0f) * (Time.deltaTime * SPEED);
	}
	#endregion
	
	#region Public Methods
	#endregion
	
	#region Protected Methods
	#endregion
	
	#region Private Methods
	#endregion
}
