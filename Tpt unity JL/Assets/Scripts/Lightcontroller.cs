using UnityEngine;

public class Lightcontroller : MonoBehaviour
{
    public GameObject PlayerObject;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - PlayerObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = PlayerObject.transform.position + offset;
    }
}
