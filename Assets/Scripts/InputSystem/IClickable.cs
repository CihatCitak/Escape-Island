namespace InputSystem
{
    public interface IClickable
    {
        public bool IsClickable { get; set; }

        public void OnCLick();

        public void ResetClick();

        public void OnClose();
    }
}