using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Vector3 mousePos;
    [SerializeField] private GameObject ShieldHolder;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Shieldupdate();
    }

    private void Shieldupdate()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 dir = (mousePos - transform.position).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public Vector2 GetShieldDir()
    {
        return new Vector2(mousePos.x-transform.position.x, mousePos.y - transform.position.y).normalized;
    }
}
