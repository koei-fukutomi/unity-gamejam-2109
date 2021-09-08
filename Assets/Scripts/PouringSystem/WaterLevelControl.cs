using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ʂ��R���g���[������B
/// </summary>
public class WaterLevelControl : MonoBehaviour
{
    public GameObject UkiGameObject;  //���ʂ̏�ʂ������I�u�W�F�N�g

    public GameObject TargetObject;  //Uki��u���I�u�W�F�N�g

    [SerializeField]
    CastingParameter CastingParameter = null;

    //�������ŏ��̈ʒu�Ɏ����Ă���B�����猩���Ƃ��ɂP�ȊO�ɂȂ������߂Ă̏ꏊ�B//CastingParameter���̓G���h��Ɏ��s����B
    public void StartupPoisition()
    {
        //�^�[�Q�b�g�̈ʒu��UKI��u���B
        UkiGameObject.transform.position = TargetObject.transform.position;

        //�^�[�Q�b�g�̈ʒu�̉���Uki��u���B//Rect�̃T�C�Y���擾����B
        float TagetObjectLowerPos = UkiGameObject.transform.position.y - TargetObject.GetComponent<SpriteRenderer>().bounds.size.y;
        TagetObjectLowerPos = TagetObjectLowerPos / 2;

        //�^�[�Q�b�g�̃T�C�Y��z�񐔂ŕ�������B
        float ArrayPartHeight= TargetObject.GetComponent<SpriteRenderer>().bounds.size.y/ CastingParameter.CastingAlpha.Count;

        int CastingLower = 0;  //���^�̈�ԉ����i�[����B
        int CastingUpper = 0;  //���^�̈�ԏ���i�[����B//�v�m�F

        for (int i=0;i< CastingParameter.CastingAlpha.Count; i++)
        {
            if(Mathf.Approximately(CastingParameter.CastingAlpha[i], 1.0f))
            {
                if (i < (CastingParameter.CastingAlpha.Count/2))  //TODO:���̏ꍇ�͔�����菬�����}�`�ɑΉ��ł��Ȃ��B
                {
                    CastingLower = i;
                }
                else
                {
                    if (CastingUpper == 0)  //�Ō�ɓ������l�ł͂Ȃ��A�ŏ��ɓ������l���~�����B
                    {
                        CastingUpper = i;
                    }
                }
            }
        }

        //CastingLower�ɍ��킹�č����𑫂����ꏊ��Uki��ݒu�B
        TagetObjectLowerPos = TagetObjectLowerPos + ArrayPartHeight * CastingLower;
        UkiGameObject.transform.position = new Vector3(UkiGameObject.transform.position.x, TagetObjectLowerPos, UkiGameObject.transform.position.z);
    }


    private void Update()
    {
        //goUp();
    }
    void goUp()
    {
        UkiGameObject.transform.position += new Vector3(0 ,5.0f * Time.deltaTime, 0);
    }
}
