using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class BotMovementManager : MonoBehaviour
{
    [SerializeField]
    private float _stoppingNearEnemyDistance = 7f;

    public bool IsMovementCompleted;
    public bool isMove;

    private Animator _anim;
    private AudioSource _audioSource;
    private NavMeshAgent _agent;

    private Vector3 _target;

    private Vector2 _smoothDeltaPosition = Vector2.zero;
    private Vector2 _velocity = Vector2.zero;

    private readonly float _stoppingDistanceDelta = 0.1f;

    private readonly float _nextStepSoundDelay = 0.3f;
    private float _nextStepSoundTime = 0f;

    private Vector3 _worldDeltaPosition;
    private float _dx;
    private float _dy;
    private float _smooth;

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
        if (_agent.pathPending)
        {
            IsMovementCompleted = false;
            return;
        }

        if (IsNearDestinationPosition())
        {
            IsMovementCompleted = true;
            StopMovement();
        }
        else
        {
            IsMovementCompleted = false;

            _worldDeltaPosition = _agent.nextPosition - transform.position;

            // Map 'worldDeltaPosition' to local space
            _dx = Vector3.Dot(transform.right, _worldDeltaPosition);
            _dy = Vector3.Dot(transform.forward, _worldDeltaPosition);

            // Low-pass filter the deltaMove
            _smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
            _smoothDeltaPosition = Vector2.Lerp(_smoothDeltaPosition, new Vector2(_dx, _dy), _smooth);

            // Update velocity if time advances
            if (Time.deltaTime > 1e-5f)
            {
                _velocity = _smoothDeltaPosition / Time.deltaTime;
            }

            isMove = _velocity.magnitude > 0.5f && _agent.remainingDistance > _agent.radius;

            if (isMove && Time.time > _nextStepSoundTime)
            {
                _audioSource.volume = Random.Range(0.8f, 1);
                _audioSource.pitch = Random.Range(0.8f, 1.1f);
                _nextStepSoundTime = Time.time + _nextStepSoundDelay;
                AudioManager.PlaySFXOnAudioSource(SFXType.Steps, _audioSource);
            }

            // Update animation parameters
            _anim.SetBool("move", isMove);
            _anim.SetFloat("speed", _agent.speed);
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

    public void MoveToEnemy(Vector3 target)
    {
        _target = target;
        SetTarget(target, _stoppingNearEnemyDistance);
    }

    public void LookAtTarget()
    {
        _agent.transform.LookAt(_target);
    }

    public Vector3 GetCurrentPossition()
    {
        return _agent.nextPosition;
    }

    public void StopMovement()
    {
        _agent.isStopped = true;
    }

    private void SetTarget(Vector3 target, float stoppingDistance)
    {
        _agent.ResetPath();

        IsMovementCompleted = false;
        _agent.SetDestination(target);
        _agent.isStopped = false;
        _agent.stoppingDistance = stoppingDistance;
    }

    private bool IsNearDestinationPosition()
    {
        return _agent.remainingDistance <= _agent.stoppingDistance + _stoppingDistanceDelta;
    }
}
