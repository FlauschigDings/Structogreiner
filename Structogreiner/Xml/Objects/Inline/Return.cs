namespace Structogreiner.Xml.Objects.Inline
{
    internal class Return : Inline
    {
        public Return(string text) : base("jump", Program.I18n.Return(text)) { }
    }
}
