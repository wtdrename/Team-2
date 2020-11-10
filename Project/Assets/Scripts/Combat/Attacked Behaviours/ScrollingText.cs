using UnityEngine;

public class ScrollingText : MonoBehaviour
{
    public float duration = 1f;
    public float speed;

    public new Camera camera;
    public TextMesh textMesh;
    public float startTime;
    public Vector3 originalposition;

    void Awake()
    {
        textMesh = GetComponent<TextMesh>();
        startTime = Time.time;
        camera = Camera.main;
        Vector3 cameraposition = camera.transform.localEulerAngles;
        transform.Rotate(cameraposition);
    }

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
