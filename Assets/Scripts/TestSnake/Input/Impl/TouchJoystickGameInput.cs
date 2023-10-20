using System;
using UnityEngine;

namespace TestSnake.Player.Impl
{
	public class TouchJoystickGameInput : MonoBehaviour, IGameInput
	{

		[SerializeField]
		private Joystick _joystick;
		

		public Vector2 GetMoveDirection()
		{
			return _joystick.Direction;
		}
	}
}