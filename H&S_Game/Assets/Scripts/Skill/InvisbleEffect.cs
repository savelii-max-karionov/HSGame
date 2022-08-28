


using UnityEngine;

public class InvisbleEffect : IEffect
{
    public void Start(GameObject producer, GameObject receiver = null)
    {
        if (receiver != null)
        {
            var renderer = receiver.GetComponent<SpriteRenderer>();
            if (renderer)
            {
                 renderer.color = new Color(1.0f,1.0f,1.0f,0.3f);
            }
            else
            {
                Debug.Log("Invisible effect of " + receiver.name + " failed because the sprite renderer cannot be found, the producer is " + producer.name);
            }
        }
    }


    
}
