using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [Tooltip("The object this GameObject looks at by default")]
    public GameObject defaultTarget;

    [Tooltip("The object currently being looked at, set by clicking")]
    public GameObject currentTarget;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        if (defaultTarget == null)
        {
            defaultTarget = gameObject;
            Debug.Log("defaultTarget not specified. Defaulting to this GameObject.");
        }

        if (currentTarget == null)
        {
            currentTarget = defaultTarget;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                currentTarget = hit.collider.gameObject;
                Debug.Log("currentTarget changed to " + currentTarget.name);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            currentTarget = defaultTarget;
            Debug.Log("currentTarget reset to " + currentTarget.name);
        }

        // Target may have been destroyed since last frame
        if (currentTarget == null)
        {
            currentTarget = defaultTarget;
        }

        if (currentTarget != null)
        {
            transform.LookAt(currentTarget.transform);
        }
    }
}