using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControllerButton : ActionButton 
{
	#region Members and Properties
	// constants
	
	// enums
	
	// public
	public PlayerInputManager.eButtonAliases m_ButtonAlias;
	public PlayerInputManager.eButtonAliases m_SelectedButtonAlias;
	
	// protected
	
	// private
	private bool m_IsOver = false;
	
	// properties
	#endregion
	
	#region Unity API
	protected virtual void Update()
	{
		if (m_AssociatedView != null && m_AssociatedView.State != View.eState.OPENED)
		{
			return;
		}

		if (PlayerInputManager.Instance.GetButtonDown(m_ButtonAlias.ToString()))
		{
			OnTouchDown(new TouchEvent(transform.position, transform.position, TouchRawType.LeftClick));
		}
		else if (PlayerInputManager.Instance.GetButtonUp(m_ButtonAlias.ToString()))
		{
			OnTouchUp(new TouchEvent(transform.position, transform.position, TouchRawType.LeftClick));
		}
		else if (PlayerInputManager.Instance.GetButtonDown(m_SelectedButtonAlias.ToString()))
		{
			OnButtonDown();
		}
		else if (PlayerInputManager.Instance.GetButtonUp(m_SelectedButtonAlias.ToString()))
		{
			OnButtonUp();
		}
	}
	#endregion
	
	#region Public Functions
	public void ButtonOver()
	{
		OnTouchOver(new TouchEvent(transform.position, transform.position, TouchRawType.LeftClick));
	}
	
	public void ButtonOut()
	{
		OnTouchOut(new TouchEvent(transform.position, transform.position, TouchRawType.LeftClick));
	}
	#endregion

	#region ControllerButton
	public virtual void OnTouchOver(TouchEvent touchEvent)
	{
		m_IsOver = true;

		m_State = State.OVER;
		NotifyObservers(m_State);
	}
	
	public virtual void OnTouchOut(TouchEvent touchEvent)
	{
		m_IsOver = false;

		m_State = State.UP;
		NotifyObservers(m_State);
	}

	public override void OnTouchUp(TouchEvent touchEvent)
	{
		base.OnTouchUp(touchEvent);

		if (m_IsOver)
		{
			OnTouchOver(touchEvent);
		}
	}
	#endregion

	#region Protected Functions
	protected virtual void OnButtonDown()
	{
		if (m_IsOver)
		{
			OnTouchDown(new TouchEvent(transform.position, transform.position, TouchRawType.LeftClick));
		}
	}
	
	protected virtual void OnButtonUp()
	{
		if (m_IsOver)
		{
			OnTouchUp(new TouchEvent(transform.position, transform.position, TouchRawType.LeftClick));
		}
	}
	#endregion

	#region Private Functions
	#endregion
}
