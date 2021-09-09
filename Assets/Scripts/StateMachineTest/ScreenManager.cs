using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using IceMilkTea.Core;

public class ScreenManager : MonoBehaviour
{
    // �X�e�[�g�}�V���ϐ��̒�`�A�������R���e�L�X�g�� Player �N���X
    private ImtStateMachine<ScreenManager> stateMachine;

    // �X�e�[�g�}�V���̃C�x���gID�񋓌^
    private enum StateEventId
    {
        Title,
        Main,
        Result,
    }

    private void Awake()
    {

        // �X�e�[�g�}�V���̑J�ڃe�[�u�����\�z�i�R���e�L�X�g�̃C���X�^���X�͂�����񎩕����g�j
        stateMachine = new ImtStateMachine<ScreenManager>(this);
        stateMachine.AddTransition<TitleState, MainState>((int)StateEventId.Main);
        stateMachine.AddTransition<MainState, ResultState>((int)StateEventId.Result);
        stateMachine.AddTransition<ResultState, TitleState>((int)StateEventId.Title);

        // �N����Ԃ�Disabled
        stateMachine.SetStartState<TitleState>();

    }



    private void Start()
    {
        // �X�e�[�g�}�V�����N��
        stateMachine.Update();
    }


    // Player�N���X�ƌ����Ă����Ȃ���ړ��R���|�[�l���g�Ȃ̂�FixedUpdate�ŃX�e�[�g�}�V������
    private void Update()
    {
        // �X�e�[�g�}�V���̍X�V
        stateMachine.Update();
    }


    // �v���C���[�̑����L���ɂ��܂�
    public void MainMove()
    {
        // �X�e�[�g�}�V���ɗL���C�x���g��@������
        stateMachine.SendEvent((int)StateEventId.Main);
    }


    // �v���C���[�̑���𖳌��ɂ��܂�
    public void ResultMove()
    {
        // �X�e�[�g�}�V���ɖ����C�x���g��@������
        stateMachine.SendEvent((int)StateEventId.Result);
    }
    public void TitleMove()
    {
        // �X�e�[�g�}�V���ɖ����C�x���g��@������
        stateMachine.SendEvent((int)StateEventId.Title);
    }

    // �v���C���[�̈ړ��������ꂽ��ԃN���X
    private class TitleState : ImtStateMachine<ScreenManager>.State
    {
        protected override void Enter()
        {
            // �^�C�g���V�[�������Z����
            SceneManager.LoadScene("0_Title", LoadSceneMode.Additive);
            Debug.Log("TitleState");
        }
    }
    private class MainState : ImtStateMachine<ScreenManager>.State
    {
        protected override void Enter()
        {
            // �^�C�g���V�[�������Z����
            SceneManager.LoadScene("1_Main", LoadSceneMode.Additive);
            Debug.Log("MainState");
        }
    }
    private class ResultState : ImtStateMachine<ScreenManager>.State
    {
        protected override void Enter()
        {
            // �^�C�g���V�[�������Z����
            SceneManager.LoadScene("2_Result", LoadSceneMode.Additive);
            Debug.Log("ResultState");
        }
    }
}