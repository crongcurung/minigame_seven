using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OptionSetting : MonoBehaviour                 // ���� ���ӿ� ������
{
    public Slider slider_Back;
    public Slider slider_Effect;

    public Slider graphicsSlider;   
    int num;

    public Image korImage;
    public Image engImage;

	void OnEnable()
	{
        slider_Back.value = DataManager.instance.nowPlayer.audioVolume_BackGround;
        slider_Effect.value = DataManager.instance.nowPlayer.audioVolume_Effect;

        graphicsSlider.value = DataManager.instance.nowPlayer.qualityLevel;

        if (DataManager.instance.nowPlayer.KorOrEngBool == true)    // �ѱ���
        {
            korImage.color = Color.blue;
            engImage.color = Color.white;
        }
        else         // ����
        {
            korImage.color = Color.white;
            engImage.color = Color.blue;
        }
    }



    public void GraphicsControll()                            // �ػ� ����
    {
        AudioManager.instance.PlayOptionSound();
        num = (int)graphicsSlider.value;
        QualitySettings.SetQualityLevel(num);

        DataManager.instance.nowPlayer.qualityLevel = num;

        DataManager.instance.SaveData();
    }

    public void BackGroundSoundControll(float volume)              // ��� ���� �Ҹ� ����
    {
        AudioManager.instance.PlayOptionSound();
        AudioManager.instance.BackGround_audioManager.volume = volume;

        DataManager.instance.nowPlayer.audioVolume_BackGround = volume;

        DataManager.instance.SaveData();
    }

    public void EffectSoundControll(float volume)                  // ȿ���� �Ҹ� ����
    {
        AudioManager.instance.PlayOptionSound();
        AudioManager.instance.Effect_audioManager.volume = volume;

        DataManager.instance.nowPlayer.audioVolume_Effect = volume;

        DataManager.instance.SaveData();
    }

    public void EffectSound()   //  ???
    {
        AudioManager.instance.PlayOptionSound();
    }

    public void Press_Language(bool isKorEn)              // ��� ��ư ���� ��... true�� �ѱ���, false�� ����
    {
        AudioManager.instance.PlayOptionSound();

        if (isKorEn == true)      // �ѱ���
        {
            korImage.color = Color.blue;
            engImage.color = Color.white;

            MainGameTexts.instance.Change_Language(true);
            CommentSetting.instance.Change_Language(true, false);
        }
        else      // ����..
        {
            korImage.color = Color.white;
            engImage.color = Color.blue;

            MainGameTexts.instance.Change_Language(false);
            CommentSetting.instance.Change_Language(false, false);
        }

        DataManager.instance.nowPlayer.KorOrEngBool = isKorEn;
        DataManager.instance.SaveData();
    }
}
