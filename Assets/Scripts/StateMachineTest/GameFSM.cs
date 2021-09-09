using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;


/// <summary>
/// �Q�[���V�[���̏�Ԃ��Ǘ�����B
/// </summary>
public class GameFSM : MonoBehaviour
{
    static public GameFSM instance;
    public CastingEvent WaterLevelUpStartEvent = null;
    public CastingEvent WaterLevelUpStopEvent = null;

    //���^���i�[����ϐ��BTODO:���̐������A���E���h���s����B
    [SerializeField]
    private List<GameObject> CastingGameObjects = new List<GameObject>();
    public int Round = 0;  //�Q�[���̃��E���h���J�E���g����B

    public enum StateEventId
    {
        Play,
        Miss,
        Stop,
        Exit,
        Finish
    }

    private ImtStateMachine<GameFSM> stateMachine;

    private void Awake()
    {
        // �X�e�[�g�}�V���̃C���X�^���X�𐶐����đJ�ڃe�[�u�����\�z
        stateMachine = new ImtStateMachine<GameFSM>(this); // ���g���R���e�L�X�g�ɂȂ�̂Ŏ��g�̃C���X�^���X��n��
        stateMachine.AddTransition<EntryState, MoldCountConfirmationState>((int)StateEventId.Finish);
        stateMachine.AddTransition<MoldCountConfirmationState, MoldEntryState>((int)StateEventId.Finish);
        stateMachine.AddTransition<MoldEntryState, UpWaterLevelState>((int)StateEventId.Play);
        stateMachine.AddTransition<UpWaterLevelState, WaterLevelOverState>((int)StateEventId.Miss);
        stateMachine.AddTransition<UpWaterLevelState, StopWaterLevelUpwardState>((int)StateEventId.Stop);
        stateMachine.AddTransition<WaterLevelOverState, ScoreCalculationState>((int)StateEventId.Finish);
        stateMachine.AddTransition<StopWaterLevelUpwardState, ScoreCalculationState>((int)StateEventId.Finish); 
        stateMachine.AddTransition<ScoreCalculationState, TotalScoreCalculationState>((int)StateEventId.Finish);
        stateMachine.AddTransition<TotalScoreCalculationState, MoldCountConfirmationState>((int)StateEventId.Finish);
        stateMachine.AddTransition<MoldCountConfirmationState, GameEndState>((int)StateEventId.Exit);

        // �N���X�e�[�g��ݒ�i�N���X�e�[�g�� EntryState�j
        stateMachine.SetStartState<EntryState>();

        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        // �X�e�[�g�}�V�����N��
        stateMachine.Update();
    }

    private void Update()
    {
        // �X�e�[�g�}�V���̍X�V
        stateMachine.Update();
    }


    //�N������
    private class EntryState : ImtStateMachine<GameFSM>.State
    {
        protected override void Enter()
        {
            Debug.Log("nowEntryState");
            StateMachine.SendEvent((int)StateEventId.Finish);
        }
    }
    //���^�c���m�F
    private class MoldCountConfirmationState : ImtStateMachine<GameFSM>.State
    {
        protected override void Enter()
        {
            Debug.Log("nowMoldCountConfirmationState");
            if (GameFSM.instance.CastingGameObjects.Count > GameFSM.instance.Round)
            {
                StateMachine.SendEvent((int)StateEventId.Finish);
                
            }
            else
            {
                StateMachine.SendEvent((int)StateEventId.Exit);
            }
        }

    }
    //�Q�[�����̃^�b�v�C�x���g����̂���B�����ăX�e�[�g��UpWaterLevel�ɕύX����B
    public void  ReciveTapEvent()
    {
        stateMachine.SendEvent((int)StateEventId.Play);
    }
    //���^�o��//TODO:���^�̏o���˗��C�x���g���t����B
    private class MoldEntryState : ImtStateMachine<GameFSM>.State
    {
        protected override void Enter()
        {
            Debug.Log("nowMoldEntryState");

            //GameObject obj = (GameObject)Resources.Load("Cube");
            GameObject obj = GameFSM.instance.CastingGameObjects[0];//TODO:�����������_���ɂ��Ă��ʔ������B

            // �v���n�u�����ɃI�u�W�F�N�g�𐶐�����
            GameObject instance = (GameObject)Instantiate(obj,
                                                          new Vector3(5.0f, 0.0f, 0.0f),
                                                          Quaternion.identity);
        }
    }
    //�㏸���ʂ��~�߂�B
    public void ReciveUnTapEvent()
    {
        stateMachine.SendEvent((int)StateEventId.Stop);
    }
    //���ʏ㏸//�C�x���g�𔭉΂�����B
    public class UpWaterLevelState : ImtStateMachine<GameFSM>.State
    {
        protected override void Enter()
        {
            Debug.Log("nowUpWaterLevelState");
            GameFSM.instance.WaterLevelUpStartEvent.Raise();
        }
    }
    //���ʃI�[�o�[//TODO:���ʃI�[�o�[�ւ̑J�ڂ́A���^�̃N���X����C�x���g�𑗕t���đJ�ڂ���B
    private class WaterLevelOverState : ImtStateMachine<GameFSM>.State
    {
        protected override void Enter()
        {
            Debug.Log("nowWaterLevelOverState");
            //GameFSM.instance.WaterLevelUpStopEvent.Raise();
        }
    }

    //���ʃX�g�b�v�i���_���j
    private class StopWaterLevelUpwardState : ImtStateMachine<GameFSM>.State
    {
        protected override void Enter()
        {
            Debug.Log("nowStopWaterLevelUpwardState");
            GameFSM.instance.WaterLevelUpStopEvent.Raise();
            StateMachine.SendEvent((int)StateEventId.Finish);
        }
    }
    //�P�̂̓��_�Z�o
    private class ScoreCalculationState : ImtStateMachine<GameFSM>.State
    {
        protected override void Enter()
        {
            Debug.Log("nowScoreCalculationState");
            GameFSM.instance.Round++;  //���E���h���J�E���g����B
            StateMachine.SendEvent((int)StateEventId.Finish);
        }
    }
    //�����_�Z�o
    private class TotalScoreCalculationState : ImtStateMachine<GameFSM>.State
    {
        protected override void Enter()
        {
            Debug.Log("nowTotalScoreCalculationState");
            StateMachine.SendEvent((int)StateEventId.Finish);
        }
    }
    //�Q�[���I��/�X�R�A��ʂ�
    private class GameEndState : ImtStateMachine<GameFSM>.State
    {
        protected override void Enter()
        {
            Debug.Log("nowGameEndState");
            //TODO:�Q�[���I���C�x���g��Raise����B
        }
    }
}
