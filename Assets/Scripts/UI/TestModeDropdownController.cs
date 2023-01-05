using UnityEngine;
using TMPro;
using System;
using System.Linq;

[RequireComponent(typeof(TMP_Dropdown))]
public class TestModeDropdownController : MonoBehaviour
{
    TMP_Dropdown TestModeDropdown;
    PerformanceTestEngine PerformanceTestEngine;

    private void Awake()
    {
        TestModeDropdown = GameObject.Find("Test Mode Dropdown").GetComponent<TMP_Dropdown>();
        TestModeDropdown.onValueChanged.AddListener(OnValueChanged);
        TestModeDropdown.AddOptions(Enum.GetNames(typeof(TestMode)).ToList());
        
        PerformanceTestEngine = GameObject.Find("Plane").GetComponent<PerformanceTestEngine>();

        if (!PerformanceTestEngine.isActiveAndEnabled)
        {
            TestModeDropdown.transform.localScale = Vector3.zero;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        TestModeDropdown.SetValueWithoutNotify((int)PerformanceTestEngine.TestMode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnValueChanged(int arg0)
    {
        PerformanceTestEngine.OnTestModeChanged(arg0);
    }
}
