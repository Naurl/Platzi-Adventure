using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;

    public bool isDialogActive;

    public string[] dialogLines;
    public int currentDialogLine;

    private bool _lastInputAxisState = false;

    // Update is called once per frame
    void Update()
    {
        if (isDialogActive && GetAxisInputLikeOnKeyDown("Jump"))
        {
            //isDialogActive = false;
            //dialogBox.SetActive(isDialogActive);

            currentDialogLine++;
            //Debug.Log("Paso varias veces: " + currentDialogLine);//Se comploco el uso de axis para el espacio ya que pasaba varias veces por este if en menos de 1 segundo.
        }

        if(currentDialogLine >= dialogLines.Length)
        {
            isDialogActive = false;
            dialogBox.SetActive(isDialogActive);
            currentDialogLine = 0;
        }
        else
        {
            dialogText.text = dialogLines[currentDialogLine];
        }
    }

    public void ShowDialog(string[] lines)
    {
        isDialogActive = true;
        dialogBox.SetActive(isDialogActive);
        currentDialogLine = 0;
        dialogLines = lines;
        dialogText.text = dialogLines[currentDialogLine];
    }

    private bool GetAxisInputLikeOnKeyDown(string axisName)
    {
        var currentInputValue = Input.GetAxis(axisName) > 0.1;

        // prevent keep returning true when axis still pressed.
        if (currentInputValue && _lastInputAxisState)
        {
            return false;
        }

        _lastInputAxisState = currentInputValue;

        return currentInputValue;
    }
}
