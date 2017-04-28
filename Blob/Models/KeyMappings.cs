using Microsoft.Xna.Framework.Input;

namespace Blob.Models
{
    public struct KeyMappings
    {
        public Keys UpKey;
        public Keys DownKey;
        public Keys LeftKey;
        public Keys RightKey;
        public Keys SpecialKey;

        public KeyMappings(Keys up, Keys down, Keys left, Keys right, Keys special)
        {
            UpKey = up;
            DownKey = down;
            LeftKey = left;
            RightKey = right;
            SpecialKey = special;
        }
    }
}