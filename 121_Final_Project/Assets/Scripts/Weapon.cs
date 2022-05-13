using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    // Sets variables
    private GameObject _camera, _explosion, _enemyHealthDisplay;
    private Slider _enemyHealthSlider;
    private RaycastHit _objectDetect;
    private AudioSource _weaponAudio;
    public GameObject enemyPrefab;
    public AudioClip fireSound;


    // Start is called before the first frame update
    void Start()
    {
        // Sets variables to current objects in game
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        _weaponAudio = gameObject.GetComponent<AudioSource>();
        _explosion = GameObject.FindGameObjectWithTag("Explosion");
        _explosion.SetActive(false);

        // Disables Enemy Health bar on start
        _enemyHealthDisplay = GameObject.FindGameObjectWithTag("EnemyHealth");
        _enemyHealthSlider = _enemyHealthDisplay.GetComponent<Slider>();
        _enemyHealthDisplay.SetActive(false);

        // Tells player they can shoot the red cube to spawn an enemy
        print("Shoot the red button (cube) to spawn an enemy");
    }

    // Update is called once per frame
    void Update()
    {
        // Creates Ray
        Ray cameraRay = new Ray(_camera.transform.position, _camera.transform.forward);

        // Plays firing effects
        if (Input.GetMouseButtonDown(0))
        {
            _weaponAudio.PlayOneShot(fireSound, 0.05f);
            StartCoroutine(FireVisual());
        }

        // Checks if enemy was hit by ray cast
        if (Physics.Raycast(cameraRay, out _objectDetect))
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Takes away from enemy's health and displays its health
                if (_objectDetect.transform.gameObject.tag == "Enemy")
                {
                    _objectDetect.transform.gameObject.GetComponent<Enemy>().health--;
                    DisplayEnemyHealth(_objectDetect.transform.gameObject);
                }
                else if (_objectDetect.transform.gameObject.tag == "Button")
                {
                    ResetEnemyHealth();
                    SpawnEnemy();
                }
                else
                {
                    // Stops displaying health
                    ResetEnemyHealth();
                }
            }
            else if (_objectDetect.transform.gameObject.tag == "Enemy")
            {
                DisplayEnemyHealth(_objectDetect.transform.gameObject);
            }
            else
            {
                // Stops displaying health
                ResetEnemyHealth();
            }
        }
    }
    
    // Displays health of enemy currently being looked at
    public void DisplayEnemyHealth(GameObject enemy)
    {
        _enemyHealthSlider.value = _objectDetect.transform.gameObject.GetComponent<Enemy>().health;
        _enemyHealthDisplay.SetActive(true);
    }

    // Disables display of enemy health
    public void ResetEnemyHealth()
    {
        _enemyHealthDisplay.SetActive(false);
    }

    // Plays simple explosion effect
    public IEnumerator FireVisual()
    {
        _explosion.SetActive(true);
        yield return new WaitForSeconds(0.10f);
        _explosion.SetActive(false);
    }

    public void SpawnEnemy()
    {
        Instantiate(enemyPrefab, new Vector3(0, 2, 12), Quaternion.identity);
    }
}
