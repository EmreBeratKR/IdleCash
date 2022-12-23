using UnityEngine;
using EmreBeratKR.IdleCash.Creator;

namespace EmreBeratKR.IdleCash.Exceptions
{
    public class IdleCashSettingsNotFoundException : UnityException
    {
        public IdleCashSettingsNotFoundException()
            : base($"{nameof(IdleCashSettingsSO)} cannot be found in Resources folders! Please Create one from {IdleCashSettingsSO.MenuItemSettings}.")
        {
            
        }
    }
}