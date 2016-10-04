#pragma warning disable 1591

namespace StealthAPI
{
    public class Skill
    {
        public Skill()
        {
            Id = -1;
        }

        public Skill(string title) : base()
        {
            Value = title;
        }

        public string Value { get; set; }
        public int Id { get; set; }
        public bool IsValidId { get { return Id >= 0 && Id < 250; } }        
    }
}
