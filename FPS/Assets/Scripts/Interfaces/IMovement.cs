using System;
using UnityEngine;

public interface IMovement
{
    event Action<bool> IsJoystickUse;
    event Action<Vector2> Direction;
}
