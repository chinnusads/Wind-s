using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeControl : MonoBehaviour
{
    public float gaugeCount;
    public float upSpeed, downSpeed;//�Q�[�W�㏸�X�s�[�h�A�����̃X�s�[�h
    Image image;
    void Start()
    {
        gaugeCount = 0;
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //�Q�[�W�W���̌v�Z
        {
            if (gaugeCount < 1)//�Q�[�W���������Ă��Ȃ�
            {
                if (Input.GetKey(KeyCode.G))//���͂����
                    gaugeCount += Time.deltaTime*upSpeed; //�Q�[�W���Z
            }
            else
                gaugeCount = 1;//1�ȏ�̏ꍇ�A�P�ɂ���


            if (gaugeCount > 0)//�Q�[�W�͎����I�Ɍ�������
            {
                gaugeCount -= Time.deltaTime * downSpeed;
            }
            else
                gaugeCount = 0;//�O�ɂȂ����炸����0�ɂ���
        }
        //�摜�ƘA������
        image.fillAmount = gaugeCount;



    }
}
