using UnityEngine;
using System.Collections;
namespace MyPlatformer
{
    /// <summary>
    /// to be considered a repeates
    /// </summary>
    class Ticker
    {
        public int tick;
        public int start;
        public Ticker(int start, float secondsWait)
        {
            tick = this.start = start;
        }
        public bool IsStop { get { return tick <= 0; } }
        public bool Tick()
        {
            if (tick >= 0)
            {
                --tick;

                return true;
            }
            return false;
        }
        public void Restart()
        {
            tick = start;
        }
    }
}
