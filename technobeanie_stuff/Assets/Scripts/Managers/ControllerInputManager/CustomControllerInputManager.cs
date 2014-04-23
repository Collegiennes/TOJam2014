using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class ControllerInputManager 
{
	#region Members and Properties
	// constants
	
	// enums
	
	// public
	
	// protected
	
	// private
	
	// properties
	#endregion
	
	#region Unity API
	#endregion

	#region ControllerInputManager Implementation
	partial void PostInputDetection()
	{
		// Controllers.
		AddKeyboard1();
		AddMouse();

		// Aliases.
		SetAliases();
	}
	#endregion
	
	#region Public Functions
	#endregion
	
	#region Protected Functions
	#endregion
	
	#region Private Functions
	private void AddKeyboard1()
	{
		Dictionary<KeyboardController.eSimulatedAxis, string> axisMap = new Dictionary<KeyboardController.eSimulatedAxis, string>();
		axisMap.Add(KeyboardController.eSimulatedAxis.LEFT_LEFT_JOYSTICK, "a");
		axisMap.Add(KeyboardController.eSimulatedAxis.RIGHT_LEFT_JOYSTICK, "d");
		axisMap.Add(KeyboardController.eSimulatedAxis.UP_LEFT_JOYSTICK, "w");
		axisMap.Add(KeyboardController.eSimulatedAxis.DOWN_LEFT_JOYSTICK, "s");
		axisMap.Add(KeyboardController.eSimulatedAxis.LEFT_RIGHT_JOYSTICK, "left");
		axisMap.Add(KeyboardController.eSimulatedAxis.RIGHT_RIGHT_JOYSTICK, "right");
		axisMap.Add(KeyboardController.eSimulatedAxis.UP_RIGHT_JOYSTICK, "up");
		axisMap.Add(KeyboardController.eSimulatedAxis.DOWN_RIGHT_JOYSTICK, "down");

		Dictionary<KeyboardController.eKeyboardButtonId, string> buttonIds = new Dictionary<KeyboardController.eKeyboardButtonId, string>();
		buttonIds.Add(KeyboardController.eKeyboardButtonId.ACTION, "z");
		buttonIds.Add(KeyboardController.eKeyboardButtonId.BACK, "x");

		KeyboardController keyboard1 = new KeyboardController(axisMap, buttonIds);
		AddController(keyboard1);
	}
	
	private void AddMouse()
	{
		MouseController mouse = new MouseController();
		AddController(mouse);
	}

	private void SetAliases()
	{
	}
	#endregion
}
