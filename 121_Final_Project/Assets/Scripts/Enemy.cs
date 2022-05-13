using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Sets variables
    private GameObject _player;
    private GameObject _enemy;
    private NavMeshAgent _enemyAgent;
    private AudioSource _enemyAudio;
    private Animator _enemyAnimator;
    private float _defaultSpeed;

    public float health = 3;

    public float sightDistance = 20f;
    public float attackDistance = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Sets variables to current objects
        _player = GameObject.FindGameObjectWithTag("Player");
        _enemy = gameObject;
        _enemyAgent = _enemy.GetComponent<NavMeshAgent>();
        _enemyAudio = _enemy.GetComponent<AudioSource>();
        _enemyAnimator = _enemy.GetComponent<Animator>();
        _enemyAudio.Play();
        _enemyAudio.Pause();
        _defaultSpeed = _enemyAgent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        // Simply sets the nav mesh agent destination to the enemy if the
        // player gets close enough
        if (Vector3.Distance(_enemy.transform.position, _player.transform.position)
            < sightDistance)
        {
            _enemyAgent.SetDestination(_player.transform.position);
        }

        // Plays attack animation if enemy gets close to player
        if (Vector3.Distance(_enemy.transform.position, _player.transform.position)
            < attackDistance)
        {
            _enemyAnimator.SetTrigger("EnemyAttack");
        }

        // Stops enemy movement if they are in the attack animation
        if (_enemyAnimator.GetBool("EnemyAttack"))
        {
            _enemyAgent.speed = 0f;
        }
        else
        {
            _enemyAgent.speed = _defaultSpeed;
        }

        // Sets the animation to either idle or walking based on the enemy moving
        // also plays audio if enemy is moving
        if (transform.hasChanged)
        {
            transform.hasChanged = false;
            _enemyAudio.UnPause();
            _enemyAnimator.SetBool("EnemyIdle", false);
            _enemyAnimator.SetFloat("EnemyStage", 2, 0.1f, Time.deltaTime);
        }
        else
        {
            _enemyAnimator.SetBool("EnemyIdle", true);
            _enemyAudio.Pause();
        }

        // Destroys this enemy instance (from prefab) once it's health
        // reaches 0
        if (health <= 0)
        {
            Destroy(_enemy);
        }
    }
}
