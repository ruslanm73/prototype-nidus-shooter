using UnityEngine;

public class MouseClickExample : MonoBehaviour
{
    private Vector3 worldPosition;
    private GameObject _primitive;

    private void OnEnable()
    {
        // _primitive = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        // _primitive.transform.localScale = Vector3.one / 2;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Clicked();
        }

        // _primitive.transform.position = worldPosition;
    }

    private void Clicked()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.gameObject.name);

            _primitive = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            _primitive.transform.position = hit.point;
            _primitive.transform.localScale = Vector3.one / 2;
        }
    }
}