using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoginMng : MonoBehaviour
{
    [SerializeField] TMP_InputField inputID;
    [SerializeField] TMP_InputField inputPassword;
    [SerializeField] TMP_InputField inputUserName;
    [SerializeField] TextMeshProUGUI statusText;

    private const string USERKEY = "USER_";
    private const string USERRPASSWORDKEY = "_PASSWORD";

    public void ClickLogin()
    {
        string userID = inputID.text.Trim();
        string userPassword = inputPassword.text.Trim();

        if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(userPassword))
        {
            SetstatusText("���̵�� ��й�ȣ�� ��� �Է����ּ���", Color.red);
            return;
        }

        Login(userID, userPassword);
    }

    public void ClickRegister()
    {
        string userID = inputID.text.Trim();
        string userPassword = inputPassword.text.Trim();

        if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(userPassword))
        {
            SetstatusText("���̵�� ��й�ȣ�� ��� �Է����ּ���", Color.red);
            return;
        }

        if (PlayerPrefs.HasKey(USERKEY + userID))
        {
            SetstatusText("�̹� �����ϴ� ���̵��Դϴ�. �ٸ� ���̵� ������ּ���.", Color.red);
            return;
        }

        if (inputUserName.text.Length > 1) return;

        PlayerPrefs.SetString(USERKEY + userID, userID);
        PlayerPrefs.SetString(USERKEY + userID + USERRPASSWORDKEY, userPassword);
        PlayerPrefs.Save();

        UserData newUserData = new UserData()
        {
            userID = userID,
            userName = inputUserName.text.Trim(),
            userLevel = 1,
            curExp = 0,
            maxExp = 100,
            gold = 1000,
            gem = 50,
            paper = 10,
            first = true
        };

        DataMng.instance.UserDataLoader.SaveUserData(newUserData);
        SetstatusText("ȸ�������� �Ϸ�Ǿ����ϴ�.", Color.green);
    }

    private void Login(string _id, string _password)
    {
        string savedID = PlayerPrefs.GetString(USERKEY + _id, "");
        string savedPassword = PlayerPrefs.GetString(USERKEY + _id + USERRPASSWORDKEY, "");

        if (savedID == _id && savedPassword == _password)
        {
            SetstatusText("���� ���� ��...", Color.black);

            UserData loadData = DataMng.instance.UserDataLoader.LoadUserData(_id);
        }
        else
        {
            SetstatusText("���̵� �Ǵ� ��й�ȣ�� �ùٸ��� �ʽ��ϴ�", Color.red);
        }
    }

    private void LoadUserData(string _id)
    {
        string userName = PlayerPrefs.GetString(USERKEY + _id, "Unknown");
    }

    private void SetstatusText(string _text, Color color)
    {
        statusText.text = _text;
        statusText.color = color;
    }
}
