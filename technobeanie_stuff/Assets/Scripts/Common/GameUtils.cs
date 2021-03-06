﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameUtils 
{
	#region Members and Properties
	// constants
	
	// enums
	
	// public
	
	// protected
	
	// private
	
	// properties
	#endregion
	
	#region Public Functions
	public static View FindAssociatedView(Transform current)
	{
		View view = null;
		if (current != null)
		{
			view = current.gameObject.GetComponent<View>();
			if (view == null)
			{
				view = FindAssociatedView(current.parent);
			}
		}
		
		return view;
	}
	#endregion
	
	#region Protected Functions
	#endregion
	
	#region Private Functions
	#endregion
}
