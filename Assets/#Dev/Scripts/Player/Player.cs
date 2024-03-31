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

        //���״̬
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
        // ����һ����ת��������ʹ��Quaternion.Euler����һ��ֻ��Y�ᣨ��ֱ�ᣩ����ת����Ԫ����
        // �����תֵȡ���������ǰ��Y����ת�Ƕȡ�
        Quaternion rot = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);

        // ����һ���µ�Vector3����ʹ�����洴����rot��ת���޸�Vector3.forward��ǰ��������
        // ���������������Y�ᣨͨ����ǰ�����ˣ���
        // Ȼ�����rot��ת��Vector3.right�������������������X�ᣨͨ�������ң���
        Vector3 dir = rot * Vector3.forward * input.y + rot * Vector3.right * input.x;

        // ����һ���µ�Vector3������dir������X��Z��ֵ��׼���������ַ��򲻱䣬���ȱ�Ϊ1����
        // ����ζ�Ų�����Y�ᣨ���£���ֻ��ע��ˮƽ���ϵ��ƶ���
        return new Vector3(dir.x, 0, dir.z).normalized;
    }
    public void OnFootstep()
    { 
    }
    public void OnLand()
    { 
    }
}
