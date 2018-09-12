using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameCurrency : MonoBehaviour {

    public int currency;
    public Text currencyInput;
    public int moneyGain;
    public int moneyGainTime;

    public void Start()
    {
        CurrencyChange();
        StartCoroutine(MoneyGain());
    }

    public IEnumerator MoneyGain()
    {
        currency += moneyGain;
        CurrencyChange();
        yield return new WaitForSeconds(moneyGainTime);
        StartCoroutine(MoneyGain());
    }

    public void CurrencyChange()
    {
        if (currencyInput)
        {
            currencyInput.text = currency.ToString();
        }
    }
}
