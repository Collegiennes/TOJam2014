using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hand : MonoBehaviour 
{
	#region Members and properties
	// constants
	private const float SPEED = 800.0f;
	private const float GRABBED_OFFSET = -500.0f;
	private const float RELEASED_OFFSET = 0.0f;
	
	// enums
	
	// public
	public ControllerInputManager.eControllerId m_ControllerId = ControllerInputManager.eControllerId.CONTROLLER_01;
	public tk2dSprite m_Asset = null;
	public string m_HandGrabbedAssetName = "";
	public string m_HandEmptyAssetName = "";

	// protected
	
	// private
	private Collider m_GrabbedObstacle = null;
	private HingeJoint m_GrabbedObstacleJoin = null;
	
	// properties
	#endregion
	
	#region Unity API
	private void Update()
	{
		// Movements.
		Vector2 movement = ControllerInputManager.Instance.GetLeftJoystick(m_ControllerId);
		transform.position += new Vector3(movement.x, 0.0f, movement.y) * (Time.deltaTime * SPEED);

		// Buttons.
		if (ControllerInputManager.Instance.GetButton(m_ControllerId, ControllerInputManager.eButtonAliases.GRAB.ToString()))
		{
			m_Asset.spriteId = m_Asset.GetSpriteIdByName(m_HandGrabbedAssetName);
			transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, GRABBED_OFFSET);

			GrabObstacle();
		}
		else
		{
			m_Asset.spriteId = m_Asset.GetSpriteIdByName(m_HandEmptyAssetName);
			transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, RELEASED_OFFSET);

			ReleaseObstacle();
		}
	}

	private void OnTriggerEnter(Collider other) 
	{
		if (m_GrabbedObstacle == null && IsAnObstacle(other))
		{
			m_GrabbedObstacle = other;
		}
	}

	private void OnTriggerExit(Collider other) 
	{
		if (other == m_GrabbedObstacle)
		{
			m_GrabbedObstacle = null;
		}
	}
	#endregion
	
	#region Public Methods
	#endregion
	
	#region Protected Methods
	#endregion
	
	#region Private Methods
	private void GrabObstacle()
	{
		if (m_GrabbedObstacle != null && m_GrabbedObstacleJoin == null)
		{
			m_GrabbedObstacle.transform.position = transform.position;

			m_GrabbedObstacleJoin = gameObject.AddComponent<HingeJoint>();
			m_GrabbedObstacleJoin.connectedBody = m_GrabbedObstacle.rigidbody;
		}
	}
	
	private void ReleaseObstacle()
	{
		if (m_GrabbedObstacleJoin != null)
		{
			Destroy(m_GrabbedObstacleJoin);
		}
	}
	
	private bool IsAnObstacle(Collider collider)
	{
		return (collider.gameObject.layer == LayerMask.NameToLayer("Death") || collider.gameObject.layer == LayerMask.NameToLayer("DeathNoCollision"));
	}
	#endregion
}
