using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ʂ��R���g���[������B
/// </summary>
public class WaterLevelControl : MonoBehaviour
{
    public GameObject UkiGameObject;  //���ʂ̏�ʂ������I�u�W�F�N�g

    [SerializeField]
    CastingParameter CastingParameter = null;

    //�������ŏ��̈ʒu�Ɏ����Ă���B�����猩���Ƃ��ɂP�ȊO�ɂȂ������߂Ă̏ꏊ�B//CastingParameter���̓G���h��Ɏ��s����B
    public void StartupPoisition()
    {
        CastingParameter.CastingAlpha.ForEach(s => Debug.Log(s));//���ׂĂ̗v�f���f�o�b�N���O�ɏo���B
    }


    private void Update()
    {
        goUp();
    }
    void goUp()
    {
        UkiGameObject.transform.position += new Vector3(0 ,5.0f * Time.deltaTime, 0);

    }
}
