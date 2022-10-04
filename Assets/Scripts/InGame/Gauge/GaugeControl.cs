using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeControl : MonoBehaviour
{
    private float gaugeCount;

    public float upSpeed, downSpeed;//�Q�[�W�㏸�X�s�[�h�A�����̃X�s�[�h
    public float stopTime;//�I�[�o�[����Ƃ����΂炭�����Ȃ��Ȃ鎞�ԑ�
    public int gaugeCharge; //�W�����v�ł��鍂�����L�^�F0�W�����v�s�G1��i�K�W�����v�G2��i�K�W�����v;3�O�i�K�i�I�[�o�[�j
    Image image;
    void Start()
    {
        gaugeCount = 0;
        gaugeCharge = 0;
        image = GetComponent<Image>();
    
    }

    
    void Update()
    {
        //�Q�[�W�W���̌v�Z
        {
            if (gaugeCount < 1)//�Q�[�W���������Ă��Ȃ�
            {
                if (Input.GetKey(KeyCode.G))//���͂����
                    gaugeCount += Time.deltaTime * upSpeed; //�Q�[�W���Z
                if (gaugeCount > 0.6)
                {
                    gaugeCharge = 2;//��i�W�����v��
                }
                else if (gaugeCount > 0.2)
                {
                    gaugeCharge = 1;//��i�W�����v��
                }
                else
                    gaugeCharge = 0;//�W�����v�s��
            }
            else
            {
                gaugeCount = 1;//1�ȏ�̏ꍇ�A�P�ɂ���
                gaugeCharge = 3;//�R�i�K�i�I�[�o�[�j
            }


            if (gaugeCount > 0)//�Q�[�W�͎����I�Ɍ������鏈��
            {
                if (gaugeCharge < 3)//�������Ă��Ȃ�
                {
                    gaugeCount -= Time.deltaTime * downSpeed;
                    if (gaugeCharge == 2)//�Q�i�W�����v
                    {
                        if (gaugeCount < 0.6)
                            gaugeCount = 0.6f;
                    }
                    else if (gaugeCharge == 1)//�P�i�W�����v
                    {
                        if (gaugeCount < 0.2)
                            gaugeCount = 0.2f;
                    }
                }
                else//�������
				{
                    gaugeCount = 1;
				}

            }
            else
                gaugeCount = 0;//0�ɂȂ����炸����0�ɕێ�����
        }
        //�摜�ƘA������
        image.fillAmount = gaugeCount;

        //�W�����v����ƈ�C�ɏ��Ղ���
        if (gaugeCharge <3)//�����ł͂Ȃ����
        {
            if (Input.GetKey(KeyCode.F))
            {
                gaugeCharge = 0;
                gaugeCount = 0;
            }
        }

        //�R�܂Ń`���[�W����Ƃ��΂炭�����Ȃ��Ȃ�
        if(gaugeCharge ==3)
		{
            stopTime -= Time.deltaTime;
             if (stopTime < 0)//time out
			{
                gaugeCharge = 0;
                gaugeCount = 0;
            }
		}
        //���_�FstopTime��񂵂��g���Ȃ��B�J��Ԃ��g����悤�ɏ�����~�����B

    }
}
