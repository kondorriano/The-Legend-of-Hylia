//#####################################################################################
//#####################################################################################
//#####################################################################################
// XBOX 360 CONTROLLER CODE SETUP FOR THE UNITY 3D ENGINE IN C SHARP
// THIS HAS BEEN WRITTEN BY KYLE D'AOUST FOR FREE AND EDUCATIONAL USE, ROYALTY FREE.
// FEEL FREE TO USE THIS CODE IN ANY OF YOUR PROJECTS
//#####################################################################################
//#####################################################################################
//#####################################################################################

using UnityEngine;
using System.Collections;

public class TestingControls : MonoBehaviour 
{
	// These are used to modify the player movement speed, and rotation speed.
	public float PlayerMovementSpeed = 30;
	public float PlayerRotationSpeed = 180;
	
	// I seperated Movement and Button inputs into seperate functions, it makes for easier debugging
	void Update ()
	{
		Movement();
		UserInputs();
	}
	
	// This function handles the Movement calculations. You can adjust the code to work with different AXES if preferred.
	// Right Thumbstick uses the 4th(Vertical) and 5th(Horizontal) Input Joystick Axes, and the Left Thumbstick uses the X(Horizontal) and Y(Vertical) Input Joystick Axes.
	// For movement the Vertical Axis is read from moving the LEFT THUMBSTICK up and down,
	// the Horizontal Axis is read from moving the LEFT THUMBSTICK left and right.
	// For Rotation I have it reading from the RIGHT THUMBSTICK.
	void Movement()
	{
		// This line is for vertical movement, right now its on the Z AXIS.
		transform.Translate(0,0,Input.GetAxis("Vertical") * Time.deltaTime * PlayerMovementSpeed);
		
		// This line is for horizontal movement, right now its on the X AXIS. When combined with vertical movement it can be used for Strafing.
		transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * PlayerMovementSpeed,0,0);
		
		// This line is for rotation, right now its on the Y AXIS. 
		transform.Rotate(0,Input.GetAxis("360_RStick") * Time.deltaTime * PlayerRotationSpeed,0);
	}
	
	// This function handles the Inputs from the buttons on the controller
	void UserInputs()
	{
		// A Button is read from Input Positive Button "joystick button 0"
		if(Input.GetButtonDown ("360_A"))
		{
			Debug.Log("A Button!");
		}
		
		// B Button is read from Input Positive Button "joystick button 1"
		if(Input.GetButtonDown ("360_B"))
		{
			Debug.Log("B Button!");
		}
		
		// X Button is read from Input Positive Button "joystick button 2"
		if(Input.GetButtonDown ("360_X"))
		{
			Debug.Log("X Button!");
		}
		
		// Y Button is read from Input Positive Button "joystick button 3"
		if(Input.GetButtonDown ("360_Y"))
		{
			Debug.Log("Y Button!");
		}
		
		// Left Bumper is read from Input Positive Button "joystick button 4"
		if(Input.GetButtonDown ("360_L1"))
		{
			Debug.Log("Left Bumper!");
		}
		
		// Right Bumper is read from Input Positive Button "joystick button 5"
		if(Input.GetButtonDown ("360_R1"))
		{
			Debug.Log("Right Bumper!");
		}
		
		// Back Button is read from Input Positive Button "joystick button 6"
		if(Input.GetButtonDown ("360_Select"))
		{
			Debug.Log("Back Button!");
		}
		
		// Start Button is read from Input Positive Button "joystick button 7"
		if(Input.GetButtonDown ("360_Start"))
		{
			Debug.Log("Start Button!");
		}
		
		// Left Thumbstick Button is read from Input Positive Button "joystick button 8"
		if(Input.GetButtonDown ("360_L3"))
		{
			Debug.Log("Left Thumbstick Button!");
		}
		
		// Right Thumbstick Button is read from Input Positive Button "joystick button 9"
		if(Input.GetButtonDown ("360_R3"))
		{
			Debug.Log("Right Thumbstick Button!");
		}
		
		// Triggers are read from the 3rd Joystick Axis and read from a Sensitivity rating from -1 to 1
		//
		// Right Trigger is activated when pressure is above 0, or the dead zone.
		if(Input.GetAxis("360_Triggers")>0.001)
		{
			Debug.Log ("Right Trigger!");
		}
		
		// Right Trigger is activated when pressure is under 0, or the dead zone.
		if(Input.GetAxis("360_Triggers")<0)
		{
			Debug.Log("Left Trigger!");
		}
		
		// The D-PAD is read from the 6th(Horizontal) and 7th(Vertical) Joystick Axes and read from a Sensitivity rating from -1 to 1, similar to the Triggers.
		//
		// Right D-PAD Button is activated when pressure is above 0, or the dead zone.
		if(Input.GetAxis("360_HorPAD")>0.001)
		{
			Debug.Log ("Right D-PAD Button!");
		}
		
		// Left D-PAD Button is activated when pressure is under 0, or the dead zone.
		if(Input.GetAxis("360_HorPAD")<0)
		{
			Debug.Log("Left D-PAD Button!");
		}
		
		// Up D-PAD Button is activated when pressure is above 0, or the dead zone.
		if(Input.GetAxis("360_VerPAD")>0.001)
		{
			Debug.Log ("Up D-PAD Button!");
		}
		
		// Down D-PAD Button is activated when pressure is under 0, or the dead zone.
		if(Input.GetAxis("360_VerPAD")<0)
		{
			Debug.Log("Down D-PAD Button!");
		}
	}
}