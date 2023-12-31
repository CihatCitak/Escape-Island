namespace InputSystem
{
    public interface IClickable
    {
        public bool IsClosed { get; set; }
        public bool IsFirstClickable { get; }
        public void OnCLick();
        public void ResetClick();
    }
}