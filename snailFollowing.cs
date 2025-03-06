using UnityEngine;

public class snailFollowing : MonoBehaviour
{
    public GameObject Path;
    public GameObject[] targets;
    public int target = 0;
    //public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Sprite[] ourFrames;
    public int thisFrame = 0;
    public float timer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int childr = Path.transform.childCount;
        targets = new GameObject[childr];
        for (int i = 0; i<childr; i++){
            targets[i] = Path.transform.GetChild(i).gameObject;
            //animator = GetComponent<Animator>();
            //transform.position = targets[target].transform.position;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.position = targets[target].transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targets == null || targets.Length == 0){
            return;
        }
        GameObject nextTarget = targets[target];
        Vector2 position = transform.position;
        Vector2 nextPosition = nextTarget.transform.position;
        //float nextMove = 2f*Time.deltaTime;
        Vector2 direction = (nextPosition-position).normalized;
        //position += direction*nextMove;
        transform.position = Vector2.MoveTowards(position, nextPosition, 2f*Time.deltaTime);
        /*if (direction.x>0){
            transform.localScale = new Vector3(1,1,1);
        } 
        else if (direction.x<0){
            transform.localScale = new Vector3(-1,1,1);
        }*/
        if (Vector2.Distance(transform.position, nextPosition)<0.1f){
            target = (target+1)%(targets.Length);
        }
        if (direction.x != 0){
            transform.localScale = new Vector3(Mathf.Sign(direction.x),1,1);
        }

        //animator.SetBool("isMoving", direction.magnitude>0);
        if (direction.magnitude>0){
            timer += Time.deltaTime;
            if (timer>0.2f){
                timer = 0f;
                thisFrame = (thisFrame+1)%(ourFrames.Length);
                spriteRenderer.sprite = ourFrames[thisFrame];
            }
        }
        
    }
}