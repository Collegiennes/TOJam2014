using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hand : MonoBehaviour 
{
	#region Members and properties
	// constants
	private const float SPEED = 800.0f;
	private const float GRABBED_OFFSET = -250f;
	private const float RELEASED_OFFSET = 0.0f;
	
	// enums
	
	// public
	public ControllerInputManager.eControllerId m_ControllerId = ControllerInputManager.eControllerId.CONTROLLER_01;
	public tk2dSprite m_Asset = null;
	public string m_HandGrabbedAssetName = "";
	public string m_HandEmptyAssetName = "";
	public string m_HandReadyAssetName = "";

	// protected
	
	// private
	private ObstacleHandle m_GrabbedObstacleHandle = null;
	private HingeJoint m_GrabbedObstacleJoin = null;
	
	// properties
	#endregion
	
	#region Unity API
	private void FixedUpdate()
	{
		// Movements.
		Vector2 movement = ControllerInputManager.Instance.GetLeftJoystick(m_ControllerId);
		transform.position += new Vector3(movement.x, 0.0f, movement.y) * (Time.deltaTime * SPEED);

		// Buttons.
		if (ControllerInputManager.Instance.GetButton(m_ControllerId, ControllerInputManager.eButtonAliases.GRAB.ToString()))
		{
			GrabObstacle();
		}
		else
		{
			StartCoroutine("ReleaseObstacle");
		}
	}

	private void OnTriggerEnter(Collider other) 
	{
		ObstacleHandle handle = other.GetComponent<ObstacleHandle>();
		if (m_GrabbedObstacleHandle == null && handle != null)
		{
			m_Asset.spriteId = m_Asset.GetSpriteIdByName(m_HandReadyAssetName);
			m_GrabbedObstacleHandle = handle;
		}
	}

	private void OnTriggerExit(Collider other) 
	{
		ObstacleHandle handle = other.GetComponent<ObstacleHandle>();
		if (handle != null && handle == m_GrabbedObstacleHandle)
		{
			m_Asset.spriteId = m_Asset.GetSpriteIdByName(m_HandEmptyAssetName);
			m_GrabbedObstacleHandle = null;
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
		m_Asset.spriteId = m_Asset.GetSpriteIdByName(m_HandGrabbedAssetName);
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, GRABBED_OFFSET);

		if (m_GrabbedObstacleHandle != null && m_GrabbedObstacleJoin == null)
		{
			m_GrabbedObstacleHandle.m_Obstacle.transform.position = transform.position - (m_GrabbedObstacleHandle.transform.position - m_GrabbedObstacleHandle.m_Obstacle.transform.position);

			m_GrabbedObstacleJoin = gameObject.AddComponent<HingeJoint>();
			m_GrabbedObstacleJoin.connectedBody = m_GrabbedObstacleHandle.m_Obstacle.rigidbody;
			m_GrabbedObstacleJoin.anchor = m_GrabbedObstacleHandle.transform.localPosition;
		}
	}
	
	private IEnumerator ReleaseObstacle()
	{
		if (m_GrabbedObstacleJoin != null)
		{
			Destroy(m_GrabbedObstacleJoin);
		}
		
		yield return null;
		
		m_Asset.spriteId = m_Asset.GetSpriteIdByName(m_HandEmptyAssetName);
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, RELEASED_OFFSET);
	}
	#endregion
}
