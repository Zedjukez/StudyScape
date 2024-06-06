using UnityEngine;
using TMPro;
using System.Globalization;
using System;

public class CurrentMonthDisplay : MonoBehaviour
{
    public TMP_Text monthText;

    void Start()
    {
        UpdateMonthText();
    }

    void UpdateMonthText()
    {
        // Set the culture to Dutch
        CultureInfo dutchCulture = new CultureInfo("nl-NL");

        // Get the current month name in Dutch and convert to lowercase
        string currentMonth = DateTime.Now.ToString("MMMM", dutchCulture).ToLower(dutchCulture);

        // Update the text component
        monthText.text = currentMonth;
    }
}
