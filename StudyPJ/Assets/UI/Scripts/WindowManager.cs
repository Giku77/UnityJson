using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public List<GenericWindow> windows;

    public Windows defaultWindow;

    public Windows CurrentWindow { get; private set; }

    private void Start()
    {
        foreach (var window in windows)
        {
            window.Init(this);
            window.Close();
            window.gameObject.SetActive(false);
        }

        CurrentWindow = defaultWindow;
        windows[(int)CurrentWindow].Open();
        //Open(defaultWindow);
    }

    public void Open(Windows id)
    { 
         if (CurrentWindow != Windows.None)
          {
                windows[(int)CurrentWindow].Close();
          }
    
          CurrentWindow = id;
          windows[(int)CurrentWindow].Open();
    }

}
