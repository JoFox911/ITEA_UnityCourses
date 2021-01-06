using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickDetector : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IMovement, IPointerUpHandler
{
    [SerializeField] 
    private GameObject _thumble;

    [SerializeField] 
    private float _radius = 160f;

    private Vector3 _thumbleStartPosition;

    public event Action<bool> IsJoystickUse;
    public event Action<Vector2> Direction;


    public void OnBeginDrag(PointerEventData eventData)
    {
        _thumbleStartPosition = _thumble.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var direction = new Vector3(eventData.position.x, eventData.position.y) - _thumbleStartPosition;
        direction = direction.normalized;
        var distance = Mathf.Clamp(Vector3.Distance(_thumbleStartPosition, eventData.position), 0, _radius);

        _thumble.transform.position = _thumbleStartPosition + direction * distance;
        var normalizedMovementDirection = direction * distance / _radius;
        RaiseJoystickDirectionEvent(normalizedMovementDirection);
        Debug.Log($"Direction: {normalizedMovementDirection}");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _thumble.transform.position = _thumbleStartPosition;
        RaiseJoystickDirectionEvent(Vector2.zero);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RaiseJoystickUseEvent(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        RaiseJoystickUseEvent(false);
    }


    private void RaiseJoystickUseEvent(bool use)
    {
        IsJoystickUse?.Invoke(use);
    }

    private void RaiseJoystickDirectionEvent(Vector2 direction)
    {
        Direction?.Invoke(direction);
    }
}
