using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollEnemy : Enemy
{
    private Rigidbody2D myRigidbody;
    
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public Animator anim;

    void Start()
    {
        currentState = EnemyState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if(Vector3.Distance(target.position, 
                            transform.position) <= chaseRadius
            && Vector3.Distance(target.position,
                            transform.position) > attackRadius)
        {
            if(currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, 
                target.position, moveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);
                anim.SetBool("wakeUp", true);
            }
        }else if(Vector3.Distance(target.position,
                            transform.position) > chaseRadius){
            anim.SetBool("wakeUp", false);
        }
    }

    private void changeAnim(Vector2 direction){
        if(direction.x > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if(direction.x < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }
    
    private void ChangeState(EnemyState newState)
    {
        if(currentState != newState)
        {
            currentState = newState;
        }
    }
}
