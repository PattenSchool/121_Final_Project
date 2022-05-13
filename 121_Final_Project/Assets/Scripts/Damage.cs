using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

// Damage script I made for the group project
public class Damage : MonoBehaviour
{
    // Sets various variables
    public float playerHealth = 3, knockbackForce, healthDisplayTimer, immuneTime;
    private GameObject _player;
    private float _velocityX, _velocityY, _velocityZ;
    private IEnumerator _coroutine, _damageCooldown;
    public Slider healthBarSlider;
    public Image healthBarBorder, healthBarFill;
    private bool _playerVulnerable = true;


    // Start is called before the first frame update
    void Start()
    {
        // Sets player to player tagged object
        _player = GameObject.FindGameObjectWithTag("Player");

        // Starts the health bar display
        healthBarBorder.enabled = false;
        healthBarFill.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collider)
    {
        // Detects if the player has collided with any object with the "enemy" tag
        if (collider.gameObject.tag == "Enemy")
        {
            // Player takes damage
            if (_playerVulnerable)
            {
                StartCooldown();
            }
            
            TrackHealth(playerHealth);
            // Knocks the player back
            Knockback(collider);
        }
        // Adds health if the player has collided with any game object tagged "healthPickup"
        else if (collider.gameObject.tag == "HealthPickup")
        {
            if (playerHealth < 3)
            {
                playerHealth = 3;
                collider.gameObject.SetActive(false);
                StartCoroutine(RespawnHealth(collider.gameObject));
                StartCoroutine(DisplayHealth(playerHealth));
            }
        }
    }
    public void StartCooldown()
    {
        _playerVulnerable = false;
        playerHealth--;
        Invoke("ResetCooldown", immuneTime);
        StartCoroutine(DisplayHealth(playerHealth));
    }
    public void ResetCooldown()
    {
        _playerVulnerable = true;
    }
    // Quits the game if the game
    // if the player's health has reached 0 or lower
    public void TrackHealth(float health)
    {
        if (health <= 0)
        {
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
    // Displays the health, waits, then disables the health icons
    IEnumerator DisplayHealth(float health)
    {
        healthBarBorder.enabled = true;
        healthBarFill.enabled = true;
        healthBarSlider.value = health;
        yield return new WaitForSeconds(healthDisplayTimer);
        healthBarBorder.enabled = false;
        healthBarFill.enabled = false;
    }

    // Respawns (Re-enables) the health
    IEnumerator RespawnHealth(GameObject health)
    {
        yield return new WaitForSeconds(10f);
        health.SetActive(true);
    }

    // Knocks back the player based on the player's location compared to the enemy's location
    public void Knockback(Collider collider)
    {
        CharacterController temp = _player.GetComponent<CharacterController>();
        _velocityX = _player.transform.position.x - collider.gameObject.transform.position.x;
        _velocityY = _player.transform.position.y - collider.gameObject.transform.position.y;
        _velocityZ = _player.transform.position.z - collider.gameObject.transform.position.z;
        temp.Move(new Vector3(_velocityX * knockbackForce * Time.deltaTime, 0, _velocityZ * knockbackForce * Time.deltaTime));
    }
}
