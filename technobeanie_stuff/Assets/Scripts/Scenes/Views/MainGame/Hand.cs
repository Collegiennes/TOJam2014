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
	public tk2dSprite m_Asset = null;
	public string m_HandGrabbedAssetName = "";
	public string m_HandEmptyAssetName = "";

	// protected
	
	// private
	
	// properties
	#endregion
	
	#region Unity API
	private void Update()
	{
		// Movements.
		Vector2 movement = ControllerInputManager.Instance.GetLeftJoystick(m_ControllerId);
		transform.position += new Vector3(movement.x, movement.y, 0.0f) * (Time.deltaTime * SPEED);

		// Buttons.
		if (ControllerInputManager.Instance.GetButton(m_ControllerId, ControllerInputManager.eButtonAliases.GRAB.ToString()))
		{
			m_Asset.spriteId = m_Asset.GetSpriteIdByName(m_HandGrabbedAssetName);
		}
		else
		{
			m_Asset.spriteId = m_Asset.GetSpriteIdByName(m_HandEmptyAssetName);
		}
	}
	#endregion
	
	#region Public Methods
	#endregion
	
	#region Protected Methods
	#endregion
	
	#region Private Methods
	#endregion
}
