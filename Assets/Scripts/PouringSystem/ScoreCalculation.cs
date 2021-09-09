using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//�C�x���g����̂�����A�_�������߂�B
public class ScoreCalculation : MonoBehaviour
{
    [SerializeField]
    private CastingParameter CastingParameter = null;

    [SerializeField]
    private Score score = null;

    [SerializeField]
    private float overScorePoint = 0;

    //Uki��(CastingParameter.CastingUpperPosition-CastingParameter.CastinglowerPosition)�̍��ق��牽�p�[�Z���g�̈ʒu�ɗ��Ă��邩�m�F����B
    //�����Scriptable�I�u�W�F�N�g�Ɋi�[����B
    public void AdditionalScoreCalculation()
    {
        //�[�������߂�B
        float CastingDepth = CastingParameter.CastingUpperPosition - CastingParameter.CastingLowerPosition;
        float FillingDepth = CastingParameter.CastingWaterLevelPostion - CastingParameter.CastingLowerPosition;
        float FillingRate = FillingDepth / CastingDepth;
        
        float Score = 100 * FillingRate;
        Score = (float)Math.Round(Score, 1);
        Debug.Log("�_����"+Score+"�_�ł�");
        score.RoundScore.Add(Score);
    }

    //�͂ݏo���ꍇ�͂O���i�[����B
    public void WaterLevelOverScoreCalculation()
    {
        Debug.Log("�_����" + overScorePoint + "�_�ł�");
        score.RoundScore.Add(overScorePoint);
    }
    //���v�_�����߂�B
    public void TotalScore()
    {
        List<int> lst3 = (new List<float>(score.RoundScore)).ConvertAll(FloatToInt10Multiple);//Sum���g�p����ׂ�10�{����int�ɕϊ�
        int a =lst3.Sum();  
        float b = a / 10f; 
        Debug.Log("���v�_����" + b + "�_�ł�");
    }
    private static int FloatToInt10Multiple(float i)
    {
        return (int)(i * 10);
    }
}
