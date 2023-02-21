using System.Linq;
using System;
public static class ShuffleArray 
{
    public static int[] Shuffle(int [] shuffle){
        Random random = new Random();
        shuffle.OrderBy(x => random.Next()).ToArray();
        return shuffle;
    }
}
