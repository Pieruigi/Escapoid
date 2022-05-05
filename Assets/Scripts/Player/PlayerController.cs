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

        float speedSmouthTime = 10f;

        // Use this for basic physics such as collision
        Rigidbody2D rb;
        //CharacterController cc;

        Vector2 targetVelocity;

        bool paused = false;
        bool moving = false;
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

        private void FixedUpdate()
        {
            if (paused)
                return;

            // Compute player velocity
            rb.velocity = Vector2.MoveTowards(rb.velocity, targetVelocity, Time.fixedDeltaTime * speedSmouthTime);
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
            

        }

        #endregion

    }

}
