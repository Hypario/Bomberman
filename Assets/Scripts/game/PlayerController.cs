using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{

    private Tilemap tilemap;

    public GameObject bombPrefab;

    public int bombRange = 2;
    public float speed = 2.5f;

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

        Vector2 move = new Vector2(horizontal, vertical);

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

        if (Input.GetKeyDown("space"))
        {
            // Get the cell the player is standing on
            Vector3Int cell = tilemap.WorldToCell(position);
            Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell); // get the center of the cell
            
            // if we aren't on a bomb, create a bomb
            if (!IsBomb(cellCenterPos))
            {
                // create a bomb
                GameObject bomb = Instantiate(bombPrefab, cellCenterPos, Quaternion.identity);

                // set the range of the bomb
                bomb.GetComponent<BombController>().SetRange(bombRange);
            }
        }
    }

    bool IsBomb(Vector3 cell)
    {
        GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");
        foreach (GameObject bombTest in bombs)
        {
            // Iterate through all bombs and find the one we are on

            // If we are on a bomb
            if (Vector3.Distance(bombTest.transform.position, cell) == 0)
            {
                return true;
            }
        }
        // we're not standing on a bomb ? return false
        return false;
    }
}
