namespace ChordDatabase.Models
{
    public class Chord
    {
        public int Id { get; set; }
        public string Quality { get; set; }
        public String? Extension { get; set; }
        public String? Alterations { get; set; }
        public String? Inversion { get; set; }
        public String? Scale { get; set; }
        public String? Functions { get; set; }
        public String? Feeling { get; set; }
        public Chord()
        {

        }

    }
}
