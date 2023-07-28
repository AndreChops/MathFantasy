using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState{
    idle,
    walk,
    attack,
    interact,
    stagger
}

public class PlayerController : MonoBehaviour
{
    public PlayerState currentState;

    private PlayerAnimation playerAnimation;

    public Rigidbody2D myRB;
    public Animator animator;

    [SerializeField]
    public float speed = 5f;

    private Vector3 movement;
    private Vector2 pointerInput;

    public bool canMove;

    private WeaponParent weaponParent;

    public FloatValue currentHealth;
    public SignalSender playerHealthSignal;

    AudioManager audioManager;

    public GameObject GameOverScreen;
    
    private void Awake()
    {
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
        weaponParent = GetComponentInChildren<WeaponParent>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        currentHealth.RuntimeValue = currentHealth.initialValue;
    }

    void Start()
    {
        currentState = PlayerState.idle;
        canMove = true;
    }

    void Update()
    {   
        pointerInput = GetPointerInput();
        weaponParent.PointerPosition = pointerInput;
 
        if(!canMove)
        {
            myRB.velocity = Vector2.zero;
            return;
        }   

        movement = Vector3.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(Input.GetButtonDown("Attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            if(PauseMenu.GameIsPaused == false){
                weaponParent.Attack(); 
            }  
        }
        else if(currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }

        if(currentState == PlayerState.stagger){
            animator.SetBool("Hit", true);
        }
        else{
            animator.SetBool("Hit", false);
        }

        RotatePlayer();
    }

    void UpdateAnimationAndMove()
    {
        if(movement != Vector3.zero)
        {
            MoveCharacter();
            playerAnimation.PlayAnimation(movement);
        }
        else
        {
            playerAnimation.StopAnimation(0);
            currentState = PlayerState.idle;
        }
    }

    void MoveCharacter()
    {
        movement.Normalize();
        myRB.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);
        currentState = PlayerState.walk;
    }

    public void Knock(float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if(currentHealth.RuntimeValue > 0)
        {
            StartCoroutine(KnockCo(knockTime));
        }else{
            playerAnimation.DeathAnimation();
            GameOverScreen.SetActive(true);
        }
    }

    public void RotatePlayer()
    {
        Vector2 lookDirection = pointerInput - (Vector2)transform.position;
        playerAnimation.RotateToPointer(lookDirection);
    }

    public void RotateToPointer(Vector2 lookDirection)
    {
        Vector3 scale = transform.localScale;
        if (lookDirection.x > 0)
        {
            scale.x = 1;
        }
        else if (lookDirection.x < 0)
        {
            scale.x = -1;
        }
        transform.localScale = scale;
    }

    private IEnumerator KnockCo(float knockTime)
    {
        if(myRB != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRB.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRB.velocity = Vector2.zero;
        }
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
