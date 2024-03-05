using UnityEngine;
using UnityEngine.PlayerLoop;

public class CheckIfPlayerIsInView : MonoBehaviour
{
    public bool view = true;
    public bool startRay;
    public bool enemydeath;

    public Transform player;

    public Vector3 lastPlayerPos;

    RaycastHit2D hit2D;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            startRay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            startRay = false;
            view = false;
        }
    }
    public bool IfPlayerIsInView()
    {
        return view;
    }

    void Update()
    {
        if (startRay && !enemydeath)
        {
            hit2D = Physics2D.Raycast(transform.position, player.transform.position - transform.position, 15, 3);

            if (hit2D && hit2D.transform.tag != "player")
            {
                Debug.Log("Hit: " + hit2D.transform.name);
                view = false;
            }
            else
            {
                Debug.Log("Player Hit");
                lastPlayerPos = hit2D.point;
                view = true;
            }

            Debug.DrawLine(transform.position, hit2D.point, Color.red, 1f);
        }

        if (view && !enemydeath)
        {
            Vector3 directionToTarget = player.position - transform.position;
            directionToTarget.Normalize(); 
            
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }


}
