using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���^�̃p�����[�^�[���i�[����SO�N���X
/// </summary>

[CreateAssetMenu(fileName = "CastingParameter", menuName = "SO/CastingParameter", order = 51)]
public class CastingParameter : ScriptableObject
{
    [SerializeField] List<float> castingAlpha = new List<float>();
    public List<float> CastingAlpha { get; set; }

    void OnEnable()
    {
        Init();
    }

    void OnValidate()
    {
        Init();
    }

    void Init()
    {
        CastingAlpha = castingAlpha;
    }
}
