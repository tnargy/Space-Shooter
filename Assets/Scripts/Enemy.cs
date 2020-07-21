using UnityEngine;

public class Enemy : Ship
{
    public GameObject[] waypointsObj;

    private Object lootRef;
    private Vector2[] waypoints;
    private int direction = 0;


    // Start is called before the first frame update
    void Start()
    {
        waypoints = new Vector2[waypointsObj.Length];
        for (int i = 0; i < waypointsObj.Length; i++)
        {
            waypoints[i] = waypointsObj[i].transform.position;
        }

        lootRef = Resources.Load("Loot");
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, NextWaypoint(),
                                moveSpeed * Time.deltaTime);
    }

    private Vector2 NextWaypoint()
    {
        if (Vector2.Distance((Vector2)transform.position, waypoints[direction]) <= .1f)
        {
            direction = ++direction % waypoints.Length;
        }
        return waypoints[direction];
    }

    private void OnDestroy()
    {
        if (Random.Range(0, 10) == 0)
        {
            GameObject loot = (GameObject)Instantiate(lootRef);
            loot.transform.position = transform.position;
        }
    }
}
