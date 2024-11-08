namespace UI.ViewModels.Requests;

public class HeaderViewModel
{
    public string Key { get; set; }
    public string Value { get; set; }

    public HeaderViewModel(string key, string value)
    {
        Key = key;
        Value = value;
    }
}