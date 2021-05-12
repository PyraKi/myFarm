using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField] 
    private float speed = 2;
    private Vector3 target;
    private float timeRemaining = 0;
    private float timeawait = 0;
    private void Start()
    {
        target = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (timeRemaining < timeawait)
        {
            timeRemaining = 0;
            if (timeawait > 0)
                timeawait -= Time.deltaTime;
            else
            {
                timeRemaining = RandomNumber(3, 30);
                timeawait = RandomNumber(0, (int) timeRemaining);
                int posX = RandomNumber(-6, 6);
                int posY = RandomNumber(-6, 6);
                Debug.Log("random");
                target = new Vector3(transform.position.x + posX, transform.position.y + posY, transform.position.z);
            }
        }
        else
        {
            timeRemaining -= Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        }
    }

    //Function to get a random number 
    private static readonly System.Random random = new System.Random();
    private static readonly object syncLock = new object();
    public static int RandomNumber(int min, int max)
    {
        lock (syncLock)
        { // synchronize
            return random.Next(min, max);
        }
    }
}
