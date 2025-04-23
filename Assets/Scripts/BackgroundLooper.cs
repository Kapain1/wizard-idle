using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    public Transform player;
    public float backgroundWidth = 14f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x - transform.position.x > backgroundWidth){
            transform.position += new Vector3(backgroundWidth * 2 , 0 , 0);
        }  


    }
}
