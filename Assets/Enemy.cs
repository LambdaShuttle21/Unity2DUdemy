using UnityEngine;

public class Enemy : Entity
{
    private bool playerDetected;
    

    protected override void Update()
    {
        HandleCollision();
        HandleAttack();
        HandleMovement();
        HandleAnimations();
    }
    protected override void HandleAttack()
    {
        if (playerDetected)
        {
            //EnableMovementAndJump(false);
            animator.SetTrigger("attack");
        }
    }
    protected override void HandleMovement()
    {
        if (canMove == true)//this cames from PlayerAnimationEvents -> AE_DisableMovementAndJump
        {
            rb.linearVelocity = new Vector2(facingDir * moveSpeed, rb.linearVelocityY);//(x,y)
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY);//(x,y)
        }
    }

    protected override void HandleCollision()
    {
        base.HandleCollision();
        //if player is within the overlap circle playerDetected = true and the enemy will execute the attack animation
        playerDetected = Physics2D.OverlapCircle(attackPoint.position, attackRadius, whatIsTarget);
    }

    //[SerializeField] protected float moveSpeed;// Protected: Only child components will be able to use
    //[SerializeField] protected string enemyName;// Encapsulation: Getting properties locked only to his
    //// childs by making them usable with a public method

    //private void Update()
    //{
    //   // MoveAround();

    //    if (Input.GetKeyDown(KeyCode.F))
    //    {
    //        Attack();
    //    }
    //}
    //private void MoveAround()
    //{
    //    Debug.Log(enemyName + " moves at speed " + moveSpeed);
    //}
    //// virtual: it allows overriding methods in childs
    //protected virtual void Attack()
    //{
    //    Debug.Log(enemyName + "attacks!");
    //}
    //public void TakeDamage()
    //{

    //}
    //public string getEnemyName()
    //{
    //    return enemyName;
    //}
}
