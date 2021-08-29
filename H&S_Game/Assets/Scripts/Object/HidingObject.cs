using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingObject : MonoBehaviour
{
    [SerializeField] private BoxCollider2D hideCollider;
    [SerializeField] private GameObject playerVisual;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        //Debug.Log("Mouse is over GameObject.");
    }
    private void OnMouseDown()
    {
        Debug.Log("Clicked on the Hiding Object");
        playerVisual.GetComponent<SpriteRenderer>().enabled = false;
    }


}
