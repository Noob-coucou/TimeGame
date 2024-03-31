using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerData playerData;
    public TestManager game;
    public CharacterController Controller;
    public Animator anim;
    public PlayerInput input;
    public IPlayerState current;
    public IPlayerState last;
    public Dictionary<PlayerState.PlayerStateType, IPlayerState> states = new Dictionary<PlayerState.PlayerStateType, IPlayerState>();
    public Vector3 inertia= Vector3.zero;


    private void Awake()
    {
        input=new PlayerInput();
        input.Enable();
        game=GameObject.FindWithTag("TestManager").GetComponent<TestManager>();
        anim=this.GetComponent<Animator>();
        Controller = this.GetComponent<CharacterController>();

        //添加状态
        states.Add(PlayerState.PlayerStateType.Idle, new PlayerState.Idle(this));
        states.Add(PlayerState.PlayerStateType.Move, new PlayerState.Move(this));
        states.Add(PlayerState.PlayerStateType.Run, new PlayerState.Run(this));
        states.Add(PlayerState.PlayerStateType.JumpStart,new PlayerState.JumpStart(this));
        states.Add(PlayerState.PlayerStateType.Air, new PlayerState.Air(this));
        states.Add(PlayerState.PlayerStateType.JumpLand, new PlayerState.JumpLand(this));
        TransState(PlayerState.PlayerStateType.Idle);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        current.OnUpdate();
        //if (input.PlayerBasic.Move.ReadValue<Vector2>().magnitude >= 0.05f)
        //    Debug.Log(input.PlayerBasic.Move.ReadValue<Vector2>();

    }

    public void TransState(PlayerState.PlayerStateType type)
    {
        if (current != null)
        { 
            current.OnExit();
            last = current;
        }
        current = states[type];
        current.OnEnter();
    }
    public Vector3 GetRelativeDirection(Vector3 input)
    {
        // 创建一个旋转变量，它使用Quaternion.Euler创建一个只在Y轴（垂直轴）上旋转的四元数。
        // 这个旋转值取自主相机当前的Y轴旋转角度。
        Quaternion rot = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);

        // 创建一个新的Vector3，它使用上面创建的rot旋转来修改Vector3.forward（前向量）。
        // 这个方向乘以输入的Y轴（通常是前进后退），
        // 然后加上rot旋转的Vector3.right（右向量）乘以输入的X轴（通常是左右）。
        Vector3 dir = rot * Vector3.forward * input.y + rot * Vector3.right * input.x;

        // 返回一个新的Vector3，它将dir向量的X和Z轴值标准化（即保持方向不变，长度变为1）。
        // 这意味着不考虑Y轴（上下），只关注在水平面上的移动。
        return new Vector3(dir.x, 0, dir.z).normalized;
    }
    public void OnFootstep()
    { 
    }
    public void OnLand()
    { 
    }
}
