// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("bVEif4h80//TnMj6hhVFepozYQWmID6GZFWd2950l5a2j81V/U0UALtnIWpDKC67E5lz1SdKUSPmsDfvXe9sT11ga2RH6yXrmmBsbGxobW5yfdAf0WkGeSZ9PIj4UuJNIdHeA2+TpL9QnP3mVEpeaCZdGr7FZp+zcuDbYxSI6BgjxWgB6NEG5OgAu1eGGLAY7o/Mnp+2CFaZL64uMhkt5e9sYm1d72xnb+9sbG3wIX0LgnfyrAsstN1wHArZsz/HjGWtzCi+KrYBacIFL074Y4hVF77acb5P6aJyvRylGUMqdjY6ONlR6SD6vAfjTmpgvEYBWP620zo9TBIVMDllFF5LWv7fS8qsznrDVbrJKx4rF0Ow+FNlVYbPbkoSRK6O+m9ubG1s");
        private static int[] order = new int[] { 10,13,3,10,11,6,6,12,13,11,13,13,12,13,14 };
        private static int key = 109;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
