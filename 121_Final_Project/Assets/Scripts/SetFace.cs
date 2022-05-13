using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFace : MonoBehaviour
{
    //Sets variables
    public Material[] cats;
    private GameObject _face;
    private int _catIndex;

    // Start is called before the first frame update
    void Start()
    {
        _catIndex = Random.Range(0, 8);
        _face = gameObject;
        _face.GetComponent<Renderer>().material = cats[_catIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
