using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginAuth : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public void OnLoginButtonClicked()
    {
        if (usernameInput.text == "admin" && passwordInput.text == "password")
        {
            Debug.Log("Login successful!\nTeleporting to the next scene...");
            SceneManager.LoadScene("BasicScene");
        }
        else
        {
            Debug.Log("Login failed. Please try again.");
        }
    }
}
