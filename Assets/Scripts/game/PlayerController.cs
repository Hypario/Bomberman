using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{

    // needed to place the bomb
    private Tilemap tilemap;
    public GameObject bombPrefab;

    // stats
    public int bombRange = 2;
    public float speed = 2.5f;

    // counter for the bombs
    public int bombPlaced = 0;
    public int bombMax = 1;

    private Animator animator;
    private Rigidbody2D body;
    private Vector2 lookDirection = new Vector2(1, 0);
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        tilemap = GameObject.FindGameObjectWithTag("Tilemap").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical); // get the movement vector via the horizontal and vertical input

        // if we are moving, set the animation to moving, and set the direction we are looking
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();

            animator.SetBool("Moving", true); // set the animation to moving
            animator.SetFloat("Move X", horizontal); // set the sprite in X when moving
            animator.SetFloat("Move Y", vertical); // set the sprite in Y when moving
        } else // else we set the animation where we are looking at and set the animation to Iddle
        {
            animator.SetBool("Moving", false);
            animator.SetFloat("Look X", lookDirection.x); // set the sprite in X when iddle
            animator.SetFloat("Look Y", lookDirection.y); // set the sprite in Y when iddle
        }

        // calculate the next position based on the direction and speed in unit per second
        Vector2 position = body.position;
        position = position + move * speed * Time.deltaTime;
        body.MovePosition(position); // move the "entity"
        
        if (Input.GetKeyDown("space") && !GameManager.instance.IsPaused()) // if we hit the spacebar and the game isn't paused
        {
            Vector3Int cell = tilemap.WorldToCell(position); // get the cell the player is standing on
            Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell); // get the center of the cell
            
            // if we aren't on a bomb, create a bomb
            if (!IsBomb(cellCenterPos) && bombPlaced < bombMax)
            {
                // create a bomb
                GameObject bomb = Instantiate(bombPrefab, cellCenterPos, Quaternion.identity);
                bombPlaced++; // increment the counter (when the bomb explose, the bomb is decremented)

                // set the range of the bomb and his owner
                BombController BombController = bomb.GetComponent<BombController>();
                BombController.SetRange(bombRange);
                BombController.SetOwner(gameObject);
            }
        }
    }

    // return true if we are on a bomb
    bool IsBomb(Vector3 cell)
    {
        // get all the bombs on the map
        GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");
        foreach (GameObject bombTest in bombs)
        {
            // Iterate through all bombs

            // If we are on a bomb
            if (Vector3.Distance(bombTest.transform.position, cell) == 0)
            {
                return true;
            }
        }
        // we are not on a bomb so return false
        return false;
    }
}
