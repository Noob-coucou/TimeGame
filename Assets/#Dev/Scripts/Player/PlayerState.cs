using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
    public enum PlayerStateType
    { 
        Idle,
        JumpStart,
        Air,
        JumpLand,
        Interact,
        Move,
        Run

    }
    public class Idle : IPlayerState
    {
        public Player self;

        public Idle(Player self)
        {
            this.self = self;
            
        }
        public void OnEnter()
        {
            self.anim.Play("Idle");
            self.inertia = Vector3.zero;

        }
        public void OnUpdate()
        {
            //if (self.input.PlayerBasic.Jump.WasPressedThisFrame())
            //    self.TransState(PlayerStateType.JumpStart);
            //else if (self.input.PlayerBasic.Move.ReadValue<Vector2>().magnitude >= 0.05f)
            //self.TransState(PlayerStateType.Move);
            if (self.input.PlayerBasic.Move.ReadValue<Vector2>().magnitude >= 0.05f)
            self.TransState(PlayerStateType.Move);
        }

        public void OnExit()
        { 
            
        }
    
    }
    public class Move : IPlayerState
    {
        public Player self;
        public Move(Player self)
        {
            this.self = self;

        }
        public void OnEnter()
        {
            self.anim.Play("Move");

        }
        public void OnUpdate()
        {   
            //跳跃禁用
            //if (self.input.PlayerBasic.Jump.WasPressedThisFrame())
            //    self.TransState(PlayerStateType.JumpStart);
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                self.TransState(PlayerStateType.Run);
            else if (self.input.PlayerBasic.Move.ReadValue<Vector2>().magnitude <= 0.05f)
                self.TransState(PlayerStateType.Idle);
            else
            {
                self.Controller.Move(Time.deltaTime * self.playerData.MovementSpeed * self.GetRelativeDirection(self.input.PlayerBasic.Move.ReadValue<Vector2>()));
                self.transform.LookAt(self.transform.position + self.GetRelativeDirection(self.input.PlayerBasic.Move.ReadValue<Vector2>()));
                self.inertia = self.GetRelativeDirection(self.input.PlayerBasic.Move.ReadValue<Vector2>());
            }
        }

        public void OnExit()
        {

        }

    }
    public class Run : IPlayerState
    {
        public Player self;

        public Run(Player self)
        {
            this.self = self;
        }

        public void OnEnter()
        {
            // 当进入奔跑状态时，播放奔跑动画
            self.anim.Play("Run");
        }

        public void OnUpdate()
        {
            //跳跃禁用
            //if (self.input.PlayerBasic.Jump.WasPressedThisFrame())
            //    self.TransState(PlayerStateType.JumpStart);
            if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
            {
                self.TransState(PlayerStateType.Move); // 回到普通移动状态
            }
            else if (self.input.PlayerBasic.Move.ReadValue<Vector2>().magnitude <= 0.05f)
            {
                self.TransState(PlayerStateType.Idle); // 回到静止状态
            }
            else
            {
                // 保持奔跑
                Vector3 direction = self.GetRelativeDirection(self.input.PlayerBasic.Move.ReadValue<Vector2>());
                self.Controller.Move(Time.deltaTime * self.playerData.RunSpeed * direction);
                self.transform.LookAt(self.transform.position + direction);
                self.inertia = self.GetRelativeDirection(self.input.PlayerBasic.Move.ReadValue<Vector2>());
            }
        }

        public void OnExit()
        {
            // 离开奔跑状态时重置
        }
    }
    public class JumpStart : IPlayerState
    {
        public Player self;
        
        private float time;
        private float speed;
        public JumpStart(Player self)
        {
            this.self = self;

        }
        public void OnEnter()
        {
            self.anim.Play("JumpStart");
            time = 0;
            speed = 15f;
        }
        public void OnUpdate()
        {
            time += Time.deltaTime;
            if (time >= 0.2f)
                self.TransState(PlayerStateType.Air);
            speed -= Time.deltaTime * 40f;
            self.Controller.Move(speed * Vector3.up * Time.deltaTime + self.inertia * Time.deltaTime * self.playerData.MovementSpeed);

        }

        public void OnExit()
        {

        }

    }
    public class Air : IPlayerState
    {
        public Player self;

        public Air(Player self)
        {
            this.self = self;

        }
        public void OnEnter()
        {

            self.anim.Play("Air");

        }
        public void OnUpdate()
        {
            if (self.Controller.isGrounded)
            {
                self.TransState(PlayerStateType.JumpLand);
            }
            else
            {
                self.Controller.Move(self.inertia * Time.deltaTime * self.playerData.MovementSpeed);
            }
        }

        public void OnExit()
        {

        }

    }
    public class JumpLand : IPlayerState
    {
        public Player self;

        private float time;
        public JumpLand(Player self)
        {
            this.self = self;

        }
        public void OnEnter()
        {
            time = 0;
            self.inertia = Vector3.zero;
            self.anim.Play("JumpLand");

        }
        public void OnUpdate()
        {
            time += Time.deltaTime;
            if (time >= 0.3f)
                self.TransState(PlayerStateType.Idle);
        }

        public void OnExit()
        {

        }

    }
}