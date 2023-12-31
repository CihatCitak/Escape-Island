namespace InputSystem
{
    public interface IClickable
    {
        public bool IsFirstClickable { get; }
        public void OnCLick();
        public void ResetClick();
    }
}