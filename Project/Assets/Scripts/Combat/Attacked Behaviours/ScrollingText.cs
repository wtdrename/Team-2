using UnityEngine;

public class ScrollingText : MonoBehaviour
{

    public float duration = 1f;
    public float speed;

    public Camera camera;
    public TextMesh textMesh;
    public float startTime;
    public Vector3 originalposition;

    // Start is called before the first frame update
    void Awake()
    {
        textMesh = GetComponent<TextMesh>();
        startTime = Time.time;
        camera = Camera.main;
        Vector3 cameraposition = camera.transform.localEulerAngles;
        transform.Rotate(cameraposition);
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.time -startTime < duration)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetText(string text)
    {
        textMesh.text = text;
    }

    public void SetColor(Color color)
    {
        textMesh.color = color;
    }
}
