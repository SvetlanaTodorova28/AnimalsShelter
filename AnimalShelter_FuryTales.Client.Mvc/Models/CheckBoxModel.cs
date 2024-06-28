using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter_FuryTales.Client.Mvc.Models;

public class CheckBoxModel{
    [HiddenInput]
    public string Text { get; set; }
    [HiddenInput]
    public int Value { get; set; }
    public bool IsChecked { get; set; }
}