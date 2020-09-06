using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    public Transform camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + camera.forward);
    }
}
