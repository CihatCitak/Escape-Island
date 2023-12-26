namespace InputSystem
{
    public interface IClickable
    {
        public bool IsClicked { get; set; }

        public void OnCLick();

        public void ResetClick();
    }
}