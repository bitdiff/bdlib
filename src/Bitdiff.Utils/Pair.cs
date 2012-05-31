namespace Bitdiff.Utils
{
    public class Pair
    {
        public Pair(string key, string value)
        {
            K = key;
            V = value;
        }

        public string K { get; set; }
        public string V { get; set; }
    }
}