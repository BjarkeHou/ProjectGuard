using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RAIN.Core;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
	class MirrorImageMovementController : MovementController
	{
	    private AIRig ai;
	    public bool LunaBlasted = false;
	    public float LunaBlastTimer;
        public const float LunaBlastTimerMax = 5f;

	    new void Start()
        {
            base.Start();
	        LunaBlastTimer = 0f;
            ai = gameObject.GetComponentInChildren<AIRig>();
            if (ai == null) Debug.Log("AIRig not found in " + this);
        }

	    void Update()
	    {
	        bool canMove = playerCanMove;

            ai.AI.WorkingMemory.SetItem("canMove", canMove);

            //Luna Blast
	        if (LunaBlasted)
	        {
	            LunaBlastTimer -= Time.deltaTime;
	            if (LunaBlastTimer <= 0)
	            {
	                LunaBlastTimer = 0;
	                LunaBlasted = false;
	            }
	        }
        }

	    public void HitWithLunaBlast()
	    {
	        LunaBlastTimer = LunaBlastTimerMax;
	        LunaBlasted = true;
	    }

	    public override bool IsLunaBlasted()
	    {
	        return LunaBlasted;
	    }

	    public override void Dodge(float v, float h)
	    {
            moveDirection = Vector3.zero;

            if (v < 0)
                moveDirection += Vector3.back;
            else if (v > 0)
                moveDirection += Vector3.forward;

            if (h < 0)
                moveDirection += Vector3.left;
            else if (h > 0)
                moveDirection += Vector3.right;

            transform.Find("Model").GetComponent<Animator>().SetBool("Attack", false);

            // Test if player is trying to dodge
            if (playerCanDodge && Time.time > dodgeDelayTimer)
            {
                /*if (GetComponent<PlayerWillController>() != null)
                {
                    GetComponent<PlayerWillController>().Dodge();
                }*/

                Vector3 sPoint = transform.position;
                Vector3 dodgeDirection = moveDirection.normalized;
                float dodgeDist = dodgeDistance;
                string animState = "Dodge_Forward";

                //if no movement is detected, do a backstep
                if (moveDirection == Vector3.zero)
                {
                    animState = "Dodge_Backwards";
                    dodgeDirection = transform.TransformDirection(Vector3.back);
                    dodgeDist = dodgeDistance * 0.75f;
                }
                anim.SetBool(animState, true);
                PerformDodge(sPoint, dodgeDirection, dodgeDist);
            }
	    }
	}
}
