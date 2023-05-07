public partial class PlayerInteractionText
{
    public struct DisplayText
    {
        public string text;
        public int prority;

        public DisplayText(string text, int priority) : this()
        {
            this.text = text;
            this.prority = priority;
        }
    }
}
