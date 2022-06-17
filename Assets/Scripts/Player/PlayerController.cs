using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Zoca
{
    public class PlayerController : MonoBehaviour
    {
        #region private fields
        [SerializeField]
        float maxSpeed;

        [SerializeField]
        Animator animator;

        [SerializeField]
        SpriteRenderer spriteRenderer;

        float speedSmouthTime = 10f;

        // Use this for basic physics such as collision
        Rigidbody2D rb;
        
        Vector2 targetVelocity;

        bool paused = false;
        bool moving = false;

        // Input vector
        Vector2 moveInput;

        // Anim parameters
        string movingAnimParam = "moving";
        string runningAnimParam = "running";
        string directionAnimParam = "direction";

        [SerializeField]
        bool leftFlipping = false;
        #endregion

        #region private methods
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
           
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
        
            if (paused)
                return;

            

        }

        private void LateUpdate()
        {
            CheckAnimations();
        }

        private void FixedUpdate()
        {
            if (paused)
                return;

            // Compute player velocity
            rb.velocity = Vector2.MoveTowards(rb.velocity, targetVelocity, Time.fixedDeltaTime * speedSmouthTime);
        }

        void CheckAnimations()
        {
            
            // Set the animation direction only when the player moves around
            if (moving)
            {
                // Check for move direction
                if (Mathf.Abs(targetVelocity.y) <= Mathf.Abs(targetVelocity.x))
                {
                    // Do we use animation flipping for left sprite ?
                    if (!leftFlipping)
                    {
                        // Set left or right animation
                        animator.SetFloat(directionAnimParam, targetVelocity.x > 0 ? 1 : 3);
                    }
                    else
                    {
                        // Set the right animation
                        animator.SetFloat(directionAnimParam, 1);

                        // Flip the renderer if needed
                        if (targetVelocity.x < 0)
                            spriteRenderer.flipX = true;
                        else
                            spriteRenderer.flipX = false;
                    }

                }
                else
                {
                    // Check front and back
                    animator.SetFloat(directionAnimParam, targetVelocity.y > 0 ? 2 : 0);

                    // Reset horizontal flipping
                    spriteRenderer.flipX = false;
                }
            }


            // Set animation
            if (moving)
            {
                // If animator is not playing the moving animation then play it
                if (!animator.GetBool(movingAnimParam))
                    animator.SetBool(movingAnimParam, true);
            }
            else
            {
                // If animator is playing the moving animation then stop it
                if (animator.GetBool(movingAnimParam))
                    animator.SetBool(movingAnimParam, false);

            }
        }

        #endregion

        #region input callbacks
        public void OnMove(InputAction.CallbackContext context)
        {

            if (context.started)
                moving = true;
            else
                if (context.canceled)
                    moving = false;
            
            // Set the target velocity 
            targetVelocity = moving ? maxSpeed * context.ReadValue<Vector2>().normalized : Vector2.zero;

            // Store the input 
            moveInput = context.ReadValue<Vector2>();
        }



        #endregion

       

    }

}
