using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class BotMovementManager : MonoBehaviour
{
    public bool IsMovementCompleted;

    private Animator _anim;
    private AudioSource _audioSource;
    private NavMeshAgent _agent;

    private Vector3 _target;

    private Vector2 smoothDeltaPosition = Vector2.zero;
    private Vector2 velocity = Vector2.zero;
    private float _stoppingDistanceDelta = 0.1f;

    private float nextStepSoundDelay = 0.3f;
    private float nextStepSoundTime = 0f;

    

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
        // Don’t update position automatically
        _agent.updatePosition = false;
    }

    void Update()
    {
        if (IsNearDestinationPosition())
        {
            IsMovementCompleted = true;
            StopMovement();
        }
        else
        {
            IsMovementCompleted = false;

            //todo может снаружи проверки
            Vector3 worldDeltaPosition = _agent.nextPosition - transform.position;

            // Map 'worldDeltaPosition' to local space
            float dx = Vector3.Dot(transform.right, worldDeltaPosition);
            float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
            Vector2 deltaPosition = new Vector2(dx, dy);

            // Low-pass filter the deltaMove
            float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
            smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

            // Update velocity if time advances
            if (Time.deltaTime > 1e-5f)
            {
                velocity = smoothDeltaPosition / Time.deltaTime;
            }

            bool isMove = velocity.magnitude > 0.5f && _agent.remainingDistance > _agent.radius;

            if (isMove && Time.time > nextStepSoundTime)
            {
                _audioSource.volume = Random.Range(0.8f, 1);
                _audioSource.pitch = Random.Range(0.8f, 1.1f);
                nextStepSoundTime = Time.time + nextStepSoundDelay;
                AudioManager.PlaySFXOnAudioSource(SFXType.Steps, _audioSource);
            }

            // Update animation parameters
            _anim.SetBool("move", isMove);
            _anim.SetFloat("speed", _agent.speed);
            //anim.SetFloat("velx", velocity.x);
            //anim.SetFloat("vely", velocity.y);
        }
    }

    void OnAnimatorMove()
    {
        // Update position to agent position
        transform.position = _agent.nextPosition;
    }

    public void MoveToTarget(Vector3 target)
    {
        _target = target;
        SetTarget(target, 0);
    }

    public void FolowTarget(Vector3 target, float distance)
    {
        _target = target;
        SetTarget(target, distance);
    }

    

    public void LookAtTarget()
    {
        _agent.transform.LookAt(_target);
    }

    public Vector3 GetCurrentPossition()
    {
        return _agent.nextPosition;
    }

    private void SetTarget(Vector3 target, float stoppingDistance)
    {
        _agent.SetDestination(target);
        _agent.isStopped = false;
        _agent.stoppingDistance = stoppingDistance;
    }


    private bool IsNearDestinationPosition()
    {
        return _agent.remainingDistance <= _agent.stoppingDistance + _stoppingDistanceDelta;
    }

    public void StopMovement()
    {
        _agent.isStopped = true;
    }

    
}
