using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField] 
    private float speed = 2;
    private Vector3 target;
    private float timeRemaining;
    private float timeawait;
    private float hungry;
    private float laying;
    private GameObject feedsign;

    private void Start()
    {
        feedsign = new GameObject("feedsign");
        feedsign.AddComponent<SpriteRenderer>();
        feedsign.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("farming")[65];
        feedsign.transform.parent = this.gameObject.transform;
        speed = 2;
        timeRemaining = 0;
        timeawait = 0;
        hungry = 10;
        laying = 15;
    }
    // Update is called once per frame
    void Update()
    {
        if(hungry > 0)
        {
            move();
            hungry -= Time.deltaTime;
            laying -= Time.deltaTime;
            if (laying < 0)
            {
                laying = 15;
                laysegg();
            }
        }
        else
        {
            feed();
        }
    }
    private void move()
    {
        if (timeRemaining < timeawait)
        {
            timeRemaining = 0;
            if (timeawait > 0)
                timeawait -= Time.deltaTime;
            else
            {
                timeRemaining = RandomNumber(3, 30);
                timeawait = RandomNumber(0, (int)timeRemaining);
                int posX = RandomNumber(-6, 6);
                int posY = RandomNumber(-6, 6);
                target = new Vector3(transform.position.x + posX, transform.position.y + posY, transform.position.z);
            }
        }
        else
        {
            timeRemaining -= Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            transform.rotation = Quaternion.identity;
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

    private void laysegg()
    {
        GameObject egg = new GameObject("Egg");
        egg.AddComponent<BoxCollider2D>();
        egg.AddComponent<SpriteRenderer>();
        egg.tag = "Eggs";
        egg.transform.position = transform.position;
        egg.AddComponent<Eggs>();
        egg.GetComponent<SpriteRenderer>().sortingOrder = 1;
        egg.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("farming")[67];
    }

    private void feed()
    {
        feedsign.transform.position = new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z);
        feedsign.GetComponent<SpriteRenderer>().sortingOrder = 1;
        //Touch cho an
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 pos = Input.GetTouch(0).position;
            //We transform the touch position into word space from screen space and store it.
            Vector3 touchPosWorld = Camera.main.ScreenToWorldPoint(pos);
            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
            //Debug.Log("Touched " + touchPosWorld.x + "" +  touchPosWorld.y);
            //We now raycast with this information. If we have hit something we can process it.
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
            if (hitInformation.collider != null)
            {
                //We should have hit something with a 2D Physics collider!
                GameObject touchedObject = hitInformation.transform.gameObject;
                //touchedObject should be the object someone touched.
                if (touchedObject.tag == "Chickens")
                {
                    //TODO - su san pham cho an, them thoi gian doi
                    feedsign.GetComponent<SpriteRenderer>().sortingOrder = 0;
                    hungry = 10;
                }
            }
        }
    }
}
