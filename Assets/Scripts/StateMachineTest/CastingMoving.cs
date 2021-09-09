using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//���^�𐶐��E�������N���X�B�e�C�x���g�ŁA�����Ɠ�����s���B
public class CastingMoving : MonoBehaviour
{
    private GameObject InstanceGameObject;
    //���^�𐶐����A�ړ�����B
    public void CastingCreate()
    {
        GameObject obj = GameFSM.instance.CastingGameObjects[0];//TODO:�����̓����_���ł�����������Ȃ��B

        // �v���n�u�����ɃI�u�W�F�N�g�𐶐�����
        InstanceGameObject = (GameObject)Instantiate(obj,
                                                      new Vector3(5.0f, 0.0f, 0.0f),
                                                      Quaternion.identity);
        InstanceGameObject.transform.DOMove(new Vector3(0f, 0f, 0f), 1f);
    }
    //���^���ړ����폜����B
    public void CastingDelete()
    {
        InstanceGameObject.transform.DOMove(new Vector3(-5f, 0f, 0f), 1f).OnComplete(() =>
        {
            Destroy(InstanceGameObject);
        });
    }
}
